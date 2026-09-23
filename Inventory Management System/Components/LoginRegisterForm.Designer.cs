using Inventory_Management_System.Components;

namespace Inventory_Management_System
{
    partial class form_loginSignUp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_loginSignUp));
            lbl_inventoryManagementSystem = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            loginRegHolder = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            gradientPanelLogin1 = new GradientPanelLogin();
            lbl_title = new Label();
            pBx_Icon = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            gradientPanelLogin1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pBx_Icon).BeginInit();
            SuspendLayout();
            // 
            // lbl_inventoryManagementSystem
            // 
            lbl_inventoryManagementSystem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_inventoryManagementSystem.Font = new Font("Poppins", 16.2782612F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_inventoryManagementSystem.ForeColor = Color.White;
            lbl_inventoryManagementSystem.Location = new Point(49, 356);
            lbl_inventoryManagementSystem.Name = "lbl_inventoryManagementSystem";
            lbl_inventoryManagementSystem.Size = new Size(325, 175);
            lbl_inventoryManagementSystem.TabIndex = 11;
            lbl_inventoryManagementSystem.Text = "INVENTORY MANAGEMENT SYSTEM";
            lbl_inventoryManagementSystem.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox2.Image = Properties.Resources.box;
            pictureBox2.Location = new Point(135, 142);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(136, 134);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(123, 176);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(120, 60);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // loginRegHolder
            // 
            loginRegHolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loginRegHolder.Dock = DockStyle.Right;
            loginRegHolder.Location = new Point(437, 0);
            loginRegHolder.Name = "loginRegHolder";
            loginRegHolder.Padding = new Padding(120, 60, 120, 60);
            loginRegHolder.Size = new Size(490, 625);
            loginRegHolder.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(gradientPanelLogin1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1040, 821);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // gradientPanelLogin1
            // 
            gradientPanelLogin1.BackColor = Color.FromArgb(61, 77, 255);
            gradientPanelLogin1.Controls.Add(lbl_title);
            gradientPanelLogin1.Controls.Add(pBx_Icon);
            gradientPanelLogin1.Dock = DockStyle.Fill;
            gradientPanelLogin1.gradientBottom = Color.FromArgb(61, 77, 255);
            gradientPanelLogin1.gradientTop = Color.FromArgb(68, 61, 255);
            gradientPanelLogin1.Location = new Point(0, 0);
            gradientPanelLogin1.Margin = new Padding(0);
            gradientPanelLogin1.Name = "gradientPanelLogin1";
            gradientPanelLogin1.Size = new Size(520, 821);
            gradientPanelLogin1.TabIndex = 0;
            // 
            // lbl_title
            // 
            lbl_title.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbl_title.BackColor = Color.Transparent;
            lbl_title.Font = new Font("Poppins", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_title.ForeColor = Color.White;
            lbl_title.Location = new Point(52, 476);
            lbl_title.Name = "lbl_title";
            lbl_title.Size = new Size(414, 148);
            lbl_title.TabIndex = 2;
            lbl_title.Text = "INVENTORY MANAGEMENT SYSTEM";
            lbl_title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pBx_Icon
            // 
            pBx_Icon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pBx_Icon.BackColor = Color.Transparent;
            pBx_Icon.BackgroundImage = Properties.Resources.box;
            pBx_Icon.BackgroundImageLayout = ImageLayout.Zoom;
            pBx_Icon.Location = new Point(149, 153);
            pBx_Icon.Margin = new Padding(3, 2, 3, 2);
            pBx_Icon.Name = "pBx_Icon";
            pBx_Icon.Size = new Size(225, 294);
            pBx_Icon.TabIndex = 0;
            pBx_Icon.TabStop = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(520, 57);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 706);
            panel1.TabIndex = 1;
            // 
            // form_loginSignUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.box;
            ClientSize = new Size(1040, 821);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MaximumSize = new Size(1056, 860);
            MinimumSize = new Size(1056, 860);
            Name = "form_loginSignUp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventory Management System";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            gradientPanelLogin1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pBx_Icon).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GradientPanelLogin gradientPanelLogin1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label lbl_inventoryManagementSystem;
        private GradientPanelLogin panel;
        private Panel loginRegHolder;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label lbl_title;
        private PictureBox pBx_Icon;
    }
}
