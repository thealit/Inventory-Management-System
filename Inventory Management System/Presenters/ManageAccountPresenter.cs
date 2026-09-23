using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class ManageAccountPresenter
    {
        private readonly IManageAccountRepository _manageAccountRepository;
        private readonly Views.IManageAccountView _view;
        private readonly IChangeLogRepository _changeLogRepo;

        public ManageAccountPresenter(IManageAccountRepository manageAccountRepository, IChangeLogRepository changeLogRepo, Views.IManageAccountView view)
        {
            _manageAccountRepository = manageAccountRepository;
            _changeLogRepo = changeLogRepo;
            _view = view;

            _view.SaveChanges += OnSave_Click;
            _view.CancelEdit += OnCancel_Click;
            _view.VerifyPass += OnVerifyPass;
            _view.NewPass += OnNewPass;
            _view.ConfirmNewPass += OnConfirmNewPass;

            PublicEvents.UserPasswordChangedByAdmin += OnUserPasswordChangedByAdmin;
        }

        private void OnUserPasswordChangedByAdmin(object? sender, PasswordChangedEventArgs e)
        {
            // update if the changed account is the currently logged-in user
            if (e.UserId != CurrentUser.UserID) return;

            _view.ClearPasswordFields();
            _view.ClearWarnings();
            _view.currentAccount.HashedPassword = e.NewHashedPassword;
        }

        private void OnVerifyPass(object? sender, EventArgs e)
        {
            string? isPassCorrect = Helpers.Validator.IsSameCurrentPassword(_view.newChanges.CurrentPassword, _view.currentAccount.HashedPassword);

            if (string.IsNullOrEmpty(_view.newChanges.CurrentPassword.Trim()))
                _view.SetCurrentPasswordWarning(string.Empty);

            if (isPassCorrect == null)
            {
                _view.SetCurrentPasswordWarning(string.Empty);
                _view.IsPasswordVerified = true;
            }
            else if (isPassCorrect != null && !string.IsNullOrWhiteSpace(_view.newChanges.CurrentPassword))
            {
                _view.SetCurrentPasswordWarning(isPassCorrect);
                _view.IsPasswordVerified = false;
            }
        }

        private void OnNewPass(object? sender, EventArgs e)
        {
            string? newPassError = Helpers.Validator.IsValidPassword(_view.newChanges.NewPassword ?? string.Empty);
            if (string.IsNullOrEmpty(_view.newChanges.NewPassword?.Trim()))
                _view.SetNewPasswordWarning(string.Empty);
            if (newPassError == null)
            {
                _view.SetNewPasswordWarning(string.Empty);
            }
            else if (newPassError != null && !string.IsNullOrEmpty(_view.newChanges.NewPassword?.Trim()))
            {
                _view.SetNewPasswordWarning(newPassError);
            }
        }

        private void OnConfirmNewPass(object? sender, EventArgs e)
        {
            string? isPassTheSame = Helpers.Validator.IsSameNewPassword(_view.newChanges.NewPassword ?? string.Empty, _view.newChanges.ConfirmPassword ?? string.Empty);

            if (string.IsNullOrEmpty(_view.newChanges.NewPassword?.Trim()) || string.IsNullOrEmpty(_view.newChanges.ConfirmPassword?.Trim()))
                _view.SetConfirmPasswordWarning(string.Empty);

            if (isPassTheSame == null)
            {
                _view.SetConfirmPasswordWarning(string.Empty);
                _view.IsPasswordConfirmed = true;
            }
            else if (isPassTheSame != null && _view.IsPasswordVerified && !string.IsNullOrEmpty(_view.newChanges.NewPassword?.Trim()))
            {
                _view.SetConfirmPasswordWarning(isPassTheSame);
                _view.IsPasswordConfirmed = false;
            }
        }

        private void OnCancel_Click(object? sender, EventArgs e)
        {
            bool hasChanges = false;
            bool isPasswordChanged = false;
            if (_view.IsPasswordVerified && _view.IsPasswordConfirmed)
            {
                isPasswordChanged = true;
                hasChanges = true;
            }

            if (_view.currentAccount.FirstName.Trim() != _view.newChanges.FirstName.Trim()) hasChanges = true;
            if (_view.currentAccount.LastName.Trim() != _view.newChanges.LastName.Trim()) hasChanges = true;
            if (_view.currentAccount.UserName.Trim() != _view.newChanges.UserName.Trim()) hasChanges = true;

            if (hasChanges == false || isPasswordChanged)
            {
                _view.ResetForm(); // fixed
                return;
            }

            bool confirmCancel = _view.ConfirmMessage("Changes would not be saved. \nDo you want to continue?", "Confirm", MessageBoxIcon.Warning);

            if (confirmCancel)
            {
                _view.ResetForm();
            }
            else return;
        }

        private async void OnSave_Click(object? sender, EventArgs e)
        {
            bool hasChanges = false;
            if (_view.IsUserNull)
            {
                _view.ShowMessage("User not found.", "System Error", MessageBoxIcon.Error);
                return;
            }

            bool isPasswordChanged = false;
            if (_view.IsPasswordVerified && _view.IsPasswordConfirmed)
            {
                isPasswordChanged = true;
                hasChanges = true;
            }

            if (_view.currentAccount.FirstName.Trim() != _view.newChanges.FirstName.Trim()) hasChanges = true;
            if (_view.currentAccount.LastName.Trim() != _view.newChanges.LastName.Trim()) hasChanges = true;
            if (_view.currentAccount.UserName.Trim() != _view.newChanges.UserName.Trim()) hasChanges = true;

            if (hasChanges == false) {
                _view.ShowMessage("No new changes found.", "Information", MessageBoxIcon.Information);
                return;
            }


            bool isInputCorrect = ValidateInput(_view.newChanges, isPasswordChanged);


            if (!isInputCorrect) return;
            else
            {
                _view.ClearWarnings();
                try
                {
                    string? newHash = PasswordHasher.HashPassword(_view.newChanges.NewPassword);
                    bool isSuccess = await _manageAccountRepository.UpdateProfileAsync(_view.newChanges, isPasswordChanged, newHash);

                    if (isSuccess) { 
                       
                        _view.RefreshUserProfile(_view.newChanges, newHash ?? string.Empty);
                        _view.ShowMessage("Profile successfully updated!", "Success", MessageBoxIcon.Information);
                        _view.ClearPasswordFields();
                        _view.EnableSaveBtn(false);

                        // log new update, update user management
                        await _changeLogRepo.LogChangeAsync(
                            ChangeTypes.UpdateUserManagement,
                            affectedUserId: null,  
                            payload: null,
                            changedBy: CurrentUser.UserID
                        );

                        PublicEvents.InvokeUpdateUserManagement(this);
                    }
                    else
                    {
                        _view.ShowMessage("Failed to save changes.", "Update Fail", MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) {
                    _view.ShowMessage($"Failed to update profile.\nError: {ex.Message}", "Database Error", MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput(NewChangesModel model, bool changePass) 
        {
            bool isValid = true;

            _view.ClearWarnings();

            string? fNameError = Helpers.Validator.IsNotEmpty(model.FirstName);
            _view.SetFirstNameWarning(fNameError);
            if (fNameError != null) isValid = false;

            string? lNameError = Helpers.Validator.IsNotEmpty(model.LastName);
            _view.SetLastNameWarning(lNameError);
            if (lNameError != null) isValid = false;

            string? uNameError = Helpers.Validator.IsNotEmpty(model.UserName);
            _view.SetUsernameWarning(uNameError);
            if (uNameError != null) isValid = false;


            if (changePass)
            {
                if (!string.IsNullOrEmpty(model.CurrentPassword))
                {
                    string? oldPassError = Helpers.Validator.IsNotEmpty(model.CurrentPassword);
                    if (oldPassError == null) oldPassError = Helpers.Validator.IsSameCurrentPassword(model.CurrentPassword, _view.currentAccount.HashedPassword);
                    _view.SetCurrentPasswordWarning(oldPassError);
                    if (oldPassError != null) isValid = false;
                }

                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    string? newPassError = Helpers.Validator.IsValidPassword(model.NewPassword);
                    _view.SetNewPasswordWarning(newPassError);
                    if (newPassError != null) isValid = false;

                    string? confirmPassError = Helpers.Validator.IsSameNewPassword(model.NewPassword ?? string.Empty, model.ConfirmPassword ?? string.Empty);
                    _view.SetConfirmPasswordWarning(confirmPassError);
                    if (confirmPassError != null) isValid = false;
                }
            }

            return isValid;
        }

    }
}
