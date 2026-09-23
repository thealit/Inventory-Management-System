using Inventory_Management_System.Models;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Properties;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace Inventory_Management_System.Components
{
    public partial class Inventory : Form, IInventoryView
    {
        private readonly InventoryForm _mainInventoryForm;
        private readonly NotificationPresenter _notifPresenter;
        System.Windows.Forms.Timer _loadUITimer = new System.Windows.Forms.Timer();
        private int _timerTicks = 0;

        public Inventory(InventoryForm mainInventoryForm)
        {
            
            InitializeComponent();

            this.DoubleBuffered = true;

            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            _mainInventoryForm = mainInventoryForm;

            
            _ctbxs2.AddRange([ctbx2_ItemId, ctbx2_Item, ctbx2_InStockAmount, ctbx2_MinStock, ctbx2_Category, ctbx2_Unit]);

            cBtn_DeleteOneOrMany.Click += (s, e) => DeleteItemClicked?.Invoke(this, EventArgs.Empty);
            cBtn_CheckBoxTop.Click += (s, e) => CheckBoxTopClicked?.Invoke(this, EventArgs.Empty);


            panel4.Dock = DockStyle.Fill;
            panel4.Margin = new Padding(0);

            _mainInventoryForm.InvalidateInventory += OnInvalidateInventory;
            this.Disposed += (s, e) =>
            {
                _mainInventoryForm.InvalidateInventory -= OnInvalidateInventory;
                _loadUITimer.Stop();
                _loadUITimer.Dispose();
            };

            _loadUITimer.Interval = 100;
            _loadUITimer.Tick += Timer_Tick;

            tile_LowStock.Click += (s, e) => FilterLowStockItems?.Invoke(this, EventArgs.Empty);
            tile_OutOfStock.Click += (s, e) => FilterOutOfStockItems?.Invoke(this, EventArgs.Empty);
            tile_TotalStocks.Click += (s, e) => FilterAllItems?.Invoke(this, EventArgs.Empty);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timerTicks++;

            if (_timerTicks >= 5)
            {
                _loadUITimer.Stop();
                _timerTicks = 0;
                dgv_inventoryTable.Show();
                itemDetails_panel.Show();
                this.PerformLayout();
            }
        }

        private void OnInvalidateInventory(object? sender, EventArgs e)
        {
            this.SuspendLayout();
            InvalidateAll();
            this.ResumeLayout(false);

            if (!_loadUITimer.Enabled)
            {
                _timerTicks = 0;
                dgv_inventoryTable.Hide();
                itemDetails_panel.Hide();
                _loadUITimer.Start();
            }

        }

        public void InvalidateAll()
        {
            this.Invalidate();
            panel3.Invalidate(true);
            panel3.Update();
            panel4.Invalidate(true);
            panel4.Update();
            itemDetails_panel.Invalidate(true);
            tblPanel_ItemDetailsButtons.Invalidate(true);
            leftPanel.Invalidate(true);
            rightPanel.Invalidate(true);
            tile_LowStock.Invalidate(true);
            tile_OutOfStock.Invalidate(true);
            tile_TotalStocks.Invalidate(true);
            ctbx2_Category.IsDropdownVisible = false;
            ctbx2_Unit.IsDropdownVisible = false;   
         
            this.Update();
        }

        public async void RefreshInventoryTable()
        {
            if (_inventoryPresenter == null) return;
            await _inventoryPresenter.LoadInventoryTableAsync();
        }
      
       
        private void cBtn_AddItem_Click(object sender, EventArgs e)
        {
            SetDefaultFields();
            ToggleActionMode(0);
            ToggleActionMode(1); // show add/cancel, hide edit/delete
        }



        public void ClearItemFields()
        {
            foreach (CustomTextBox2 tb in _ctbxs2)
            {
                tb.Input = string.Empty;
            }
            ctbx2_Category.Placeholder = "Select category";
            ctbx2_Unit.Placeholder = "Select unit"; 

        }
        public void ClearWarnings()
        {
            foreach (CustomTextBox2 tb in _ctbxs2)
            {
                tb.WarningLabel = string.Empty;
            }
        }

        public void FocusOnItemField() => ctbx2_Item.Focus();

        // Check for texts
        public bool AnyTxtBoxesIsNotEmpty()
        {
            List<CustomTextBox2> txtbxs = new([ctbx2_Item, ctbx2_InStockAmount, ctbx2_MinStock]);

            foreach (CustomTextBox2 tbx in txtbxs)
            {
                if (!string.IsNullOrEmpty(tbx.Input))
                    return true;
                return false;
            }

            return false;
        }
        public bool AnyComboBoxesHasSelection()
        {
            if(CategoryId != 0 || UnitId != 0)
                return true;
            return false;
        }

        private ManageCategory? _manageCategoryPanel;
        // Populate checkboxes/item details
        public void PopulateCategories(List<CategoryModel>? categories)
        {
            if (_manageCategoryPanel == null)
            {
                _manageCategoryPanel = new ManageCategory(_mainInventoryForm, this);

                _manageCategoryPanel.CategoryChanged += async (s, e) =>
                {
                    await _inventoryPresenter.RefreshCategories();
                };
            }

            ctbx2_Category.AddItemToDropdown(
                options: categories,
                displayMember: "Category",
                valueMember: "CategoryId",
                customBtnDisplayText: "Add/Edit category",
                panelToShow: _manageCategoryPanel
            );
        }

        private ManageUnit? _manageUnitPanel; // field

        public void PopulateUnits(List<UnitModel>? units)
        {
            if (_manageUnitPanel == null)
            {
                _manageUnitPanel = new ManageUnit(_mainInventoryForm, this);

                _manageUnitPanel.UnitChanged += async (s, e) =>
                {
                    await _inventoryPresenter.RefreshUnits();
                };
            }
          
            ctbx2_Unit.AddItemToDropdown(
               options: units,
                displayMember: "Unit",
                valueMember: "UnitId",
                customBtnDisplayText: "Add/Edit unit",
                panelToShow: _manageUnitPanel
            );
        }
        public void PopulateItemDetails(InventoryModel item)
        {
            ctbx2_ItemId.Input = item.ItemId.ToString();
            ctbx2_Item.Input = item.Item;
            ctbx2_Category.SelectedId = item.CategoryId;
            ctbx2_Unit.SelectedId = item.UnitId;

            ctbx2_InStockAmount.Input = item.InStock.ToString(); 
            ctbx2_MinStock.Input = item.MinimumStock.ToString(); 
            ToggleActionMode(state: 2); // show Edit/Delete buttons, , hide add/cancel
        }

        // Set fields warnings
        public void SetItemWarning(string? message) => ctbx2_Item.WarningLabel = message ?? "";
        public void SetCategoryWarning(string? message) => ctbx2_Category.WarningLabel = message ?? "";
        public void SetInStockWarning(string? message) => ctbx2_InStockAmount.WarningLabel = message ?? ""; 
        public void SetUnitWarning(string? message) => ctbx2_Unit.WarningLabel = message ?? ""; 
        public void SetMinStockWarning(string? message) => ctbx2_MinStock.WarningLabel = message ?? ""; 

        // Show/Confirm Message
        public bool ConfirmAction(string message, string title)
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            return result == DialogResult.OK;
        }
        public void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) => MessageBox.Show(message, title, buttons, icon);

        public void ToggleActionMode(int state)
        {
            if (btn_Add == null || btn_Edit == null) return;
            // 0 hide all
            // 1 show add/cancel, hide edit/delete
            // 2 show edit/delete, hide add/cancel
            switch (state)
            {
                case 0:
                    btn_Add.Visible = false;
                    btn_CancelAdd.Visible = false;
                    btn_Edit.Visible = false;
                    btn_Delete.Visible = false;
                    break;
                case 1:
                    btn_Add.Visible = true;
                    btn_CancelAdd.Visible = true;
                    btn_Edit.Visible = false;
                    btn_Delete.Visible = false;

                    btn_Add.BringToFront();
                    btn_CancelAdd.BringToFront();
                    break;
                case 2:
                    btn_Add.Visible = false;
                    btn_CancelAdd.Visible = false;
                    btn_Edit.Visible = true;
                    btn_Delete.Visible = true;

                    btn_Edit.BringToFront();
                    btn_Delete.BringToFront();
                    break;
            }
        }

        // Single/Mass Delete Button
        public void SetTopDeleteButton(int count)
        {
            bool hasCheckedRow = count >= 1;
            string label = "Delete item";

            if (hasCheckedRow && count == 1)
            {
                cBtn_DeleteOneOrMany.Visible = true;
            }
            else if (hasCheckedRow && count > 1)
            {
                label = $"Delete {count} items";
                cBtn_DeleteOneOrMany.Visible = true;
            }
            else { cBtn_DeleteOneOrMany.Visible = false; }

            cBtn_DeleteOneOrMany.Text = label;
        }


        // Other setups
        public void SetFieldsHeadersBold()
        {
            Font font = new("Segoe UI Semibold", 9, FontStyle.Regular);

            if (_ctbxs2 != null)
            {
                foreach (var item in _ctbxs2)
                {
                    if (item != null)
                    {
                        item.HeaderFont = font;
                    }
                }
            }
        }
        public void SetDefaultFields()
        {
            ClearItemFields();
            ctbx2_ItemId.Input = "This field is auto-generated";
            CategoryId = 0;
            UnitId = 0;
            ctbx2_Item?.Focus();
        }
        public void SetUpActionButtons()
        {

            // Instantiate buttons
            btn_Add = new CustomButton
            {
                Text = "Add",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(47, 39, 206), // blue
                BackgroundColor = Color.FromArgb(47, 39, 206), //
                BorderColor = Color.FromArgb(47, 39, 206), //
                BorderRadius = 7,
                BorderSize = 1,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                MouseEnterColor = Color.FromArgb(47, 39, 206), // blue
                MouseLeaveColor = Color.FromArgb(47, 39, 206), // blue
                TextOnHoverColor = Color.White,
                TextOnLeaveColor = Color.White
            };


            btn_CancelAdd = new CustomButton
            {
                Text = "Discard",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                BackColor = Color.White,
                BackgroundColor = Color.White, //
                BorderColor = Color.White, //
                BorderRadius = 7,
                BorderSize = 1,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                MouseEnterColor = Color.White,
                MouseLeaveColor = Color.White,
                TextOnHoverColor = Color.Black,
                TextOnLeaveColor = Color.Black
            };

            btn_Edit = new CustomButton
            {
                Text = "Update",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(47, 39, 206), // blue
                BackgroundColor = Color.FromArgb(47, 39, 206), //
                BorderColor = Color.FromArgb(47, 39, 206), //
                BorderRadius = 7,
                BorderSize = 1,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                MouseEnterColor = Color.FromArgb(47, 39, 206), // blue
                MouseLeaveColor = Color.FromArgb(47, 39, 206), // blue
                TextOnHoverColor = Color.White,
                TextOnLeaveColor = Color.White
            };

            btn_Delete = new CustomButton
            {
                Text = "Delete",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(231, 76, 60), // Red color
                BackgroundColor = Color.FromArgb(231, 76, 60), //
                BorderColor = Color.FromArgb(231, 76, 60), //
                BorderRadius = 7,
                BorderSize = 1,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                MouseEnterColor = Color.FromArgb(231, 76, 60), // Red color
                MouseLeaveColor = Color.FromArgb(231, 76, 60), // Red color
                TextOnHoverColor = Color.White,
                TextOnLeaveColor = Color.White
            };

            btn_Add.Click += (s, e) => AddItemClicked?.Invoke(this, EventArgs.Empty);
            btn_Edit.Click += (s, e) => UpdateItemClicked?.Invoke(this, EventArgs.Empty);
            btn_Delete.Click += (s, e) => DeleteItemClicked?.Invoke(this, EventArgs.Empty);
            btn_CancelAdd.Click += (s, e) => CancelAddClicked?.Invoke(this, EventArgs.Empty);

            tblPanel_ItemDetailsButtons.Controls.Clear(); // Ensure it's empty first

            leftPanel.Controls.Add(btn_CancelAdd);
            leftPanel.Controls.Add(btn_Delete);

            rightPanel.Controls.Add(btn_Add);
            rightPanel.Controls.Add(btn_Edit);

            tblPanel_ItemDetailsButtons.Controls.Add(leftPanel, 0, 0);
            tblPanel_ItemDetailsButtons.Controls.Add(rightPanel, 1, 0);
        }


        // Datagridview
        public void DataGridViewSetUp()
        {
            dgv_inventoryTable = new CustomDataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Name = "dgv_inventoryTable"
            };


            dgv_inventoryTable.AutoGenerateColumns = false;


            dgv_inventoryTable.Columns.AddRange(
            [
                CreateCheckBoxColumn("IsSelected", "Select", "Select", 0),
                CreateDataGridColumn("ItemId", "Item ID", "ItemId", 1),
                CreateDataGridColumn("Category", "Category", "Category", 2),
                CreateDataGridColumn("Item", "Item", "Item", 3),
                CreateDataGridColumn("InStock", "In-Stock", "InStock", 4),
                CreateDataGridColumn("ItemUnit", "Item Unit", "ItemUnit", 5),
                CreateDataGridColumn("MinimumStock", "Minimum Stock", "MinimumStock", 6)
            ]);

            dgv_inventoryTable.CurrentCellDirtyStateChanged += Dgv_inventoryTable_CurrentCellDirtyStateChanged;
            dgv_inventoryTable.CellValueChanged += Dgv_inventoryTable_CellValueChanged;
            dgv_inventoryTable.SelectionChanged += (s, e) => SelectionChanged?.Invoke(this, e);
            dgv_inventoryTable.ClientSizeChanged += (s, e) => dgv_inventoryTable.Refresh();

            StyleModernGrid(dgv_inventoryTable);
            panel4.Controls.Add(dgv_inventoryTable);
            dgv_inventoryTable.Dock = DockStyle.Fill;
            dgv_inventoryTable.BringToFront();
        }
        private DataGridViewTextBoxColumn CreateDataGridColumn(string propertyName, string headerText, string name, int displayIndex)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                DisplayIndex = displayIndex,
                ReadOnly = true,
                MinimumWidth = 20
            };
        }
        private DataGridViewCheckBoxColumn CreateCheckBoxColumn(string propertyName, string headerText, string name, int displayIndex)
        {
            return new DataGridViewCheckBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                DisplayIndex = displayIndex,
                ReadOnly = false,
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,

                DefaultCellStyle = new DataGridViewCellStyle
                {
                    ForeColor = Color.Black,
                    SelectionForeColor = Color.Black
                }
            };
        }
        private void StyleModernGrid(CustomDataGridView dgv)
        {
            // 1. General Grid Setup
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(220, 220, 220); // Soft, light gray gridlines
            dgv.RowHeadersVisible = false; // Hides the blank column on the far left
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false; // Removes the blank row at the bottom
            dgv.BorderSize = 1;
            dgv.BorderRadius = 12;
            dgv.BorderColor = Color.FromArgb(242, 242, 255);

            // 2. Column Auto-sizing behavior
            // Makes columns fill the available width automatically
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 3. Column Header Styling
            dgv.EnableHeadersVisualStyles = false; // Allows custom header colors
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 61, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(68, 61, 255);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 4. Default Row & Cell Styling
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64); 
            dgv.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0); 

            // 5. Alternating Row Colors (Zebra Striping)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

            // 6. Selection Styling
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selects the whole row when clicked
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214,214,250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 7. Row Height
            dgv.RowTemplate.Height = 35; 
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.ActiveControl = null;
        } 
        public void RefreshInventoryTable(List<InventoryModel> items)
        {
            dgv_inventoryTable.DataSource = null;
            dgv_inventoryTable.DataSource = items;
        } // feed new data to datagrid
        private void Dgv_inventoryTable_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            // Check if the cell is dirty or changed AND if it is specifically a CheckBox cell
            if (dgv_inventoryTable.IsCurrentCellDirty && dgv_inventoryTable.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Force the check/uncheck to save to your data source immediately
                dgv_inventoryTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void Dgv_inventoryTable_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_inventoryTable.Columns[e.ColumnIndex].Name == "IsSelected")
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }


        public void SetAllRowsCheckboxesState(bool checkAll)
        {
            foreach (DataGridViewRow row in dgv_inventoryTable.Rows)
            {
                if (row.Cells["IsSelected"].Value != null)
                {
                    row.Cells["IsSelected"].Value = checkAll;
                }
            }
        }


        public void SetCheckBoxTopStatusIcon(int status) 
        {
            // 0: unchecked, 1: checkedAll, 2: indeterminate (not all are selected)
            switch (status)
            {
                case 0: cBtn_CheckBoxTop.Image = Properties.Resources.icons8_unchecked_checkbox_96; break;
                case 1: cBtn_CheckBoxTop.Image = Properties.Resources.icons8_checked_checkbox_96; break;
                case 2: cBtn_CheckBoxTop.Image = Properties.Resources.icons8_indeterminate_checkbox_96; break;
            }

            cBtn_CheckBoxTop.BackgroundImageLayout = ImageLayout.Zoom;
            cBtn_CheckBoxTop.ImageSize = 21;
        }

        public void SetTilesValues(int totalStocks, int lowStock, int outOfStock)
        {
            tile_TotalStocks.TileItemCount = totalStocks;
            tile_LowStock.TileItemCount = lowStock;
            tile_OutOfStock.TileItemCount = outOfStock;
        }

        public void InvokeOnUI(Action action)
        {
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
        }

       
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            && (((int)m.WParam & 0xFFFF) == 5))
            {
                // Change SB_THUMBTRACK to SB_THUMBPOSITION
                m.WParam = (IntPtr)(((int)m.WParam & ~0xFFFF) | 4);
            }
            base.WndProc(ref m);
        }



        // Implementations
        public int ItemId
        {
            get
            {
                if (int.TryParse(ctbx2_ItemId.Input, out int id))
                    return id;
                return 0; //default, used in add item
            }
        }

        public string Item => ctbx2_Item.Input.Trim();
        public string InStock => ctbx2_InStockAmount.Input; 
        public string MinStock => ctbx2_MinStock.Input;
        public int CategoryId { 
            get => ctbx2_Category.SelectedId; 
            set => ctbx2_Category.SelectedId = value; 
        }
        public int UnitId { 
            get => ctbx2_Unit.SelectedId; 
            set => ctbx2_Unit.SelectedId = value; 
        }

        public List<KeyValuePair<int, string>> ItemIds
        {
            get
            {
                var ids = new List<KeyValuePair<int, string>>();
                // mass checked items for deletion
                foreach (DataGridViewRow row in dgv_inventoryTable.Rows)
                    if (Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        if (row.DataBoundItem is InventoryModel item)
                            ids.Add(new KeyValuePair<int, string>(item.ItemId, item.Item));

                // single highlighted row
                if (ids.Count == 0 && dgv_inventoryTable.SelectedRows.Count == 1)
                    if (dgv_inventoryTable.SelectedRows[0].DataBoundItem is InventoryModel selected)
                        ids.Add(new KeyValuePair<int, string>(selected.ItemId, selected.Item));
                return ids;
            }
        }
        public int ItemToUpdate
        {
            get
            {
                int id = 0;
                // single highlighted row
                if (dgv_inventoryTable.SelectedRows.Count == 1)
                    if (dgv_inventoryTable.SelectedRows[0].DataBoundItem is InventoryModel selected)
                        id = selected.ItemId;
                return id;
            }
        }
        public int CheckedCount
        {
            get
            {
                int count = 0;
                foreach (DataGridViewRow row in dgv_inventoryTable.Rows)
                    if (Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        count++;
                return count;
            }
        }

        public bool AreAllRowsChecked
        {
            get
            {
                foreach (DataGridViewRow row in dgv_inventoryTable.Rows)
                {
                    // if any row is NOT checked
                    if (!Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        return false;
                }
                // if all rows is NOT checked
                return true;
            }
        }

        public InventoryModel? SelectedItem
        {
            // gets data of selected item in dgv
            get
            {
                // Check checkboxes first
                DataGridViewRow? lastChecked = null;
                int checkedCount = 0;

                foreach (DataGridViewRow row in dgv_inventoryTable.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["IsSelected"].Value))
                    {
                        checkedCount++;
                        lastChecked = row;
                    }
                }

                // exactly one checkbox ticked
                if (checkedCount == 1)
                    return lastChecked?.DataBoundItem as InventoryModel;

                // no checkbox, but one row highlighted
                if (checkedCount == 0 && dgv_inventoryTable.SelectedRows.Count == 1)
                    return dgv_inventoryTable.CurrentRow.DataBoundItem as InventoryModel;

                return null; // 0 or multiple selected
            }
        }

        public void SetPresenter(InventoryPresenter presenter)
        {
            _inventoryPresenter = presenter;
        }

        private InventoryPresenter _inventoryPresenter;
        private CustomDataGridView dgv_inventoryTable;
        private List<CustomTextBox2> _ctbxs2 = [];
        private CustomButton btn_Add, btn_CancelAdd, btn_Edit, btn_Delete;

        public string SearchItem
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                SearchChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private string _searchTerm = string.Empty;

        public event EventHandler AddItemClicked;
        public event EventHandler DeleteItemClicked;
        public event EventHandler UpdateItemClicked;
        public event EventHandler CancelAddClicked;
        public event EventHandler SearchChanged;
        public event EventHandler SelectionChanged;
        public event EventHandler CheckBoxTopClicked;

        public event EventHandler FilterLowStockItems;
        public event EventHandler FilterOutOfStockItems;
        public event EventHandler FilterAllItems;
    }
}
