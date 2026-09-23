using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface IAuditTrailView
    {
        string SearchLog { get; set; }

        event EventHandler SearchChanged;

        void ShowMessage(string message, string errorHeader, MessageBoxButtons btn, MessageBoxIcon icon);
        void LoadLogsTable(List<AuditTrailModel> logs);
    }
}
