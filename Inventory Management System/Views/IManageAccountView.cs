using Inventory_Management_System.Components;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface IManageAccountView
    {
        public CurrentAccount currentAccount { get; set; }
        public NewChangesModel newChanges { get; }
        public bool IsUserNull { get; }
        public bool IsPasswordVerified { get; set; }
        public bool IsPasswordConfirmed { get; set; }

        event EventHandler SaveChanges;
        event EventHandler CancelEdit;
        event EventHandler VerifyPass;
        event EventHandler NewPass;
        event EventHandler ConfirmNewPass;

        void ClearWarnings();
        void ClearPasswordFields();
        void ResetForm();
        void CheckForTextChanges();
        void EnableSaveBtn(bool isBtnEnabled);
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        bool ConfirmMessage(string message, string header, MessageBoxIcon icon);
        void SetUsernameWarning(string? message); 
        void SetFirstNameWarning(string? message); 
        void SetLastNameWarning(string? message); 
        void SetCurrentPasswordWarning(string? message); 
        void SetNewPasswordWarning(string? message);
        void SetConfirmPasswordWarning(string? message);
        void RefreshUserProfile(NewChangesModel newChanges, string? newHashedPassword);
        void PassCredentials(AppUsers currentUser, InventoryForm inventoryForm);
    }
}
