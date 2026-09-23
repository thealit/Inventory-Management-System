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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class ManageUnit : UserControl, IManageUnitView
    {

        private readonly Inventory _inventory;

        public ManageUnit(InventoryForm inventoryForm, Inventory inventory)
        {
            InitializeComponent();

            _inventoryForm = inventoryForm;
            _inventory = inventory;

            InstantiateFields_Buttons();
            WireEvents();
            _presenter = new ManageUnitPresenter(new ModelsData.ManageUnitRepository(), new ChangeLogRepository(),this);

            ViewAddUnit();

            this.Location = new Point(
                (_inventoryForm.ClientSize.Width - this.Width) / 2,
                (_inventoryForm.ClientSize.Height - this.Height) / 2
            );


            _inventoryForm.Controls.Add(this);
            this.BringToFront();
            this.Hide();
            this.Visible = false;
            CenterControlOnLoad.ClickOutsideDetected += HideOnClickOutside;
        }

        public async void ManageUnit_Load(object sender, EventArgs e)
        {
            await _presenter.View_RefreshUnits();
            ViewAddUnit();
        }

        private void HideOnClickOutside(object? sender, EventArgs e)
        {
            if (sender == this)
            {
                CloseManageUnit();
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


        public void CloseManageUnit()
        {
            ClearFields();
            ClearWarnings();
            selectUnit.SelectedId = 0;
            this.Hide();
        }
        public void ViewAddUnit()
        {
            ResizeBaseControl(1);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(addNewUnit);
            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnAddNew);
            btnAddNew.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();

        }
        public void ViewEditUnit()
        {
            ResizeBaseControl(2);
            fieldsPanel.Controls.Clear();
            fieldsPanel.Controls.Add(selectUnit);
            selectUnit.BringToFront();
            fieldsPanel.Controls.Add(editedUnit);
            editedUnit.BringToFront();

            bottomButtons.Controls.Clear();
            bottomButtons.Controls.Add(btnDelete);
            bottomButtons.Controls.Add(btnSaveEdit);
            btnSaveEdit.BringToFront();
            bottomButtons.Controls.Add(btnCancel);
            btnCancel.BringToFront();

        }
        public void ShowAddUnitSide() => ViewAddUnit();
        public void ShowEditUnitSide()
        {
            ViewEditUnit();
            //var units = await _presenter.GetUnitsAsync();
            //PopulateUnitDetails(units);
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
            addNewUnit.WarningLabel = string.Empty;
            selectUnit.WarningLabel = string.Empty;
            editedUnit.WarningLabel = string.Empty;
        }
        public void ClearFields()
        {
            addNewUnit.Input = string.Empty;
            selectUnit.Input = string.Empty;
            editedUnit.Input = string.Empty;
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



        public void SetNewUnitWarning(string? message) => addNewUnit.WarningLabel = message ?? string.Empty;
        public void SetChooseUnitToEditWarning(string? message) => selectUnit.WarningLabel = message ?? string.Empty;
        public void SetEditUnitWarning(string? message) => editedUnit.WarningLabel = message ?? string.Empty;

        public void InstantiateFields_Buttons()
        {
            addNewUnit = new()
            {
                Name = "addNewUnit",
                Label = "New Unit",
                Size = new Size(288, 100),
                Margin = new Padding(0,0,0,0),
                Dock = DockStyle.Top,
                TextBoxBackColor = fieldsPanel.BackColor
            };

            selectUnit = new()
            {
                Name = "selectUnit",
                Label = "Select Unit",
                IsDropDown = true,
                Size = new Size(288, 102),
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 0),
                Location = new Point(0, 0),
                TextBoxBackColor = fieldsPanel.BackColor
            };
            selectUnit.SetManageUnitControl(this);

            editedUnit = new()
            {
                Name = "editedUnit",
                Label = "New Unit Name",
                Size = new Size(288, 102),
                Margin = new Padding(0, 0, 0, 0),
                Dock = DockStyle.Top,
                TextBoxBackColor = fieldsPanel.BackColor
            };

            btnAddNew = new()
            {
                Name = "btnAddNewUnit",
                Text = "Add Unit",
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
                TextOnLeaveColor = Color.Black
            };

            btnCancel.MouseEnter += (s, e) => btnCancel.ForeColor = Color.Black;
            btnCancel.Cursor = Cursors.Hand;
            btnDelete.Cursor = Cursors.Hand;
            btnSaveEdit.Cursor = Cursors.Hand;
            btnAddNew.Cursor = Cursors.Hand;
        }
        public void PopulateUnitDetails(List<UnitModel> units)
        {
            // thread safety
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => PopulateUnitDetails(units)));
                return;
            }

            selectUnit.AddItemToDropdown(
                    options: units,
                    displayMember: "Unit",
                    valueMember: "UnitId",
                    customBtnDisplayText: null,
                    panelToShow: null
            );
        }
        public void WireEvents()
        {
            btnAddNew.Click += (s, e) => AddNewUnit?.Invoke(this, EventArgs.Empty);
            btnSaveEdit.Click += (s, e) => SaveEditChanges?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => DeleteUnit?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (s, e) => CancelChanges?.Invoke(this, EventArgs.Empty);

            btnAddOption.Click += (s, e) => AddOption?.Invoke(this, EventArgs.Empty);
            btnEditOption.Click += (s, e) => EditOption?.Invoke(this, EventArgs.Empty);

            btn_Close.Click += (s, e) => CloseManageUnit();

            selectUnit.SelectedChanged += (s, e) =>
            {
                if (!e.IsSelectedChangeByUser) return; // new

                // auto-populate edit field when category is selected
                if (selectUnit.Selected is UnitModel selected)
                    editedUnit.Input = selected.Unit;
            };
        }


        public void OnUnitAdded() // call this after successful DB insert
        {
            UnitChanged?.Invoke(this, EventArgs.Empty);
        }

        CustomTextBox2 addNewUnit, selectUnit, editedUnit;
        CustomButton btnAddNew, btnSaveEdit, btnDelete, btnCancel;

        public string NewUnitName => addNewUnit.Input;
        public string EditedUnitName => editedUnit.Input;
        public UnitModel? SelectedUnit
        {
            get => selectUnit.Selected as UnitModel;
            set
            {
                selectUnit.SelectedId = (value == null) ? 0 : value.UnitId;
            }
        }

        public event EventHandler AddOption;
        public event EventHandler EditOption;
        public event EventHandler AddNewUnit;
        public event EventHandler DeleteUnit;
        public event EventHandler SaveEditChanges;
        public event EventHandler CancelChanges;
        public event EventHandler? UnitChanged;

        private ManageUnitPresenter _presenter;
        private InventoryForm _inventoryForm;
    }
}
