using Inventory_Management_System.Helpers;
using Inventory_Management_System.ModelsData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory_Management_System.Models
{
    public class NotificationModel
    {
        public string? Header { get; set; }
        public string? SubMessage { get; set; }
        public bool IsRead { get; set; }
        public string? TimeLabel { get; set; }
        public NotificationType Type { get; set; }
    }

    public enum NotificationType
    { 
        Info,
        Warning,
        Critical
    }

    public interface INotificationRepository
    { 
        Task MarkAllRead(int userId);
        Task Clear(int userId, bool past30Days);
        Task<int> GetUnreadCountAsync(int userId);
        Task<int> GetToDeleteCountAsync(int userId, bool past30Days);

        Task<List<NotificationModel>>UnreadNotificationsAsync(int userId);
        Task<List<NotificationModel>> GetAllNotificationsAsync(int userId);
        Task GenerateStockNotificationsAsync(List<InventoryModel> items, int status);
        Task GenerateGeneralNotificationsAsync(string header, string description, List<int> directedToUserIds);
    }

    public class NotificationRepository : INotificationRepository
    {
        // Get all notifs for user
        public async Task<List<NotificationModel>> GetAllNotificationsAsync(int userId)
        {
            string query = @"
                SELECT 
                    n.Header, 
                    n.Details, 
                    n.CreatedAt, 
                    nr.IsRead 
                FROM Notifications n
                INNER JOIN NotificationRecipients nr ON n.NotifID = nr.NotifID
                WHERE nr.UserId = @UserId
                ORDER BY n.CreatedAt DESC";


            List<Models.NotificationModel> notifItemList = [];

            using (SqlConnection conn = ApplicationDB.GetConnection())
            using (SqlCommand cmd = new(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                await conn.OpenAsync();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string? headerText = reader["Header"] as string;

                    NotificationType currentType = NotificationType.Info;

                    if (headerText == "Out of Stock")
                    {
                        currentType = NotificationType.Critical;
                    }
                    else if (headerText == "Low on Stock")
                    {
                        currentType = NotificationType.Warning;
                    }

                    notifItemList.Add(new NotificationModel
                    {
                        Header = headerText,
                        SubMessage = reader["Details"] as string,
                        IsRead = reader["IsRead"] != DBNull.Value && Convert.ToBoolean(reader["IsRead"]),
                        TimeLabel = NotifDateSimplifier.Shorten((DateTime)reader["CreatedAt"]),
                        Type = currentType
                    });

                }

            }
            
            return notifItemList;
        }

        public async Task<List<NotificationModel>> UnreadNotificationsAsync(int userId)
        {

            var unreadNotifs = await GetAllNotificationsAsync(userId);

            return unreadNotifs.Where(n => n.IsRead == false).ToList();
        }


        public async Task<int> GetUnreadCountAsync(int userId)
        {
            string query = @"
                SELECT COUNT(*) 
                FROM NotificationRecipients 
                WHERE UserId = @UserId AND IsRead = 0";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();
            return (int)(await cmd.ExecuteScalarAsync() ?? 0);
        }

        public async Task<int> GetToDeleteCountAsync(int userId, bool past30Days)
        {
            string query = (past30Days)
                ? @"SELECT COUNT(*) 
                    FROM NotificationRecipients nr
                    INNER JOIN Notifications n ON nr.NotifID = n.NotifID
                    WHERE nr.UserId = @UserId 
                    AND n.CreatedAt < DATEADD(DAY, -30, GETDATE())"
                : "SELECT COUNT(*) FROM NotificationRecipients WHERE UserId = @UserId";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();
            return (int)(await cmd.ExecuteScalarAsync() ?? 0);
        }

        public async Task MarkAllRead(int userId)
        {
            string query = "UPDATE NotificationRecipients SET IsRead = 1 WHERE UserId = @UserId";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Clear(int userId, bool past30Days)
        {
            string query = (past30Days)
                ? @"DELETE nr FROM NotificationRecipients nr
                    INNER JOIN Notifications n ON nr.NotifID = n.NotifID
                    WHERE nr.UserId = @UserId 
                    AND n.CreatedAt < DATEADD(DAY, -30, GETDATE())"
                : "DELETE FROM NotificationRecipients WHERE UserId = @UserId";


            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync(); 
        }

        public async Task GenerateStockNotificationsAsync(List<InventoryModel> items, int status)
        {
            
            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();

            // Fetch all users once outside the loop
            List<int> userIds = [];
            using (SqlCommand userCmd = new("SELECT UserId FROM AppUser", conn))
            using (SqlDataReader reader = await userCmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    userIds.Add(reader.GetInt32(0));

            if (userIds.Count == 0 || items.Count == 0) return;

            foreach (var item in items)
            {
                
                // Generate the header and details 
                string header = status == 0 ? "Out of Stock" : "Low on Stock";
                string details = status == 0
                    ? $"{item.Item} is out of stock."
                    : $"{item.Item} has dropped to {item.InStock} {item.ItemUnit?.ToLower()}. (Minimum: {item.MinimumStock})";

                // Fetch the exact message of the LAST notification sent for this item.
                string checkQuery = @"SELECT TOP 1 Details FROM Notifications WHERE ItemID = @ItemID ORDER BY CreatedAt DESC";

                bool skipNotification = false;
                using (SqlCommand checkCmd = new(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@ItemID", item.ItemId);
                    var lastDetails = await checkCmd.ExecuteScalarAsync() as string;

                    // If the exact same message was already sent recently, don't spam them again.
                    // But if the stock number changed, 'details' will be different, allowing the new alert!
                    if (lastDetails == details)
                    {
                        skipNotification = true;
                    }
                }

                if (skipNotification) continue;

                // Insert notification
                int newNotifId;
                string insertQuery = @"INSERT INTO Notifications (Header, Details, CreatedAt, ItemID) OUTPUT INSERTED.NotifID VALUES (@Header, @Details, @CreatedAt, @ItemID)";

                using (SqlCommand insertCmd = new(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Header", header);
                    insertCmd.Parameters.AddWithValue("@Details", details);
                    insertCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    insertCmd.Parameters.AddWithValue("@ItemID", item.ItemId);
                    newNotifId = Convert.ToInt32(await insertCmd.ExecuteScalarAsync());
                }

                // Assign notification to all users
                using SqlCommand assignCmd = new(@"INSERT INTO NotificationRecipients (NotifID, UserId, IsRead) VALUES (@NotifID, @UserId, 0)", conn);

                assignCmd.Parameters.Add("@NotifID", SqlDbType.Int);
                assignCmd.Parameters.Add("@UserId", SqlDbType.Int);

                foreach (int userId in userIds)
                {
                    assignCmd.Parameters["@NotifID"].Value = newNotifId;
                    assignCmd.Parameters["@UserId"].Value = userId;
                    await assignCmd.ExecuteNonQueryAsync();
                }
            }
            
        }

        public async Task GenerateGeneralNotificationsAsync(string header, string description, List<int> directedToUserIds)
        {
            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();

            // insert the notification (no ItemID since it's general)
            int newNotifId;
            string insertQuery = @"INSERT INTO Notifications (Header, Details, CreatedAt, ItemID) 
                           OUTPUT INSERTED.NotifID 
                           VALUES (@Header, @Details, @CreatedAt, NULL)";

            using (SqlCommand insertCmd = new(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@Header", header);
                insertCmd.Parameters.AddWithValue("@Details", description);
                insertCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                newNotifId = Convert.ToInt32(await insertCmd.ExecuteScalarAsync());
            }

            // if no specific users provided, fetch all users
            List<int> targetUserIds = directedToUserIds ?? [];

            if (targetUserIds.Count == 0)
            {
                using SqlCommand userCmd = new("SELECT UserId FROM AppUser", conn);
                using SqlDataReader reader = await userCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                    targetUserIds.Add(reader.GetInt32(0));
            }


            // assign to target users
            using SqlCommand assignCmd = new(@"INSERT INTO NotificationRecipients (NotifID, UserId, IsRead) VALUES (@NotifID, @UserId, 0)", conn);
            assignCmd.Parameters.Add("@NotifID", SqlDbType.Int);
            assignCmd.Parameters.Add("@UserId", SqlDbType.Int);

            foreach (int userId in targetUserIds)
            {
                assignCmd.Parameters["@NotifID"].Value = newNotifId;
                assignCmd.Parameters["@UserId"].Value = userId;
                await assignCmd.ExecuteNonQueryAsync();
            }
        }
    }
}
