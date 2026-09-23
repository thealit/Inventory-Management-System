namespace Inventory_Management_System.Components
{
    partial class AuditTrailForm
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
            label2 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            searchLogs = new SearchBar();
            searchUsers = new SearchBar();
            dgvPanel = new Panel();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 67);
            label2.Name = "label2";
            label2.Size = new Size(282, 15);
            label2.TabIndex = 40;
            label2.Text = "Keep a detailed log of all inventory-related activities.";
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
            tableLayoutPanel1.TabIndex = 39;
            // 
            // panel1
            // 
            panel1.Controls.Add(searchLogs);
            panel1.Controls.Add(searchUsers);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(993, 45);
            panel1.TabIndex = 0;
            // 
            // searchLogs
            // 
            searchLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            searchLogs.BackColor = Color.Transparent;
            searchLogs.Input = "";
            searchLogs.Location = new Point(657, 3);
            searchLogs.Name = "searchLogs";
            searchLogs.PlaceholderText = "Search by name, or activity";
            searchLogs.Size = new Size(333, 35);
            searchLogs.TabIndex = 1;
            searchLogs.TextBoxBackColor = Color.White;
            // 
            // searchUsers
            // 
            searchUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            searchUsers.BackColor = Color.Transparent;
            searchUsers.Input = "";
            searchUsers.Location = new Point(2269, 3);
            searchUsers.Name = "searchUsers";
            searchUsers.PlaceholderText = "Search by name, username, or role";
            searchUsers.Size = new Size(307, 0);
            searchUsers.TabIndex = 0;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 35);
            label1.Name = "label1";
            label1.Size = new Size(133, 32);
            label1.TabIndex = 0;
            label1.Text = "Audit Trail";
            // 
            // AuditTrailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1064, 768);
            Controls.Add(label2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AuditTrailForm";
            Padding = new Padding(35, 35, 30, 30);
            Text = "Audit";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private SearchBar searchUsers;
        private Panel dgvPanel;
        private Label label1;
        private SearchBar searchLogs;
    }
}