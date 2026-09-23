namespace Inventory_Management_System.Components
{
    partial class NotificationItem
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
            panel2 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lbl_Header = new Label();
            panel1 = new Panel();
            date = new Label();
            lbl_Message = new Label();
            pBx_notifType = new PictureBox();
            pbx_RedDot = new PictureBox();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pBx_notifType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbx_RedDot).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(tableLayoutPanel2);
            panel2.Controls.Add(pBx_notifType);
            panel2.Controls.Add(pbx_RedDot);
            panel2.Cursor = Cursors.Hand;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 10, 3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(287, 65);
            panel2.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(lbl_Header, 0, 0);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(23, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(3, 0, 0, 0);
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel2.Size = new Size(252, 65);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lbl_Header
            // 
            lbl_Header.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_Header.AutoEllipsis = true;
            lbl_Header.BackColor = Color.Transparent;
            lbl_Header.FlatStyle = FlatStyle.Flat;
            lbl_Header.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Header.ForeColor = Color.Black;
            lbl_Header.Location = new Point(6, 1);
            lbl_Header.Margin = new Padding(3, 0, 15, 0);
            lbl_Header.Name = "lbl_Header";
            lbl_Header.Size = new Size(231, 17);
            lbl_Header.TabIndex = 0;
            lbl_Header.Text = "Header";
            // 
            // panel1
            // 
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.Controls.Add(date);
            panel1.Controls.Add(lbl_Message);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(6, 20);
            panel1.Margin = new Padding(3, 0, 15, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 1, 0, 0);
            panel1.Size = new Size(231, 45);
            panel1.TabIndex = 1;
            // 
            // date
            // 
            date.Dock = DockStyle.Top;
            date.FlatStyle = FlatStyle.Flat;
            date.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            date.ForeColor = SystemColors.ControlDark;
            date.Location = new Point(0, 18);
            date.Margin = new Padding(0);
            date.Name = "date";
            date.Size = new Size(231, 18);
            date.TabIndex = 2;
            date.Text = "3 days ago";
            // 
            // lbl_Message
            // 
            lbl_Message.AutoEllipsis = true;
            lbl_Message.BackColor = Color.Transparent;
            lbl_Message.Dock = DockStyle.Top;
            lbl_Message.FlatStyle = FlatStyle.Flat;
            lbl_Message.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_Message.ForeColor = Color.Black;
            lbl_Message.Location = new Point(0, 1);
            lbl_Message.Margin = new Padding(3, 1, 15, 0);
            lbl_Message.Name = "lbl_Message";
            lbl_Message.Size = new Size(231, 17);
            lbl_Message.TabIndex = 1;
            lbl_Message.Text = "Details";
            // 
            // pBx_notifType
            // 
            pBx_notifType.BackColor = Color.Transparent;
            pBx_notifType.Dock = DockStyle.Left;
            pBx_notifType.Image = Properties.Resources.icons8_information_48;
            pBx_notifType.Location = new Point(0, 0);
            pBx_notifType.Name = "pBx_notifType";
            pBx_notifType.Size = new Size(23, 65);
            pBx_notifType.SizeMode = PictureBoxSizeMode.Zoom;
            pBx_notifType.TabIndex = 2;
            pBx_notifType.TabStop = false;
            // 
            // pbx_RedDot
            // 
            pbx_RedDot.BackColor = Color.Transparent;
            pbx_RedDot.Dock = DockStyle.Right;
            pbx_RedDot.Image = Properties.Resources.icons8_circle_72;
            pbx_RedDot.Location = new Point(275, 0);
            pbx_RedDot.Name = "pbx_RedDot";
            pbx_RedDot.Size = new Size(12, 65);
            pbx_RedDot.SizeMode = PictureBoxSizeMode.Zoom;
            pbx_RedDot.TabIndex = 1;
            pbx_RedDot.TabStop = false;
            // 
            // NotificationItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(panel2);
            DoubleBuffered = true;
            Name = "NotificationItem";
            Size = new Size(287, 66);
            Load += NotificationItem_Load;
            panel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pBx_notifType).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbx_RedDot).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lbl_Header;
        private Label lbl_Message;
        private PictureBox pbx_RedDot;
        private Panel panel1;
        private Label date;
        private PictureBox pBx_notifType;
    }
}
