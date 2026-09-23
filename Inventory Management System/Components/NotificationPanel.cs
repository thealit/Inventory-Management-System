using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class NotificationPanel : UserControl, INotificationViews
    {
        public event EventHandler? MarkAllAsReadClicked;
        public event EventHandler? ViewAllNotifClicked;
        public event EventHandler? ViewUnreadClicked;
        public event EventHandler? NotifMenuClicked;
        public event Action<string>? NotifMenuActionClicked;
        public event EventHandler? OnClearAll;
        public event EventHandler? OnClearPast30DaysNotifs;
        public event EventHandler? RefreshNotifs;

        public NotificationPanel()
        {
            InitializeComponent();

            notifMenu.AddButtonToDropdown(new List<KeyValuePair<string, string>>()
            {
                new("clear_past30days", "Clear older than 30 days"),
                new("clear_all",     "Clear all"),
                new("mark_all_read", "Mark all as read"),
                new("view_unread",   "View unread"),
                new("view_all",   "View all"),
            }, false);

            notifMenu.DropDownButtonItemClicked += (action) =>
                NotifMenuActionClicked?.Invoke(action);

            this.ParentChanged += (s, e) =>
            {
                if (this.Parent == null) return;

                // Walk up to find the Form
                Control p = this.Parent;
                while (p.Parent != null) p = p.Parent;

                if (p is Form form)
                {
                    EventHandler resizeHandler = (rs, re) =>
                    {
                        if (notifMenu.IsDropDownVisible)
                            notifMenu.PerformClick();
                    };

                    form.Resize += resizeHandler;

                    this.Disposed += (s, e) =>
                    {
                        form.Resize -= resizeHandler;
                    };
                }
            };


            this.HandleCreated += (s, e) =>
            {
                NotifMenuActionClicked?.Invoke("view_all");
            };

            this.VisibleChanged += (s, e) =>
            {
                if (!this.Visible)
                {
                    notifMenu.IsDropDownVisible = false;
                    CollapseAllNotifItems();
                }
            };

            
            this.DoubleBuffered = true;
        }

       

        private void CollapseAllNotifItems()
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is NotificationItem item)
                    item.CollapseAllOnClose();
            }
            flowLayoutPanel1.PerformLayout();
        }

       

        public void AddNotification(Models.NotificationModel model)
        {
            
            int scrollbarWidth = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            var notifItem = new NotificationItem
            {
                MessageHeader = model.Header,
                MessageDetails = model.SubMessage,
                IsMessageRead = model.IsRead,
                NotifType = model.Type,
                TimeLabel = model.TimeLabel,
                Width = flowLayoutPanel1.ClientSize.Width - (scrollbarWidth + 15),
                ExpandableMessage = false
            };

            notifItem.SuspendLayout();
            flowLayoutPanel1.Controls.Add(notifItem);
            notifItem.ResumeLayout(false);
        }

        public void ClearNotifications()
        {

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.Dispose();
            }

            flowLayoutPanel1.Controls.Clear();

            flowLayoutPanel1.Padding = new Padding(0);
        }

        public void DisplayNoNotif(string customText)
        {
            Label noNotifFound = new()
            {
                Text = customText,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Width = 160,
                Padding = new Padding(0)
            };

            int flowLayoutPadding_TopBottom = (flowLayoutPanel1.ClientSize.Height - noNotifFound.ClientSize.Height) / 2;
            int flowLayoutPadding_LeftRight = (flowLayoutPanel1.ClientSize.Width - noNotifFound.ClientSize.Width) / 2;

            flowLayoutPanel1.Padding = new Padding(flowLayoutPadding_LeftRight, flowLayoutPadding_TopBottom, flowLayoutPadding_LeftRight, flowLayoutPadding_TopBottom);
            flowLayoutPanel1.Controls.Add(noNotifFound);
        }

        public void BeginLoadNotifications()
        {
            flowLayoutPanel1.SuspendLayout();
        }
        public void EndLoadNotifications()
        { 
            flowLayoutPanel1.ResumeLayout(true);
            flowLayoutPanel1.PerformLayout();
        }

      
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            && (((int)m.WParam & 0xFFFF) == 5))
            {
                // Change SB_THUMBTRACK to SB_THUMBPOSITION
                m.WParam = (IntPtr)(((int)m.WParam & ~0xFFFF) | 4);
            }
            base.WndProc(ref m);
        }

    }
}
