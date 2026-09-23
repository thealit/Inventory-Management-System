using Inventory_Management_System.Components;

namespace Inventory_Management_System
{
    partial class SignUp
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
            lbl_signInRedirect = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ctbx2_confirmPass = new CustomTextBox2();
            ctbx2_pass = new CustomTextBox2();
            lbl_signUp = new Label();
            ctbx2_lName = new CustomTextBox2();
            ctbx2_uName = new CustomTextBox2();
            ctbx2_fName = new CustomTextBox2();
            tableLayoutPanel3 = new TableLayoutPanel();
            lbl_bottomRegister = new Label();
            btn_registerAccount = new Button();
            panel1 = new Panel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_signInRedirect
            // 
            lbl_signInRedirect.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_signInRedirect.AutoSize = true;
            lbl_signInRedirect.Cursor = Cursors.Hand;
            lbl_signInRedirect.Font = new Font("Segoe UI", 9.75F);
            lbl_signInRedirect.ForeColor = Color.DodgerBlue;
            lbl_signInRedirect.Location = new Point(192, 5);
            lbl_signInRedirect.Margin = new Padding(0);
            lbl_signInRedirect.Name = "lbl_signInRedirect";
            lbl_signInRedirect.Size = new Size(47, 17);
            lbl_signInRedirect.TabIndex = 1;
            lbl_signInRedirect.Text = "Sign In";
            lbl_signInRedirect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(ctbx2_confirmPass, 0, 5);
            tableLayoutPanel1.Controls.Add(ctbx2_pass, 0, 4);
            tableLayoutPanel1.Controls.Add(lbl_signUp, 0, 0);
            tableLayoutPanel1.Controls.Add(ctbx2_lName, 0, 2);
            tableLayoutPanel1.Controls.Add(ctbx2_uName, 0, 3);
            tableLayoutPanel1.Controls.Add(ctbx2_fName, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.Size = new Size(295, 548);
            tableLayoutPanel1.TabIndex = 27;
            // 
            // ctbx2_confirmPass
            // 
            ctbx2_confirmPass.EnableText = true;
            ctbx2_confirmPass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_confirmPass.Input = "";
            ctbx2_confirmPass.IsDropDown = false;
            ctbx2_confirmPass.IsDropdownVisible = false;
            ctbx2_confirmPass.IsPassword = true;
            ctbx2_confirmPass.IsReadOnly = false;
            ctbx2_confirmPass.Label = "Confirm Password";
            ctbx2_confirmPass.Location = new Point(3, 446);
            ctbx2_confirmPass.Name = "ctbx2_confirmPass";
            ctbx2_confirmPass.Padding = new Padding(5);
            ctbx2_confirmPass.Selected = null;
            ctbx2_confirmPass.SelectedId = 0;
            ctbx2_confirmPass.Size = new Size(289, 80);
            ctbx2_confirmPass.TabIndex = 10;
            ctbx2_confirmPass.TextBoxBackColor = Color.White;
            ctbx2_confirmPass.TextBoxColor = SystemColors.WindowText;
            ctbx2_confirmPass.WarningLabel = "";
            // 
            // ctbx2_pass
            // 
            ctbx2_pass.Dock = DockStyle.Fill;
            ctbx2_pass.EnableText = true;
            ctbx2_pass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_pass.Input = "";
            ctbx2_pass.IsDropDown = false;
            ctbx2_pass.IsDropdownVisible = false;
            ctbx2_pass.IsPassword = true;
            ctbx2_pass.IsReadOnly = false;
            ctbx2_pass.Label = "Password";
            ctbx2_pass.Location = new Point(3, 338);
            ctbx2_pass.Name = "ctbx2_pass";
            ctbx2_pass.Padding = new Padding(5);
            ctbx2_pass.Selected = null;
            ctbx2_pass.SelectedId = 0;
            ctbx2_pass.Size = new Size(289, 102);
            ctbx2_pass.TabIndex = 9;
            ctbx2_pass.TextBoxBackColor = Color.White;
            ctbx2_pass.TextBoxColor = SystemColors.WindowText;
            ctbx2_pass.WarningLabel = "";
            // 
            // lbl_signUp
            // 
            lbl_signUp.AutoSize = true;
            lbl_signUp.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_signUp.Location = new Point(3, 0);
            lbl_signUp.Margin = new Padding(3, 0, 3, 10);
            lbl_signUp.Name = "lbl_signUp";
            lbl_signUp.Size = new Size(194, 30);
            lbl_signUp.TabIndex = 0;
            lbl_signUp.Text = "CREATE ACCOUNT";
            // 
            // ctbx2_lName
            // 
            ctbx2_lName.EnableText = true;
            ctbx2_lName.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_lName.Input = "";
            ctbx2_lName.IsDropDown = false;
            ctbx2_lName.IsDropdownVisible = false;
            ctbx2_lName.IsPassword = false;
            ctbx2_lName.IsReadOnly = false;
            ctbx2_lName.Label = "Last Name";
            ctbx2_lName.Location = new Point(3, 158);
            ctbx2_lName.Name = "ctbx2_lName";
            ctbx2_lName.Padding = new Padding(5);
            ctbx2_lName.Selected = null;
            ctbx2_lName.SelectedId = 0;
            ctbx2_lName.Size = new Size(289, 80);
            ctbx2_lName.TabIndex = 7;
            ctbx2_lName.TextBoxBackColor = Color.White;
            ctbx2_lName.TextBoxColor = SystemColors.WindowText;
            ctbx2_lName.WarningLabel = "";
            // 
            // ctbx2_uName
            // 
            ctbx2_uName.EnableText = true;
            ctbx2_uName.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_uName.Input = "";
            ctbx2_uName.IsDropDown = false;
            ctbx2_uName.IsDropdownVisible = false;
            ctbx2_uName.IsPassword = false;
            ctbx2_uName.IsReadOnly = false;
            ctbx2_uName.Label = "Username";
            ctbx2_uName.Location = new Point(3, 248);
            ctbx2_uName.Name = "ctbx2_uName";
            ctbx2_uName.Padding = new Padding(5);
            ctbx2_uName.Selected = null;
            ctbx2_uName.SelectedId = 0;
            ctbx2_uName.Size = new Size(289, 80);
            ctbx2_uName.TabIndex = 8;
            ctbx2_uName.TextBoxBackColor = Color.White;
            ctbx2_uName.TextBoxColor = SystemColors.WindowText;
            ctbx2_uName.WarningLabel = "";
            // 
            // ctbx2_fName
            // 
            ctbx2_fName.EnableText = true;
            ctbx2_fName.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_fName.Input = "";
            ctbx2_fName.IsDropDown = false;
            ctbx2_fName.IsDropdownVisible = false;
            ctbx2_fName.IsPassword = false;
            ctbx2_fName.IsReadOnly = false;
            ctbx2_fName.Label = "First Name";
            ctbx2_fName.Location = new Point(3, 68);
            ctbx2_fName.Name = "ctbx2_fName";
            ctbx2_fName.Padding = new Padding(5);
            ctbx2_fName.Selected = null;
            ctbx2_fName.SelectedId = 0;
            ctbx2_fName.Size = new Size(289, 80);
            ctbx2_fName.TabIndex = 6;
            ctbx2_fName.TextBoxBackColor = Color.White;
            ctbx2_fName.TextBoxColor = SystemColors.WindowText;
            ctbx2_fName.WarningLabel = "";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(lbl_bottomRegister, 1, 0);
            tableLayoutPanel3.Controls.Add(lbl_signInRedirect, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Location = new Point(10, 51);
            tableLayoutPanel3.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel3.Size = new Size(275, 28);
            tableLayoutPanel3.TabIndex = 28;
            // 
            // lbl_bottomRegister
            // 
            lbl_bottomRegister.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_bottomRegister.AutoSize = true;
            lbl_bottomRegister.Font = new Font("Segoe UI", 9.75F);
            lbl_bottomRegister.Location = new Point(36, 5);
            lbl_bottomRegister.Margin = new Padding(0);
            lbl_bottomRegister.Name = "lbl_bottomRegister";
            lbl_bottomRegister.RightToLeft = RightToLeft.No;
            lbl_bottomRegister.Size = new Size(156, 17);
            lbl_bottomRegister.TabIndex = 20;
            lbl_bottomRegister.Text = "Already have an account?";
            lbl_bottomRegister.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_registerAccount
            // 
            btn_registerAccount.BackColor = Color.LimeGreen;
            btn_registerAccount.Cursor = Cursors.Hand;
            btn_registerAccount.Dock = DockStyle.Bottom;
            btn_registerAccount.FlatAppearance.BorderSize = 0;
            btn_registerAccount.FlatStyle = FlatStyle.Flat;
            btn_registerAccount.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_registerAccount.ForeColor = Color.White;
            btn_registerAccount.Location = new Point(10, 10);
            btn_registerAccount.Margin = new Padding(3, 15, 3, 2);
            btn_registerAccount.Name = "btn_registerAccount";
            btn_registerAccount.Size = new Size(275, 41);
            btn_registerAccount.TabIndex = 0;
            btn_registerAccount.Text = "Sign Up";
            btn_registerAccount.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_registerAccount);
            panel1.Controls.Add(tableLayoutPanel3);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 551);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10, 0, 10, 0);
            panel1.Size = new Size(295, 79);
            panel1.TabIndex = 29;
            // 
            // SignUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SignUp";
            Size = new Size(295, 630);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lbl_signInRedirect;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbl_bottomRegister;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btn_registerAccount;
        private CustomTextBox2 ctbx2_fName;
        private CustomTextBox2 ctbx2_lName;
        private CustomTextBox2 ctbx2_uName;
        private CustomTextBox2 ctbx2_pass;
        private CustomTextBox2 ctbx2_confirmPass;
        private Panel panel1;
        private Label lbl_signUp;
    }
}
