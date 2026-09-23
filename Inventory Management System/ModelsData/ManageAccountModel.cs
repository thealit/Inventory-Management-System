using Inventory_Management_System.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public class CurrentAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
    }

    public class NewChangesModel    
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public interface IManageAccountRepository {
        Task<bool> UpdateProfileAsync(NewChangesModel user, bool isPasswordChanged, string newHashedPass);
    }

    public class ManageAccountRepository : IManageAccountRepository
    {

        public async Task<bool> UpdateProfileAsync(NewChangesModel user, bool isPasswordChanged, string newHashedPass)
        {
            string newHash = isPasswordChanged ? newHashedPass : string.Empty;

            string query;

            if (isPasswordChanged == false)
            {
                query = "UPDATE AppUser SET FirstName = @FName, LastName = @LName, UserName = @UName WHERE UserId = @ID";
            }
            else
            {
                query = "UPDATE AppUser SET FirstName = @FName, LastName = @LName, UserName = @UName, HashedPassword = @NewPass WHERE UserId = @ID";
            }

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@FName", user.FirstName);
            cmd.Parameters.AddWithValue("@LName", user.LastName);
            cmd.Parameters.AddWithValue("@UName", user.UserName);

            if (isPasswordChanged && user.NewPassword != null)
            {
                cmd.Parameters.AddWithValue("@NewPass", newHash);
            }

            cmd.Parameters.AddWithValue("@ID", user.Id);

            await conn.OpenAsync();
            int count = await cmd.ExecuteNonQueryAsync();

            return count > 0;
        }
    }
}
