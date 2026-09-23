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
    public partial class AuditTrailForm : Form, IAuditTrailView
    {
        private readonly InventoryForm _mainInventoryForm;
        private AuditTrailPresenter _auditTrailPresenter;

        public AuditTrailForm(InventoryForm mainInventoryForm)
        {
            InitializeComponent();
            _mainInventoryForm = mainInventoryForm;

            this.DoubleBuffered = true;
            this.Dock = DockStyle.Fill;

            DataGridViewSetUp();
        }


        public async Task SetPresenter(AuditTrailPresenter auditTrailPresenter)
        {
            _auditTrailPresenter = auditTrailPresenter;
            await _auditTrailPresenter.LoadInventoryLogs();
            SetUpSearchBar();
        }

        public void ShowMessage(string message, string errorHeader, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            MessageBox.Show(message, errorHeader, btn, icon);
        }

        public void LoadLogsTable(List<AuditTrailModel> logs)
        {
            dgv_logsTable.DataSource = null;
            dgv_logsTable.DataSource = logs;
        }

        public void DataGridViewSetUp()
        {
            dgv_logsTable = new CustomDataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Name = "dgv_logsTable"
            };


            dgv_logsTable.AutoGenerateColumns = false;


            dgv_logsTable.Columns.AddRange(
            [
                CreateDataGridColumn("Fullname", "Name", "Fullname", 0),
                CreateDataGridColumn("ActionDateTime", "Date and Time", "ActionDateTime", 1),
                CreateDataGridColumn("ActionDescription", "Description", "ActionDescription", 2),
            ]);


            dgv_logsTable.DataError += (s, e) =>
            {
                // Suppress the default error dialog
                //e.Cancel = true;
                ShowMessage("Failed to load logs table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };


            dgv_logsTable.ClientSizeChanged += (s, e) => dgv_logsTable.Refresh();
            dgv_logsTable.Columns["ActionDateTime"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt";

            StyleModernGrid(dgv_logsTable);
            //dgv_logsTable.RowTemplate.Height = 50;  // min row height
            dgv_logsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_logsTable.Columns["ActionDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True; // wrap text

            dgvPanel.Controls.Add(dgv_logsTable);
            dgv_logsTable.Dock = DockStyle.Fill;
            dgv_logsTable.BringToFront();
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


        public void SetUpSearchBar()
        {
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 500; // wait 300ms after user stops typing
            _searchTimer.Tick += (s, e) =>
            {
                _searchTimer.Stop();

                this.SearchLog = searchLogs.Input.Trim();
            };

            searchLogs.InputChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start(); // reset timer on each keystroke
            };
        }

        private string _searchTerm = string.Empty;
        public string SearchLog
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                SearchChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler SearchChanged;

        private System.Windows.Forms.Timer _searchTimer;
        private CustomDataGridView dgv_logsTable;
    }
}
