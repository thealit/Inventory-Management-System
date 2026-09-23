using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class AppUsers
    {
        
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string HashPassword { get; set; }

        public bool IsAdmin { get; set; } 
        
        public DateTime DateAdded { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsSelected { get; set; } = false;
    }
}
