using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Views;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class EditUser : UserControl, IEditUserView
    {
        private readonly InventoryForm _mainForm;
        private readonly EditUserPresenter _presenter;
        private readonly UserManagementForm _userManagementForm;
        

        public EditUser(InventoryForm mainForm, UserManagementForm userManagementForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _userManagementForm = userManagementForm;


            InstantiateFields_Buttons();
            WireEvents();
            PopulateUserRoleDetails();

            _presenter = new EditUserPresenter(new ModelsData.EditUserRepository(), new ChangeLogRepository(), this);
            ViewEditUserRoleSide();


            CenterControlOnLoad.ClickOutsideDetected += HideOnClickOutside;

            _ctbxs.AddRange([selectRole, fieldResetPass, fieldConfirmPass]);

        }

        public void AutoSignOut(string reason)
        {
            _mainForm.ForceSignOut(reason);
        }

        private void HideOnClickOutside(object? sender, EventArgs e)
        {
            if (sender == this)
                CloseEditUser();
        }

        protected override void Dispose(bool disposing)
        {
            // unwire outside click events to prevent memory leaks
            if (disposing)
            {
                CenterControlOnLoad.ClickOutsideDetected -= HideOnClickOutside;
            }
            base.Dispose(disposing);
        }


        private void SetActiveButton(bool isBtnRoleOptionActive)
        {
            if (isBtnRoleOptionActive)
            {
                btnChangeRoleOption.BorderColor = Color.Pink;
            }
            else {
                btnChangeRoleOption.BorderColor = Color.White;
            }
        } 


        public void CloseEditUser() {
            ClearFields();
            ClearWarnings();
            fieldResetPass.ResetPasswordEyeIcon();
            fieldConfirmPass.ResetPasswordEyeIcon();
            this.Hide();
        }
        public void ViewEditUserRoleSide()
        {
            SetActiveButton(isBtnRoleOptionActive: true);
            ResizeBaseControl(1);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(selectRole);
            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnChangeRoleSave);
            btnChangeRoleSave.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();

            bool isAdmin = GetSelectedUserData?.IsAdmin ?? false;
            selectRole.SelectedId = isAdmin ? 1 : 2;  // 1 = Administrator, 2 = Staff
            ClearWarnings();
        }

        public void ViewEditPasswordSide()
        {
            SetActiveButton(isBtnRoleOptionActive: true);
            ResizeBaseControl(1);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(fieldConfirmPass);
            fieldConfirmPass.BringToFront();
            fieldsPanel.Controls.Add(fieldResetPass);
            fieldConfirmPass.BringToFront();
            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnSaveNewUserPass);
            btnSaveNewUserPass.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();


            ClearWarnings();
        }

        public async Task RefreshUsersTable() => await _userManagementForm.RefreshTableAfterEdit();


        public void ResizeBaseControl(int panelNum)
        {
            fieldsPanel.Padding = panelNum == 1
                ? new Padding(0, 20, 0, 0)
                : new Padding(0, 0, 0, 0);

            basePanel.Invalidate();
            this.PerformLayout();
        }

        public void ClearWarnings()
        {
            if (_ctbxs != null && _ctbxs.Count > 1)
            {
                foreach (var c in _ctbxs)
                {
                    c.WarningLabel = string.Empty;
                }
            }
        }
        public void ClearFields()
        {
            // set combobox to default value
            if (_ctbxs != null && _ctbxs.Count > 1) {
                foreach (var c in _ctbxs)
                {
                    c.Input = string.Empty;
                }
            }
        }

        public void ShowMessage(string message, string header, MessageBoxIcon icon)
        {
            MessageBox.Show(message, header, MessageBoxButtons.OK, icon);
        }
        public bool ConfirmMessage(string message, string header, MessageBoxIcon icon)
        {
            DialogResult result = MessageBox.Show(message, header, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return result == DialogResult.OK;
        }

        public void SetRoleWarning(string? message) => selectRole.WarningLabel = message ?? string.Empty;

        public void SetResetPasswordWarning(string? message) => fieldResetPass.WarningLabel = message ?? string.Empty;

        public void SetConfirmPasswordWarning(string? message) => fieldConfirmPass.WarningLabel = message ?? string.Empty;


        public void InstantiateFields_Buttons()
        {
            // change pass side
            fieldResetPass = new()
            {
                Name = "fieldResetPass",
                Label = "Enter New Password",
                IsPassword = true,
                Margin = new Padding(0, 0, 0, 0),
                Size = new Size(288, 100),
                Dock = DockStyle.Top,
                Location = new Point(0, 0)
            };

            fieldConfirmPass = new()
            {
                Name = "fieldConfirmPass",
                Label = "Confirm New Password",
                IsPassword = true,
                Size = new Size(288, 100),
                Margin = new Padding(0, 0, 0, 0),
                Dock = DockStyle.Top
            };
            

            // change role side
            selectRole = new()
            {
                Name = "selectRole",
                Label = "Select Role",
                IsDropDown = true,
                Size = new Size(288, 100),
                Dock = DockStyle.Top,
                Location = new Point(0, 0)
            };
            selectRole.SetEditUserControl(this);


            btnChangeRoleSave = new()
            {
                Name = "btnSaveNewRole",
                Text = "Save Changes",
                Size = new Size(100, 40),
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(47, 39, 206),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.FromArgb(47, 39, 206),
                BorderSize = 1
            };
            
          
            btnSaveNewUserPass = new()
            {
                Name = "btnSaveNewUserPass",
                Text = "Save Changes",
                Size = new Size(100, 40),
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(47, 39, 206),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.FromArgb(47, 39, 206),
                BorderSize = 1
            };


            btnCancel = new()
            {
                Name = "btnCancel",
                Text = "Cancel",
                Size = new Size(70, 40),
                Dock = DockStyle.Right,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.White,// Color.FromArgb(47, 39, 206),
                MouseEnterColor = Color.FromArgb(229, 228, 226),
                MouseLeaveColor = Color.White,
                BorderSize = 1,
                TextOnHoverColor = Color.Black,
                TextOnLeaveColor = Color.Black,

            };

            btnCancel.Cursor = Cursors.Hand;
            btnChangeRoleOption.Cursor = Cursors.Hand;
            btnChangeRoleSave.Cursor = Cursors.Hand;
            btnEditOption.Cursor = Cursors.Hand;
            btnSaveNewUserPass.Cursor = Cursors.Hand;
        }

        public void PopulateUserRoleDetails()
        {
            // thread safety
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => PopulateUserRoleDetails()));
                return;
            }

            // 0 - no selection/null, 1 - admin, 2 - staff
            selectRole.AddItemToDropdown(
                 options: new List<AccountType>
                {
                    new AccountType { UserRole = "Staff",         Value = 2 },
                    new AccountType { UserRole = "Administrator", Value = 1 }
                }.Cast<object>().ToList(),
                displayMember: "UserRole",
                valueMember: "Value",
                customBtnDisplayText: null,
                panelToShow: null
            );

            
        }

        public void WireEvents()
        {
            // change role side
            btnChangeRoleSave.Click += (s, e) => SaveNewRoleChanges?.Invoke(this, EventArgs.Empty);

            btnChangeRoleOption.Click += (s, e) => ChangeRoleOption?.Invoke(this, EventArgs.Empty);

            // change pass side
            btnEditOption.Click += (s, e) => ChangePasswordOption?.Invoke(this, EventArgs.Empty);

            btnSaveNewUserPass.Click += (s, e) => SaveNewPassword?.Invoke(this, EventArgs.Empty);


            btnCancel.Click += (s, e) => CancelChanges?.Invoke(this, EventArgs.Empty);
            btn_Close.Click += (s, e) => CloseEditUser();
        }

        public void PassUserDataToEditUserRole(AppUsers user)
        {
            GetSelectedUserData = user;

            bool isAdmin = GetSelectedUserData?.IsAdmin ?? false;
            selectRole.SelectedId = isAdmin ? 1 : 2;  // 1 = Administrator, 2 = Staff
            ViewEditUserRoleSide();
        }

        public void UpdateSelectedUserRole(bool isAdmin)
        { 
            if(_selectedUserData != null)
                _selectedUserData.IsAdmin = isAdmin;

            GetUpdateUserRoleModel.NewRole = (isAdmin) ? 1 : 2;
            selectRole.SelectedId = isAdmin ? 1 : 2;
        }

        public void ResetManageAccountPass(string newHashedPass)
        {
   
            _mainForm.ResetUserPass(newHashedPass);
        }


        public UpdateUserRoleModel GetUpdateUserRoleModel
        {
            get
            {
                return new UpdateUserRoleModel
                {
                    UserId = GetSelectedUserData?.UserID ?? 0,
                    IsCurrentlyAdmin = GetSelectedUserData?.IsAdmin ?? false,
                    NewRole = selectRole.Selected is AccountType selectedRole ? selectedRole.Value : 0 // 0 - no selection/null, 1 - admin, 2 - staff
                };
            }
        }

        // for getting selection's int value and display string value to field 
        private AppUsers? _selectedUserData;
        public AppUsers? GetSelectedUserData
        {
            get
            {
                return _selectedUserData;
            }
            set
            {
                if (value != null)
                    _selectedUserData = value;
                else
                    _selectedUserData = null;
            }
        }

        private EditUserRoleModel _selectedRole;
        public EditUserRoleModel? SelectedRole
        {
            get
            {
                if (selectRole.Selected is EditUserRoleModel role)
                    return role;

                return _selectedRole;
            }

            set => _selectedRole = value;
        }

        public ResetUserPasswordModel ResetUserPasswordModel 
        {
            get
            {
                return new ResetUserPasswordModel
                {
                    Password = fieldResetPass.Input,
                    ConfirmPassword = fieldConfirmPass.Input,
                };

            }
        
        }

     


        CustomTextBox2 selectRole, fieldResetPass, fieldConfirmPass;
        CustomButton btnChangeRoleSave, btnCancel, btnSaveNewUserPass;

        List<CustomTextBox2> _ctbxs = [];

        public event EventHandler SaveNewRoleChanges;
        public event EventHandler ChangeRoleOption;
        public event EventHandler ChangePasswordOption;
        public event EventHandler SaveNewPassword;
        public event EventHandler CancelChanges;
    }
}
