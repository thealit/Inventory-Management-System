using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public class SignUpModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string HashedPassword { get; set; }
        public int Role { get; set; }

    }

    public class  AccountType
    {
        public string UserRole { get; set; }
        public int Value { get; set; } // 0 - no selection/null, 1 - admin, 2 - staff
    }

    public interface ISignUpRepository
    {
        Task<AppUsers> RegisterAsync(SignUpModel newCredentials);
        Task<bool> UsernameTakenAsync(SignUpModel newCredentials);
        Task<bool> IsDBNullAsync();
    }

    public class SignUpRepository : ISignUpRepository
    {
        public async Task<AppUsers> RegisterAsync(SignUpModel newCredentials)
        {
            string query = @"INSERT INTO AppUser (UserName, FirstName, LastName, HashedPassword, IsAdmin, DateAdded, LastActive) VALUES (@uName, @fName, @lName, @pass, @isAdmin, @dateAdded, @lastActive);
                 SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = ApplicationDB.GetConnection())
            {

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var now = DateTime.Now;
                    cmd.Parameters.AddWithValue("@fName", newCredentials.FirstName);
                    cmd.Parameters.AddWithValue("@lName", newCredentials.LastName);
                    cmd.Parameters.AddWithValue("@uName", newCredentials.UserName);
                    cmd.Parameters.AddWithValue("@pass", newCredentials.HashedPassword);
                    cmd.Parameters.Add("@isAdmin", SqlDbType.Bit).Value = newCredentials.Role == 1 ? 1 : 0; // 0-staff, 1-admin
                    cmd.Parameters.AddWithValue("@dateAdded", now);
                    cmd.Parameters.AddWithValue("@lastActive", now);


                    await conn.OpenAsync();

                    var result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        int newUserId = Convert.ToInt32(result);

                        return new AppUsers
                        {
                            UserID = newUserId,
                            UserName = newCredentials.UserName,
                            FirstName = newCredentials.FirstName,
                            LastName = newCredentials.LastName,
                            HashPassword = newCredentials.HashedPassword,
                            IsAdmin = newCredentials.Role == 1
                        };
                    }

                    return null;
                }
            }
        }

        public async Task<bool> IsDBNullAsync()
        {
            string query = @"SELECT CASE WHEN EXISTS (SELECT 1 FROM AppUser) THEN 0 ELSE 1 END";

            using (SqlConnection conn = ApplicationDB.GetConnection())
            {

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    if (result == null || Convert.ToInt32(result) == 0)
                        return false;
                    return true;
                }
            }
        }

      

        public async Task<bool> UsernameTakenAsync(SignUpModel newCredentials)
        {
            string query = @"SELECT CASE WHEN EXISTS (SELECT 1 FROM AppUser WHERE UserName = @uName) THEN 1 ELSE 0 END";

            using (SqlConnection conn = ApplicationDB.GetConnection())
            {

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uName", newCredentials.UserName);

                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    if (result == null || Convert.ToInt32(result) == 0)
                        return false;
                    return true;
                }
            }
        }
    }
}
