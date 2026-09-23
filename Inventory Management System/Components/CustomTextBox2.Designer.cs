namespace Inventory_Management_System.Components
{
    partial class CustomTextBox2
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        // Duplicates on same file .s
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
            textBx_Header = new Label();
            txtBx_warning = new Label();
            inputPanel = new CustomPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pbx_Btn = new PictureBox();
            textBox1 = new TextBox();
            inputPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_Btn).BeginInit();
            SuspendLayout();
            // 
            // textBx_Header
            // 
            textBx_Header.AutoSize = true;
            textBx_Header.Dock = DockStyle.Top;
            textBx_Header.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBx_Header.Location = new Point(5, 5);
            textBx_Header.Margin = new Padding(0, 0, 0, 8);
            textBx_Header.Name = "textBx_Header";
            textBx_Header.Size = new Size(52, 17);
            textBx_Header.TabIndex = 1;
            textBx_Header.Text = "Header";
            // 
            // txtBx_warning
            // 
            txtBx_warning.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBx_warning.BackColor = Color.Transparent;
            txtBx_warning.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBx_warning.ForeColor = Color.Red;
            txtBx_warning.Location = new Point(8, 68);
            txtBx_warning.Margin = new Padding(0, 5, 0, 0);
            txtBx_warning.Name = "txtBx_warning";
            txtBx_warning.Size = new Size(247, 27);
            txtBx_warning.TabIndex = 2;
            // 
            // inputPanel
            // 
            inputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputPanel.BackColor = Color.Transparent;
            inputPanel.BackgroundColor = Color.Transparent;
            inputPanel.BorderColor = Color.Gainsboro;
            inputPanel.BorderRadius = 5;
            inputPanel.BorderSize = 1;
            inputPanel.Controls.Add(tableLayoutPanel1);
            inputPanel.Location = new Point(8, 33);
            inputPanel.Margin = new Padding(0);
            inputPanel.Name = "inputPanel";
            inputPanel.Padding = new Padding(5, 2, 5, 2);
            inputPanel.Size = new Size(252, 32);
            inputPanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Controls.Add(pbx_Btn, 1, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(5, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(242, 28);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pbx_Btn
            // 
            pbx_Btn.Cursor = Cursors.Hand;
            pbx_Btn.Dock = DockStyle.Right;
            pbx_Btn.Location = new Point(217, 5);
            pbx_Btn.Margin = new Padding(5);
            pbx_Btn.Name = "pbx_Btn";
            pbx_Btn.Size = new Size(20, 18);
            pbx_Btn.SizeMode = PictureBoxSizeMode.Zoom;
            pbx_Btn.TabIndex = 0;
            pbx_Btn.TabStop = false;
            pbx_Btn.Click += pbx_Btn_Click_1;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Dock = DockStyle.Fill;
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(0, 6);
            textBox1.Margin = new Padding(0, 6, 8, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(204, 18);
            textBox1.TabIndex = 1;
            // 
            // CustomTextBox2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(inputPanel);
            Controls.Add(textBx_Header);
            Controls.Add(txtBx_warning);
            Name = "CustomTextBox2";
            Padding = new Padding(5);
            Size = new Size(268, 100);
            Load += UserControl1_Load;
            inputPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_Btn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label textBx_Header;
        private Label txtBx_warning;
        private CustomPanel inputPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox1;
        private PictureBox pbx_Btn;
    }
}
