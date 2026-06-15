using System;
using System.Linq;
using System.Windows.Forms;

namespace csharp_michels_database
{
    public partial class CategoriesForm : Form
    {
        private DataGridView categoriesGrid = new DataGridView();
        private Button addCategoryButton = new Button();

        private MainForm? Main => MdiParent as MainForm;

        public CategoriesForm()
        {
            InitializeComponent();

            Text = "Categorieën";

            categoriesGrid.Dock = DockStyle.Fill;
            categoriesGrid.AllowUserToAddRows = false;
            categoriesGrid.AllowUserToDeleteRows = false;
            categoriesGrid.ReadOnly = true;
            categoriesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            categoriesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            categoriesGrid.RowHeadersVisible = false;

            categoriesGrid.Columns.Add("Id", "Id");
            categoriesGrid.Columns["Id"].Visible = false;

            categoriesGrid.Columns.Add("CategoryName", "Categorie");
            categoriesGrid.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            categoriesGrid.Columns.Add("UsedCount", "Gebruikt");
            categoriesGrid.Columns["UsedCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn renameButton = new DataGridViewButtonColumn();
            renameButton.Name = "Rename";
            renameButton.HeaderText = "";
            renameButton.Text = "Hernoemen";
            renameButton.UseColumnTextForButtonValue = true;
            categoriesGrid.Columns.Add(renameButton);
            categoriesGrid.Columns["Rename"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "";
            deleteButton.Text = "Verwijderen";
            deleteButton.UseColumnTextForButtonValue = true;
            categoriesGrid.Columns.Add(deleteButton);
            categoriesGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            categoriesGrid.CellContentClick += categoriesGrid_CellContentClick;

            addCategoryButton.Text = "Categorie toevoegen";
            addCategoryButton.Dock = DockStyle.Bottom;
            addCategoryButton.Height = 40;
            addCategoryButton.Click += addCategoryButton_Click;

            Controls.Add(categoriesGrid);
            Controls.Add(addCategoryButton);

            Load += CategoriesForm_Load;
        }

        private void CategoriesForm_Load(object? sender, EventArgs e)
        {
            if (Main != null)
                Main.RefreshRequested += Main_RefreshRequested;

            RefreshCategoryGrid();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (Main != null)
                Main.RefreshRequested -= Main_RefreshRequested;

            base.OnFormClosed(e);
        }
        private void Main_RefreshRequested(object? sender, EventArgs e)
        {
            RefreshCategoryGrid();
        }
        private void RefreshCategoryGrid()
        {
            if (Main == null)
                return;

            categoriesGrid.Rows.Clear();

            foreach (ContentCategory category in Main.AppDatabase.Categories.OrderBy(c => c.CategoryName))
            {
                int usedCount = CountCategoryUsage(category.Id);

                categoriesGrid.Rows.Add(
                    category.Id.ToString(),
                    category.CategoryName,
                    usedCount
                );
            }
        }

        private int CountCategoryUsage(Guid categoryId)
        {
            if (Main == null)
                return 0;

            int count = 0;

            foreach (CollectionEntry entry in Main.AppDatabase.Entries)
            {
                foreach (EntryContent content in entry.Contents)
                {
                    if (content.MainCategoryId == categoryId)
                        count++;

                    if (content.SubCategoryIds.Contains(categoryId))
                        count++;
                }
            }

            return count;
        }

        private void categoriesGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (Main == null)
                return;

            if (e.RowIndex < 0)
                return;

            string? idText = categoriesGrid.Rows[e.RowIndex].Cells["Id"].Value?.ToString();

            if (!Guid.TryParse(idText, out Guid categoryId))
                return;

            ContentCategory? category = Main.AppDatabase.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
                return;

            string columnName = categoriesGrid.Columns[e.ColumnIndex].Name;

            if (columnName == "Rename")
            {
                RenameCategory(category);
            }
            else if (columnName == "Delete")
            {
                DeleteCategory(category);
            }
        }

        private void addCategoryButton_Click(object? sender, EventArgs e)
        {
            if (Main == null)
                return;

            if (!InputDialogBox.Show("Categorie toevoegen", "Naam van nieuwe categorie:", out string categoryName))
                return;

            categoryName = categoryName.Trim();

            if (categoryName == "")
            {
                MessageBox.Show("Categorie naam mag niet leeg zijn!");
                return;
            }

            bool alreadyExists = Main.AppDatabase.Categories.Any(c =>
                string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase)
            );

            if (alreadyExists)
            {
                MessageBox.Show(categoryName + " bestaat al als een categorie!");
                return;
            }

            ContentCategory newCategory = new ContentCategory
            {
                Id = Guid.NewGuid(),
                CategoryName = categoryName
            };

            Main.AppDatabase.Categories.Add(newCategory);

            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshCategoryGrid();
        }

        private void RenameCategory(ContentCategory category)
        {
            if (Main == null)
                return;

            if (!InputDialogBox.Show("Categorie hernoemen", "Nieuwe naam:", out string newName, category.CategoryName))
                return;

            newName = newName.Trim();

            if (newName == "")
            {
                MessageBox.Show("Categorie naam mag niet leeg zijn!");
                return;
            }

            bool alreadyExists = Main.AppDatabase.Categories.Any(c =>
                c.Id != category.Id &&
                string.Equals(c.CategoryName, newName, StringComparison.OrdinalIgnoreCase)
            );

            if (alreadyExists)
            {
                MessageBox.Show(newName + " bestaat al als een categorie!");
                return;
            }

            category.CategoryName = newName;
            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshCategoryGrid();
        }

        private void DeleteCategory(ContentCategory category)
        {
            if (Main == null)
                return;

            int usedCount = CountCategoryUsage(category.Id);

            DialogResult result = MessageBox.Show(
                $"Wil je '{category.CategoryName}' verwijderen?\n\nDeze categorie wordt ook verwijderd uit {usedCount} content item(s).",
                "Categorie verwijderen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            Main.AppDatabase.Categories.Remove(category);

            foreach (CollectionEntry entry in Main.AppDatabase.Entries)
            {
                foreach (EntryContent content in entry.Contents)
                {
                    if (content.MainCategoryId == category.Id)
                        content.MainCategoryId = null;

                    content.SubCategoryIds.RemoveAll(id => id == category.Id);
                }
            }

            Main.SaveDatabase();
            Main.BroadcastRefresh();
            RefreshCategoryGrid();
        }
    }
}