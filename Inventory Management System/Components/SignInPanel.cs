using Inventory_Management_System.Components;
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

namespace Inventory_Management_System
{
    public partial class SignInPanel : UserControl, ISignInView
    {
        private readonly form_loginSignUp _parentForm;
        private readonly SignInPresenter _presenter;
        private InventoryForm? mainForm;
        private InventoryForm? _preloadedInventoryForm;
        private bool _isPreloading = false;

        public SignInPanel(form_loginSignUp parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;

            _presenter = new SignInPresenter(this, new SignInRepository());

            ctbx_uname.TabIndex = 1;
            ctbx_pass.TabIndex = 2;
            btn_login.TabIndex = 3;
            lbl_signUpRedirect.TabIndex = 4;
            WireEvents();
        }

        public void ClearWarnings()
        {
            ctbx_uname.WarningLabel = "";
            ctbx_pass.WarningLabel = "";
        }
        public void ClearFields()
        {
            ctbx_uname.Input = string.Empty;
            ctbx_pass.Input = string.Empty;
        }
        public void SetUserNameWarning(string? message)
        {
            ctbx_uname.WarningLabel = message ?? string.Empty;
        }
        public void SetPasswordWarning(string? message)
        {
            ctbx_pass.WarningLabel = message ?? string.Empty;
        }
        public void ShowMessage(string message, string header, MessageBoxIcon icon)
        {
            MessageBox.Show(message, header, MessageBoxButtons.OK, icon);
        }
        public void NavigateToRegister()
        {
            _parentForm.ShowSignUp();
        }
        public async Task NavigateToDashboard(AppUsers user)
        {
            if (user == null)
            {
                ShowMessage("Login failed. Please try again.", "Error", MessageBoxIcon.Error);
                _isLoggingIn = false;
                return;
            }
            else
            {
                _isLoggingIn = true;
                btn_login.Cursor = Cursors.WaitCursor;
                btn_login.Enabled = false;
                btn_login.ForeColor = Color.White;
                btn_login.Text = "Loading...";

         
                while (_isPreloading)
                    await Task.Delay(100);

                if (_preloadedInventoryForm == null)
                {
                    // fallback if user clicked before typing
                    _preloadedInventoryForm = new InventoryForm(user, _parentForm);
                    await _preloadedInventoryForm.PreloadApplicationAsync();
                }

                _preloadedInventoryForm.SetCurrentUser(user); // apply the actual logged-in user
                mainForm = _preloadedInventoryForm;

                CentralizedPoller.Initialize(new ChangeLogRepository(), user.UserID);
                await CentralizedPoller.Instance.StartAsync(intervalMs: 3000);

                mainForm.Show();
                _parentForm.Hide();

                _isLoggingIn = false;
                btn_login.Cursor = Cursors.Hand;
                btn_login.Enabled = true;
                btn_login.Text = "Sign In";
            }
        }

        private bool _isLoggingIn = false;
        public void WireEvents()
        {
            btn_login.Click += (s, e) =>
            {
                if (_isLoggingIn) return;
                OnLogin_Click?.Invoke(this, EventArgs.Empty);
            };
            
            lbl_signUpRedirect.Click += (s, e) => OnGoToSignUp_Click?.Invoke(this, EventArgs.Empty);

            // preload mainform and child forms on input
            ctbx_pass.InputChanged += OnInput;
            ctbx_uname.InputChanged += OnInput;
        }

        private async void OnInput(object? sender, EventArgs e)
        {
            // unwire immediately so it only fires once
            ctbx_uname.InputChanged -= OnInput;
            ctbx_pass.InputChanged -= OnInput;

            if (_isPreloading || _preloadedInventoryForm != null) return;

            _isPreloading = true;
            _preloadedInventoryForm = new InventoryForm(null, _parentForm); // null user, just preload UI
            await _preloadedInventoryForm.PreloadApplicationAsync();
            _isPreloading = false;
        }

        public SignInModel LoginInput
        {
            get
            {
                return new SignInModel
                {
                    UserName = ctbx_uname.Input,
                    Password = ctbx_pass.Input,
                };
            }
        }

        public event EventHandler? OnLogin_Click;
        public event EventHandler? OnGoToSignUp_Click;
    }
}
