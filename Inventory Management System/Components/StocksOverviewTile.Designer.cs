namespace Inventory_Management_System.Components
{
    partial class StocksOverviewTile
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
            lbl_TileItemCount = new Label();
            lbl_TileDescription = new Label();
            lbl_TileHeader = new Label();
            cPnl_TileImage = new CustomPanel();
            pbx_Icon = new PictureBox();
            cPnl_TileImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_Icon).BeginInit();
            SuspendLayout();
            // 
            // lbl_TileItemCount
            // 
            lbl_TileItemCount.AutoSize = true;
            lbl_TileItemCount.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_TileItemCount.Location = new Point(23, 141);
            lbl_TileItemCount.Name = "lbl_TileItemCount";
            lbl_TileItemCount.Size = new Size(88, 20);
            lbl_TileItemCount.TabIndex = 3;
            lbl_TileItemCount.Text = "Item Count";
            // 
            // lbl_TileDescription
            // 
            lbl_TileDescription.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_TileDescription.Location = new Point(23, 88);
            lbl_TileDescription.Name = "lbl_TileDescription";
            lbl_TileDescription.Size = new Size(161, 36);
            lbl_TileDescription.TabIndex = 2;
            lbl_TileDescription.Text = "Description";
            lbl_TileDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl_TileHeader
            // 
            lbl_TileHeader.AutoSize = true;
            lbl_TileHeader.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_TileHeader.Location = new Point(86, 38);
            lbl_TileHeader.Name = "lbl_TileHeader";
            lbl_TileHeader.Size = new Size(59, 20);
            lbl_TileHeader.TabIndex = 1;
            lbl_TileHeader.Text = "Header";
            // 
            // cPnl_TileImage
            // 
            cPnl_TileImage.BackColor = Color.Gray;
            cPnl_TileImage.BackgroundColor = Color.Gray;
            cPnl_TileImage.BorderColor = Color.Transparent;
            cPnl_TileImage.BorderRadius = 7;
            cPnl_TileImage.BorderSize = 1;
            cPnl_TileImage.Controls.Add(pbx_Icon);
            cPnl_TileImage.Location = new Point(23, 23);
            cPnl_TileImage.Margin = new Padding(0, 0, 15, 20);
            cPnl_TileImage.Name = "cPnl_TileImage";
            cPnl_TileImage.Padding = new Padding(10);
            cPnl_TileImage.Size = new Size(45, 45);
            cPnl_TileImage.TabIndex = 0;
            // 
            // pbx_Icon
            // 
            pbx_Icon.BackColor = Color.Transparent;
            pbx_Icon.BackgroundImageLayout = ImageLayout.None;
            pbx_Icon.Dock = DockStyle.Fill;
            pbx_Icon.Location = new Point(10, 10);
            pbx_Icon.Name = "pbx_Icon";
            pbx_Icon.Size = new Size(25, 25);
            pbx_Icon.SizeMode = PictureBoxSizeMode.Zoom;
            pbx_Icon.TabIndex = 4;
            pbx_Icon.TabStop = false;
            // 
            // StocksOverviewTile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lbl_TileHeader);
            Controls.Add(lbl_TileDescription);
            Controls.Add(lbl_TileItemCount);
            Controls.Add(cPnl_TileImage);
            Name = "StocksOverviewTile";
            Size = new Size(295, 184);
            cPnl_TileImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbx_Icon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbl_TileHeader;
        private CustomPanel cPnl_TileImage;
        private Label lbl_TileItemCount;
        private Label lbl_TileDescription;
        private PictureBox pbx_Icon;
    }
}
