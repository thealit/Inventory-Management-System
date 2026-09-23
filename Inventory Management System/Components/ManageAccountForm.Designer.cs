namespace Inventory_Management_System.Components
{
    partial class ManageAccountForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cTxt_Username = new CustomTextBox2();
            cTxt_Lastname = new CustomTextBox2();
            cTxt_Firstname = new CustomTextBox2();
            cTxt_CurrentPass = new CustomTextBox2();
            cTxt_NewPass = new CustomTextBox2();
            cTxt_ConfirmPass = new CustomTextBox2();
            btn_CancelChanges = new CustomButton();
            btn_Save = new CustomButton();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 35);
            label1.Name = "label1";
            label1.Size = new Size(208, 32);
            label1.TabIndex = 1;
            label1.Text = "Manage Account";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(28, 104);
            label2.Margin = new Padding(0, 0, 0, 10);
            label2.Name = "label2";
            label2.Padding = new Padding(4, 0, 0, 0);
            label2.Size = new Size(45, 15);
            label2.TabIndex = 9;
            label2.Text = "Profile";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(507, 103);
            label3.Margin = new Padding(0, 0, 0, 10);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 10;
            label3.Text = "Change Password";
            // 
            // cTxt_Username
            // 
            cTxt_Username.EnableText = true;
            cTxt_Username.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_Username.Input = "";
            cTxt_Username.IsDropDown = false;
            cTxt_Username.IsDropdownVisible = false;
            cTxt_Username.IsPassword = false;
            cTxt_Username.IsReadOnly = false;
            cTxt_Username.Label = "Username";
            cTxt_Username.Location = new Point(28, 131);
            cTxt_Username.Name = "cTxt_Username";
            cTxt_Username.Padding = new Padding(5);
            cTxt_Username.Selected = null;
            cTxt_Username.SelectedId = 0;
            cTxt_Username.Size = new Size(351, 80);
            cTxt_Username.TabIndex = 11;
            cTxt_Username.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_Username.TextBoxColor = SystemColors.WindowText;
            cTxt_Username.WarningLabel = "";
            // 
            // cTxt_Lastname
            // 
            cTxt_Lastname.EnableText = true;
            cTxt_Lastname.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_Lastname.Input = "";
            cTxt_Lastname.IsDropDown = false;
            cTxt_Lastname.IsDropdownVisible = false;
            cTxt_Lastname.IsPassword = false;
            cTxt_Lastname.IsReadOnly = false;
            cTxt_Lastname.Label = "Last name";
            cTxt_Lastname.Location = new Point(28, 343);
            cTxt_Lastname.Name = "cTxt_Lastname";
            cTxt_Lastname.Padding = new Padding(5);
            cTxt_Lastname.Selected = null;
            cTxt_Lastname.SelectedId = 0;
            cTxt_Lastname.Size = new Size(351, 80);
            cTxt_Lastname.TabIndex = 13;
            cTxt_Lastname.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_Lastname.TextBoxColor = SystemColors.WindowText;
            cTxt_Lastname.WarningLabel = "";
            // 
            // cTxt_Firstname
            // 
            cTxt_Firstname.EnableText = true;
            cTxt_Firstname.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_Firstname.Input = "";
            cTxt_Firstname.IsDropDown = false;
            cTxt_Firstname.IsDropdownVisible = false;
            cTxt_Firstname.IsPassword = false;
            cTxt_Firstname.IsReadOnly = false;
            cTxt_Firstname.Label = "First name";
            cTxt_Firstname.Location = new Point(28, 237);
            cTxt_Firstname.Name = "cTxt_Firstname";
            cTxt_Firstname.Padding = new Padding(5);
            cTxt_Firstname.Selected = null;
            cTxt_Firstname.SelectedId = 0;
            cTxt_Firstname.Size = new Size(351, 80);
            cTxt_Firstname.TabIndex = 12;
            cTxt_Firstname.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_Firstname.TextBoxColor = SystemColors.WindowText;
            cTxt_Firstname.WarningLabel = "";
            // 
            // cTxt_CurrentPass
            // 
            cTxt_CurrentPass.EnableText = true;
            cTxt_CurrentPass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_CurrentPass.Input = "";
            cTxt_CurrentPass.IsDropDown = false;
            cTxt_CurrentPass.IsDropdownVisible = false;
            cTxt_CurrentPass.IsPassword = true;
            cTxt_CurrentPass.IsReadOnly = false;
            cTxt_CurrentPass.Label = "Current password";
            cTxt_CurrentPass.Location = new Point(507, 131);
            cTxt_CurrentPass.Name = "cTxt_CurrentPass";
            cTxt_CurrentPass.Padding = new Padding(5);
            cTxt_CurrentPass.Selected = null;
            cTxt_CurrentPass.SelectedId = 0;
            cTxt_CurrentPass.Size = new Size(355, 80);
            cTxt_CurrentPass.TabIndex = 14;
            cTxt_CurrentPass.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_CurrentPass.TextBoxColor = SystemColors.WindowText;
            cTxt_CurrentPass.WarningLabel = "";
            // 
            // cTxt_NewPass
            // 
            cTxt_NewPass.EnableText = true;
            cTxt_NewPass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_NewPass.Input = "";
            cTxt_NewPass.IsDropDown = false;
            cTxt_NewPass.IsDropdownVisible = false;
            cTxt_NewPass.IsPassword = true;
            cTxt_NewPass.IsReadOnly = false;
            cTxt_NewPass.Label = "New password";
            cTxt_NewPass.Location = new Point(507, 237);
            cTxt_NewPass.Name = "cTxt_NewPass";
            cTxt_NewPass.Padding = new Padding(5);
            cTxt_NewPass.Selected = null;
            cTxt_NewPass.SelectedId = 0;
            cTxt_NewPass.Size = new Size(355, 80);
            cTxt_NewPass.TabIndex = 15;
            cTxt_NewPass.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_NewPass.TextBoxColor = SystemColors.WindowText;
            cTxt_NewPass.WarningLabel = "";
            // 
            // cTxt_ConfirmPass
            // 
            cTxt_ConfirmPass.EnableText = true;
            cTxt_ConfirmPass.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTxt_ConfirmPass.Input = "";
            cTxt_ConfirmPass.IsDropDown = false;
            cTxt_ConfirmPass.IsDropdownVisible = false;
            cTxt_ConfirmPass.IsPassword = true;
            cTxt_ConfirmPass.IsReadOnly = false;
            cTxt_ConfirmPass.Label = "Confirm new password";
            cTxt_ConfirmPass.Location = new Point(507, 343);
            cTxt_ConfirmPass.Name = "cTxt_ConfirmPass";
            cTxt_ConfirmPass.Padding = new Padding(5);
            cTxt_ConfirmPass.Selected = null;
            cTxt_ConfirmPass.SelectedId = 0;
            cTxt_ConfirmPass.Size = new Size(355, 80);
            cTxt_ConfirmPass.TabIndex = 16;
            cTxt_ConfirmPass.TextBoxBackColor = Color.FromArgb(251, 251, 254);
            cTxt_ConfirmPass.TextBoxColor = SystemColors.WindowText;
            cTxt_ConfirmPass.WarningLabel = "";
            // 
            // btn_CancelChanges
            // 
            btn_CancelChanges.BackColor = Color.FromArgb(251, 251, 254);
            btn_CancelChanges.BackgroundColor = Color.FromArgb(251, 251, 254);
            btn_CancelChanges.BorderColor = Color.FromArgb(251, 251, 254);
            btn_CancelChanges.BorderRadius = 7;
            btn_CancelChanges.BorderSize = 1;
            btn_CancelChanges.Cursor = Cursors.Hand;
            btn_CancelChanges.FlatAppearance.BorderSize = 0;
            btn_CancelChanges.FlatStyle = FlatStyle.Popup;
            btn_CancelChanges.ImageSize = 0;
            btn_CancelChanges.IsDropDown = false;
            btn_CancelChanges.IsDropDownVisible = false;
            btn_CancelChanges.Location = new Point(641, 523);
            btn_CancelChanges.MouseEnterColor = Color.Gray;
            btn_CancelChanges.MouseLeaveColor = Color.White;
            btn_CancelChanges.Name = "btn_CancelChanges";
            btn_CancelChanges.Size = new Size(101, 44);
            btn_CancelChanges.TabIndex = 17;
            btn_CancelChanges.Text = "Cancel";
            btn_CancelChanges.TextOnHoverColor = Color.White;
            btn_CancelChanges.TextOnLeaveColor = Color.Black;
            btn_CancelChanges.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            btn_Save.BackColor = Color.FromArgb(47, 39, 206);
            btn_Save.BackgroundColor = Color.FromArgb(47, 39, 206);
            btn_Save.BorderColor = Color.FromArgb(47, 39, 206);
            btn_Save.BorderRadius = 7;
            btn_Save.BorderSize = 1;
            btn_Save.Cursor = Cursors.Hand;
            btn_Save.FlatStyle = FlatStyle.Popup;
            btn_Save.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Save.ForeColor = Color.White;
            btn_Save.ImageSize = 0;
            btn_Save.IsDropDown = false;
            btn_Save.IsDropDownVisible = false;
            btn_Save.Location = new Point(748, 525);
            btn_Save.MouseEnterColor = Color.FromArgb(47, 39, 206);
            btn_Save.MouseLeaveColor = Color.White;
            btn_Save.Name = "btn_Save";
            btn_Save.Padding = new Padding(8, 0, 0, 0);
            btn_Save.Size = new Size(114, 42);
            btn_Save.TabIndex = 18;
            btn_Save.Text = "Save";
            btn_Save.TextOnHoverColor = Color.White;
            btn_Save.TextOnLeaveColor = Color.Black;
            btn_Save.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 67);
            label4.Name = "label4";
            label4.Size = new Size(236, 15);
            label4.TabIndex = 35;
            label4.Text = "Manage user profile, and change password.";
            // 
            // ManageAccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(251, 251, 254);
            ClientSize = new Size(1330, 648);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(btn_Save);
            Controls.Add(btn_CancelChanges);
            Controls.Add(cTxt_ConfirmPass);
            Controls.Add(cTxt_NewPass);
            Controls.Add(cTxt_CurrentPass);
            Controls.Add(cTxt_Firstname);
            Controls.Add(cTxt_Lastname);
            Controls.Add(cTxt_Username);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManageAccountForm";
            Padding = new Padding(35, 35, 30, 30);
            Text = "ManageAccountForm";
            Load += ManageAccountForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private CustomTextBox2 cTxt_Lastname;
        private CustomTextBox2 cTxt_Firstname;
        private CustomTextBox2 cTxt_CurrentPass;
        private CustomPanel customPanel1;
        private CustomPanel customPanel2;
        private CustomTextBox2 cTxt_ConfirmPass;
        private CustomTextBox2 cTxt_Username;
        private CustomTextBox2 cTxt_NewPass;
        private CustomButton btn_Save;
        private CustomButton btn_CancelChanges;
        private Label label4;
    }
}