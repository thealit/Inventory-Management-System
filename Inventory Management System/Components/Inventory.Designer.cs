namespace Inventory_Management_System.Components
{
    partial class Inventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            dgv_panel = new Panel();
            lbl_ItemDetails = new Label();
            tile_TotalStocks = new StocksOverviewTile();
            tile_LowStock = new StocksOverviewTile();
            tile_OutOfStock = new StocksOverviewTile();
            customPanel1 = new CustomPanel();
            panel4 = new Panel();
            itemDetails_panel = new CustomPanel();
            ctbx2_MinStock = new CustomTextBox2();
            ctbx2_Unit = new CustomTextBox2();
            ctbx2_InStockAmount = new CustomTextBox2();
            ctbx2_Category = new CustomTextBox2();
            ctbx2_Item = new CustomTextBox2();
            ctbx2_ItemId = new CustomTextBox2();
            tblPanel_ItemDetailsButtons = new TableLayoutPanel();
            leftPanel = new Panel();
            rightPanel = new Panel();
            label1 = new Label();
            cBtn_DeleteOneOrMany = new CustomButton();
            cBtn_CheckBoxTop = new CustomButton();
            cBtn_AddItem = new CustomButton();
            panel3 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            customPanel1.SuspendLayout();
            panel4.SuspendLayout();
            itemDetails_panel.SuspendLayout();
            tblPanel_ItemDetailsButtons.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgv_panel
            // 
            dgv_panel.BackColor = Color.FromArgb(251, 251, 254);
            dgv_panel.Dock = DockStyle.Fill;
            dgv_panel.Location = new Point(0, 0);
            dgv_panel.Margin = new Padding(3, 2, 3, 2);
            dgv_panel.Name = "dgv_panel";
            dgv_panel.Size = new Size(839, 615);
            dgv_panel.TabIndex = 3;
            // 
            // lbl_ItemDetails
            // 
            lbl_ItemDetails.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_ItemDetails.AutoSize = true;
            lbl_ItemDetails.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_ItemDetails.Location = new Point(30, 30);
            lbl_ItemDetails.Margin = new Padding(0);
            lbl_ItemDetails.Name = "lbl_ItemDetails";
            lbl_ItemDetails.Size = new Size(130, 30);
            lbl_ItemDetails.TabIndex = 1;
            lbl_ItemDetails.Text = "Item Details";
            // 
            // tile_TotalStocks
            // 
            tile_TotalStocks.BackColor = Color.FromArgb(242, 242, 255);
            tile_TotalStocks.BackgroundColor = Color.FromArgb(242, 242, 255);
            tile_TotalStocks.BorderColor = Color.FromArgb(242, 242, 255);
            tile_TotalStocks.BorderRadius = 12;
            tile_TotalStocks.BorderSize = 1;
            tile_TotalStocks.Cursor = Cursors.Hand;
            tile_TotalStocks.Location = new Point(33, 33);
            tile_TotalStocks.Margin = new Padding(0, 0, 20, 0);
            tile_TotalStocks.Name = "tile_TotalStocks";
            tile_TotalStocks.Size = new Size(295, 191);
            tile_TotalStocks.TabIndex = 0;
            tile_TotalStocks.TileDescription = "Total items in stock";
            tile_TotalStocks.TileHeader = "Total Items";
            tile_TotalStocks.TileImage = Properties.Resources.icons8_package_100;
            tile_TotalStocks.TileImagePanelColor = Color.FromArgb(207, 255, 197);
            tile_TotalStocks.TileItemCount = 0;
            // 
            // tile_LowStock
            // 
            tile_LowStock.BackColor = Color.FromArgb(242, 242, 255);
            tile_LowStock.BackgroundColor = Color.FromArgb(242, 242, 255);
            tile_LowStock.BorderColor = Color.FromArgb(242, 242, 255);
            tile_LowStock.BorderRadius = 12;
            tile_LowStock.BorderSize = 1;
            tile_LowStock.Cursor = Cursors.Hand;
            tile_LowStock.Location = new Point(351, 33);
            tile_LowStock.Margin = new Padding(0, 0, 20, 0);
            tile_LowStock.Name = "tile_LowStock";
            tile_LowStock.Size = new Size(295, 191);
            tile_LowStock.TabIndex = 1;
            tile_LowStock.TileDescription = "Number of items that are \nrunning low";
            tile_LowStock.TileHeader = "Low Stock Items";
            tile_LowStock.TileImage = Properties.Resources.icons8_decline_48;
            tile_LowStock.TileImagePanelColor = Color.FromArgb(255, 224, 189);
            tile_LowStock.TileItemCount = 0;
            // 
            // tile_OutOfStock
            // 
            tile_OutOfStock.BackColor = Color.FromArgb(242, 242, 255);
            tile_OutOfStock.BackgroundColor = Color.FromArgb(242, 242, 255);
            tile_OutOfStock.BorderColor = Color.FromArgb(242, 242, 255);
            tile_OutOfStock.BorderRadius = 12;
            tile_OutOfStock.BorderSize = 1;
            tile_OutOfStock.Cursor = Cursors.Hand;
            tile_OutOfStock.Location = new Point(669, 33);
            tile_OutOfStock.Margin = new Padding(0);
            tile_OutOfStock.Name = "tile_OutOfStock";
            tile_OutOfStock.Size = new Size(295, 191);
            tile_OutOfStock.TabIndex = 2;
            tile_OutOfStock.TileDescription = "Number of items that are \ncurrently out of stock";
            tile_OutOfStock.TileHeader = "Out of Stock Items";
            tile_OutOfStock.TileImage = Properties.Resources.icons8_out_of_stock_100;
            tile_OutOfStock.TileImagePanelColor = Color.FromArgb(255, 204, 204);
            tile_OutOfStock.TileItemCount = 0;
            // 
            // customPanel1
            // 
            customPanel1.BackColor = Color.FromArgb(242, 242, 255);
            customPanel1.BackgroundColor = Color.FromArgb(242, 242, 255);
            customPanel1.BorderColor = Color.FromArgb(242, 242, 255);
            customPanel1.BorderRadius = 12;
            customPanel1.BorderSize = 1;
            customPanel1.Controls.Add(panel4);
            customPanel1.Controls.Add(cBtn_DeleteOneOrMany);
            customPanel1.Controls.Add(cBtn_CheckBoxTop);
            customPanel1.Controls.Add(cBtn_AddItem);
            customPanel1.Controls.Add(panel3);
            customPanel1.Dock = DockStyle.Fill;
            customPanel1.Location = new Point(0, 0);
            customPanel1.Margin = new Padding(0, 30, 0, 0);
            customPanel1.Name = "customPanel1";
            customPanel1.Padding = new Padding(20);
            customPanel1.Size = new Size(1270, 726);
            customPanel1.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(itemDetails_panel);
            panel4.Location = new Point(20, 76);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1230, 630);
            panel4.TabIndex = 10;
            // 
            // itemDetails_panel
            // 
            itemDetails_panel.BackColor = Color.White;
            itemDetails_panel.BackgroundColor = Color.White;
            itemDetails_panel.BorderColor = Color.FromArgb(242, 242, 255);
            itemDetails_panel.BorderRadius = 12;
            itemDetails_panel.BorderSize = 1;
            itemDetails_panel.Controls.Add(ctbx2_MinStock);
            itemDetails_panel.Controls.Add(ctbx2_Unit);
            itemDetails_panel.Controls.Add(ctbx2_InStockAmount);
            itemDetails_panel.Controls.Add(ctbx2_Category);
            itemDetails_panel.Controls.Add(ctbx2_Item);
            itemDetails_panel.Controls.Add(ctbx2_ItemId);
            itemDetails_panel.Controls.Add(tblPanel_ItemDetailsButtons);
            itemDetails_panel.Controls.Add(label1);
            itemDetails_panel.Dock = DockStyle.Right;
            itemDetails_panel.Location = new Point(898, 0);
            itemDetails_panel.Margin = new Padding(0);
            itemDetails_panel.Name = "itemDetails_panel";
            itemDetails_panel.Padding = new Padding(20);
            itemDetails_panel.Size = new Size(332, 630);
            itemDetails_panel.TabIndex = 7;
            // 
            // ctbx2_MinStock
            // 
            ctbx2_MinStock.EnableText = true;
            ctbx2_MinStock.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_MinStock.Input = "";
            ctbx2_MinStock.IsDropDown = false;
            ctbx2_MinStock.IsDropdownVisible = false;
            ctbx2_MinStock.IsPassword = false;
            ctbx2_MinStock.IsReadOnly = false;
            ctbx2_MinStock.Label = "Minimum Stock Requirement";
            ctbx2_MinStock.Location = new Point(23, 459);
            ctbx2_MinStock.Margin = new Padding(0);
            ctbx2_MinStock.Name = "ctbx2_MinStock";
            ctbx2_MinStock.Padding = new Padding(5);
            ctbx2_MinStock.Selected = null;
            ctbx2_MinStock.SelectedId = 0;
            ctbx2_MinStock.Size = new Size(284, 80);
            ctbx2_MinStock.TabIndex = 14;
            ctbx2_MinStock.TextBoxBackColor = Color.White;
            ctbx2_MinStock.TextBoxColor = SystemColors.WindowText;
            ctbx2_MinStock.WarningLabel = "";
            // 
            // ctbx2_Unit
            // 
            ctbx2_Unit.EnableText = true;
            ctbx2_Unit.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_Unit.Input = "";
            ctbx2_Unit.IsDropDown = true;
            ctbx2_Unit.IsDropdownVisible = false;
            ctbx2_Unit.IsPassword = false;
            ctbx2_Unit.IsReadOnly = true;
            ctbx2_Unit.Label = "Unit";
            ctbx2_Unit.Location = new Point(162, 344);
            ctbx2_Unit.Margin = new Padding(3, 0, 3, 0);
            ctbx2_Unit.Name = "ctbx2_Unit";
            ctbx2_Unit.Padding = new Padding(5);
            ctbx2_Unit.Selected = null;
            ctbx2_Unit.SelectedId = 0;
            ctbx2_Unit.Size = new Size(145, 80);
            ctbx2_Unit.TabIndex = 13;
            ctbx2_Unit.TextBoxBackColor = Color.White;
            ctbx2_Unit.TextBoxColor = SystemColors.WindowText;
            ctbx2_Unit.WarningLabel = "";
            // 
            // ctbx2_InStockAmount
            // 
            ctbx2_InStockAmount.EnableText = true;
            ctbx2_InStockAmount.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_InStockAmount.Input = "";
            ctbx2_InStockAmount.IsDropDown = false;
            ctbx2_InStockAmount.IsDropdownVisible = false;
            ctbx2_InStockAmount.IsPassword = false;
            ctbx2_InStockAmount.IsReadOnly = false;
            ctbx2_InStockAmount.Label = "Stock";
            ctbx2_InStockAmount.Location = new Point(23, 344);
            ctbx2_InStockAmount.Margin = new Padding(3, 0, 3, 0);
            ctbx2_InStockAmount.Name = "ctbx2_InStockAmount";
            ctbx2_InStockAmount.Padding = new Padding(5);
            ctbx2_InStockAmount.Selected = null;
            ctbx2_InStockAmount.SelectedId = 0;
            ctbx2_InStockAmount.Size = new Size(145, 80);
            ctbx2_InStockAmount.TabIndex = 12;
            ctbx2_InStockAmount.TextBoxBackColor = Color.White;
            ctbx2_InStockAmount.TextBoxColor = SystemColors.WindowText;
            ctbx2_InStockAmount.WarningLabel = "";
            // 
            // ctbx2_Category
            // 
            ctbx2_Category.EnableText = true;
            ctbx2_Category.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_Category.Input = "";
            ctbx2_Category.IsDropDown = true;
            ctbx2_Category.IsDropdownVisible = false;
            ctbx2_Category.IsPassword = false;
            ctbx2_Category.IsReadOnly = true;
            ctbx2_Category.Label = "Category";
            ctbx2_Category.Location = new Point(23, 244);
            ctbx2_Category.Margin = new Padding(0);
            ctbx2_Category.Name = "ctbx2_Category";
            ctbx2_Category.Padding = new Padding(5);
            ctbx2_Category.Selected = null;
            ctbx2_Category.SelectedId = 0;
            ctbx2_Category.Size = new Size(284, 80);
            ctbx2_Category.TabIndex = 11;
            ctbx2_Category.TextBoxBackColor = Color.White;
            ctbx2_Category.TextBoxColor = SystemColors.WindowText;
            ctbx2_Category.WarningLabel = "";
            // 
            // ctbx2_Item
            // 
            ctbx2_Item.EnableText = true;
            ctbx2_Item.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_Item.Input = "";
            ctbx2_Item.IsDropDown = false;
            ctbx2_Item.IsDropdownVisible = false;
            ctbx2_Item.IsPassword = false;
            ctbx2_Item.IsReadOnly = false;
            ctbx2_Item.Label = "Item";
            ctbx2_Item.Location = new Point(23, 144);
            ctbx2_Item.Margin = new Padding(0);
            ctbx2_Item.Name = "ctbx2_Item";
            ctbx2_Item.Padding = new Padding(5);
            ctbx2_Item.Selected = null;
            ctbx2_Item.SelectedId = 0;
            ctbx2_Item.Size = new Size(284, 80);
            ctbx2_Item.TabIndex = 10;
            ctbx2_Item.TextBoxBackColor = Color.White;
            ctbx2_Item.TextBoxColor = SystemColors.WindowText;
            ctbx2_Item.WarningLabel = "";
            // 
            // ctbx2_ItemId
            // 
            ctbx2_ItemId.EnableText = false;
            ctbx2_ItemId.HeaderFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbx2_ItemId.Input = "";
            ctbx2_ItemId.IsDropDown = false;
            ctbx2_ItemId.IsDropdownVisible = false;
            ctbx2_ItemId.IsPassword = false;
            ctbx2_ItemId.IsReadOnly = false;
            ctbx2_ItemId.Label = "Item ID";
            ctbx2_ItemId.Location = new Point(23, 64);
            ctbx2_ItemId.Margin = new Padding(0);
            ctbx2_ItemId.Name = "ctbx2_ItemId";
            ctbx2_ItemId.Padding = new Padding(5);
            ctbx2_ItemId.Selected = null;
            ctbx2_ItemId.SelectedId = 0;
            ctbx2_ItemId.Size = new Size(284, 80);
            ctbx2_ItemId.TabIndex = 9;
            ctbx2_ItemId.TextBoxBackColor = Color.White;
            ctbx2_ItemId.TextBoxColor = SystemColors.WindowText;
            ctbx2_ItemId.WarningLabel = "";
            // 
            // tblPanel_ItemDetailsButtons
            // 
            tblPanel_ItemDetailsButtons.ColumnCount = 2;
            tblPanel_ItemDetailsButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPanel_ItemDetailsButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPanel_ItemDetailsButtons.Controls.Add(leftPanel, 0, 0);
            tblPanel_ItemDetailsButtons.Controls.Add(rightPanel, 1, 0);
            tblPanel_ItemDetailsButtons.Location = new Point(23, 559);
            tblPanel_ItemDetailsButtons.Margin = new Padding(0);
            tblPanel_ItemDetailsButtons.Name = "tblPanel_ItemDetailsButtons";
            tblPanel_ItemDetailsButtons.RowCount = 1;
            tblPanel_ItemDetailsButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPanel_ItemDetailsButtons.Size = new Size(284, 48);
            tblPanel_ItemDetailsButtons.TabIndex = 8;
            // 
            // leftPanel
            // 
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Location = new Point(3, 3);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(136, 42);
            leftPanel.TabIndex = 0;
            // 
            // rightPanel
            // 
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(145, 3);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(136, 42);
            rightPanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 20);
            label1.Margin = new Padding(0, 0, 0, 30);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 0;
            label1.Text = "Item Details";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cBtn_DeleteOneOrMany
            // 
            cBtn_DeleteOneOrMany.AutoSize = true;
            cBtn_DeleteOneOrMany.BackColor = Color.FromArgb(251, 251, 254);
            cBtn_DeleteOneOrMany.BackgroundColor = Color.FromArgb(251, 251, 254);
            cBtn_DeleteOneOrMany.BorderColor = Color.Gray;
            cBtn_DeleteOneOrMany.BorderRadius = 7;
            cBtn_DeleteOneOrMany.BorderSize = 1;
            cBtn_DeleteOneOrMany.Cursor = Cursors.Hand;
            cBtn_DeleteOneOrMany.FlatStyle = FlatStyle.Flat;
            cBtn_DeleteOneOrMany.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cBtn_DeleteOneOrMany.ForeColor = Color.Black;
            cBtn_DeleteOneOrMany.Image = (Image)resources.GetObject("cBtn_DeleteOneOrMany.Image");
            cBtn_DeleteOneOrMany.ImageSize = 20;
            cBtn_DeleteOneOrMany.IsDropDown = false;
            cBtn_DeleteOneOrMany.IsDropDownVisible = false;
            cBtn_DeleteOneOrMany.Location = new Point(73, 20);
            cBtn_DeleteOneOrMany.MouseEnterColor = Color.White;
            cBtn_DeleteOneOrMany.MouseLeaveColor = Color.White;
            cBtn_DeleteOneOrMany.Name = "cBtn_DeleteOneOrMany";
            cBtn_DeleteOneOrMany.Padding = new Padding(8, 0, 0, 0);
            cBtn_DeleteOneOrMany.Size = new Size(130, 42);
            cBtn_DeleteOneOrMany.TabIndex = 4;
            cBtn_DeleteOneOrMany.Text = " Delete item";
            cBtn_DeleteOneOrMany.TextImageRelation = TextImageRelation.ImageBeforeText;
            cBtn_DeleteOneOrMany.TextOnHoverColor = Color.Black;
            cBtn_DeleteOneOrMany.TextOnLeaveColor = Color.Black;
            cBtn_DeleteOneOrMany.UseVisualStyleBackColor = false;
            cBtn_DeleteOneOrMany.Visible = false;
            // 
            // cBtn_CheckBoxTop
            // 
            cBtn_CheckBoxTop.BackColor = Color.FromArgb(251, 251, 254);
            cBtn_CheckBoxTop.BackgroundColor = Color.FromArgb(251, 251, 254);
            cBtn_CheckBoxTop.BackgroundImageLayout = ImageLayout.Zoom;
            cBtn_CheckBoxTop.BorderColor = Color.Gray;
            cBtn_CheckBoxTop.BorderRadius = 7;
            cBtn_CheckBoxTop.BorderSize = 1;
            cBtn_CheckBoxTop.Cursor = Cursors.Hand;
            cBtn_CheckBoxTop.FlatAppearance.BorderColor = Color.Gray;
            cBtn_CheckBoxTop.FlatStyle = FlatStyle.Flat;
            cBtn_CheckBoxTop.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cBtn_CheckBoxTop.ForeColor = Color.Black;
            cBtn_CheckBoxTop.Image = (Image)resources.GetObject("cBtn_CheckBoxTop.Image");
            cBtn_CheckBoxTop.ImageSize = 21;
            cBtn_CheckBoxTop.IsDropDown = false;
            cBtn_CheckBoxTop.IsDropDownVisible = false;
            cBtn_CheckBoxTop.Location = new Point(23, 20);
            cBtn_CheckBoxTop.MouseEnterColor = Color.White;
            cBtn_CheckBoxTop.MouseLeaveColor = Color.White;
            cBtn_CheckBoxTop.Name = "cBtn_CheckBoxTop";
            cBtn_CheckBoxTop.Padding = new Padding(1, 0, 0, 0);
            cBtn_CheckBoxTop.Size = new Size(44, 42);
            cBtn_CheckBoxTop.TabIndex = 6;
            cBtn_CheckBoxTop.TextOnHoverColor = Color.White;
            cBtn_CheckBoxTop.TextOnLeaveColor = Color.Black;
            cBtn_CheckBoxTop.UseVisualStyleBackColor = false;
            // 
            // cBtn_AddItem
            // 
            cBtn_AddItem.Anchor = AnchorStyles.Right;
            cBtn_AddItem.BackColor = Color.FromArgb(47, 39, 206);
            cBtn_AddItem.BackgroundColor = Color.FromArgb(47, 39, 206);
            cBtn_AddItem.BorderColor = Color.FromArgb(47, 39, 206);
            cBtn_AddItem.BorderRadius = 7;
            cBtn_AddItem.BorderSize = 1;
            cBtn_AddItem.Cursor = Cursors.Hand;
            cBtn_AddItem.FlatStyle = FlatStyle.Flat;
            cBtn_AddItem.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cBtn_AddItem.ForeColor = Color.White;
            cBtn_AddItem.Image = (Image)resources.GetObject("cBtn_AddItem.Image");
            cBtn_AddItem.ImageSize = 20;
            cBtn_AddItem.IsDropDown = false;
            cBtn_AddItem.IsDropDownVisible = false;
            cBtn_AddItem.Location = new Point(1133, 20);
            cBtn_AddItem.MouseEnterColor = Color.FromArgb(47, 39, 206);
            cBtn_AddItem.MouseLeaveColor = Color.FromArgb(47, 39, 206);
            cBtn_AddItem.Name = "cBtn_AddItem";
            cBtn_AddItem.Padding = new Padding(8, 0, 0, 0);
            cBtn_AddItem.Size = new Size(114, 42);
            cBtn_AddItem.TabIndex = 1;
            cBtn_AddItem.Text = "Add Item";
            cBtn_AddItem.TextImageRelation = TextImageRelation.ImageBeforeText;
            cBtn_AddItem.TextOnHoverColor = Color.White;
            cBtn_AddItem.TextOnLeaveColor = Color.White;
            cBtn_AddItem.UseVisualStyleBackColor = false;
            cBtn_AddItem.Click += cBtn_AddItem_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(20, 20);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1230, 56);
            panel3.TabIndex = 9;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(30, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(1270, 231);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(customPanel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(30, 261);
            panel2.Name = "panel2";
            panel2.Size = new Size(1270, 726);
            panel2.TabIndex = 5;
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(251, 251, 254);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1330, 1017);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(tile_OutOfStock);
            Controls.Add(tile_LowStock);
            Controls.Add(tile_TotalStocks);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Inventory";
            Padding = new Padding(30);
            Text = "Inventory";
            customPanel1.ResumeLayout(false);
            customPanel1.PerformLayout();
            panel4.ResumeLayout(false);
            itemDetails_panel.ResumeLayout(false);
            itemDetails_panel.PerformLayout();
            tblPanel_ItemDetailsButtons.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel dgv_panel;
        private Label lbl_ItemDetails;
        //private CustomTextBox cTBx_InStockAmount;
        //private CustomComboBox cBx_Unit;
        //private CustomTextBox cTBx_MinStock;
        private CustomPanel customPanel1;
        private CustomButton cBtn_AddItem;
        private CustomPanel customPanel3;
        private CustomButton cBtn_MassDelete;
        private CustomButton cBtn_FilterInventory;
        private CustomButton cBtn_CheckBoxInventory;
        private CustomButton cBtn_DeleteOneOrMany;
        private CustomPanel customPanel4;
        private CustomPanel customPanel6;
        private CustomPanel customPanel5;
        private StocksOverviewTile tile_TotalStocks;
        private StocksOverviewTile tile_LowStock;
        private StocksOverviewTile tile_OutOfStock;
        private CustomButton cBtn_CheckBoxTop;
        private CustomPanel itemDetails_panel;
        private Label label1;
        private TableLayoutPanel tblPanel_ItemDetailsButtons;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel3;
        private Panel panel4;
        private CustomTextBox2 ctbx2_InStockAmount;
        private CustomTextBox2 ctbx2_Category;
        private CustomTextBox2 ctbx2_Item;
        private CustomTextBox2 ctbx2_ItemId;
        private CustomTextBox2 ctbx2_MinStock;
        private CustomTextBox2 ctbx2_Unit;
        private Panel leftPanel;
        private Panel rightPanel;
    }
}