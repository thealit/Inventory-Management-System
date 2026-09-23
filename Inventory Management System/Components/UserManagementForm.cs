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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class UserManagementForm : Form, IUserManagementView
    {
        private readonly InventoryForm _mainInventoryForm;
        private UserManagementPresenter _presenter;
        private EditUser _editUserControl;  

        public UserManagementForm(InventoryForm mainInventoryForm)
        {
            InitializeComponent();
            _mainInventoryForm = mainInventoryForm;

            this.DoubleBuffered = true;
            this.Dock = DockStyle.Fill;

            DataGridViewSetUp();
            SetUpSearchBar();

            cBtn_DeleteOneOrMany.Click += (s, e) => DeleteUserClicked?.Invoke(this, EventArgs.Empty);
            cBtn_CheckBoxTop.Click += (s, e) => CheckBoxTopClicked?.Invoke(this, EventArgs.Empty);
        }

   

        public void PassDataToEditUserControl(AppUsers user)
        {
            if (user == null) return;
            if (_editUserControl == null)
            {
                _editUserControl = new EditUser(_mainInventoryForm, this);
                _editUserControl.Visible = false;
                _mainInventoryForm.Controls.Add(_editUserControl);
            }
           
            _editUserControl.PassUserDataToEditUserRole(user);

            CenterControlOnLoad.ShowCentered(_editUserControl);
            _editUserControl.Visible = true;
        }

        public async Task SetPresenter(UserManagementPresenter presenter)
        {
            _presenter = presenter;
            await _presenter.Initialize();
        }

        public void ShowMessage(string message, string errorHeader, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            MessageBox.Show(message, errorHeader, btn, icon);
        }
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

        public void RefreshUsersTable(List<AppUsers> users) 
        { 
            dgv_usersTable.DataSource = null;
            dgv_usersTable.DataSource = users;
        }
        public async Task RefreshTableAfterEdit()
        {
            await _presenter.LoadUsersTableAsync();
        }

        public void DataGridViewSetUp()
        {
            dgv_usersTable = new CustomDataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Name = "dgv_usersTable"
            };


            dgv_usersTable.AutoGenerateColumns = false;


            dgv_usersTable.Columns.AddRange(
            [
                CreateCheckBoxColumn("IsSelected", "Select", "Select", 0),
                CreateDataGridColumn("FullName", "Full Name", "FullName", 1),
                CreateDataGridColumn("UserName", "Username", "UserName", 2),
                CreateDataGridColumn("IsAdmin", "Role", "IsAdmin", 3),
                CreateDataGridColumn("DateAdded", "Date Added", "DateAdded", 4),
                CreateDataGridColumn("LastActive", "Last Active", "LastActive", 5)
            ]);
            AddActionColumns();


            dgv_usersTable.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                string colName = dgv_usersTable.Columns[e.ColumnIndex].Name;

                if (dgv_usersTable.Columns[e.ColumnIndex].Name == "IsAdmin" && e.Value is bool isAdmin)
                {
                    e.Value = isAdmin ? "Administrator" : "Staff";
                    e.FormattingApplied = true;
                }
            };

            dgv_usersTable.CellPainting += (s, e) =>
            {
                // ignore headers
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                string colName = dgv_usersTable.Columns[e.ColumnIndex].Name;

                if (colName == "EditAction" || colName == "DeleteAction")
                {
                    // let WinForms draw the background and borders first
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                    // set colors 
                    Color normalColor = (colName == "EditAction") ? Color.FromArgb(0, 71, 171) : Color.FromArgb(210, 4, 45);
                    Color hoverColor = (colName == "EditAction") ? Color.FromArgb(0, 150, 255) : Color.FromArgb(220, 53, 69);

                    // determine current state (Hovered vs Selected)
                    Point cursorPosition = dgv_usersTable.PointToClient(Cursor.Position);
                    bool isHovered = e.CellBounds.Contains(cursorPosition);
                    bool isSelected = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;

                    // Apply logic: Priority to Hover, then Selection, then Normal
                    Color finalBackColor = normalColor;
                    if (isHovered) finalBackColor = hoverColor;
                    else if (isSelected) finalBackColor = hoverColor; 

                    // shrink drawing area slightly, prevents painting over the gridlines
                    Rectangle buttonArea = e.CellBounds;
                    buttonArea.Inflate(0,0);

                    // draw custom button background
                    using (SolidBrush brush = new SolidBrush(finalBackColor))
                    {
                        e.Graphics.FillRectangle(brush, buttonArea);
                    }

                    // draw text inside the button
                    string buttonText = (colName == "EditAction") ? "Edit" : "Delete";
                    TextRenderer.DrawText(
                        e.Graphics,
                        buttonText,
                        e.CellStyle.Font,
                        buttonArea,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );

                    // prevents drawing the grey default button
                    e.Handled = true;
                }
            };

            dgv_usersTable.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                string colName = dgv_usersTable.Columns[e.ColumnIndex].Name;

                if (colName == "EditAction" || colName == "DeleteAction")
                {
                    dgv_usersTable.Cursor = Cursors.Hand;
                    dgv_usersTable.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
               
            };

            dgv_usersTable.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                string colName = dgv_usersTable.Columns[e.ColumnIndex].Name;

                if (colName == "EditAction" || colName == "DeleteAction")
                {
                    dgv_usersTable.Cursor = Cursors.Default;
                    dgv_usersTable.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }

            };

           

            dgv_usersTable.DataError += (s, e) =>
            {
                // Suppress the default error dialog
                e.Cancel = true;
                ShowMessage("Failed to load users table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            dgv_usersTable.CurrentCellDirtyStateChanged += Dgv_usersTable_CurrentCellDirtyStateChanged;
            dgv_usersTable.CellValueChanged += Dgv_usersTable_CellValueChanged;
            dgv_usersTable.SelectionChanged += (s, e) =>
            {
                dgv_usersTable.Invalidate();
                SelectionChanged?.Invoke(this, e);
            };
            dgv_usersTable.ClientSizeChanged += (s, e) => dgv_usersTable.Refresh();
            dgv_usersTable.Columns["DateAdded"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt";
            dgv_usersTable.Columns["LastActive"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt";


           

            dgv_usersTable.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return; // ignore header clicks

                if (dgv_usersTable.Columns[e.ColumnIndex].Name == "EditAction")
                {
                    EditUserClicked?.Invoke(this, EventArgs.Empty);
                }
                else if (dgv_usersTable.Columns[e.ColumnIndex].Name == "DeleteAction")
                {
                    DeleteUserClicked?.Invoke(this, EventArgs.Empty);
                }
            };

         
                StyleModernGrid(dgv_usersTable);
                dgvPanel.Controls.Add(dgv_usersTable);
                dgv_usersTable.Dock = DockStyle.Fill;
                dgv_usersTable.BringToFront();
            
        }

        private void Dgv_usersTable_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            // Check if the cell is dirty or changed AND if it is specifically a CheckBox cell
            if (dgv_usersTable.IsCurrentCellDirty && dgv_usersTable.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Force the check/uncheck to save to your data source immediately
                dgv_usersTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Dgv_usersTable_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_usersTable.Columns[e.ColumnIndex].Name == "IsSelected")
                SelectionChanged?.Invoke(this, EventArgs.Empty);
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

        private void AddActionColumns()
        {
            var editCol = new DataGridViewButtonColumn
            {
                Name = "EditAction",
                HeaderText = "Actions",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                DisplayIndex = 6,
                MinimumWidth = 30,
                FlatStyle = FlatStyle.Flat,
                ReadOnly = false
               
            };
            editCol.DefaultCellStyle.BackColor = Color.FromArgb(0, 71, 171);
            editCol.DefaultCellStyle.ForeColor = Color.White;
            editCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            editCol.DefaultCellStyle.Padding = new Padding(4);
            editCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 255);
            editCol.DefaultCellStyle.SelectionForeColor = Color.White;
            editCol.FillWeight = 40;

            var deleteCol = new DataGridViewButtonColumn
            {
                Name = "DeleteAction",
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                DisplayIndex = 7,
                MinimumWidth = 30,
                FlatStyle = FlatStyle.Flat,
                ReadOnly = false
            };
            deleteCol.DefaultCellStyle.BackColor = Color.FromArgb(210, 4, 45);
            deleteCol.DefaultCellStyle.ForeColor = Color.White;
            deleteCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteCol.DefaultCellStyle.Padding = new Padding(4);
            deleteCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 53, 69);
            deleteCol.DefaultCellStyle.SelectionForeColor = Color.White;
            deleteCol.FillWeight = 40;

            dgv_usersTable.Columns.Add(editCol);
            dgv_usersTable.Columns.Add(deleteCol);

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
            dgv.EnableHeadersVisualStyles = false; // CRITICAL: Allows custom header colors
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
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64); // Dark gray text is softer than pure black
            dgv.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0); // Adds breathing room to text

            // 5. Alternating Row Colors (Zebra Striping)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

            // 6. Selection Styling
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selects the whole row when clicked
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 214, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 7. Row Height
            dgv.RowTemplate.Height = 35; // Taller rows look cleaner
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



        public void SetUpSearchBar()
        {
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 500; // wait 300ms after user stops typing
            _searchTimer.Tick += (s, e) =>
            {
                _searchTimer.Stop();

                this.SearchUser = searchUsers.Input.Trim();
            };

            searchUsers.InputChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start(); // reset timer on each keystroke
            };
        }

        // Single/Mass Delete Button
        public void SetTopDeleteButton(int count)
        {
            bool hasCheckedRow = count >= 1;
            string label = "Delete user";

            if (hasCheckedRow && count == 1)
            {
                //ToggleActionMode(state: 2); //show item detail's edit/ delete buttons, hide add/ cancel buttons
                cBtn_DeleteOneOrMany.Visible = true;
            }
            else if (hasCheckedRow && count > 1)
            {
                //ToggleActionMode(state: 0); // 0 hide all item details buttons
                label = $"Delete {count} users";
                cBtn_DeleteOneOrMany.Visible = true;
            }
            else { cBtn_DeleteOneOrMany.Visible = false; }

            cBtn_DeleteOneOrMany.Text = label;
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

        public void SetAllRowsCheckboxesState(bool checkAll)
        {
            foreach (DataGridViewRow row in dgv_usersTable.Rows)
            {
                if (row.Cells["IsSelected"].Value != null)
                {
                    row.Cells["IsSelected"].Value = checkAll;
                }
            }
        }





        private CustomDataGridView dgv_usersTable;
        private System.Windows.Forms.Timer _searchTimer;

        private string _searchTerm = string.Empty;
        public string SearchUser
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                SearchChanged?.Invoke(this, EventArgs.Empty);
            }
        }
       
        public List<DeleteUsersModel> DeleteUsers
        {
            get
            {
                var ids = new List<DeleteUsersModel>();
                // mass checked items for deletion
                foreach (DataGridViewRow row in dgv_usersTable.Rows)
                    if (Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        if (row.DataBoundItem is AppUsers user)
                            ids.Add(new DeleteUsersModel 
                            {   UserId = user.UserID, 
                                IsCurrentlyAdmin = user.IsAdmin }
                            );

                // single highlighted row
                if (ids.Count == 0 && dgv_usersTable.SelectedRows.Count == 1)
                    if (dgv_usersTable.SelectedRows[0].DataBoundItem is AppUsers selected)
                        ids.Add(new DeleteUsersModel 
                        {   UserId = selected.UserID, 
                            IsCurrentlyAdmin = selected.IsAdmin });
                return ids;
            }
        }

        public UpdateUserRoleModel GetUpdateUserRoleModel
        {
            get
            {
                if (dgv_usersTable.SelectedRows.Count == 0) return null;
                var user = dgv_usersTable.SelectedRows[0].DataBoundItem as AppUsers;
                return new UpdateUserRoleModel
                {
                    UserId = user?.UserID ?? -1,
                    IsCurrentlyAdmin = user?.IsAdmin ?? false
                };
            }
        }

        public int CheckedCount
        {
            get
            {
                int count = 0;
                foreach (DataGridViewRow row in dgv_usersTable.Rows)
                    if (Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        count++;
                return count;
            }
        }

        public bool AreAllRowsChecked
        {
            get
            {
                foreach (DataGridViewRow row in dgv_usersTable.Rows)
                {
                    // if any row is NOT checked
                    if (!Convert.ToBoolean(row.Cells["IsSelected"].Value))
                        return false;
                }
                // if all rows is NOT checked
                return true;
            }
        }

        public AppUsers? SelectedUser
        {
            get
            {
                if (dgv_usersTable.SelectedRows.Count == 0) return null;
                return dgv_usersTable.SelectedRows[0].DataBoundItem as AppUsers;
            }

            
        }

        public event EventHandler SearchChanged;
        public event EventHandler DeleteUserClicked;
        public event EventHandler EditUserClicked;
        public event EventHandler OnResetUserRoleClicked;
        public event EventHandler SelectionChanged;
        public event EventHandler CheckBoxTopClicked;
    }
}
