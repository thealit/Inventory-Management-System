using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public class SignInModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public interface ISignInRepository
    {
        Task<AppUsers?> LoginAsync(SignInModel credentials);
        Task<bool> UserExistsAsync(int userId);
    }

    public class SignInRepository : ISignInRepository
    {
        public async Task<AppUsers?> LoginAsync(SignInModel input)

        {
            string query = @"SELECT UserId, UserName, FirstName, LastName, HashedPassword, IsAdmin FROM AppUser WHERE UserName = @username";

            AppUsers? user = null;

            using (SqlConnection conn = ApplicationDB.GetConnection())
            {

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", input.UserName);
                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                            return null;

                        string? storedHash = reader["HashedPassword"].ToString();

                        if (!PasswordHasher.VerifyPassword(input.Password, storedHash))
                            return null;

                        user = new AppUsers
                        {
                            UserID = (int)reader["UserId"],
                            UserName = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            HashPassword = storedHash
                        };
                    }
                }

                if (user != null)
                {
                    string updateQuery = "UPDATE AppUser SET LastActive = GETDATE() WHERE UserId = @userId";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@userId", user.UserID);
                        await updateCmd.ExecuteNonQueryAsync();
                    }
                }
            }
            return user;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            string query = "SELECT COUNT(1) FROM AppUser WHERE UserId = @UserId";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
        }
    }
}
