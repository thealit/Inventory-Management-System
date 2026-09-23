namespace Inventory_Management_System.Components
{
    partial class EditUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            basePanel = new CustomPanel();
            fieldsPanel = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            topButtons = new Panel();
            btnEditOption = new CustomButton();
            btnChangeRoleOption = new CustomButton();
            panel2 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            btn_Close = new PictureBox();
            image = new CustomPanel();
            pictureBox1 = new PictureBox();
            bottomButtons = new Panel();
            basePanel.SuspendLayout();
            topButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btn_Close).BeginInit();
            image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // basePanel
            // 
            basePanel.BackColor = Color.White;
            basePanel.BackgroundColor = Color.White;
            basePanel.BorderColor = Color.Gainsboro;
            basePanel.BorderRadius = 5;
            basePanel.BorderSize = 1;
            basePanel.Controls.Add(fieldsPanel);
            basePanel.Controls.Add(panel4);
            basePanel.Controls.Add(panel3);
            basePanel.Controls.Add(topButtons);
            basePanel.Controls.Add(panel2);
            basePanel.Controls.Add(tableLayoutPanel1);
            basePanel.Controls.Add(bottomButtons);
            basePanel.Dock = DockStyle.Fill;
            basePanel.Location = new Point(1, 1);
            basePanel.Name = "basePanel";
            basePanel.Padding = new Padding(20, 17, 17, 17);
            basePanel.Size = new Size(338, 391);
            basePanel.TabIndex = 1;
            // 
            // fieldsPanel
            // 
            fieldsPanel.Dock = DockStyle.Fill;
            fieldsPanel.Location = new Point(20, 124);
            fieldsPanel.Name = "fieldsPanel";
            fieldsPanel.Size = new Size(301, 203);
            fieldsPanel.TabIndex = 9;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(20, 327);
            panel4.Name = "panel4";
            panel4.Size = new Size(301, 11);
            panel4.TabIndex = 12;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(20, 106);
            panel3.Name = "panel3";
            panel3.Size = new Size(301, 18);
            panel3.TabIndex = 11;
            // 
            // topButtons
            // 
            topButtons.Controls.Add(btnEditOption);
            topButtons.Controls.Add(btnChangeRoleOption);
            topButtons.Dock = DockStyle.Top;
            topButtons.Location = new Point(20, 70);
            topButtons.Margin = new Padding(0, 15, 0, 15);
            topButtons.Name = "topButtons";
            topButtons.Size = new Size(301, 36);
            topButtons.TabIndex = 3;
            // 
            // btnEditOption
            // 
            btnEditOption.BackColor = Color.Transparent;
            btnEditOption.BackgroundColor = Color.Transparent;
            btnEditOption.BorderColor = Color.Transparent;
            btnEditOption.BorderRadius = 0;
            btnEditOption.BorderSize = 0;
            btnEditOption.Dock = DockStyle.Fill;
            btnEditOption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditOption.ImageSize = 0;
            btnEditOption.IsDropDown = false;
            btnEditOption.IsDropDownVisible = false;
            btnEditOption.Location = new Point(148, 0);
            btnEditOption.MouseEnterColor = Color.FromArgb(47, 39, 206);
            btnEditOption.MouseLeaveColor = Color.White;
            btnEditOption.Name = "btnEditOption";
            btnEditOption.Size = new Size(153, 36);
            btnEditOption.TabIndex = 2;
            btnEditOption.Text = "Edit Password";
            btnEditOption.TextOnHoverColor = Color.White;
            btnEditOption.TextOnLeaveColor = Color.Black;
            btnEditOption.UseVisualStyleBackColor = false;
            // 
            // btnChangeRoleOption
            // 
            btnChangeRoleOption.BackColor = Color.White;
            btnChangeRoleOption.BackgroundColor = Color.White;
            btnChangeRoleOption.BorderColor = Color.PaleVioletRed;
            btnChangeRoleOption.BorderRadius = 0;
            btnChangeRoleOption.BorderSize = 0;
            btnChangeRoleOption.Dock = DockStyle.Left;
            btnChangeRoleOption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangeRoleOption.ImageSize = 0;
            btnChangeRoleOption.IsDropDown = false;
            btnChangeRoleOption.IsDropDownVisible = false;
            btnChangeRoleOption.Location = new Point(0, 0);
            btnChangeRoleOption.MouseEnterColor = Color.FromArgb(47, 39, 206);
            btnChangeRoleOption.MouseLeaveColor = Color.White;
            btnChangeRoleOption.Name = "btnChangeRoleOption";
            btnChangeRoleOption.Size = new Size(148, 36);
            btnChangeRoleOption.TabIndex = 1;
            btnChangeRoleOption.Text = "Change Role";
            btnChangeRoleOption.TextOnHoverColor = Color.White;
            btnChangeRoleOption.TextOnLeaveColor = Color.Black;
            btnChangeRoleOption.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(20, 59);
            panel2.Name = "panel2";
            panel2.Size = new Size(301, 11);
            panel2.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.8914728F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.10853F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_Close, 2, 0);
            tableLayoutPanel1.Controls.Add(image, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(20, 17);
            tableLayoutPanel1.Margin = new Padding(0, 0, 0, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(301, 42);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(43, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(228, 42);
            panel1.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(0, 20);
            label2.Name = "label2";
            label2.Size = new Size(175, 15);
            label2.TabIndex = 4;
            label2.Text = "Change user role, and password";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(228, 20);
            label1.TabIndex = 1;
            label1.Text = "Edit User";
            // 
            // btn_Close
            // 
            btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Close.Cursor = Cursors.Hand;
            btn_Close.Image = Properties.Resources.icons8_close_24;
            btn_Close.Location = new Point(278, 3);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(20, 20);
            btn_Close.SizeMode = PictureBoxSizeMode.Zoom;
            btn_Close.TabIndex = 7;
            btn_Close.TabStop = false;
            // 
            // image
            // 
            image.BackColor = Color.FromArgb(65, 102, 163);
            image.BackgroundColor = Color.FromArgb(65, 102, 163);
            image.BorderColor = Color.Transparent;
            image.BorderRadius = 5;
            image.BorderSize = 1;
            image.Controls.Add(pictureBox1);
            image.Location = new Point(0, 0);
            image.Margin = new Padding(0, 0, 8, 0);
            image.Name = "image";
            image.Padding = new Padding(9);
            image.Size = new Size(33, 33);
            image.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.tile__4_;
            pictureBox1.Location = new Point(9, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(15, 15);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // bottomButtons
            // 
            bottomButtons.Dock = DockStyle.Bottom;
            bottomButtons.Location = new Point(20, 338);
            bottomButtons.Name = "bottomButtons";
            bottomButtons.Size = new Size(301, 36);
            bottomButtons.TabIndex = 8;
            // 
            // EditUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            Controls.Add(basePanel);
            DoubleBuffered = true;
            Name = "EditUser";
            Padding = new Padding(1);
            Size = new Size(340, 393);
            basePanel.ResumeLayout(false);
            topButtons.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btn_Close).EndInit();
            image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomPanel basePanel;
        private Panel fieldsPanel;
        private Panel panel4;
        private Panel panel3;
        private Panel topButtons;
        private CustomButton btnEditOption;
        private CustomButton btnChangeRoleOption;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label label2;
        private Label label1;
        private PictureBox btn_Close;
        private CustomPanel image;
        private PictureBox pictureBox1;
        private Panel bottomButtons;
    }
}
