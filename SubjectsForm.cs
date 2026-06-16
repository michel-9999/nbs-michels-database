using System;
using System.Linq;
using System.Windows.Forms;

namespace csharp_michels_database
{
    public partial class SubjectsForm : Form
    {
        private DataGridView subjectsGrid = new DataGridView();
        private Button addSubjectButton = new Button();

        private MainForm? Main => MdiParent as MainForm;

        public SubjectsForm()
        {
            InitializeComponent();

            Text = "Onderwerpen";

            subjectsGrid.Dock = DockStyle.Fill;
            subjectsGrid.AllowUserToAddRows = false;
            subjectsGrid.AllowUserToDeleteRows = false;
            subjectsGrid.ReadOnly = true;
            subjectsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            subjectsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            subjectsGrid.RowHeadersVisible = false;

            subjectsGrid.Columns.Add("Id", "Id");
            subjectsGrid.Columns["Id"].Visible = false;

            subjectsGrid.Columns.Add("SubjectName", "Onderwerp");
            subjectsGrid.Columns["SubjectName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            subjectsGrid.Columns.Add("UsedCount", "Gebruikt");
            subjectsGrid.Columns["UsedCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn renameButton = new DataGridViewButtonColumn();
            renameButton.Name = "Rename";
            renameButton.HeaderText = "";
            renameButton.Text = "Hernoemen";
            renameButton.UseColumnTextForButtonValue = true;
            subjectsGrid.Columns.Add(renameButton);
            subjectsGrid.Columns["Rename"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "";
            deleteButton.Text = "Verwijderen";
            deleteButton.UseColumnTextForButtonValue = true;
            subjectsGrid.Columns.Add(deleteButton);
            subjectsGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            subjectsGrid.CellContentClick += subjectsGrid_CellContentClick;

            addSubjectButton.Text = "Onderwerp toevoegen";
            addSubjectButton.Dock = DockStyle.Bottom;
            addSubjectButton.Height = 40;
            addSubjectButton.Click += addSubjectButton_Click;

            Controls.Add(subjectsGrid);
            Controls.Add(addSubjectButton);

            Load += SubjectsForm_Load;
        }

        private void SubjectsForm_Load(object? sender, EventArgs e)
        {
            if (Main != null)
                Main.RefreshRequested += Main_RefreshRequested;

            RefreshSubjectGrid();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (Main != null)
                Main.RefreshRequested -= Main_RefreshRequested;

            base.OnFormClosed(e);
        }

        private void Main_RefreshRequested(object? sender, EventArgs e)
        {
            RefreshSubjectGrid();
        }

        private void RefreshSubjectGrid()
        {
            if (Main == null)
                return;

            subjectsGrid.Rows.Clear();

            foreach (ContentSubject subject in Main.AppDatabase.Subjects.OrderBy(s => s.SubjectName))
            {
                int usedCount = CountSubjectUsage(subject.Id);

                subjectsGrid.Rows.Add(
                    subject.Id.ToString(),
                    subject.SubjectName,
                    usedCount
                );
            }
        }

        private int CountSubjectUsage(Guid subjectId)
        {
            if (Main == null)
                return 0;

            int count = 0;

            foreach (CollectionEntry entry in Main.AppDatabase.Entries)
            {
                foreach (EntryContent content in entry.Contents)
                {
                    content.SubjectIds ??= [];

                    if (content.SubjectIds.Contains(subjectId))
                        count++;
                }
            }

            return count;
        }

        private void subjectsGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (Main == null)
                return;

            if (e.RowIndex < 0)
                return;

            string? idText = subjectsGrid.Rows[e.RowIndex].Cells["Id"].Value?.ToString();

            if (!Guid.TryParse(idText, out Guid subjectId))
                return;

            ContentSubject? subject = Main.AppDatabase.Subjects.FirstOrDefault(s => s.Id == subjectId);

            if (subject == null)
                return;

            string columnName = subjectsGrid.Columns[e.ColumnIndex].Name;

            if (columnName == "Rename")
            {
                RenameSubject(subject);
            }
            else if (columnName == "Delete")
            {
                DeleteSubject(subject);
            }
        }

        private void addSubjectButton_Click(object? sender, EventArgs e)
        {
            if (Main == null)
                return;

            if (!InputDialogBox.Show("Onderwerp toevoegen", "Naam van nieuw onderwerp:", out string subjectName))
                return;

            subjectName = subjectName.Trim();

            if (subjectName == "")
            {
                MessageBox.Show("Onderwerp naam mag niet leeg zijn!");
                return;
            }

            bool alreadyExists = Main.AppDatabase.Subjects.Any(s =>
                string.Equals(s.SubjectName, subjectName, StringComparison.OrdinalIgnoreCase)
            );

            if (alreadyExists)
            {
                MessageBox.Show(subjectName + " bestaat al als een onderwerp!");
                return;
            }

            ContentSubject newSubject = new ContentSubject
            {
                Id = Guid.NewGuid(),
                SubjectName = subjectName
            };

            Main.AppDatabase.Subjects.Add(newSubject);

            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshSubjectGrid();
        }

        private void RenameSubject(ContentSubject subject)
        {
            if (Main == null)
                return;

            if (!InputDialogBox.Show("Onderwerp hernoemen", "Nieuwe naam:", out string newName, subject.SubjectName))
                return;

            newName = newName.Trim();

            if (newName == "")
            {
                MessageBox.Show("Onderwerp naam mag niet leeg zijn!");
                return;
            }

            bool alreadyExists = Main.AppDatabase.Subjects.Any(s =>
                s.Id != subject.Id &&
                string.Equals(s.SubjectName, newName, StringComparison.OrdinalIgnoreCase)
            );

            if (alreadyExists)
            {
                MessageBox.Show(newName + " bestaat al als een onderwerp!");
                return;
            }

            subject.SubjectName = newName;
            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshSubjectGrid();
        }

        private void DeleteSubject(ContentSubject subject)
        {
            if (Main == null)
                return;

            int usedCount = CountSubjectUsage(subject.Id);

            DialogResult result = MessageBox.Show(
                $"Wil je '{subject.SubjectName}' verwijderen?\n\nDit onderwerp wordt ook verwijderd uit {usedCount} content item(s).",
                "Onderwerp verwijderen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            Main.AppDatabase.Subjects.Remove(subject);

            foreach (CollectionEntry entry in Main.AppDatabase.Entries)
            {
                foreach (EntryContent content in entry.Contents)
                {
                    content.SubjectIds ??= [];
                    content.SubjectIds.RemoveAll(id => id == subject.Id);
                }
            }

            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshSubjectGrid();
        }
    }
}
