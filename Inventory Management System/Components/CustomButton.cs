using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;
using Inventory_Management_System.DesignRenderers;
using Microsoft.EntityFrameworkCore.Query;


namespace Inventory_Management_System.Components
{
    public class CustomButton : Button, IBorderStyle
    {
        private int _borderSize = 0;
        private int _borderRadius = 0;   
        private Color _borderColor = Color.PaleVioletRed;

        private bool _isDropDown = false;
        private bool _isDropDownVisible = false;
        CustomPanel dropdownPanel = new CustomPanel();
        Image? dropdownImage = Properties.Resources.icons8_expand_arrow_48;
        private int btnHeight = 0, btnCount = 0, btnWidthMax = 0;

        private Color _mouseEnterColor = Color.FromArgb(47, 39, 206);
        private Color _mouseLeaveColor = Color.White;
        private Color _textOnHoverColor = Color.White;
        private Color _textOnLeaveColor = Color.Black;


        [Category("Border Styles")]
        public int BorderSize
        {
            get { return _borderSize; }
            set { 
                _borderSize = value; 
                this.Invalidate(); 
            }
        }

        [Category("Border Styles")]
        public int BorderRadius 
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("Border Styles")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Border Styles")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("Border Styles")]
        public int ImageSize
        {
            get {
                return this.Image?.Size.Width ?? 0; 
            }
            set
            {
                if (this.Image != null)
                {
                    this.Image = new Bitmap(this.Image, new Size(value, value));
                    this.Invalidate();
                }
            }
        }

        [Browsable(true)]
        public bool IsDropDown
        {
            get => _isDropDown;
            set
            {
                _isDropDown = value;
                if (value)
                    ApplyDropdown();
            }
        }

        [Browsable(false)]
        public bool IsDropDownVisible
        {
            get => _isDropDownVisible;
            set {
                _isDropDownVisible = value;
                if (dropdownPanel != null) {
                    dropdownPanel.Visible = value;
                }
            }
        }


        [Browsable(true)]
        [Category("Appearance")]
        public Color MouseEnterColor
        {
            get => _mouseEnterColor;
            set { _mouseEnterColor = value; Invalidate(); }
        }
        [Browsable(true)]
        [Category("Appearance")]
        public Color MouseLeaveColor
        {
            get => _mouseLeaveColor;
            set { _mouseLeaveColor = value; Invalidate(); }
        }
        [Browsable(true)]
        [Category("Appearance")]
        public Color TextOnHoverColor
        {
            get => _textOnHoverColor;
            set { _textOnHoverColor = value; Invalidate(); }
        }
        [Browsable(true)]
        [Category("Appearance")]
        public Color TextOnLeaveColor
        {
            get => _textOnLeaveColor;
            set { _textOnLeaveColor = value; Invalidate(); }
        }
    
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = _mouseEnterColor;
            this.ForeColor = _textOnHoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = _mouseLeaveColor;
            this.ForeColor = _textOnLeaveColor;
        }

  

        private void ApplyDropdown()
        {
            this.Click -= OnDropdownButtonClick; 
            this.Click += OnDropdownButtonClick;
        }

        private void OnDropdownButtonClick(object? sender, EventArgs e)
        {
            IsDropDownVisible = !IsDropDownVisible;

            if (IsDropDownVisible) AttachDropdownPanel();
        }

        private void AttachDropdownPanel()
        {
            // Find parent Form
            Control topLevel = this;
            while (topLevel.Parent != null)
                topLevel = topLevel.Parent;

            if (topLevel is not Form form) return;

            if (!form.Controls.Contains(dropdownPanel)){
                dropdownPanel.BackColor = Color.White;
                dropdownPanel.BorderStyle = BorderStyle.None;
                dropdownPanel.Cursor = Cursors.Default;
                dropdownPanel.AutoScroll = false;
                dropdownPanel.AutoSize = false;
                dropdownPanel.BorderSize = 1;
                dropdownPanel.BorderRadius = 5;
                dropdownPanel.BorderColor = Color.Gainsboro;
                dropdownPanel.Margin = new Padding(0);
                dropdownPanel.Padding = new Padding(0, 1, 0, 1);
                dropdownPanel.Visible = false;
                dropdownPanel.Name = "cBtnDropdownPanel";

                dropdownPanel.MouseLeave += OnDropdownPanelMouseLeave;

                WireClickOutside(form);
                form.Controls.Add(dropdownPanel);
            }

            dropdownPanel.Size = new Size(btnWidthMax, (btnHeight * btnCount) + dropdownPanel.Padding.Vertical);

            Point screenPos = this.PointToScreen(new Point(-125, this.Height + 5));
            Point formPos = form.PointToClient(screenPos);
            dropdownPanel.Location = formPos;
            dropdownPanel.BringToFront();
            dropdownPanel.Visible = true;
        }

        private void OnDropdownPanelMouseLeave(object? sender, EventArgs e)
        {
            Point mouse = dropdownPanel.PointToClient(Control.MousePosition);
            if (!dropdownPanel.ClientRectangle.Contains(mouse))
                IsDropDownVisible = false;
        }

        // Enable close dropdown panel on outside click [->
        private void OnFormClickOutside(object? sender, EventArgs e)
        {
            if (!_isDropDownVisible) return;
            if (sender == this) return;
            Point mouse = dropdownPanel.PointToClient(Control.MousePosition);
            if (!dropdownPanel.ClientRectangle.Contains(mouse))
                IsDropDownVisible = false;
        }

        // Recursively wire all controls on the form
        private void WireClickOutside(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child == dropdownPanel) continue; // skip the panel itself

                child.MouseDown += OnFormClickOutside;
                WireClickOutside(child); // recurse into nested controls
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Find parent form and unwire
                Control topLevel = this;
                while (topLevel.Parent != null)
                    topLevel = topLevel.Parent;

                if (topLevel is Form form)
                {
                    form.MouseDown -= OnFormClickOutside;
                    form.Click -= OnFormClickOutside;
                    UnwireClickOutside(form);
                }
            }
            base.Dispose(disposing);
        }

        private void UnwireClickOutside(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child == dropdownPanel) continue;
                child.MouseDown -= OnFormClickOutside;
                UnwireClickOutside(child);
            }
        }
        // <-]

        // Dynamically create dropdown button's options
        // KeyValuePair: <btnName, btnText>
        public void AddButtonToDropdown(List<KeyValuePair<string, string>> buttonName, bool showOptionValueAsButtonTitle) {
            
            dropdownPanel.Controls.Clear();

            int measuredMaxWidth = 0;
            int measuredHeight = 0;
            var buttons = new List<CustomButton>();


            foreach (KeyValuePair<string, string> btnName in buttonName)
            {
                CustomButton button = new CustomButton()    
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Text = btnName.Value,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular),
                    Name = btnName.Key,
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Padding = new Padding(2)    
                };

                button.BorderSize = 1;
                button.BorderColor = Color.Gainsboro;

                button.MouseEnter += (s, e) => button.BackColor = Color.LightGray;//.FromArgb(221, 219, 255);
                button.MouseLeave += (s, e) =>
                {
                    // Small delay check — mouse might be moving to another button in the panel
                    button.BackColor = Color.White;
                    Point mouse = dropdownPanel.PointToClient(Control.MousePosition);
                    if (!dropdownPanel.ClientRectangle.Contains(mouse))
                        IsDropDownVisible = false;
                };

                button.Click += (s, e) =>
                {
                    IsDropDownVisible = false; // Cloe dropdown on button click
                    if (showOptionValueAsButtonTitle)
                    {
                        this.Text = btnName.Value ?? null; // Set Dropdown button to selected option 
                        if (btnName.Key == "btn_ViewAll") this.Width = 70;
                    }
                    OnDropDownItemClicked(button.Name); // Call function to raise event
                };

                // Measure text width 
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    SizeF textSize = g.MeasureString(button.Text, button.Font);
                    int btnWidth = (int)Math.Ceiling(textSize.Width) + button.Padding.Horizontal + 16;
                    int btnH = Math.Max(35, (int)Math.Ceiling(textSize.Height) + 16);

                    measuredMaxWidth = Math.Max(measuredMaxWidth, btnWidth);
                    measuredHeight = btnH;
                }

                buttons.Add(button);
                dropdownPanel.Controls.Add(button);
            }

            foreach (var btn in buttons)
                btn.Height = measuredHeight;

            int finalWidth = Math.Max(measuredMaxWidth, this.Width);

            btnHeight = measuredHeight;
            btnCount = buttonName.Count;
            btnWidthMax = finalWidth;

            dropdownPanel.AutoSize = false;
            dropdownPanel.Size = new Size(finalWidth, (measuredHeight * btnCount) + dropdownPanel.Padding.Vertical);
        }


        // Assign event to buttons
        public event Action<string>? DropDownButtonItemClicked;
        private void OnDropDownItemClicked(string buttonName) { 
            DropDownButtonItemClicked?.Invoke(buttonName);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            BorderRenderer.DrawBorder(this, pevent, this);
        }

    }
}
