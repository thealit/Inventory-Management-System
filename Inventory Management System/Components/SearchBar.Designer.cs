namespace Inventory_Management_System.Components
{
    partial class SearchBar
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
            customPanel1 = new CustomPanel();
            Pbx_IconClear = new PictureBox();
            Pbx_IconSearch = new PictureBox();
            textBox1 = new TextBox();
            customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Pbx_IconClear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pbx_IconSearch).BeginInit();
            SuspendLayout();
            // 
            // customPanel1
            // 
            customPanel1.BackColor = Color.FromArgb(251, 251, 254);
            customPanel1.BackgroundColor = Color.FromArgb(251, 251, 254);
            customPanel1.BorderColor = Color.Gray;
            customPanel1.BorderRadius = 7;
            customPanel1.BorderSize = 1;
            customPanel1.Controls.Add(Pbx_IconClear);
            customPanel1.Controls.Add(Pbx_IconSearch);
            customPanel1.Controls.Add(textBox1);
            customPanel1.Dock = DockStyle.Fill;
            customPanel1.Location = new Point(0, 0);
            customPanel1.Name = "customPanel1";
            customPanel1.Size = new Size(371, 48);
            customPanel1.TabIndex = 0;
            customPanel1.MouseLeave += customPanel1_MouseLeave;
            // 
            // Pbx_IconClear
            // 
            Pbx_IconClear.Anchor = AnchorStyles.Right;
            Pbx_IconClear.BackColor = Color.Transparent;
            Pbx_IconClear.BackgroundImageLayout = ImageLayout.None;
            Pbx_IconClear.Cursor = Cursors.Hand;
            Pbx_IconClear.Image = Properties.Resources.icons8_close_24;
            Pbx_IconClear.Location = new Point(333, 16);
            Pbx_IconClear.Name = "Pbx_IconClear";
            Pbx_IconClear.Size = new Size(19, 17);
            Pbx_IconClear.SizeMode = PictureBoxSizeMode.Zoom;
            Pbx_IconClear.TabIndex = 2;
            Pbx_IconClear.TabStop = false;
            Pbx_IconClear.Click += Pbx_IconClear_Click;
            // 
            // Pbx_IconSearch
            // 
            Pbx_IconSearch.Anchor = AnchorStyles.Left;
            Pbx_IconSearch.Image = Properties.Resources.icons8_search_90;
            Pbx_IconSearch.Location = new Point(21, 15);
            Pbx_IconSearch.Name = "Pbx_IconSearch";
            Pbx_IconSearch.Size = new Size(21, 18);
            Pbx_IconSearch.SizeMode = PictureBoxSizeMode.Zoom;
            Pbx_IconSearch.TabIndex = 1;
            Pbx_IconSearch.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(48, 15);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Search by item or category...";
            textBox1.Size = new Size(279, 18);
            textBox1.TabIndex = 0;
            textBox1.WordWrap = false;
            // 
            // SearchBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(customPanel1);
            Name = "SearchBar";
            Size = new Size(371, 48);
            customPanel1.ResumeLayout(false);
            customPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Pbx_IconClear).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pbx_IconSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomPanel customPanel1;
        private TextBox textBox1;
        private PictureBox Pbx_IconSearch;
        private PictureBox Pbx_IconClear;
    }
}
