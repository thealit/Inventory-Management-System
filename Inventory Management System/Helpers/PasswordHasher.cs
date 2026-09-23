using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Inventory_Management_System.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string passsword)
        { 
            return BCrypt.Net.BCrypt.EnhancedHashPassword(passsword);
        }

        public static bool VerifyPassword(string password, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, storedHashedPassword);
        }
    }
}
