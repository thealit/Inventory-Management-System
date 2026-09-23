using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public class EditUserRoleModel
    {
        public bool IsAdmin { get; set; }
    }

    public class ResetUserPasswordModel
    { 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public interface IEditUserRepository
    {
        Task<bool> ResetUserRoleAsync(int userId, bool role);
        Task<int> GetAdminCountAsync();
        Task<bool> ChangeUserPassword(int userId, string hashedPass);
    }

    public class EditUserRepository : IEditUserRepository
    {
        public async Task<bool> ResetUserRoleAsync(int userId, bool role)
        {
            string query = "UPDATE AppUser SET IsAdmin = @IsAdmin WHERE UserId = @UserId";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@IsAdmin", role);
            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
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

        public async Task<bool> ChangeUserPassword(int userId, string hashedPass) 
        {
            string query = "UPDATE AppUser SET HashedPassword = @HashedPassword WHERE UserId = @UserId";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@HashedPassword", hashedPass);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}