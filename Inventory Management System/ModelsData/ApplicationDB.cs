using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Inventory_Management_System.ModelsData
{
    public class ApplicationDB
    {
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            return new SqlConnection(connStr);
        }
    }
}
