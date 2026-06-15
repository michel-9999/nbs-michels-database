using System;
using System.Windows.Forms;

namespace csharp_michels_database
{
    public partial class EntriesForm : Form
    {
        private MainForm? Main => MdiParent as MainForm;

        private readonly DataGridView entriesDataGridView = new DataGridView();

        public EntriesForm()
        {
            InitializeComponent();

            SetupGrid();
        }

        private void EntriesForm_Load(object sender, EventArgs e)
        {
            if (Main != null)
            {
                Main.RefreshRequested += Main_RefreshRequested;
            }

            RefreshEntriesList();
        }

        private void Main_RefreshRequested(object? sender, EventArgs e)
        {
            RefreshEntriesList();
        }

        private void SetupGrid()
        {
            entriesDataGridView.Dock = DockStyle.Fill;
            entriesDataGridView.AllowUserToAddRows = false;
            entriesDataGridView.AllowUserToDeleteRows = false;
            entriesDataGridView.ReadOnly = true;
            entriesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            entriesDataGridView.MultiSelect = false;
            entriesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            entriesDataGridView.RowHeadersVisible = false;

            entriesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdColumn",
                HeaderText = "Id",
                Visible = false
            });

            entriesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TitleColumn",
                HeaderText = "Titel",
                FillWeight = 80
            });
            entriesDataGridView.Columns["TitleColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            entriesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "OpenColumn",
                HeaderText = "",
                Text = "Openen",
                UseColumnTextForButtonValue = true,
                FillWeight = 20
            });
            entriesDataGridView.Columns["OpenColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            entriesDataGridView.CellContentClick += entriesDataGridView_CellContentClick;

            Controls.Add(entriesDataGridView);
        }

        public void RefreshEntriesList()
        {
            entriesDataGridView.Rows.Clear();

            if (Main == null)
                return;

            foreach (CollectionEntry entry in Main.AppDatabase.Entries)
            {
                entriesDataGridView.Rows.Add(
                    entry.Id.ToString(),
                    string.IsNullOrWhiteSpace(entry.Title) ? "(Geen titel)" : entry.Title
                );
            }
        }

        private void entriesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (entriesDataGridView.Columns[e.ColumnIndex].Name != "OpenColumn")
                return;

            string? idText = entriesDataGridView.Rows[e.RowIndex].Cells["IdColumn"].Value?.ToString();

            if (!Guid.TryParse(idText, out Guid entryId))
                return;

            Main?.OpenEntryForm(entryId);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (Main != null)
            {
                Main.RefreshRequested -= Main_RefreshRequested;
            }

            base.OnFormClosed(e);
        }
    }
}