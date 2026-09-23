using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Presenters;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class ManageCategory : UserControl, IManageCategoryView
    {
        private readonly Inventory _inventory;
        public ManageCategory(InventoryForm inventoryForm, Inventory inventory)
        {
            InitializeComponent();

            _inventoryForm = inventoryForm;
            _inventory = inventory;


            InstantiateFields_Buttons();
            WireEvents();
            _presenter = new ManageCategoryPresenter(new ModelsData.ManageCategoryRepository(), new ChangeLogRepository(), this);
            ViewAddCategory();

            _inventoryForm.Controls.Add(this);
            this.BringToFront();
            this.Hide();
            CenterControlOnLoad.ClickOutsideDetected += HideOnClickOutside;
        }

        private async void ManageCategory_Load(object sender, EventArgs e)
        {
            await _presenter.View_RefreshCategories();
            ViewAddCategory();
        }

        private void HideOnClickOutside(object? sender, EventArgs e)
        {
            if (sender == this)
            {
                CloseManageCategory();
            }
        }

        protected override void Dispose(bool disposing)
        {
            // unwire outside click events to prevent memory leaks
            if (disposing)
            {
                CenterControlOnLoad.ClickOutsideDetected -= HideOnClickOutside;
            }
            base.Dispose(disposing);
        }


        public void CloseManageCategory() {
            ClearFields();
            ClearWarnings();
            selectCategory.SelectedId = 0;
            this.Hide();
        }
        public void ViewAddCategory()
        {
            ResizeBaseControl(1);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(addNewCategory);
            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnAddNew);
            btnAddNew.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();
        }
        public void ViewEditCategory()
        {
            ResizeBaseControl(2);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(selectCategory);
            selectCategory.BringToFront();
            fieldsPanel.Controls.Add(editedCategory);
            editedCategory.BringToFront();

            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnDelete);
            bottomButtons.Controls.Add(btnSaveEdit);
            btnSaveEdit.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();
            

        }
        public void ShowAddCategorySide() => ViewAddCategory();
        public void ShowEditCategorySide() {
            ViewEditCategory();
        }

        public void RefreshInventoryTable() => _inventory.RefreshInventoryTable();

        public void ResizeBaseControl(int panelNum)
        {
            fieldsPanel.Padding = panelNum == 1
                ? new Padding(0, 20, 0, 0)
                : new Padding(0, 0, 0, 0);

            basePanel.Invalidate();
            this.PerformLayout();
        }
        
        public void ClearWarnings()
        {
            addNewCategory.WarningLabel = string.Empty;
            selectCategory.WarningLabel = string.Empty;
            editedCategory.WarningLabel = string.Empty;
        }
        public void ClearFields()
        {
            addNewCategory.Input = string.Empty;
            selectCategory.Input = string.Empty;
            editedCategory.Input = string.Empty;
        }
        public void ShowMessage(string message, string header, MessageBoxIcon icon) 
        { 
            MessageBox.Show(message, header, MessageBoxButtons.OK, icon);
        }
        public bool ConfirmMessage(string message, string header, MessageBoxIcon icon) 
        {
            DialogResult result = MessageBox.Show(message, header, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return result == DialogResult.OK;
        }



        public void SetNewCategoryWarning(string? message) => addNewCategory.WarningLabel = message ?? string.Empty;
        public void SetChooseCategoryToEditWarning(string? message) => selectCategory.WarningLabel = message ?? string.Empty;
        public void SetEditCategoryWarning(string? message) => editedCategory.WarningLabel = message ?? string.Empty;

        public void InstantiateFields_Buttons()
        {
            addNewCategory = new()
            {
                Name = "addNewCategory",
                Label = "New Category",
                Size = new Size(288, 100),
                Dock = DockStyle.Top,
                TextBoxBackColor = fieldsPanel.BackColor
            };

            selectCategory = new()
            {
                Name = "selectCategory",
                Label = "Select Category",
                IsDropDown = true,
                Size = new Size(288, 100),
                Dock = DockStyle.Top,
                Location = new Point(0, 0),
                TextBoxBackColor = fieldsPanel.BackColor
            };
            selectCategory.SetManageCategoryControl(this);

            editedCategory = new()
            {
                Name = "editedCategory",
                Label = "New Category Name",
                Size = new Size(288, 100),
                Dock = DockStyle.Top,
                TextBoxBackColor = fieldsPanel.BackColor
            };

            btnAddNew = new()
            {
                Name = "btnAddNewCategory",
                Text = "Add Category",
                Size = new Size(100, 40),
                Dock = DockStyle.Right,
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.FromArgb(47, 39, 206),
                BorderSize = 1
            };

            btnSaveEdit = new()
            {
                Name = "btnSaveEdit",
                Text = "Save Changes",
                Size = new Size(100, 40),
                Dock = DockStyle.Right,
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.FromArgb(47, 39, 206),
                BorderSize = 1
            };

            btnDelete = new()
            {
                Name = "btnDelete",
                Text = "Delete",
                Size = new Size(80, 40),
                Dock = DockStyle.Left,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.White,// Color.FromArgb(47, 39, 206),
                MouseEnterColor = Color.IndianRed,
                MouseLeaveColor = Color.White,
                BorderSize = 1
            };

            btnCancel = new()
            {
                Name = "btnCancel",
                Text = "Cancel",
                Size = new Size(70, 40),
                Dock = DockStyle.Right,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                BorderRadius = 7,
                BorderColor = Color.White,// Color.FromArgb(47, 39, 206),
                MouseEnterColor = Color.FromArgb(229, 228, 226),
                MouseLeaveColor = Color.White,
                BorderSize = 1,
                TextOnHoverColor = Color.Black,
                TextOnLeaveColor = Color.Black,
                
            };
            btnCancel.Cursor = Cursors.Hand;
            btnDelete.Cursor = Cursors.Hand;
            btnSaveEdit.Cursor = Cursors.Hand;
            btnAddNew.Cursor = Cursors.Hand;
        }
        public void PopulateCategoryDetails(List<CategoryModel> categories)
        {
            // thread safety
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => PopulateCategoryDetails(categories)));
                return;
            }

            selectCategory.AddItemToDropdown(
                    options: categories,
                    displayMember: "Category",
                    valueMember: "CategoryId",
                    customBtnDisplayText: null,
                    panelToShow: null
            );
        }
        public void WireEvents()
        {
            btnAddNew.Click += (s, e) => AddNewCategory?.Invoke(this, EventArgs.Empty);
            btnSaveEdit.Click += (s, e) => SaveEditChanges?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => DeleteCategory?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (s, e) => CancelChanges?.Invoke(this, EventArgs.Empty);

            btnAddOption.Click += (s, e) => AddOption?.Invoke(this, EventArgs.Empty);
            btnEditOption.Click += (s, e) => EditOption?.Invoke(this, EventArgs.Empty);

            btn_Close.Click += (s, e) => CloseManageCategory();

            selectCategory.SelectedChanged += (s, e) =>
            {
                if (!e.IsSelectedChangeByUser) return;

                // auto-populate edit field when category is selected
                if (selectCategory.Selected is CategoryModel selected)
                    editedCategory.Input = selected.Category;
            };
        }

        public void OnCategoryAdded()
        {
            CategoryChanged?.Invoke(this, EventArgs.Empty);
        }

        CustomTextBox2 addNewCategory, selectCategory, editedCategory;
        CustomButton btnAddNew, btnSaveEdit, btnDelete, btnCancel;

        public string NewCategoryName => addNewCategory.Input;
        public string EditedCategoryName => editedCategory.Input;
        public CategoryModel? SelectedCategory
        {
   
            get => selectCategory.Selected as CategoryModel;
            set
            {
                selectCategory.SelectedId = (value == null) ? 0 : value.CategoryId;
            }
        }

        public event EventHandler AddOption;
        public event EventHandler EditOption;
        public event EventHandler AddNewCategory;
        public event EventHandler DeleteCategory;
        public event EventHandler SaveEditChanges;
        public event EventHandler CancelChanges;
        public event EventHandler? CategoryChanged;

        private ManageCategoryPresenter _presenter;
        private InventoryForm _inventoryForm;
    }
}
