using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Inventory_Management_System
{
    internal static class Program
    {
        //public static Icon AppIcon = new Icon("Resources/icons8_box_100.ico");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                Application.Run(new form_loginSignUp());
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error establishing database connection.\n\nAdditional Message:" + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }

        }
    }
}