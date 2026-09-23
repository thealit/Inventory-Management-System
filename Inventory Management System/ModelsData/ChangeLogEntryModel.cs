using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// for pushing updates in app

namespace Inventory_Management_System.ModelsData
{
    public class ChangeLogEntryModel
    {
        public int ChangeId { get; set; }
        public string ChangeType { get; set; }
        public int? AffectedUserId { get; set; }
        public string? Payload { get; set; }
        public DateTime ChangedAt { get; set; }
        public int? ChangedBy { get; set; }
    }


    public class PasswordPayload
    {
        public int AffectedUserId { get; set; }
        public string NewHash { get; set; }
    }

    public class NewNotifPayload
    {
        public int TargetUserId { get; set; }
    }


    public static class ChangeTypes
    {
        // manage account update, then update user management --
        public const string UpdateUserManagement = "UpdateUserManagement";

        // user management update, then update targeted user's manage account current password field checker--
        public const string PasswordChangedByAdmin = "PasswordChangedByAdmin";

        public const string RoleChangedByAdmin = "RoleChangedByAdmin";

        // trigger table, & tiles updates in inventory
        public const string InventoryChanged = "InventoryChanged";

        // update audit trail when inventory changed
        public const string UpdateAuditTrail = "UpdateAuditTrail";

        // force signout user if deleted by admin --
        public const string UserDeletedByAdmin = "UserDeletedByAdmin";

        // update notifs (general, and targeted) 
        public const string NewNotification = "NewNotification";

        // update dropdowns
        public const string UpdateUnit_Category = "UpdateUnit_Category";
    }

    public interface IChangeLogRepository
    {
        Task<int> GetLatestChangeIdAsync();
        Task<List<ChangeLogEntryModel>> GetChangesSinceAsync(int lastChangeId, int currentUserId);
        Task LogChangeAsync(string changeType, int? affectedUserId, string? payload, int changedBy);
    }

    public class ChangeLogRepository : IChangeLogRepository
    {
        // Called on app start
        public async Task<int> GetLatestChangeIdAsync()
        {
            string query = "SELECT ISNULL(MAX(ChangeId), 0) FROM ChangeLog";
            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(query, conn);
            return (int)await cmd.ExecuteScalarAsync();
        }

        // Called every poll tick — get only new rows since last check
        public async Task<List<ChangeLogEntryModel>> GetChangesSinceAsync(int lastChangeId, int currentUserId)
        {
            string query = @"
            SELECT ChangeId, ChangeType, AffectedUserId, Payload, ChangedAt, ChangedBy
            FROM ChangeLog
            WHERE ChangeId > @LastChangeId
            AND (AffectedUserId IS NULL OR AffectedUserId = @UserId)
            AND (ChangedBy != @UserId OR ChangedBy IS NULL)
            ORDER BY ChangeId ASC";


            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LastChangeId", lastChangeId);
            cmd.Parameters.AddWithValue("@UserId", currentUserId);

            var changes = new List<ChangeLogEntryModel>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                changes.Add(new ChangeLogEntryModel
                {
                    ChangeId = reader.GetInt32(0),
                    ChangeType = reader.GetString(1),
                    AffectedUserId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                    Payload = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ChangedAt = reader.GetDateTime(4),
                    ChangedBy = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                });
            }
            return changes;
        }

        // Called whenever something meaningful happens
        public async Task LogChangeAsync(string changeType, int? affectedUserId, string? payload, int changedBy)
        {
            string query = @"
            INSERT INTO ChangeLog (ChangeType, AffectedUserId, Payload, ChangedBy)
            VALUES (@ChangeType, @AffectedUserId, @Payload, @ChangedBy)";

            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ChangeType", changeType);
            cmd.Parameters.AddWithValue("@AffectedUserId", (object?)affectedUserId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Payload", (object?)payload ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ChangedBy", changedBy);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
