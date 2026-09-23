using Inventory_Management_System.Components;
using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Threading.Channels;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Inventory_Management_System
{
    public partial class InventoryForm : Form
    {


        // onHoverNavBarTabs
        private readonly System.Windows.Forms.Timer _panelHideTimer = new();

        private CustomPanel? _activeNavPanel;
        private System.Windows.Forms.UserControl? _activeSubPanel;
        private readonly Color _activeColor = Color.FromArgb(240, 240, 251);
        private readonly Color _defaultColor = Color.Transparent;
        private readonly Views.IInventoryView _view;
        private readonly Views.INotificationViews _notifview;
        public event EventHandler InvalidateInventory;

        public InventoryForm(AppUsers user, form_loginSignUp formLoginSignup)
        {
            InitializeComponent();
            _currentUser = user;
            _loginSignUpForm = formLoginSignup;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _notifPresenter = new NotificationPresenter(this, new NotificationRepository(), new InventoriesRepository());

            this.Controls.Add(_notifPanel);
            if (_notifPanel != null)
                _notifPanel.Visible = false;

            this.ResizeRedraw = true;

            System.IO.MemoryStream ms = new(Properties.Resources.icons8_box_100);
            this.Icon = new System.Drawing.Icon(ms);

            this.FormClosed += (s, e) =>
            {
                PublicEvents.UserDeletedByAdmin -= OnUserDeletedByAdmin;
                PublicEvents.UserRoleChangedByAdmin -= OnUserRoleChangedByAdmin;
                PublicEvents.InventoryChanged -= _notifPresenter.OnInventoryChanged;
            };

            if (user != null)
            {
                _currentUser = user;
                btn_ManageUsers.Visible = user.IsAdmin;
                btn_AuditTrail.Visible = user.IsAdmin;
            }
            else
            {
                // preload mode — user will be set later via SetCurrentUser
                btn_ManageUsers.Visible = false;
                btn_AuditTrail.Visible = false;
            }

            this.Resize += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Normal ||
                    this.WindowState == FormWindowState.Maximized)
                {
                    InvalidateAll();
                    InvalidateInventory?.Invoke(this, EventArgs.Empty);
                }
            };

            pbx_redDot.Visible = false;
        }

        public void SetCurrentUser(AppUsers user)
        {

            _currentUser = user;
            btn_ManageUsers.Visible = user.IsAdmin;
            btn_AuditTrail.Visible = user.IsAdmin;

            if (_manageAccountForm != null)
            {
                _manageAccountForm.PassCredentials(_currentUser, this);
            }

            _ = _notifPresenter.LoadNotificationsAsync();

            // instant logout when admin deletes/change role a user
            PublicEvents.UserDeletedByAdmin += OnUserDeletedByAdmin;
            PublicEvents.UserRoleChangedByAdmin += OnUserRoleChangedByAdmin;
        }
        private async void OnUserDeletedByAdmin(object? sender, UserDeletedEventArgs e)
        {
            if (CurrentUser.UserID == 0) return;

            if (e.DeletedUserId != CurrentUser.UserID) return;

            ForceSignOut("Your account has been removed by an administrator. You will now be signed out.");
        }

        private async void OnUserRoleChangedByAdmin(object? sender, UserRoleChangedEventArgs e)
        {
            if (e.UserId != CurrentUser.UserID) return;

            ForceSignOut("Your account role has been updated by an administrator.You will be signed out to apply these changes.");
        }

        // closing/opening app window by clicking app icon in taskbar
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;
            const int SC_MAXIMIZE = 0xF030;

            if (m.Msg == WM_SYSCOMMAND)
            {
                int command = m.WParam.ToInt32() & 0xFFF0;

                if (command == SC_RESTORE || command == SC_MAXIMIZE)
                {
                    InvalidateAll();
                    _notifPanel.Hide();
                    InvalidateInventory?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        // preload forms and data
        public async Task PreloadApplicationAsync()
        {
            if (_inventoryForm == null)
            {
                _inventoryForm = new Inventory(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    MdiParent = this,
                    Dock = DockStyle.Top,

                };
                _inventoryForm.Show();

                _inventoryPresenter = new InventoryPresenter(_inventoryRepository, new ChangeLogRepository(), _inventoryForm);

                _inventoryForm.SetPresenter(_inventoryPresenter);

                await _inventoryPresenter.Initialize();
            }


            if (_manageAccountForm == null)
            {
                _manageAccountForm = new ManageAccountForm();
                _manageAccountForm.PassCredentials(_currentUser, this);
                _manageAccountForm.MdiParent = this;
                _manageAccountForm.Dock = DockStyle.Fill;
                _manageAccountForm.Show();
                _manageAccountForm.Hide();
            }

            if (_userManagementForm == null /*&& _currentUser?.IsAdmin ?? false*/)
            {
                _userManagementForm = new UserManagementForm(this)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };
                _userManagementForm.Show();
                _userManagementForm.Hide();

                _userManagementPresenter = new UserManagementPresenter(new UserManagementRepository(), new ChangeLogRepository(), _userManagementForm);

                await _userManagementForm.SetPresenter(_userManagementPresenter);

            }

            if (_auditTrailForm == null)
            {
                _auditTrailForm = new AuditTrailForm(this)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };
                _auditTrailForm.Show();
                _auditTrailForm.Hide();

                _auditTrailPresenter = new AuditTrailPresenter(new AuditTrailRepository(), _auditTrailForm);
                await _auditTrailForm.SetPresenter(_auditTrailPresenter);
            }

            _currentForm = _inventoryForm;
        }





        private void Form2_Load(object sender, EventArgs e)
        {
            // navbar
            OnHoverNavBarTabs();
            SetUpSearchBar();

            lbl_Username.Text = _currentUser.IsAdmin ? "Admin" : "Staff";
            lbl_Fullname.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";

            List<Button> btn = [btn_Inventory, btn_ManageAccount, btn_SignOut, btn_ManageUsers, btn_AuditTrail];
            SetUpSideBarButtons(btn);

            btn_Inventory.PerformClick(); // open default form on load
            btn_SignOut.BackColor = Color.Transparent;

            this.Resize += (s, e) => SetNavBarTabsPanelLocation(_notifPanel, pBx_NotifBell);

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient mdiClient)
                {
                    // remove sunken border
                    this.SetBevel(false);

                    // double buffering to reduce flicker
                    typeof(Control)
                        .GetProperty("DoubleBuffered",
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance)
                        ?.SetValue(mdiClient, true);

                    mdiClient.BackColor = Color.FromArgb(251, 251, 254);
                }
            }
        }


        private void OnHoverNavBarTabs()
        {
            // timer for delay off visible of subPanels
            _panelHideTimer.Interval = 50;
            _panelHideTimer.Tick += (s, e) =>
            {
                _panelHideTimer.Stop();
                if (_activeNavPanel == null || _activeSubPanel == null) return;

                if (_activeNavPanel.IsDisposed || _activeSubPanel.IsDisposed)
                {
                    _activeNavPanel = null;
                    _activeSubPanel = null;
                    return;
                }

                var mouse = System.Windows.Forms.Control.MousePosition;
                bool overNav = _activeNavPanel.ClientRectangle.Contains(
                                   _activeNavPanel.PointToClient(mouse));
                bool overSub = _activeSubPanel.ClientRectangle.Contains(
                                   _activeSubPanel.PointToClient(mouse));

                if (!overNav && !overSub)
                {
                    _activeSubPanel.Visible = false;
                    _activeNavPanel.BackColor = _defaultColor;
                    _activeNavPanel.BackgroundColor = _defaultColor;
                    _activeNavPanel.BorderColor = _defaultColor;
                    _activeNavPanel = null;
                    _activeSubPanel = null;
                }
            };

            // notif
            SetNavBarTabsPanelLocation(_notifPanel, pBx_NotifBell);
            OnHover(cPanel_NotifBell, pBx_NotifBell, _activeColor, _notifPanel);

            cPanel_NotifBell.Disposed += (s, e) =>
            {
                _panelHideTimer.Stop();
                if (_activeNavPanel == cPanel_NotifBell) _activeNavPanel = null;
                if (_activeSubPanel == _notifPanel) _activeSubPanel = null;
            };
        }

        private void OnHover(CustomPanel panel, PictureBox pbx, Color color, System.Windows.Forms.UserControl subPanel)
        {
            void Show()
            {
                if (_activeSubPanel != null && _activeSubPanel != subPanel)
                {
                    _activeSubPanel.Visible = false;

                    if (_activeNavPanel != null)
                    {
                        _activeNavPanel.BackColor = _defaultColor;
                        _activeNavPanel.BackgroundColor = _defaultColor;
                        _activeNavPanel.BorderColor = _defaultColor;
                    }
                }

                // show the new one
                _activeNavPanel = panel;
                _activeSubPanel = subPanel;

                panel.Disposed += (s, e) =>
                {
                    _panelHideTimer.Stop();
                    _activeNavPanel = null;
                    _activeSubPanel = null;
                };

                panel.BackColor = color;
                panel.BackgroundColor = color;
                panel.BorderColor = color;
                subPanel.Visible = true;
                subPanel.BringToFront();
            }

            void StartHide() => _panelHideTimer.Start();

            panel.MouseEnter += (s, e) => Show();
            panel.MouseLeave += (s, e) => StartHide();
            panel.MouseHover += (s, e) => Show();
            pbx.MouseEnter += (s, e) => Show();
            pbx.MouseHover += (s, e) => Show();

            // Cancel hide when mouse enters the sub panel
            subPanel.MouseEnter += (s, e) => _panelHideTimer.Stop();
            subPanel.MouseLeave += (s, e) => StartHide();

            WireChildControls(subPanel, StartHide);
        }

        private void WireChildControls(System.Windows.Forms.Control parent, Action onLeave)
        {
            foreach (System.Windows.Forms.Control child in parent.Controls)
            {
                child.MouseEnter += (s, e) => _panelHideTimer.Stop();
                child.MouseLeave += (s, e) => onLeave();
                WireChildControls(child, onLeave);
            }
        }

        public void ShowNotifRedDot(bool hasUnreadNotif)
        {
            pbx_redDot.Visible = hasUnreadNotif;
        }


        int toggleCount = 0;    // 1-show, 0-hide
        private void PBx_NotifBell_Click(object sender, EventArgs e)
        {
            if (toggleCount == 0)
            {
                toggleCount++;
                SetNavBarTabsPanelLocation(_notifPanel, pBx_NotifBell);
                if (!_notifPanel.Visible)
                {
                    _notifPanel.Visible = true;
                    _notifPanel.BringToFront();
                }
            }
            else if (toggleCount == 1)
            {
                toggleCount--;
                _notifPanel.Visible = false;
            }
        }



        // Switch Mdi child form
        private Form? _currentForm;
        private async Task SwitchFormAsync(Form newForm)
        {
            if (_currentForm != null && _currentForm != newForm)
                _currentForm.Hide(); // hide previous, don't close it

            _currentForm = newForm;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Invalidate();
            await Task.Delay(60);
            newForm.Visible = true;
        }
        private async void Btn_Inventory_Click(object sender, EventArgs e)
        {
            if (_inventoryForm == null)
            {
                _inventoryForm = new Inventory(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    MdiParent = this,
                    Dock = DockStyle.Top
                };
                _inventoryForm.Show();

                _inventoryPresenter = new InventoryPresenter(_inventoryRepository, new ChangeLogRepository(), _inventoryForm);
                _inventoryForm.SetPresenter(_inventoryPresenter); // newly added
                _inventoryForm.InvalidateAll();
                await _inventoryPresenter.Initialize();
                if (!_inventoryForm.Visible)
                {

                    _inventoryForm.Show();

                }
            }
            _inventoryForm.InvalidateAll();
            _inventoryForm.RefreshInventoryTable();
            await SwitchFormAsync(_inventoryForm);
        }

        private async void Btn_ManageAccount_Click(object sender, EventArgs e)
        {
            if (_manageAccountForm == null)
            {
                _manageAccountForm = new ManageAccountForm();
                _manageAccountForm.PassCredentials(_currentUser, this);
                _manageAccountForm.MdiParent = this;
                _manageAccountForm.Dock = DockStyle.Fill;
                _manageAccountForm.Visible = true;
            }

            _manageAccountForm.ResetForm();
            _manageAccountForm.Dock = DockStyle.Fill;
            _manageAccountForm.PassCredentials(_currentUser, this);
            await SwitchFormAsync(_manageAccountForm);
        }

        private async void Btn_ManageUsers_Click(object sender, EventArgs e)
        {
            if (!_currentUser.IsAdmin) return;

            if (_userManagementForm == null)
            {
                _userManagementForm = new UserManagementForm(this)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill,
                    Visible = true
                };
            }
            _userManagementForm.Dock = DockStyle.Fill;

            await SwitchFormAsync(_userManagementForm);
        }

        private async void Btn_AuditTrail_Click(object sender, EventArgs e)
        {
            if (!_currentUser.IsAdmin) return;

            if (_auditTrailForm == null)
            {
                _auditTrailForm = new AuditTrailForm(this)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill,
                    Visible = true
                };
            }
            _auditTrailForm.Dock = DockStyle.Fill;

            await SwitchFormAsync(_auditTrailForm);
        }


        // Setup side bar buttons
        private Button? _activeButton = null;
        private void SetUpSideBarButtons(List<Button> button)
        {
            Color activeColor = Color.FromArgb(68, 61, 255);
            Color inactiveColor = Color.Transparent;

            foreach (var btn in button)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = activeColor;

                // only reset to inactive if this button is not the active one
                btn.MouseLeave += (s, e) =>
                {
                    if (_activeButton != btn)
                        btn.BackColor = inactiveColor;
                };

                btn.Click += (s, e) =>
                {
                    // reset the previously active button
                    if (_activeButton != null)
                        _activeButton.BackColor = inactiveColor;

                    // set the new active button
                    _activeButton = btn;
                    btn.BackColor = activeColor;
                };
            }
        }

        // Setup side bar user profile n pass
        public void SetUserInfo(NewChangesModel? newChanges, string newHashPass)
        {
           // lbl_Username.Text = newChanges?.UserName;
            lbl_Fullname.Text = $"{newChanges?.FirstName} {newChanges?.LastName}";

            // Update current user object too // new
            _currentUser.UserName = newChanges.UserName;
            _currentUser.FirstName = newChanges.FirstName;
            _currentUser.LastName = newChanges.LastName;

            if (!string.IsNullOrWhiteSpace(newHashPass))
                _currentUser.HashPassword = newHashPass;
        }

        public void ResetUserPass(string newHashPass)
        {
            if (!string.IsNullOrWhiteSpace(newHashPass))
                _currentUser.HashPassword = newHashPass;
        }

        // Search item
        public void SetUpSearchBar()
        {
            _searchTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // wait 300ms after user stops typing
            };
            _searchTimer.Tick += (s, e) =>
            {
                _searchTimer.Stop();

                if (_inventoryForm == null) return;
                if (_currentForm != _inventoryForm) btn_Inventory.PerformClick();

                _inventoryForm.SearchItem = searchBar1.Input.Trim();
            };

            searchBar1.InputChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start(); // reset timer on each keystroke
            };
        }

        // Set navbar subpanels location
        public void SetNavBarTabsPanelLocation(System.Windows.Forms.UserControl control, PictureBox pbx)
        {
            Point buttonBottomRightOnScreen = pbx.PointToScreen(new Point(pbx.Width, pbx.Height));
            Point targetLocationOnForm = this.PointToClient(buttonBottomRightOnScreen);

            int x = (targetLocationOnForm.X - control.Width) + 20;

            int y = targetLocationOnForm.Y + (tblLP_NavBar.Height - targetLocationOnForm.Y);

            control.Location = new Point(x, y);
        }

        private void Btn_SignOut_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to sign out?",
                "Sign Out",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.OK) return;

            ProceedSignOut();
        }


        private void TblLP_NavBar_Paint(object sender, PaintEventArgs e)
        {
            // Set bottom border of navbar 

            // Set border color and thickness
            Pen pen = new(Color.Gainsboro, 1);

            // Calculate coordinates for the bottom line
            int startX = 0;
            int startY = tblLP_NavBar.Height - 1;
            int endX = tblLP_NavBar.Width;
            int endY = tblLP_NavBar.Height - 1;

            // Draw the line
            e.Graphics.DrawLine(pen, startX, startY, endX, endY);
            pen.Dispose();
        }


        private void InvalidateAll()
        {
            // invalidate the MDI parent itself
            this.Invalidate(true);
            this.PerformLayout();
            this.Update();

            // invalidate all active MDI child forms
            foreach (Form child in this.MdiChildren)
            {
                child.Invalidate(true);
                child.PerformLayout();
                child.Update();

                // also invalidate all nested controls inside each child
                InvalidateControls(child);
            }
        }

        private static void InvalidateControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Invalidate(true);
                ctrl.PerformLayout();
                ctrl.Update();

                if (ctrl.Controls.Count > 0)
                    InvalidateControls(ctrl); // recurse into nested controls
            }
        }



        public void ForceSignOut(string reason)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => ForceSignOut(reason));
                return;
            }

            MessageBox.Show(reason, "Signing Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            ProceedSignOut();
        }

        private void ProceedSignOut()
        {
            CentralizedPoller.Instance?.Stop();
            CentralizedPoller.Clear();
            CurrentUser.ClearSession();
            _loginSignUpForm.ResetPanels();
            _loginSignUpForm.Show();
            _loginSignUpForm.ShowSignIn();
            this?.Close();
        }


        private System.Windows.Forms.Timer _searchTimer;

        private AppUsers _currentUser;
        private readonly form_loginSignUp _loginSignUpForm;
        private readonly SignInRepository _signInRepo = new();
        // Inventory
        private Inventory _inventoryForm;
        private InventoryPresenter _inventoryPresenter;
        private readonly InventoriesRepository _inventoryRepository = new();
        // Notification
        private readonly NotificationPresenter _notifPresenter;
        // Notification panel
        private readonly NotificationPanel _notifPanel = new();
        public NotificationPanel NotifPanel => _notifPanel;
        // Manage Account
        private ManageAccountForm _manageAccountForm;
        // UserManagementPresenter
        public UserManagementForm _userManagementForm;
        public UserManagementPresenter _userManagementPresenter;
        // Audit Trail
        public AuditTrailForm _auditTrailForm;
        public AuditTrailPresenter _auditTrailPresenter;

   
    }
}



