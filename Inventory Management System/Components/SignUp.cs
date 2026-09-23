using Inventory_Management_System.Components;
using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Views;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory_Management_System
{
    public partial class SignUp : UserControl, ISignUpView
    {
        private readonly form_loginSignUp _parentForm;
        private readonly SignUpPresenter _presenter;
        private InventoryForm? mainForm;
        private InventoryForm? _preloadedInventoryForm;
        private bool _isPreloading = false;


        public SignUp(form_loginSignUp parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;

            _presenter = new SignUpPresenter(this, new ChangeLogRepository(), new SignUpRepository());

            fields.AddRange([ctbx2_fName, ctbx2_lName, ctbx2_uName, ctbx2_pass, ctbx2_confirmPass]);

            WireEvents();
        }


        public void ClearFields()
        {
            foreach (CustomTextBox2 field in fields)
            {
                field.Input = string.Empty;
            }
        }
        public void ClearWarnings()
        {
            foreach (CustomTextBox2 field in fields)
            {
                field.WarningLabel = string.Empty;
            }
        }

        public void SetFirstNameWarning(string? message)
        {
            ctbx2_fName.WarningLabel = message ?? string.Empty;
        }
        public void SetLastNameWarning(string? message)
        {
            ctbx2_lName.WarningLabel = message ?? string.Empty;
        }
        public void SetUserNameWarning(string? message)
        {
            ctbx2_uName.WarningLabel = message ?? string.Empty;
        }
        public void SetPasswordWarning(string? message)
        {
            ctbx2_pass.WarningLabel = message ?? string.Empty;
        }
        public void SetConfirmPasswordWarning(string? message)
        {
            ctbx2_confirmPass.WarningLabel = message ?? string.Empty;
        }
        public void ShowMessage(string message, string header, MessageBoxIcon icon)
        {
            MessageBox.Show(message, header, MessageBoxButtons.OK, icon);
        }
        public void NavigateToLogin()
        {
            _parentForm.ShowSignIn();
        }

        private bool _isLoggingIn = false;
        public async Task NavigateToDashboard(AppUsers user)
        {
            if (user == null)
            {
                ShowMessage("Registration failed. Please try again.", "Error", MessageBoxIcon.Error);
                _isLoggingIn = false;
                return;
            }
            else
            {
                _isLoggingIn = true;
                btn_registerAccount.Cursor = Cursors.WaitCursor;
                btn_registerAccount.Enabled = false;
                btn_registerAccount.Text = "Loading...";

                while (_isPreloading)
                    await Task.Delay(100);

                // discard if disposed from a previous session
                if (_preloadedInventoryForm != null && _preloadedInventoryForm.IsDisposed)
                    _preloadedInventoryForm = null;


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
                btn_registerAccount.Cursor = Cursors.Hand;
                btn_registerAccount.Enabled = true;
                btn_registerAccount.Text = "Sign Up";
            }
        }


        public void WireEvents()
        {
            btn_registerAccount.Click += (s, e) => OnRegisterAccount_Click?.Invoke(this, EventArgs.Empty);
            lbl_signInRedirect.Click += (s, e) => OnGoToLogin_Click?.Invoke(this, EventArgs.Empty);

            RewireInputEvents();
        }

        public void RewireInputEvents() {
            ctbx2_fName.InputChanged += OnInput;
            ctbx2_lName.InputChanged += OnInput;
            ctbx2_uName.InputChanged += OnInput;
            ctbx2_pass.InputChanged += OnInput;
            ctbx2_confirmPass.InputChanged += OnInput;
        }

        private async void OnInput(object? sender, EventArgs e)
        {
            // unwire immediately so it only fires once
            ctbx2_fName.InputChanged -= OnInput;
            ctbx2_lName.InputChanged -= OnInput;
            ctbx2_uName.InputChanged -= OnInput;
            ctbx2_pass.InputChanged -= OnInput;
            ctbx2_confirmPass.InputChanged -= OnInput;


            // discard disposed instance
            if (_preloadedInventoryForm != null && _preloadedInventoryForm.IsDisposed)
                _preloadedInventoryForm = null;
            if (_isPreloading || _preloadedInventoryForm != null) return;

            _isPreloading = true;
            _preloadedInventoryForm = new InventoryForm(null, _parentForm); // null user, just preload UI
            await _preloadedInventoryForm.PreloadApplicationAsync();
            _isPreloading = false;
        }

        private readonly List<CustomTextBox2> fields = [];
        public SignUpModel RegisterInput
        {
            get
            {
                return new SignUpModel
                {
                    FirstName = ctbx2_fName.Input,
                    LastName = ctbx2_lName.Input,
                    UserName = ctbx2_uName.Input,
                    Password = ctbx2_pass.Input,
                    ConfirmPassword = ctbx2_confirmPass.Input
                };
            }
        }

        public event EventHandler? OnRegisterAccount_Click;
        public event EventHandler? OnGoToLogin_Click;
    }
}
