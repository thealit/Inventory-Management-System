namespace Inventory_Management_System
{
    partial class InventoryForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            tblLP_NavBar = new TableLayoutPanel();
            panel1 = new Panel();
            searchBar1 = new Inventory_Management_System.Components.SearchBar();
            cPanel_NotifBell = new Inventory_Management_System.Components.CustomPanel();
            pbx_redDot = new PictureBox();
            pictureBox1 = new PictureBox();
            pBx_NotifBell = new PictureBox();
            txtBx_Search = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            panel2 = new Panel();
            btn_AuditTrail = new Button();
            btn_ManageUsers = new Button();
            btn_SignOut = new Button();
            btn_ManageAccount = new Button();
            label2 = new Label();
            btn_Inventory = new Button();
            panel3 = new Panel();
            lbl_Username = new Label();
            lbl_Fullname = new Label();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            tblLP_NavBar.SuspendLayout();
            panel1.SuspendLayout();
            cPanel_NotifBell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_redDot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBx_NotifBell).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tblLP_NavBar
            // 
            tblLP_NavBar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblLP_NavBar.BackColor = Color.FromArgb(251, 251, 254);
            tblLP_NavBar.BackgroundImageLayout = ImageLayout.None;
            tblLP_NavBar.ColumnCount = 2;
            tblLP_NavBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.23474F));
            tblLP_NavBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.76526F));
            tblLP_NavBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.Controls.Add(panel1, 1, 0);
            tblLP_NavBar.Dock = DockStyle.Top;
            tblLP_NavBar.Location = new Point(254, 0);
            tblLP_NavBar.Margin = new Padding(3, 2, 3, 2);
            tblLP_NavBar.Name = "tblLP_NavBar";
            tblLP_NavBar.Padding = new Padding(26, 0, 26, 0);
            tblLP_NavBar.RowCount = 1;
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLP_NavBar.Size = new Size(1430, 58);
            tblLP_NavBar.TabIndex = 0;
            tblLP_NavBar.Paint += TblLP_NavBar_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(searchBar1);
            panel1.Controls.Add(cPanel_NotifBell);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(718, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 6, 0, 6);
            panel1.Size = new Size(686, 58);
            panel1.TabIndex = 1;
            // 
            // searchBar1
            // 
            searchBar1.Anchor = AnchorStyles.Right;
            searchBar1.BackColor = Color.Transparent;
            searchBar1.Input = "";
            searchBar1.Location = new Point(217, 9);
            searchBar1.Margin = new Padding(0, 0, 20, 0);
            searchBar1.Name = "searchBar1";
            searchBar1.PlaceholderText = "Search by item or category...";
            searchBar1.Size = new Size(413, 40);
            searchBar1.TabIndex = 14;
            searchBar1.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            // 
            // cPanel_NotifBell
            // 
            cPanel_NotifBell.Anchor = AnchorStyles.Right;
            cPanel_NotifBell.BackColor = Color.Transparent;
            cPanel_NotifBell.BackgroundColor = Color.Transparent;
            cPanel_NotifBell.BorderColor = Color.Transparent;
            cPanel_NotifBell.BorderRadius = 5;
            cPanel_NotifBell.BorderSize = 1;
            cPanel_NotifBell.Controls.Add(pbx_redDot);
            cPanel_NotifBell.Controls.Add(pictureBox1);
            cPanel_NotifBell.Controls.Add(pBx_NotifBell);
            cPanel_NotifBell.Cursor = Cursors.Hand;
            cPanel_NotifBell.Location = new Point(642, 9);
            cPanel_NotifBell.Margin = new Padding(0);
            cPanel_NotifBell.Name = "cPanel_NotifBell";
            cPanel_NotifBell.Padding = new Padding(8);
            cPanel_NotifBell.Size = new Size(41, 40);
            cPanel_NotifBell.TabIndex = 6;
            // 
            // pbx_redDot
            // 
            pbx_redDot.BackColor = Color.Transparent;
            pbx_redDot.BackgroundImageLayout = ImageLayout.None;
            pbx_redDot.Image = Properties.Resources.icons8_circle_72;
            pbx_redDot.Location = new Point(20, 9);
            pbx_redDot.Name = "pbx_redDot";
            pbx_redDot.Size = new Size(12, 10);
            pbx_redDot.SizeMode = PictureBoxSizeMode.Zoom;
            pbx_redDot.TabIndex = 5;
            pbx_redDot.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.icons8_profile_96;
            pictureBox1.Location = new Point(-71, -16);
            pictureBox1.Margin = new Padding(9, 20, 22, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(37, 28);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pBx_NotifBell
            // 
            pBx_NotifBell.BackColor = Color.Transparent;
            pBx_NotifBell.Dock = DockStyle.Fill;
            pBx_NotifBell.Image = Properties.Resources.icons8_notification_bell_96;
            pBx_NotifBell.Location = new Point(8, 8);
            pBx_NotifBell.Margin = new Padding(0);
            pBx_NotifBell.Name = "pBx_NotifBell";
            pBx_NotifBell.Size = new Size(25, 24);
            pBx_NotifBell.SizeMode = PictureBoxSizeMode.Zoom;
            pBx_NotifBell.TabIndex = 3;
            pBx_NotifBell.TabStop = false;
            pBx_NotifBell.Click += PBx_NotifBell_Click;
            // 
            // txtBx_Search
            // 
            txtBx_Search.BorderStyle = BorderStyle.None;
            txtBx_Search.Dock = DockStyle.Fill;
            txtBx_Search.Font = new Font("Segoe UI", 10.0173912F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBx_Search.ForeColor = Color.Black;
            txtBx_Search.Location = new Point(25, 11);
            txtBx_Search.Margin = new Padding(0);
            txtBx_Search.Name = "txtBx_Search";
            txtBx_Search.Size = new Size(370, 18);
            txtBx_Search.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.BackColor = Color.FromArgb(250, 250, 250);
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.6767673F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0, 0, 0, 20);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.Padding = new Padding(5);
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(847, 80);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.6173248F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.3826714F));
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(5, 5);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(837, 50);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(47, 39, 206);
            panel2.Controls.Add(btn_AuditTrail);
            panel2.Controls.Add(btn_ManageUsers);
            panel2.Controls.Add(btn_SignOut);
            panel2.Controls.Add(btn_ManageAccount);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btn_Inventory);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox2);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20);
            panel2.Size = new Size(254, 871);
            panel2.TabIndex = 5;
            // 
            // btn_AuditTrail
            // 
            btn_AuditTrail.BackColor = Color.Transparent;
            btn_AuditTrail.BackgroundImageLayout = ImageLayout.None;
            btn_AuditTrail.Cursor = Cursors.Hand;
            btn_AuditTrail.FlatAppearance.BorderSize = 0;
            btn_AuditTrail.FlatStyle = FlatStyle.Flat;
            btn_AuditTrail.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_AuditTrail.ForeColor = Color.FromArgb(251, 251, 254);
            btn_AuditTrail.Image = Properties.Resources.icons8_business_report_16;
            btn_AuditTrail.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AuditTrail.Location = new Point(0, 290);
            btn_AuditTrail.Margin = new Padding(0);
            btn_AuditTrail.Name = "btn_AuditTrail";
            btn_AuditTrail.Padding = new Padding(25, 0, 0, 0);
            btn_AuditTrail.Size = new Size(254, 47);
            btn_AuditTrail.TabIndex = 15;
            btn_AuditTrail.Text = "      Audit Trail";
            btn_AuditTrail.TextAlign = ContentAlignment.MiddleLeft;
            btn_AuditTrail.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_AuditTrail.UseVisualStyleBackColor = false;
            btn_AuditTrail.Click += Btn_AuditTrail_Click;
            // 
            // btn_ManageUsers
            // 
            btn_ManageUsers.BackColor = Color.Transparent;
            btn_ManageUsers.BackgroundImageLayout = ImageLayout.None;
            btn_ManageUsers.Cursor = Cursors.Hand;
            btn_ManageUsers.FlatAppearance.BorderSize = 0;
            btn_ManageUsers.FlatStyle = FlatStyle.Flat;
            btn_ManageUsers.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ManageUsers.ForeColor = Color.FromArgb(251, 251, 254);
            btn_ManageUsers.Image = Properties.Resources.icons8_people_24;
            btn_ManageUsers.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ManageUsers.Location = new Point(0, 243);
            btn_ManageUsers.Margin = new Padding(0);
            btn_ManageUsers.Name = "btn_ManageUsers";
            btn_ManageUsers.Padding = new Padding(25, 0, 0, 0);
            btn_ManageUsers.Size = new Size(254, 47);
            btn_ManageUsers.TabIndex = 14;
            btn_ManageUsers.Text = "    User Management";
            btn_ManageUsers.TextAlign = ContentAlignment.MiddleLeft;
            btn_ManageUsers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ManageUsers.UseVisualStyleBackColor = false;
            btn_ManageUsers.Click += Btn_ManageUsers_Click;
            // 
            // btn_SignOut
            // 
            btn_SignOut.Anchor = AnchorStyles.Bottom;
            btn_SignOut.BackColor = Color.FromArgb(47, 39, 206);
            btn_SignOut.BackgroundImageLayout = ImageLayout.None;
            btn_SignOut.Cursor = Cursors.Hand;
            btn_SignOut.FlatAppearance.BorderSize = 0;
            btn_SignOut.FlatStyle = FlatStyle.Flat;
            btn_SignOut.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SignOut.ForeColor = Color.FromArgb(251, 251, 254);
            btn_SignOut.Image = Properties.Resources.icons8_sign_out_16;
            btn_SignOut.ImageAlign = ContentAlignment.MiddleLeft;
            btn_SignOut.Location = new Point(0, 796);
            btn_SignOut.Margin = new Padding(0);
            btn_SignOut.Name = "btn_SignOut";
            btn_SignOut.Padding = new Padding(25, 0, 0, 0);
            btn_SignOut.Size = new Size(254, 47);
            btn_SignOut.TabIndex = 13;
            btn_SignOut.Text = "      Sign Out";
            btn_SignOut.TextAlign = ContentAlignment.MiddleLeft;
            btn_SignOut.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_SignOut.UseVisualStyleBackColor = false;
            btn_SignOut.Click += Btn_SignOut_Click;
            // 
            // btn_ManageAccount
            // 
            btn_ManageAccount.BackColor = Color.Transparent;
            btn_ManageAccount.BackgroundImageLayout = ImageLayout.None;
            btn_ManageAccount.Cursor = Cursors.Hand;
            btn_ManageAccount.FlatAppearance.BorderSize = 0;
            btn_ManageAccount.FlatStyle = FlatStyle.Flat;
            btn_ManageAccount.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ManageAccount.ForeColor = Color.FromArgb(251, 251, 254);
            btn_ManageAccount.Image = Properties.Resources.icons8_writer_male_16;
            btn_ManageAccount.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ManageAccount.Location = new Point(0, 196);
            btn_ManageAccount.Margin = new Padding(0);
            btn_ManageAccount.Name = "btn_ManageAccount";
            btn_ManageAccount.Padding = new Padding(25, 0, 0, 0);
            btn_ManageAccount.Size = new Size(254, 47);
            btn_ManageAccount.TabIndex = 12;
            btn_ManageAccount.Text = "      Manage Account";
            btn_ManageAccount.TextAlign = ContentAlignment.MiddleLeft;
            btn_ManageAccount.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ManageAccount.UseVisualStyleBackColor = false;
            btn_ManageAccount.Click += Btn_ManageAccount_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 6.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(251, 251, 254);
            label2.Location = new Point(23, 121);
            label2.Margin = new Padding(0, 0, 0, 5);
            label2.Name = "label2";
            label2.Size = new Size(157, 23);
            label2.TabIndex = 9;
            label2.Text = "NAVIGATION";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btn_Inventory
            // 
            btn_Inventory.BackColor = Color.Transparent;
            btn_Inventory.BackgroundImageLayout = ImageLayout.None;
            btn_Inventory.Cursor = Cursors.Hand;
            btn_Inventory.FlatAppearance.BorderSize = 0;
            btn_Inventory.FlatStyle = FlatStyle.Flat;
            btn_Inventory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Inventory.ForeColor = Color.FromArgb(251, 251, 254);
            btn_Inventory.Image = Properties.Resources.icons8_package_16;
            btn_Inventory.ImageAlign = ContentAlignment.MiddleLeft;
            btn_Inventory.Location = new Point(0, 149);
            btn_Inventory.Margin = new Padding(0);
            btn_Inventory.Name = "btn_Inventory";
            btn_Inventory.Padding = new Padding(25, 0, 0, 0);
            btn_Inventory.Size = new Size(254, 47);
            btn_Inventory.TabIndex = 8;
            btn_Inventory.Text = "      Inventory";
            btn_Inventory.TextAlign = ContentAlignment.MiddleLeft;
            btn_Inventory.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_Inventory.UseVisualStyleBackColor = false;
            btn_Inventory.Click += Btn_Inventory_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom;
            panel3.BackColor = Color.FromArgb(31, 25, 148);
            panel3.Controls.Add(lbl_Username);
            panel3.Controls.Add(lbl_Fullname);
            panel3.Controls.Add(pictureBox3);
            panel3.Location = new Point(0, 722);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(20, 15, 20, 15);
            panel3.Size = new Size(254, 74);
            panel3.TabIndex = 5;
            // 
            // lbl_Username
            // 
            lbl_Username.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_Username.ForeColor = Color.FromArgb(251, 251, 254);
            lbl_Username.Location = new Point(74, 41);
            lbl_Username.Name = "lbl_Username";
            lbl_Username.Size = new Size(157, 15);
            lbl_Username.TabIndex = 2;
            lbl_Username.Text = "Username";
            // 
            // lbl_Fullname
            // 
            lbl_Fullname.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Fullname.ForeColor = Color.FromArgb(251, 251, 254);
            lbl_Fullname.Location = new Point(74, 20);
            lbl_Fullname.Name = "lbl_Fullname";
            lbl_Fullname.Size = new Size(157, 23);
            lbl_Fullname.TabIndex = 1;
            lbl_Fullname.Text = "Full Name";
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Image = Properties.Resources.icons8_profile_96_HEX2596BE;
            pictureBox3.Location = new Point(23, 18);
            pictureBox3.Margin = new Padding(0, 0, 10, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(38, 38);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(251, 251, 254);
            label1.Location = new Point(67, 15);
            label1.Name = "label1";
            label1.Size = new Size(149, 43);
            label1.TabIndex = 1;
            label1.Text = "Inventory Management System";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.Image = Properties.Resources.box;
            pictureBox2.Location = new Point(20, 18);
            pictureBox2.Margin = new Padding(0, 0, 10, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(34, 34);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // InventoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(251, 251, 254);
            ClientSize = new Size(1684, 871);
            Controls.Add(tblLP_NavBar);
            Controls.Add(panel2);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1700, 910);
            Name = "InventoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventory Management System";
            WindowState = FormWindowState.Maximized;
            Load += Form2_Load;
            tblLP_NavBar.ResumeLayout(false);
            panel1.ResumeLayout(false);
            cPanel_NotifBell.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbx_redDot).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBx_NotifBell).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tblLP_NavBar;
        private Label lbl_NavBar;
        private TextBox txtBx_Search;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel7;
        private PictureBox pBx_NotifBell;
        private Components.CustomPanel cPanel_NotifBell;
        private PictureBox pictureBox1;
        private Components.SearchBar searchBar1;
        private PictureBox pbx_redDot;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label1;
        private Panel panel3;
        private PictureBox pictureBox3;
        private Label lbl_Username;
        private Label lbl_Fullname;
        private Button btn_Inventory;
        private Button button3;
        private Label label2;
        private Button btn_SignOut;
        private Button btn_ManageAccount;
        private Button btn_ManageUsers;
        private Button btn_AuditTrail;
    }
}