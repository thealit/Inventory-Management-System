namespace Inventory_Management_System.Components
{
    partial class UserManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagementForm));
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            cBtn_DeleteOneOrMany = new CustomButton();
            cBtn_CheckBoxTop = new CustomButton();
            searchUsers = new SearchBar();
            dgvPanel = new Panel();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 35);
            label1.Name = "label1";
            label1.Size = new Size(223, 32);
            label1.TabIndex = 0;
            label1.Text = "User Management";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvPanel, 0, 1);
            tableLayoutPanel1.Location = new Point(38, 118);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 541F));
            tableLayoutPanel1.Size = new Size(993, 586);
            tableLayoutPanel1.TabIndex = 33;
            // 
            // panel1
            // 
            panel1.Controls.Add(cBtn_DeleteOneOrMany);
            panel1.Controls.Add(cBtn_CheckBoxTop);
            panel1.Controls.Add(searchUsers);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(993, 45);
            panel1.TabIndex = 0;
            // 
            // cBtn_DeleteOneOrMany
            // 
            cBtn_DeleteOneOrMany.AutoSize = true;
            cBtn_DeleteOneOrMany.BackColor = Color.FromArgb(251, 251, 254);
            cBtn_DeleteOneOrMany.BackgroundColor = Color.FromArgb(251, 251, 254);
            cBtn_DeleteOneOrMany.BorderColor = Color.Gray;
            cBtn_DeleteOneOrMany.BorderRadius = 7;
            cBtn_DeleteOneOrMany.BorderSize = 1;
            cBtn_DeleteOneOrMany.Cursor = Cursors.Hand;
            cBtn_DeleteOneOrMany.FlatStyle = FlatStyle.Flat;
            cBtn_DeleteOneOrMany.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cBtn_DeleteOneOrMany.ForeColor = Color.Black;
            cBtn_DeleteOneOrMany.Image = (Image)resources.GetObject("cBtn_DeleteOneOrMany.Image");
            cBtn_DeleteOneOrMany.ImageSize = 20;
            cBtn_DeleteOneOrMany.IsDropDown = false;
            cBtn_DeleteOneOrMany.IsDropDownVisible = false;
            cBtn_DeleteOneOrMany.Location = new Point(47, 3);
            cBtn_DeleteOneOrMany.MouseEnterColor = Color.White;
            cBtn_DeleteOneOrMany.MouseLeaveColor = Color.White;
            cBtn_DeleteOneOrMany.Name = "cBtn_DeleteOneOrMany";
            cBtn_DeleteOneOrMany.Padding = new Padding(8, 0, 0, 0);
            cBtn_DeleteOneOrMany.Size = new Size(130, 35);
            cBtn_DeleteOneOrMany.TabIndex = 3;
            cBtn_DeleteOneOrMany.Text = " Delete user";
            cBtn_DeleteOneOrMany.TextImageRelation = TextImageRelation.ImageBeforeText;
            cBtn_DeleteOneOrMany.TextOnHoverColor = Color.Black;
            cBtn_DeleteOneOrMany.TextOnLeaveColor = Color.Black;
            cBtn_DeleteOneOrMany.UseVisualStyleBackColor = false;
            cBtn_DeleteOneOrMany.Visible = false;
            // 
            // cBtn_CheckBoxTop
            // 
            cBtn_CheckBoxTop.BackColor = Color.FromArgb(251, 251, 254);
            cBtn_CheckBoxTop.BackgroundColor = Color.FromArgb(251, 251, 254);
            cBtn_CheckBoxTop.BackgroundImageLayout = ImageLayout.Zoom;
            cBtn_CheckBoxTop.BorderColor = Color.Gray;
            cBtn_CheckBoxTop.BorderRadius = 7;
            cBtn_CheckBoxTop.BorderSize = 1;
            cBtn_CheckBoxTop.Cursor = Cursors.Hand;
            cBtn_CheckBoxTop.FlatAppearance.BorderColor = Color.Gray;
            cBtn_CheckBoxTop.FlatStyle = FlatStyle.Flat;
            cBtn_CheckBoxTop.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cBtn_CheckBoxTop.ForeColor = Color.Black;
            cBtn_CheckBoxTop.Image = (Image)resources.GetObject("cBtn_CheckBoxTop.Image");
            cBtn_CheckBoxTop.ImageSize = 21;
            cBtn_CheckBoxTop.IsDropDown = false;
            cBtn_CheckBoxTop.IsDropDownVisible = false;
            cBtn_CheckBoxTop.Location = new Point(4, 3);
            cBtn_CheckBoxTop.MouseEnterColor = Color.White;
            cBtn_CheckBoxTop.MouseLeaveColor = Color.White;
            cBtn_CheckBoxTop.Name = "cBtn_CheckBoxTop";
            cBtn_CheckBoxTop.Size = new Size(37, 35);
            cBtn_CheckBoxTop.TabIndex = 2;
            cBtn_CheckBoxTop.TextOnHoverColor = Color.White;
            cBtn_CheckBoxTop.TextOnLeaveColor = Color.Black;
            cBtn_CheckBoxTop.UseVisualStyleBackColor = false;
            // 
            // searchUsers
            // 
            searchUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            searchUsers.BackColor = Color.Transparent;
            searchUsers.Input = "";
            searchUsers.Location = new Point(657, 3);
            searchUsers.Name = "searchUsers";
            searchUsers.PlaceholderText = "Search by name, username, or role";
            searchUsers.Size = new Size(333, 35);
            searchUsers.TabIndex = 4;
            searchUsers.TextBoxBackColor = Color.White;
            // 
            // dgvPanel
            // 
            dgvPanel.Dock = DockStyle.Fill;
            dgvPanel.Location = new Point(0, 45);
            dgvPanel.Margin = new Padding(0);
            dgvPanel.Name = "dgvPanel";
            dgvPanel.Size = new Size(993, 541);
            dgvPanel.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 67);
            label2.Name = "label2";
            label2.Size = new Size(349, 15);
            label2.TabIndex = 34;
            label2.Text = "Manage user accounts, assign roles, and monitor system activity.";
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1064, 768);
            Controls.Add(label2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UserManagementForm";
            Padding = new Padding(35, 35, 30, 30);
            Text = "UserManagement";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private SearchBar searchUsers;
        private Panel dgvPanel;
        private Label label2;
        private CustomButton cBtn_DeleteOneOrMany;
        private CustomButton cBtn_CheckBoxTop;
    }
}