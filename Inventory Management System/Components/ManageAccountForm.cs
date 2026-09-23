using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Views;
using Microsoft.Data.SqlClient;
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
    public partial class ManageAccountForm : Form, IManageAccountView
    {
        public ManageAccountForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            _manageAccountPresenter = new ManageAccountPresenter(new ManageAccountRepository(), new ChangeLogRepository(), this);
            this.Visible = false;
            WireEvents();
            InitializeVariables();
        }

       
        private void ManageAccountForm_Load(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void WireEvents()
        {
            btn_Save.Click += (s, e) => SaveChanges?.Invoke(this, EventArgs.Empty);
            btn_CancelChanges.Click += (s, e) => CancelEdit?.Invoke(this, EventArgs.Empty);

            cTxt_Firstname.InputChanged += (s, e) => CheckForTextChanges();
            cTxt_Lastname.InputChanged += (s, e) => CheckForTextChanges();
            cTxt_Username.InputChanged += (s, e) => CheckForTextChanges();
            cTxt_CurrentPass.InputChanged += (s, e) =>
            {
                CheckForTextChanges();
                VerifyPass?.Invoke(this, EventArgs.Empty);
            };
            cTxt_NewPass.InputChanged += (s, e) =>
            {
                NewPass?.Invoke(this, EventArgs.Empty);
            };
            cTxt_ConfirmPass.InputChanged += (s, e) =>
            {
                CheckForTextChanges();
                ConfirmNewPass?.Invoke(this, EventArgs.Empty);
            };

        }

        private void InitializeVariables()
        {
            _ctbxs.AddRange(
            [   cTxt_Firstname,
                cTxt_Lastname,
                cTxt_Username,
                cTxt_NewPass,
                cTxt_ConfirmPass,
                cTxt_CurrentPass
            ]);
        }

        public void ClearWarnings()
        {
            if (_ctbxs != null)
            {
                foreach (CustomTextBox2 tb in _ctbxs)
                {
                    tb.WarningLabel = string.Empty;
                }
            }
        }
        public void ClearPasswordFields()
        {
            cTxt_CurrentPass.Input = string.Empty;
            cTxt_NewPass.Input = string.Empty;
            cTxt_ConfirmPass.Input = string.Empty;
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

        public void SetUsernameWarning(string? message) => cTxt_Username.WarningLabel = message ?? "";
        public void SetFirstNameWarning(string? message) => cTxt_Firstname.WarningLabel = message ?? "";
        public void SetLastNameWarning(string? message) => cTxt_Lastname.WarningLabel = message ?? "";
        public void SetCurrentPasswordWarning(string? message) => cTxt_CurrentPass.WarningLabel = message ?? "";
        public void SetNewPasswordWarning(string? message) => cTxt_NewPass.WarningLabel = message ?? "";
        public void SetConfirmPasswordWarning(string? message) => cTxt_ConfirmPass.WarningLabel = message ?? "";

        public void RefreshUserProfile(NewChangesModel newChanges, string? newHashedPassword)
        {
            cTxt_Firstname.Input = newChanges.FirstName;
            cTxt_Lastname.Input = newChanges.LastName;
            cTxt_Username.Input = newChanges.UserName;

            currentAccount.FirstName = newChanges.FirstName;
            currentAccount.LastName = newChanges.LastName;
            currentAccount.UserName = newChanges.UserName;

            if (!string.IsNullOrWhiteSpace(newHashedPassword))
                currentAccount.HashedPassword = newHashedPassword;

            _isResetting = false;

            _inventoryForm.SetUserInfo(newChanges, newHashedPassword);
        }

        public void PassCredentials(AppUsers currentUser, InventoryForm inventoryForm)
        {

            _user = currentUser;
            _inventoryForm = inventoryForm;

            if (_user != null)
            {
                currentAccount.Id = _user.UserID;
                currentAccount.FirstName = _user.FirstName;
                currentAccount.LastName = _user.LastName;
                currentAccount.UserName = _user.UserName;
                currentAccount.HashedPassword = _user.HashPassword;

                cTxt_Username.Input = currentAccount.UserName;
                cTxt_Lastname.Input = currentAccount.LastName;
                cTxt_Firstname.Input = currentAccount.FirstName;

                EnableSaveBtn(false);
                cTxt_NewPass.EnableText = false;
                cTxt_ConfirmPass.EnableText = false;

                
            }
        }
        public void ResetForm()
        {
            _isResetting = true;
            cTxt_Username.Input = currentAccount.UserName;
            cTxt_Lastname.Input = currentAccount.LastName;
            cTxt_Firstname.Input = currentAccount.FirstName;

            ClearPasswordFields();
            ClearWarnings();

            cTxt_NewPass.EnableText = false;
            cTxt_ConfirmPass.EnableText = false;

            _isResetting = false;

            EnableSaveBtn(false);
        }


        public void CheckForTextChanges()
        {
            if (_isResetting) return;
            if (_user == null) { return; }
            bool textHasChanges = false;

            if (cTxt_Firstname.Input.Trim() != currentAccount.FirstName) textHasChanges = true;
            if (cTxt_Lastname.Input.Trim() != currentAccount.LastName) textHasChanges = true;
            if (cTxt_Username.Input.Trim() != currentAccount.UserName) textHasChanges = true;

            EnableSaveBtn(textHasChanges);
        }

        public void EnableSaveBtn(bool isBtnEnabled)
        {
           
            if (isBtnEnabled == true)
            {
                btn_Save.Enabled = true;
                btn_Save.Cursor = Cursors.Hand;
                btn_Save.BackColor = Color.FromArgb(47, 39, 206);
                btn_Save.BorderColor = Color.FromArgb(47, 39, 206);
                btn_Save.ForeColor = Color.FromArgb(245, 245, 245);
            }
            else
            {
                btn_Save.Enabled = false;
                btn_Save.Cursor = Cursors.Default;
                btn_Save.BackColor = Color.FromArgb(100, 149, 237);
                btn_Save.BorderColor = Color.FromArgb(100, 149, 237);
                btn_Save.ForeColor = Color.FromArgb(245, 245, 245);
            }
        }

        

        private AppUsers _user;
        private List<CustomTextBox2> _ctbxs = new List<CustomTextBox2>();
        public NewChangesModel newChanges => new NewChangesModel
        {
            FirstName = cTxt_Firstname.Input.Trim(),
            LastName = cTxt_Lastname.Input.Trim(),
            UserName = cTxt_Username.Input.Trim(),
            CurrentPassword = cTxt_CurrentPass.Input.Trim(),
            NewPassword = cTxt_NewPass.Input.Trim(),
            ConfirmPassword = cTxt_ConfirmPass.Input.Trim(),
            Id = currentAccount.Id
        };


        public CurrentAccount currentAccount { get; set; } = new CurrentAccount();
        public bool IsUserNull => currentAccount == null;
        private bool _IsPasswordVerified;
        public bool IsPasswordVerified
        {
            get => _IsPasswordVerified;
            set
            {
                _IsPasswordVerified = value;
                cTxt_NewPass.EnableText = value;
                cTxt_ConfirmPass.EnableText = value;
            }
        }
        private bool _isPasswordConfirmed;
        public bool IsPasswordConfirmed
        {
            get => _isPasswordConfirmed;
            set { 
                _isPasswordConfirmed = value; 
                if(value == true) EnableSaveBtn(true);
            }
        }


        private bool _isResetting = false;

        public event EventHandler SaveChanges;
        public event EventHandler CancelEdit;
        public event EventHandler VerifyPass;
        public event EventHandler ConfirmNewPass;
        public event EventHandler InputChanged;
        public event EventHandler NewPass;

        private ManageAccountPresenter _manageAccountPresenter;
        private InventoryForm _inventoryForm;

    }
}
