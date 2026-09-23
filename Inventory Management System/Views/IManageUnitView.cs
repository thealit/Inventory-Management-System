using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface IManageUnitView
    {
        public UnitModel? SelectedUnit { get; set; }
        public string NewUnitName { get; }
        public string EditedUnitName { get; }

        event EventHandler AddOption;
        event EventHandler EditOption;
        event EventHandler AddNewUnit;
        event EventHandler DeleteUnit;
        event EventHandler SaveEditChanges;
        event EventHandler CancelChanges;

        void ClearWarnings();
        void ClearFields();
        void PopulateUnitDetails(List<UnitModel> units);
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        bool ConfirmMessage(string message, string header, MessageBoxIcon icon);
        void SetNewUnitWarning(string? message);
        void SetChooseUnitToEditWarning(string? message);
        void SetEditUnitWarning(string? message);
        void CloseManageUnit();
        void ShowAddUnitSide();
        void ShowEditUnitSide();
        void RefreshInventoryTable();
        void OnUnitAdded(); 
    }
}
