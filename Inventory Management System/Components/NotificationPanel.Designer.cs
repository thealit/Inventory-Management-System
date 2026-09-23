namespace Inventory_Management_System.Components
{
    partial class NotificationPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationPanel));
            customPanel1 = new CustomPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            notifMenu = new CustomButton();
            _lbl_Notif = new Label();
            customPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // customPanel1
            // 
            customPanel1.BackColor = Color.White;
            customPanel1.BackgroundColor = Color.White;
            customPanel1.BorderColor = Color.Gainsboro;
            customPanel1.BorderRadius = 0;
            customPanel1.BorderSize = 1;
            customPanel1.Controls.Add(flowLayoutPanel1);
            customPanel1.Controls.Add(panel1);
            customPanel1.Dock = DockStyle.Fill;
            customPanel1.Location = new Point(0, 0);
            customPanel1.Name = "customPanel1";
            customPanel1.Padding = new Padding(12, 14, 12, 11);
            customPanel1.Size = new Size(374, 444);
            customPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(12, 63);
            flowLayoutPanel1.Margin = new Padding(0, 0, 0, 10);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(350, 360);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(notifMenu);
            panel1.Controls.Add(_lbl_Notif);
            panel1.Cursor = Cursors.Hand;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(12, 14);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 49);
            panel1.TabIndex = 3;
            // 
            // notifMenu
            // 
            notifMenu.BackColor = Color.Transparent;
            notifMenu.BackgroundColor = Color.Transparent;
            notifMenu.BorderColor = Color.PaleVioletRed;
            notifMenu.BorderRadius = 0;
            notifMenu.BorderSize = 0;
            notifMenu.FlatAppearance.BorderSize = 0;
            notifMenu.FlatStyle = FlatStyle.Flat;
            notifMenu.Image = (Image)resources.GetObject("notifMenu.Image");
            notifMenu.ImageSize = 15;
            notifMenu.IsDropDown = true;
            notifMenu.IsDropDownVisible = false;
            notifMenu.Location = new Point(313, 4);
            notifMenu.MouseEnterColor = Color.Transparent;
            notifMenu.MouseLeaveColor = Color.Transparent;
            notifMenu.Name = "notifMenu";
            notifMenu.Size = new Size(34, 22);
            notifMenu.TabIndex = 2;
            notifMenu.TextOnHoverColor = Color.Transparent;
            notifMenu.TextOnLeaveColor = Color.Transparent;
            notifMenu.UseVisualStyleBackColor = false;
            // 
            // _lbl_Notif
            // 
            _lbl_Notif.AutoSize = true;
            _lbl_Notif.Dock = DockStyle.Top;
            _lbl_Notif.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lbl_Notif.ForeColor = SystemColors.ControlDarkDark;
            _lbl_Notif.Location = new Point(0, 0);
            _lbl_Notif.Margin = new Padding(3, 0, 3, 15);
            _lbl_Notif.Name = "_lbl_Notif";
            _lbl_Notif.Size = new Size(126, 25);
            _lbl_Notif.TabIndex = 0;
            _lbl_Notif.Text = "Notifications";
            // 
            // NotificationPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(customPanel1);
            Name = "NotificationPanel";
            Size = new Size(374, 444);
            customPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private CustomPanel customPanel1;
        private Label _lbl_Notif;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox notifMenus;
        private CustomButton notifMenu;
    }
}
