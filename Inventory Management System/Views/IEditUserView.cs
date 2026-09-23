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
    public interface IEditUserView
    {
        EditUserRoleModel SelectedRole { get; set; }
        UpdateUserRoleModel GetUpdateUserRoleModel { get; }
        ResetUserPasswordModel ResetUserPasswordModel { get; }

        AppUsers? GetSelectedUserData { get; set; }

        event EventHandler ChangeRoleOption;
        event EventHandler ChangePasswordOption;
        event EventHandler SaveNewPassword;
        event EventHandler SaveNewRoleChanges;
        event EventHandler CancelChanges;

        void ClearWarnings();
        void ClearFields();
        void AutoSignOut(string reason);
        void ResetManageAccountPass(string newHashedPass);
        void UpdateSelectedUserRole(bool isAdmin);
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        bool ConfirmMessage(string message, string header, MessageBoxIcon icon);
        void SetRoleWarning(string? message);
        void SetResetPasswordWarning(string? message);
        void SetConfirmPasswordWarning(string? message);
        void CloseEditUser();
        void ViewEditUserRoleSide();
        void ViewEditPasswordSide();
        Task RefreshUsersTable();
    }
}
