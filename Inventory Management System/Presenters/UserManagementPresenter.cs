using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class UserManagementPresenter
    {
        private readonly IUserManagementRepository _repo;
        private readonly Views.IUserManagementView _view;
        private readonly IChangeLogRepository _changeLogRepo;
        private readonly EventHandler _onUpdateUserManagement;


        public UserManagementPresenter(IUserManagementRepository repo, IChangeLogRepository changeLogRepo, Views.IUserManagementView view)
        {
            _repo = repo;
            _changeLogRepo = changeLogRepo;
            _view = view;

            // events
            _view.DeleteUserClicked += OnDeleteUser_Clicked;
            _view.SearchChanged += OnSearchChanged;
            _view.EditUserClicked += OnEditUser;
            _view.CheckBoxTopClicked += OnCheckBoxTop_Clicked;
            _view.SelectionChanged += OnSelectionChanged;

            // reflect changes from user's profile
            _onUpdateUserManagement = async (s, e) => await LoadUsersTableAsync();
            PublicEvents.UpdateUserManagement += _onUpdateUserManagement;
        }

        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            UpdateSelectionState();
        }

        private int toggle = 0;
        private void OnCheckBoxTop_Clicked(object? sender, EventArgs e)
        {

            if (toggle == 0)
            {
                toggle = 1;
                _view.SetCheckBoxTopStatusIcon(status: 1);
                _view.SetAllRowsCheckboxesState(checkAll: true);
            }
            else if (toggle == 1 || toggle == 2)
            {
                toggle = 0;
                _view.SetCheckBoxTopStatusIcon(status: 0);
                _view.SetAllRowsCheckboxesState(checkAll: false);
            }

            UpdateSelectionState();
        }

        private void UpdateSelectionState()
        {
     
            int count = _view.CheckedCount;
            _view.SetTopDeleteButton(count); 

            if (count == 0)
            {
                _view.SetCheckBoxTopStatusIcon(status: 0); // unchecked
            }
            else if (count > 0 && !_view.AreAllRowsChecked)
            {
                _view.SetCheckBoxTopStatusIcon(status: 2); // indeterminate
            }
            else if (_view.AreAllRowsChecked)
            {
                _view.SetCheckBoxTopStatusIcon(status: 1); // all checked
            }
        }

        

        private void OnEditUser(object? sender, EventArgs e)
        {
            _view.PassDataToEditUserControl(_view.SelectedUser);
        }

        public async Task Initialize()
        {
            // Load table
            try
            {
                await LoadUsersTableAsync();
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Failed to load table.\nError: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void OnSearchChanged(object? sender, EventArgs e)
        {
            string term = _view.SearchUser;

            if (string.IsNullOrEmpty(term))
            {
                await LoadUsersTableAsync();
                return;
            }

            var results = await _repo.SearchUsersAsync(term);

            if (!string.IsNullOrEmpty(term) && results.Count == 0)
            {
                _view.RefreshUsersTable(results);

                _view.ShowMessage($"No results found for \"{term.Trim()}\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _view.RefreshUsersTable(results);
            }
        }
 
        private async void OnDeleteUser_Clicked(object? sender, EventArgs e)
        {
            var selectedUsers = _view.DeleteUsers?.ToList() ?? new List<DeleteUsersModel>();

            if (selectedUsers.Count == 0)
            {
                _view.ShowMessage(
                    "Please select at least one account to delete.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool ownAccountSelected = selectedUsers.Any(u => u.UserId == CurrentUser.UserID);
            bool selectedContainsAdmin = selectedUsers.Any(u => u.IsCurrentlyAdmin);

            if (ownAccountSelected)
            {
                selectedUsers.RemoveAll(u => u.UserId == CurrentUser.UserID);

                if (selectedUsers.Count > 1)
                {
                    _view.ShowMessage(
                        "Your account was excluded from deletion. Other selected users will still be processed.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else {
                    _view.ShowMessage(
                        "Cannot delete your own account.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }


                if (selectedUsers.Count == 0) return; // nothing left
            }

            int totalAdmins = await _repo.GetAdminCountAsync();
            int adminsToDelete = selectedUsers.Count(u => u.IsCurrentlyAdmin);

            if (totalAdmins - adminsToDelete < 1)
            {
                // Remove admin users from deletion
                selectedUsers.RemoveAll(u => u.IsCurrentlyAdmin);

                if (selectedUsers.Count == 0)
                {
                    _view.ShowMessage(
                        "Cannot delete the only admin account. At least one admin must remain.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                _view.ShowMessage(
                    "Admin account(s) were excluded to preserve system access.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            string confirmMsg =
                $"{selectedUsers.Count} user{(selectedUsers.Count > 1 ? "s" : "")} " +
                "will be permanently deleted. Do you want to proceed?";

            bool confirmedDeletion = _view.ConfirmAction(confirmMsg, "Confirm Delete");
            if (!confirmedDeletion) return;

            try
            {
                bool success = await _repo.DeleteAsync(
                    selectedUsers.Select(u => u.UserId).ToList()
                );

                string userString = (selectedUsers.Count == 1) ? "User" : "Users";
                
                if (success)
                {
                    _view.ShowMessage(
                        $"{userString} successfully removed!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersTableAsync();


                    foreach (var user in selectedUsers)
                    {
                        await _changeLogRepo.LogChangeAsync(
                            ChangeTypes.UserDeletedByAdmin,
                            affectedUserId: user.UserId,   // target only certain user/s
                            payload: null,
                            changedBy: CurrentUser.UserID
                        );
                    }

                    // refresh users table
                    await _changeLogRepo.LogChangeAsync(
                        ChangeTypes.UpdateUserManagement,
                        affectedUserId: null,              // affects everyone
                        payload: null,
                        changedBy: CurrentUser.UserID
                    );

                    // same-machine updates
                    foreach (var user in selectedUsers)
                    {
                        PublicEvents.InvokeUserDeletedByAdmin(this, user.UserId);
                    }
                    PublicEvents.InvokeUpdateUserManagement(this);
                }
                else
                {
                    _view.ShowMessage(
                        "Failed to delete the selected user(s).",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage(
                    $"Failed to apply changes.\nError: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        public async Task LoadUsersTableAsync()
        {
            try
            {
                var users = await _repo.GetAllUsersAsync();
                _view.RefreshUsersTable(users);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"An error occurred while loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
