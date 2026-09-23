using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Inventory_Management_System.Components
{
    public partial class CustomTextBox2 : UserControl
    {
        public CustomTextBox2()
        {
            InitializeComponent();

            textBox1.TextChanged += (s, e) => InputChanged?.Invoke(this, e);
            textBox1.Enter += (s, e) => inputPanel.BorderColor = Color.FromArgb(68, 61, 255);
            textBox1.Leave += (s, e) => inputPanel.BorderColor = Color.Gainsboro;

            this.LocationChanged += (s, e) => RepositionDropdown();
            this.SizeChanged += (s, e) => RepositionDropdown();

        }

       

        [Browsable(false)]
        public string Input
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        [Browsable(false)]
        public string Placeholder
        {
            set { textBox1.PlaceholderText = value; }
        }

        [Browsable(true)]
        public bool EnableText
        {
            get { return textBox1.Enabled; }
            set { textBox1.Enabled = value; }
        }


        [Browsable(false)]
        public string WarningLabel
        {
            get { return txtBx_warning.Text; }
            set
            {
                txtBx_warning.Text = value;
                ApplyWarningLabelChanges();
            }
        }


        public string? Label
        {
            get { return _label; }
            set
            {
                _label = value;
                textBx_Header.Text = value;
            }
        }

        public Font HeaderFont
        {
            get { return textBx_Header.Font; }
            set
            {
                textBx_Header.Font = value;
                Invalidate();
            }
        }


        public Color TextBoxColor
        {
            get { return textBox1.ForeColor; }
            set
            {
                textBox1.ForeColor = value;
                Invalidate();
            }
        }

        private Color _txtBxBgColor = Color.White;
        [Browsable(true)]
        public Color TextBoxBackColor
        {
            get { return _txtBxBgColor; }
            set
            {
                if (_txtBxBgColor == value) return;
                _txtBxBgColor = value;
                textBox1.BackColor = value;
                inputPanel.BackColor = value;
                Invalidate();
            }
        }



        public bool IsPassword
        {
            get { return _isPassword; }
            set
            {
                _isPassword = value;
                UpdatePBx();
                UpdateTabOrder();
                Invalidate();
            }
        }




        public bool IsReadOnly
        {
            get { return textBox1.ReadOnly; }
            set
            {
                if (_isDropdown)
                {
                    textBox1.ReadOnly = true;
                }
                else
                {
                    textBox1.ReadOnly = value;
                }
                textBox1.BackColor = Color.White;
                Invalidate();
            }
        }

        public bool IsDropDown
        {
            get { return _isDropdown; }
            set
            {
                _isDropdown = value;
                UpdatePBx();
                UpdateTabOrder();
                Invalidate();
            }
        }

        [Browsable(false)]
        public bool IsDropdownVisible
        {
            get => _isDropdownVisible;
            set
            {
                _isDropdownVisible = value;
                if (txtBxDropdownPanel != null)
                    txtBxDropdownPanel.Visible = value;

                inputPanel.BorderColor = value ? Color.FromArgb(68, 61, 255) : Color.Gainsboro;
            }
        }

        [Browsable(false)]
        public object? Selected
        {
            get => _selection;
            set => _selection = value;
        }

        private bool _suppressSelectedChanged = false;

        public int SelectedId
        {
            get
            {
                if (_selection == null) return 0;

                var value = _selection?.GetType()
                                       .GetProperty(_valueMember) // _valueMember -> private property which value is assigned on CustomTextBox2's AddItemToDropdown()
                                       ?.GetValue(_selection);

                return value is int id ? id : 0;
            }
            set
            {
                if (value == 0)  // reset if 0 -> default no value
                {
                    _selection = null;
                    textBox1.Text = string.Empty;
                    return;
                }

                // find the item in the dropdown list that matches the given ID
                foreach (var item in _dropdownItems)
                {
                    var itemId = item?.GetType()
                                     .GetProperty(_valueMember)  // ex., CategoryId/UnitId
                                     ?.GetValue(item);

                    if (itemId is int id && id == value)  // found the matching item
                    {
                        _selection = item;  // store the matched object as selection
                        _suppressSelectedChanged = true;
                        // extract and display the display value in the textbox
                        textBox1.Text = item?.GetType()
                                             .GetProperty(_displayMember)  // ex., Category/Unit name
                                             ?.GetValue(item)
                                             ?.ToString() ?? string.Empty;
                        _suppressSelectedChanged = false;
                        return;
                    }
                }

                // no match found, reset
                _selection = null;
                textBox1.Text = string.Empty;
            }
        }



        private void UpdatePBx()
        {
            if (_isPassword && _isDropdown)
                throw new InvalidOperationException("Control cannot be both Password and Dropdown.");

            if (_isPassword == true)
            {
                pbx_Btn.Image = Properties.Resources.view_password;
                pbx_Btn.SizeMode = PictureBoxSizeMode.Zoom;
                pbx_Btn.Visible = true;
                textBox1.UseSystemPasswordChar = true;
            }
            else if (_isDropdown)
            {
                pbx_Btn.Image = Properties.Resources.icons8_dropdown_48;
                pbx_Btn.SizeMode = PictureBoxSizeMode.Zoom;
                IsReadOnly = true;
                pbx_Btn.Visible = true;
            }
            else
            {
                pbx_Btn.Visible = false;
            }
        }

        private void AttachDropdownPanel()
        {
            // Find parent Form
            Control topLevel = this;
            while (topLevel.Parent != null)
                topLevel = topLevel.Parent;

            if (topLevel is not Form form) return;

            if (!form.Controls.Contains(txtBxDropdownPanel))
            {
                txtBxDropdownPanel.BackColor = Color.White;
                txtBxDropdownPanel.BorderStyle = BorderStyle.None;
                txtBxDropdownPanel.Cursor = Cursors.Default;
                txtBxDropdownPanel.AutoSize = false;
                txtBxDropdownPanel.AutoScroll = true;
                txtBxDropdownPanel.BorderSize = 1;
                txtBxDropdownPanel.BorderRadius = 5;
                txtBxDropdownPanel.BorderColor = Color.Gainsboro;
                txtBxDropdownPanel.Margin = new Padding(0);
                txtBxDropdownPanel.Padding = new Padding(0, 1, 0, 1);
                txtBxDropdownPanel.Visible = false;
                txtBxDropdownPanel.Name = "txtBxDropdownPanel";
                txtBxDropdownPanel.VerticalScroll.Visible = true;
                
                WireClickOutside(form);
                form.Resize += OnFormResize;
                form.Controls.Add(txtBxDropdownPanel);
            }


            IsDropdownVisible = true;
            RepositionDropdown();

            txtBxDropdownPanel.BringToFront();
            txtBxDropdownPanel.Visible = true;
        }

        private int btnCount = 1, btnHeight = 35;    // add button included
        private const int MIN_BTN_TO_SCROLL = 4; 
        public void AddItemToDropdown<T>(List<T>? options, string? displayMember, string? valueMember, string? customBtnDisplayText, Control? panelToShow)
        {
            
            btnCount = 1;
            
            txtBxDropdownPanel.Controls.Clear();

            _displayMember = displayMember;
            _valueMember = valueMember;
            _dropdownItems = options.Cast<object>().ToList();

            int y = 0;

            foreach (var item in options)
            {
                var displayValue = item?.GetType()
                                       .GetProperty(displayMember)
                                       ?.GetValue(item)
                                       ?.ToString();

                var btn = new CustomButton
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    AutoSize = false,
                    Margin = new Padding(0),
                    Padding = new Padding(2),
                    Text = displayValue,
                    Dock = DockStyle.Top,
                    Height = 35,
                    Tag = item,
                    Location = new Point(0, y)
                };

                btn.BorderSize = 1;
                btn.BorderColor = Color.Gainsboro;

                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(221, 219, 255);
                btn.MouseLeave += (s, e) => btn.BackColor = Color.White;

                btn.Click += (s, e) =>
                {
                    textBox1.Text = displayValue;
                    _selection = btn.Tag;  // store selected item
                    txtBxDropdownPanel.Visible = false;
                    _isDropdownVisible = false;
                    SelectedChanged?.Invoke(this, new SelectedChangedEventArgs(selectedChangeByUser: true));
                };

                txtBxDropdownPanel.Controls.Add(btn);
                y += btn.Height;
                btnCount++;
                btnHeight = btn.Height;
            }


            if (customBtnDisplayText != null && panelToShow != null)
            {
                var customBtn = CreateCustomButton(customBtnDisplayText, panelToShow);

                customBtn.Location = new Point(0, y);
                customBtn.Width = txtBxDropdownPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

                txtBxDropdownPanel.Controls.Add(customBtn);

                y += customBtn.Height;
            }
            // fixed
            txtBxDropdownPanel.Height =
                 (btnCount > MIN_BTN_TO_SCROLL
                    ? (btnHeight * MIN_BTN_TO_SCROLL) + txtBxDropdownPanel.Padding.Vertical
                    : (btnHeight * (customBtnDisplayText != null ? (btnCount) : (btnCount - 1))) + txtBxDropdownPanel.Padding.Vertical);
            // fixed

            txtBxDropdownPanel.Width = inputPanel.Width;
            txtBxDropdownPanel.PerformLayout();
        }


        
        public void SetManageCategoryControl(ManageCategory manageCategory)
        {
            _manageCategory = manageCategory;
        }

        public void SetManageUnitControl(ManageUnit manageUnit)
        {
            _manageUnit = manageUnit;
        }

        public void SetEditUserControl(EditUser editUser)
        {
            _editUser = editUser;
        }

        private CustomButton CreateCustomButton(string displayText, Control panelToShow) {
            var btn = new CustomButton
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                AutoSize = false,
                Margin = new Padding(0),
                Padding = new Padding(2),
                Dock = DockStyle.Top,
                Height = 35
            };

            btn.Text = displayText;

            btn.BorderSize = 1;
            btn.BorderColor = Color.Gainsboro;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(221, 219, 255);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.White;

            btn.Click += (s, e) =>
            {
                txtBxDropdownPanel.Visible = false;
                _isDropdownVisible = false;

                CenterControlOnLoad.ShowCentered(panelToShow);
            };

            return btn;
        }

    


        // -- Close panel when clicked outside it [->
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Control topLevel = this;
                    while (topLevel?.Parent != null)   
                        topLevel = topLevel.Parent;

                    if (topLevel is Form form && !form.IsDisposed) 
                    {
                        form.MouseDown -= OnFormClickOutside;
                        form.Click -= OnFormClickOutside;
                        form.Resize -= OnFormResize;
                        UnwireClickOutside(form);

                        //inputPanel.BorderColor = Color.Gainsboro; // set border color to default
                    }

                    // Guard the UI property access
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                            this.BeginInvoke(new Action(() => {
                                if (!inputPanel.IsDisposed)
                                    inputPanel.BorderColor = Color.Gainsboro;
                            }));
                        else
                            inputPanel.BorderColor = Color.Gainsboro;
                    }
                }
                catch (ObjectDisposedException) { }
                catch (InvalidOperationException) { }
            }
            base.Dispose(disposing);
        }


        private void OnFormClickOutside(object? sender, EventArgs e)
        {
            if (!_isDropdownVisible) return;

            Point mouse = txtBxDropdownPanel.PointToClient(Control.MousePosition);
            if (!txtBxDropdownPanel.ClientRectangle.Contains(mouse))
                IsDropdownVisible = false;
        }
        private void UnwireClickOutside(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child == txtBxDropdownPanel) continue;
                child.MouseDown -= OnFormClickOutside;
                UnwireClickOutside(child);
            }
        }
        private void WireClickOutside(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child == txtBxDropdownPanel) continue; // skip the panel itself

                child.MouseDown += OnFormClickOutside;
                WireClickOutside(child); // recurse into nested controls
            }
        }
        // <-]





        // Other Events
        private void pbx_Btn_Click_1(object sender, EventArgs e)
        {
            if (_isPassword)
            {
                if (_isPassToggleStateActive)
                {
                    // show
                    pbx_Btn.Image = null;
                    pbx_Btn.Image = Properties.Resources.hide_password;
                    textBox1.UseSystemPasswordChar = false;
                    _isPassToggleStateActive = !_isPassToggleStateActive;
                }
                else
                {
                    // hide
                    pbx_Btn.Image = null;
                    pbx_Btn.Image = Properties.Resources.view_password;
                    textBox1.UseSystemPasswordChar = true;
                    _isPassToggleStateActive = !_isPassToggleStateActive;
                }
            }

            if (_isDropdown)
            {
                if (_isDropdownToggleStateActive)
                {
                    AttachDropdownPanel();
                    _isDropdownToggleStateActive = !_isDropdownToggleStateActive;
                }
                else{
                    IsDropdownVisible = false;
                    _isDropdownToggleStateActive = !_isDropdownToggleStateActive;
                }
            }
        }

        public void ResetPasswordEyeIcon() {
            // hide
            pbx_Btn.Image = null;
            pbx_Btn.Image = Properties.Resources.view_password;
            textBox1.UseSystemPasswordChar = true;
            _isPassToggleStateActive = !_isPassToggleStateActive;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;


        }



        // Overrrides
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // redraw border on every resize
        }


        // Methods
        private void UpdateTabOrder()
        {
            textBox1.TabIndex = 1;
            pbx_Btn.TabIndex = 2;
        }
        private void ApplyWarningLabelChanges()
        {
            if (string.IsNullOrEmpty(txtBx_warning.Text))
            {
                // warning is null/empty, hide the label
                txtBx_warning.Visible = false;
                this.Height = DEFAULT_HEIGHT;
                return;
            }
            else { txtBx_warning.Visible = true; }
            
            SizeF textSize = TextRenderer.MeasureText(
                txtBx_warning.Text, 
                txtBx_warning.Font,
                new Size(inputPanel.Width, int.MaxValue), //txtBx_warning.Width
                TextFormatFlags.WordBreak);

            int warningHeight = (int)Math.Ceiling(textSize.Height);// + 1; padding
            this.Height = DEFAULT_HEIGHT + warningHeight;
            txtBx_warning.Height = warningHeight;
            txtBx_warning.Width = inputPanel.Width;
        }

        private void RepositionDropdown()
        {
            if (!_isDropdownVisible || txtBxDropdownPanel == null || txtBxDropdownPanel.Parent == null) return;

            Control topLevel = this;
            while (topLevel.Parent != null) topLevel = topLevel.Parent;
            if (topLevel is not Form form) return;

            Point screenPos = inputPanel.PointToScreen(new Point(0, inputPanel.Height + 1));
            Point formPos = form.PointToClient(screenPos);
            txtBxDropdownPanel.Location = formPos;
        }

        private void OnFormResize(object? sender, EventArgs e)
        {
            RepositionDropdown();
        }


        CustomPanel txtBxDropdownPanel = new();

        private object? _selection;
        private string? _label = "Header";
        private bool _isPassword = false;
        private bool _isPassToggleStateActive = false, _isDropdownToggleStateActive = false;
        private bool _isDropdown = false;
        private bool _isDropdownVisible = false;
        private const int DEFAULT_HEIGHT = 80;

        public event EventHandler? InputChanged;
        public event EventHandler<SelectedChangedEventArgs> SelectedChanged;

        private ManageCategory? _manageCategory;
        private ManageUnit? _manageUnit;
        private EditUser? _editUser;


        private List<object> _dropdownItems = new();
        private string _displayMember = string.Empty;
        private string _valueMember = string.Empty;
    }

    
}
