using Inventory_Management_System.Components;

namespace Inventory_Management_System
{
    partial class SignInPanel
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
            lbl_signInHeader = new Label();
            lbl_signUpRedirect = new Label();
            lbl_footer = new Label();
            btn_login = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ctbx_uname = new CustomTextBox2();
            ctbx_pass = new CustomTextBox2();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_signInHeader
            // 
            lbl_signInHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_signInHeader.AutoSize = true;
            lbl_signInHeader.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_signInHeader.Location = new Point(3, 0);
            lbl_signInHeader.Margin = new Padding(3, 0, 3, 2);
            lbl_signInHeader.Name = "lbl_signInHeader";
            lbl_signInHeader.Size = new Size(270, 30);
            lbl_signInHeader.TabIndex = 0;
            lbl_signInHeader.Text = "SIGN IN";
            // 
            // lbl_signUpRedirect
            // 
            lbl_signUpRedirect.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_signUpRedirect.AutoSize = true;
            lbl_signUpRedirect.Cursor = Cursors.Hand;
            lbl_signUpRedirect.Font = new Font("Segoe UI", 9.75F);
            lbl_signUpRedirect.ForeColor = Color.DodgerBlue;
            lbl_signUpRedirect.Location = new Point(171, 5);
            lbl_signUpRedirect.Margin = new Padding(0);
            lbl_signUpRedirect.Name = "lbl_signUpRedirect";
            lbl_signUpRedirect.Size = new Size(54, 17);
            lbl_signUpRedirect.TabIndex = 1;
            lbl_signUpRedirect.Text = "Sign Up";
            lbl_signUpRedirect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_footer
            // 
            lbl_footer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_footer.AutoSize = true;
            lbl_footer.Font = new Font("Segoe UI", 9.75F);
            lbl_footer.Location = new Point(28, 5);
            lbl_footer.Margin = new Padding(0);
            lbl_footer.Name = "lbl_footer";
            lbl_footer.Size = new Size(143, 17);
            lbl_footer.TabIndex = 0;
            lbl_footer.Text = "Don't have an account?";
            lbl_footer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_login
            // 
            btn_login.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_login.AutoSize = true;
            btn_login.BackColor = Color.LimeGreen;
            btn_login.Cursor = Cursors.Hand;
            btn_login.FlatAppearance.BorderSize = 0;
            btn_login.FlatStyle = FlatStyle.Flat;
            btn_login.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_login.ForeColor = Color.White;
            btn_login.Location = new Point(11, 260);
            btn_login.Margin = new Padding(11, 15, 11, 2);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(254, 38);
            btn_login.TabIndex = 3;
            btn_login.Text = "Sign In";
            btn_login.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lbl_signUpRedirect, 2, 0);
            tableLayoutPanel1.Controls.Add(lbl_footer, 1, 0);
            tableLayoutPanel1.Location = new Point(11, 302);
            tableLayoutPanel1.Margin = new Padding(11, 2, 11, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel1.Size = new Size(254, 26);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(lbl_signInHeader, 0, 0);
            tableLayoutPanel2.Controls.Add(ctbx_uname, 0, 1);
            tableLayoutPanel2.Controls.Add(ctbx_pass, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 4);
            tableLayoutPanel2.Controls.Add(btn_login, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(276, 336);
            tableLayoutPanel2.TabIndex = 24;
            // 
            // ctbx_uname
            // 
            ctbx_uname.EnableText = true;
            ctbx_uname.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx_uname.Input = "";
            ctbx_uname.IsDropDown = false;
            ctbx_uname.IsDropdownVisible = false;
            ctbx_uname.IsPassword = false;
            ctbx_uname.IsReadOnly = false;
            ctbx_uname.Label = "Username";
            ctbx_uname.Location = new Point(3, 68);
            ctbx_uname.Name = "ctbx_uname";
            ctbx_uname.Padding = new Padding(5);
            ctbx_uname.Selected = null;
            ctbx_uname.SelectedId = 0;
            ctbx_uname.Size = new Size(270, 80);
            ctbx_uname.TabIndex = 5;
            ctbx_uname.TextBoxBackColor = Color.White;
            ctbx_uname.TextBoxColor = SystemColors.WindowText;
            ctbx_uname.WarningLabel = "";
            // 
            // ctbx_pass
            // 
            ctbx_pass.Dock = DockStyle.Fill;
            ctbx_pass.EnableText = true;
            ctbx_pass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx_pass.Input = "";
            ctbx_pass.IsDropDown = false;
            ctbx_pass.IsDropdownVisible = false;
            ctbx_pass.IsPassword = true;
            ctbx_pass.IsReadOnly = false;
            ctbx_pass.Label = "Password";
            ctbx_pass.Location = new Point(3, 158);
            ctbx_pass.Name = "ctbx_pass";
            ctbx_pass.Padding = new Padding(5);
            ctbx_pass.Selected = null;
            ctbx_pass.SelectedId = 0;
            ctbx_pass.Size = new Size(270, 84);
            ctbx_pass.TabIndex = 6;
            ctbx_pass.TextBoxBackColor = Color.White;
            ctbx_pass.TextBoxColor = SystemColors.WindowText;
            ctbx_pass.WarningLabel = "";
            // 
            // SignInPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel2);
            Location = new Point(0, 50);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SignInPanel";
            Size = new Size(276, 336);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lbl_signInHeader;
        private Label lbl_signUpRedirect;
        private Label lbl_footer;
        private Button btn_login;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private CustomTextBox2 ctbx_uname;
        private CustomTextBox2 ctbx_pass;
    }
}
