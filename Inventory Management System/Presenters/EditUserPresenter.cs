using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Views;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
  
    public class EditUserPresenter
    {
        private readonly IEditUserView _view;
        private readonly IEditUserRepository _repo;
        private readonly IChangeLogRepository _changeLogRepo;
        private readonly NotificationRepository _notifRepo;


        public EditUserPresenter(IEditUserRepository repo, IChangeLogRepository changeLogRepository, IEditUserView view)
        {
            _repo = repo;
            _view = view;
            _changeLogRepo = changeLogRepository;
            _notifRepo = new NotificationRepository();

            _view.ChangeRoleOption += OnChangeRoleOption_Click;
            _view.SaveNewRoleChanges += OnSaveNewRoleChanges_Click;
            _view.ChangePasswordOption += OnChangePasswordOption_Click;
            _view.SaveNewPassword += OnSaveNewPassword_Click;
            _view.CancelChanges += OnCancelChanges_Click;
        }

        private async void OnSaveNewPassword_Click(object? sender, EventArgs e)
        {
           
            UpdateUserRoleModel user = _view.GetUpdateUserRoleModel;
            ResetUserPasswordModel resetUserPasswordInputs = _view.ResetUserPasswordModel;


            if (user == null || user.UserId == 0) {
                _view.ShowMessage("No user selected or user data is invalid.", "Error", MessageBoxIcon.Error);
                return;
            }

            bool isCorrect = ValidateInput(resetUserPasswordInputs);

            if (!isCorrect) return;

            string hashedPass = PasswordHasher.HashPassword(resetUserPasswordInputs.Password);

            bool isSaveSuccess = await _repo.ChangeUserPassword(user.UserId, hashedPass);

            if (isSaveSuccess)
            {
                _view.ShowMessage("Password changed successfully!", "Success", MessageBoxIcon.Information);
                _view.ClearFields();
                _view.ClearWarnings();
                if (user.UserId != CurrentUser.UserID)
                {   
                    await _notifRepo.GenerateGeneralNotificationsAsync(
                            header: "Password Changed",
                            description: $"Your password has been changed by an admin. Contact admin for more details.",
                            directedToUserIds: [user.UserId]
                        );
                }

                if (user.UserId == CurrentUser.UserID)
                {
                    _view.ResetManageAccountPass(hashedPass);
                }
                else
                {
                    // notify user of password change
                    await _changeLogRepo.LogChangeAsync(
                               ChangeTypes.NewNotification,
                               affectedUserId: user.UserId,   // target only certain user
                               payload: JsonSerializer.Serialize(new NewNotifPayload
                               {
                                   TargetUserId = user.UserId
                               }),
                               changedBy: CurrentUser.UserID
                    );


                    // reflect new changes in current password field on user's manage account
                    await _changeLogRepo.LogChangeAsync(
                               ChangeTypes.PasswordChangedByAdmin,
                               affectedUserId: user.UserId,   // target only certain user
                               payload: JsonSerializer.Serialize(new PasswordPayload
                               {
                                   AffectedUserId = user.UserId,
                                   NewHash = hashedPass
                               }),
                               changedBy: CurrentUser.UserID
                    );
                }
     
            }
            else {
                _view.ShowMessage("Failed to change password. Please try again.", "Error", MessageBoxIcon.Error);
            }
        }

        private void OnChangePasswordOption_Click(object? sender, EventArgs e)
        {
            _view.ViewEditPasswordSide();
            _view.ClearWarnings();
            _view.ClearFields();
        }

        private void OnCancelChanges_Click(object? sender, EventArgs e)
        {
            _view.ClearFields();
            _view.ClearWarnings();
            _view.CloseEditUser();
        }

        private async void OnSaveNewRoleChanges_Click(object? sender, EventArgs e)
        {
            UpdateUserRoleModel UpdateUserRoleModel = _view.GetUpdateUserRoleModel;

            if (UpdateUserRoleModel.UserId == 0)
            {
                _view.ShowMessage("No user selected or user data is invalid.", "Error", MessageBoxIcon.Error);
                return;
            }

            int adminCount = await _repo.GetAdminCountAsync();

            if (UpdateUserRoleModel.NewRole == 0)
            {
                _view.SetRoleWarning("Please select a role");
                return;
            }
            

            // 0 - no selection/null, 1 - admin, 2 - staff
            // cannot change role self admin if they are the only admin
            if (UpdateUserRoleModel.IsCurrentlyAdmin && (UpdateUserRoleModel.NewRole == 2) && adminCount == 1)
            {
                _view.SetRoleWarning("You cannot change your own role if you are the only admin.");
                return;
            }

            // cannot change to the same role
            if (UpdateUserRoleModel.IsCurrentlyAdmin && UpdateUserRoleModel.NewRole == 1)
            {
                _view.SetRoleWarning("This user is already an admin.");
                return;
            }
            else if (!UpdateUserRoleModel.IsCurrentlyAdmin && UpdateUserRoleModel.NewRole == 2)
            {
                _view.SetRoleWarning("This user is already a staff member.");
                return;
            }

            bool willForceLogout = adminCount > 1
                        && UpdateUserRoleModel.UserId == CurrentUser.UserID
                        && UpdateUserRoleModel.NewRole == 2;

            if (willForceLogout)
            {
                bool confirmed = _view.ConfirmMessage(
                    "This account will be forced logout to apply changes. Do you want to continue?",
                    "Confirmation", MessageBoxIcon.Warning);

                if (!confirmed) return;  
            }


            try
            {
                bool isAdmin = false;

                if (UpdateUserRoleModel.NewRole != 0)
                {
                    isAdmin = UpdateUserRoleModel.NewRole == 1 ? true : false;
                }

                bool success = await _repo.ResetUserRoleAsync(UpdateUserRoleModel.UserId, isAdmin);
                if (success)
                {
                    // reapply role selection changes
                    _view.UpdateSelectedUserRole(isAdmin);

                    _view.ShowMessage($"User successfully set as {(isAdmin ? "admin" : "staff")}.", "Success", MessageBoxIcon.Information);
                    await _view.RefreshUsersTable();
                    
                    _view.ClearWarnings();
                    await _notifRepo.GenerateGeneralNotificationsAsync(
                        header: "Role Updated",
                        description: $"Your account role has been changed to {(isAdmin ? "Administrator" : "Staff")} by an admin.",
                        directedToUserIds: [UpdateUserRoleModel.UserId]
                    );

                    
                    await _changeLogRepo.LogChangeAsync(
                            ChangeTypes.NewNotification,
                            affectedUserId: UpdateUserRoleModel.UserId,   // target only certain user
                            payload: JsonSerializer.Serialize(new NewNotifPayload
                            {
                                TargetUserId = UpdateUserRoleModel.UserId
                            }),
                            changedBy: CurrentUser.UserID
                    );

                    await _changeLogRepo.LogChangeAsync(
                            ChangeTypes.RoleChangedByAdmin,
                            affectedUserId: UpdateUserRoleModel.UserId,   // target only certain user
                            payload: null,
                            changedBy: CurrentUser.UserID
                    );

                    await _changeLogRepo.LogChangeAsync(
                        ChangeTypes.UpdateUserManagement,
                        affectedUserId: null,
                        payload: null,
                        changedBy: CurrentUser.UserID
                    );


                    if (willForceLogout) _view.AutoSignOut("Resetting account role...");
                    PublicEvents.InvokeUpdateUserManagement(this);
                    PublicEvents.InvokeUserRoleChangedByAdmin(this, UpdateUserRoleModel.UserId);

                }
                else
                {
                    _view.ShowMessage($"Failed to set user as {(isAdmin ? "admin" : "staff")}.", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Failed to apply changes.\nError: {ex.Message}", "Database Error", MessageBoxIcon.Error);
                return;
            }

            
        }

        private void OnChangeRoleOption_Click(object? sender, EventArgs e)
        {
            _view.ViewEditUserRoleSide();
            _view.ClearWarnings();
        }

        private bool ValidateInput(ResetUserPasswordModel input)
        {
            // Implement input validation logic
            bool isValid = true;

            _view.ClearWarnings();


            string? passError = Validator.IsNotEmpty(input.Password)
                ?? Validator.IsValidPassword(input.Password);
            if (passError != null)
            {
                _view.SetResetPasswordWarning(passError);
                isValid = false;
            }

            string? confirmPassError = Validator.IsNotEmpty(input.ConfirmPassword)
                ?? Validator.IsSameNewPassword(input.Password, input.ConfirmPassword);
            if (confirmPassError != null)
            {
                _view.SetConfirmPasswordWarning(confirmPassError);
                isValid = false;
            }

            return isValid;
        }
    }
}
