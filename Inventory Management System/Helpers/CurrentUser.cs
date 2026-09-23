using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Helpers
{
    public static class CurrentUser
    {
        public static int UserID { get; set; }
        public static string? UserName { get; set; }
        public static string? Fullname { get; set; }
        public static bool IsAdmin { get; set; }

        public static void ClearSession()
        {
            CurrentUser.UserID = 0;
            CurrentUser.UserName = null;
            CurrentUser.IsAdmin = false;
        }
    }
}
