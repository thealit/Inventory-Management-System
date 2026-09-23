using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class NotificationItem : UserControl
    {

        private NotificationType _notifType;
        private bool _expandMessage = false;
        private int _collapsedHeight;
        private int _collapsedPanel1Height;
        private int _collapsedPanel2Height;
        private int _collapsedMessageHeight;

        public NotificationItem()
        {
            InitializeComponent();
            this.Paint += NotificationItem_Paint;

            this.Cursor = Cursors.Hand;
            WireClickToToggle(this);
        }

        private void ToggleExpand()
        {
            _expandMessage = !_expandMessage;

            // original heights
            if (_collapsedHeight == 0)
            {
                _collapsedHeight = this.Height;
                _collapsedPanel1Height = panel1.Height;
                _collapsedPanel2Height = panel2.Height;
                _collapsedMessageHeight = lbl_Message.Height;
            }

            if (_expandMessage)
            {
                lbl_Message.AutoEllipsis = false;
                lbl_Message.AutoSize = false;

                // full text height at current label width
                int fullTextHeight = TextRenderer.MeasureText(
                    lbl_Message.Text,
                    lbl_Message.Font,
                    new Size(lbl_Message.Width, int.MaxValue),
                    TextFormatFlags.WordBreak
                ).Height;

                int extraHeight = fullTextHeight - _collapsedMessageHeight;

                lbl_Message.Height = fullTextHeight;
                panel1.Height = _collapsedPanel1Height + extraHeight;
                panel2.Height = _collapsedPanel2Height + extraHeight;
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                tableLayoutPanel2.Height = _collapsedPanel1Height + extraHeight;
                this.Height = _collapsedHeight + extraHeight;
            }
            else
            {
                lbl_Message.AutoEllipsis = true;
                lbl_Message.AutoSize = false;

                lbl_Message.Height = _collapsedMessageHeight;
                panel1.Height = _collapsedPanel1Height;
                panel2.Height = _collapsedPanel2Height;
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.AutoSize, _collapsedPanel1Height);
                tableLayoutPanel2.Height = _collapsedPanel1Height;
                this.Height = _collapsedHeight;
            }

            this.Invalidate();
            this.Parent?.PerformLayout();
        }

        public void CollapseAllOnClose()
        {
            if (_collapsedHeight == 0) return; // never expanded, nothing to collapse
            if (!_expandMessage) return;       // already collapsed

            _expandMessage = false;

            lbl_Message.AutoEllipsis = true;
            lbl_Message.AutoSize = false;

            lbl_Message.Height = _collapsedMessageHeight;
            panel1.Height = _collapsedPanel1Height;
            panel2.Height = _collapsedPanel2Height;
            tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Absolute, _collapsedPanel1Height);
            tableLayoutPanel2.Height = _collapsedPanel1Height;
            this.Height = _collapsedHeight;

            this.Invalidate();
        }

        private void WireClickToToggle(Control parent)
        {
            parent.Click += (s, e) => ToggleExpand();
            foreach (Control child in parent.Controls)
                WireClickToToggle(child);  // recurse into nested controls
        }

        private void NotificationItem_Paint(object? sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Gainsboro, 1);

            // Calculate coordinates for the bottom line
            int startX = 0;
            int startY = this.Height - 1;
            int endX = this.Width;
            int endY = this.Height - 1;

            // Draw the line
            e.Graphics.DrawLine(pen, startX, startY, endX, endY);
            pen.Dispose();
        }

        public string? MessageHeader
        {
            get { return lbl_Header.Text; }
            set
            {
                lbl_Header.Text = value;
            }
        }

        public string? MessageDetails
        {
            get { return lbl_Message.Text; }
            set
            {
                lbl_Message.Text = value;
            }
        }

        public string? TimeLabel
        {
            get { return date.Text; }
            set { date.Text = value; }
        }

        public bool IsMessageRead
        {
            get { return !pbx_RedDot.Visible; }
            set { pbx_RedDot.Visible = !value; }
        }

        public NotificationType NotifType
        {
            get { return _notifType; }
            set
            {
                _notifType = value;
                ApplyPBX();
            }
        }

        public bool ExpandableMessage
        {
            get { return _expandMessage; }
            set
            {
                _expandMessage = value;
                ExpandDetails();
            }
        }



        private void ApplyPBX()
        {
            pBx_notifType.Image = _notifType switch
            {
                NotificationType.Critical => Properties.Resources.icons8_danger_48__2_,
                NotificationType.Warning => Properties.Resources.icons8_alert_48__1_,
                _ => Properties.Resources.icons8_information_48,
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }


        private void ExpandDetails()
        {
            lbl_Header.AutoEllipsis = !_expandMessage;
            lbl_Message.AutoEllipsis = !_expandMessage;

            lbl_Header.AutoSize = _expandMessage;
            lbl_Message.AutoSize = _expandMessage;
        }

        private void NotificationItem_Load(object sender, EventArgs e)
        {
        }


    }
}
