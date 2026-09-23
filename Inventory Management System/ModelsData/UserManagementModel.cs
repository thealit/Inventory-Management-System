using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace Inventory_Management_System.ModelsData
{
    public class DeleteUsersModel 
    {
        public int UserId { get; set; }
        public bool IsCurrentlyAdmin { get; set; }
    }

    public class UpdateUserRoleModel : DeleteUsersModel
    {
        public int NewRole { get; set; }
    }

   

    public interface IUserManagementRepository
    {
        Task<List<AppUsers>> GetAllUsersAsync();
        Task<List<AppUsers>> SearchUsersAsync(string searchTerm);
        Task<bool> DeleteAsync(List<int> userIds);
        Task<int> GetAdminCountAsync();
    }

    public class UserManagementRepository : IUserManagementRepository
    {

        public async Task<List<AppUsers>> GetAllUsersAsync()
        {
            var list = new List<AppUsers>();
            string query = @"
                SELECT 
                    UserId,
                    UserName,
                    (FirstName + ' ' + LastName) AS FullName,
                    HashedPassword,
                    IsAdmin,
                    DateAdded,  
                    LastActive
                FROM [dbo].[AppUser]
                ORDER BY IsAdmin DESC";

            using (SqlConnection conn = ApplicationDB.GetConnection())
            using (SqlCommand cmd = new(query, conn))
            {
                await conn.OpenAsync();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new AppUsers
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserId")),
                        UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        HashPassword = reader.GetString(reader.GetOrdinal("HashedPassword")),
                        IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                        DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                        LastActive = reader.GetDateTime(reader.GetOrdinal("LastActive"))
                    });
                }
            }
            return list;
        }


        public async Task<List<AppUsers>> SearchUsersAsync(string searchTerm) {
            var filteredList = new List<AppUsers>();
            string query = @"
                    SELECT
                        UserId,
                        UserName,
                        (FirstName + ' ' + LastName) AS FullName,
                        HashedPassword,
                        IsAdmin,
                        DateAdded,
                        LastActive
                    FROM[dbo].[AppUser]
                    WHERE FirstName  LIKE @Search
                        OR LastName   LIKE @Search
                        OR UserName   LIKE @Search
                        OR(FirstName + ' ' + LastName) LIKE @Search
                        OR(LastName + ' ' + FirstName) LIKE @Search
                        OR (@IsAdminSearch IS NOT NULL AND IsAdmin = @IsAdminSearch)
                    ORDER BY IsAdmin DESC";

            bool? isAdminSearch = searchTerm.Trim().ToLower() switch
            {
                "admin" or "administrator" => true,
                "staff" => false,
                _ => null
            };

            using SqlConnection conn = ApplicationDB.GetConnection();
            using (SqlCommand cmd = new(query, conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = $"%{searchTerm}%";
                cmd.Parameters.Add("@IsAdminSearch", SqlDbType.Bit).Value = isAdminSearch.HasValue ? isAdminSearch.Value : DBNull.Value;

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    filteredList.Add(new AppUsers
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserId")),
                        UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        HashPassword = reader.GetString(reader.GetOrdinal("HashedPassword")),
                        IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                        DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                        LastActive = reader.GetDateTime(reader.GetOrdinal("LastActive"))
                    });
                }
            }
            return filteredList;
        }


        public async Task<bool> DeleteAsync(List<int> userIds)
        {
            if (userIds == null || userIds.Count == 0)
                return false;

            var users = userIds.Select((id, i) => $"@ID{i}").ToList();
            string inClause = string.Join(", ", users);

            string query1 = $"DELETE FROM NotificationRecipients WHERE UserId IN ({inClause})";
            string query2 = $"DELETE FROM AppUser WHERE UserId IN ({inClause})";

            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();

            int rowsAffectedNotifs = 0;
            int rowsAffectedAppUsers = 0;

            // delete notifs of user first
            using (SqlCommand cmd1 = new(query1, conn))
            {
                for (int i = 0; i < userIds.Count; i++)
                    cmd1.Parameters.Add($"@ID{i}", SqlDbType.Int).Value = userIds[i];
                rowsAffectedNotifs = await cmd1.ExecuteNonQueryAsync();

            }

            // delete user
            using (SqlCommand cmd2 = new(query2, conn))
            {
                for (int i = 0; i < userIds.Count; i++)
                    cmd2.Parameters.Add($"@ID{i}", SqlDbType.Int).Value = userIds[i];
                rowsAffectedAppUsers = await cmd2.ExecuteNonQueryAsync();
            }

            return rowsAffectedAppUsers > 0;
        }


        public async Task<int> GetAdminCountAsync()
        {
            string query = "SELECT COUNT(*) FROM AppUser WHERE IsAdmin = 1";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            await conn.OpenAsync();
            int adminCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return adminCount;
        }
       
    }
}   