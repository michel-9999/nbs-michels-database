using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace csharp_michels_database
{
    public partial class EntryContentForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid ParentEntryId { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EntryContent Content { get; set; } = new EntryContent();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ContentCategory> Categories { get; set; } = [];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ContentSubject> Subjects { get; set; } = [];

        private MainForm? Main => MdiParent as MainForm;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Action<EntryContent>? ContentSaved;

        private TextBox titleTextBox = new TextBox();
        private NumericUpDown pageStartNumericUpDown = new NumericUpDown();
        private NumericUpDown pageEndNumericUpDown = new NumericUpDown();
        private ComboBox mainCategoryComboBox = new ComboBox();
        private CheckedListBox subjectsCheckedListBox = new CheckedListBox();
        private Button addSubjectButton = new Button();
        private Button saveButton = new Button();
        private Button closeButton = new Button();

        private bool modified = false;

        public EntryContentForm()
        {
            InitializeComponent();
            BuildInterface();
        }

        private void EntryContentForm_Load(object sender, EventArgs e)
        {
            LoadContentIntoForm();
        }

        private void BuildInterface()
        {
            Text = "Inhoud bewerken";
            Width = 800;
            Height = 600;

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(12)
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            Label titleLabel = new Label
            {
                Text = "Titel",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            titleTextBox.Dock = DockStyle.Fill;
            titleTextBox.TextChanged += MarkModified;

            Label pageStartLabel = new Label
            {
                Text = "Startpagina",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            pageStartNumericUpDown.Dock = DockStyle.Left;
            pageStartNumericUpDown.Minimum = 0;
            pageStartNumericUpDown.Maximum = 100000;
            pageStartNumericUpDown.Width = 120;
            pageStartNumericUpDown.ValueChanged += MarkModified;

            Label pageEndLabel = new Label
            {
                Text = "Eindpagina",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            pageEndNumericUpDown.Dock = DockStyle.Left;
            pageEndNumericUpDown.Minimum = 0;
            pageEndNumericUpDown.Maximum = 100000;
            pageEndNumericUpDown.Width = 120;
            pageEndNumericUpDown.ValueChanged += MarkModified;

            Label mainCategoryLabel = new Label
            {
                Text = "Hoofdcategorie",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            mainCategoryComboBox.Dock = DockStyle.Fill;
            mainCategoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mainCategoryComboBox.SelectedIndexChanged += mainCategoryComboBox_SelectedIndexChanged;

            Label subjectsLabel = new Label
            {
                Text = "Onderwerpen",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.TopLeft
            };

            subjectsCheckedListBox.Dock = DockStyle.Fill;
            subjectsCheckedListBox.CheckOnClick = true;
            subjectsCheckedListBox.IntegralHeight = false;
            subjectsCheckedListBox.ItemCheck += subjectsCheckedListBox_ItemCheck;

            addSubjectButton.Text = "+";
            addSubjectButton.Width = 40;
            addSubjectButton.Height = 32;
            addSubjectButton.Dock = DockStyle.Top;
            addSubjectButton.Click += addSubjectButton_Click;

            TableLayoutPanel subjectsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0)
            };

            subjectsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            subjectsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46));
            subjectsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            subjectsPanel.Controls.Add(subjectsCheckedListBox, 0, 0);
            subjectsPanel.Controls.Add(addSubjectButton, 1, 0);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            saveButton.Text = "Opslaan";
            saveButton.Width = 110;
            saveButton.Height = 40;
            saveButton.Click += saveButton_Click;

            closeButton.Text = "Sluiten";
            closeButton.Width = 110;
            closeButton.Height = 40;
            closeButton.Click += closeButton_Click;

            buttonPanel.Controls.Add(saveButton);
            buttonPanel.Controls.Add(closeButton);

            table.Controls.Add(titleLabel, 0, 0);
            table.Controls.Add(titleTextBox, 1, 0);

            table.Controls.Add(pageStartLabel, 0, 1);
            table.Controls.Add(pageStartNumericUpDown, 1, 1);

            table.Controls.Add(pageEndLabel, 0, 2);
            table.Controls.Add(pageEndNumericUpDown, 1, 2);

            table.Controls.Add(mainCategoryLabel, 0, 3);
            table.Controls.Add(mainCategoryComboBox, 1, 3);

            table.Controls.Add(subjectsLabel, 0, 4);
            table.Controls.Add(subjectsPanel, 1, 4);

            table.Controls.Add(buttonPanel, 1, 5);

            Controls.Add(table);
        }

        private void LoadContentIntoForm()
        {
            Content.SubjectIds ??= [];

            Text = string.IsNullOrWhiteSpace(Content.Title)
                ? "Nieuwe inhoud"
                : $"Inhoud bewerken - {Content.Title}";

            titleTextBox.Text = Content.Title;
            pageStartNumericUpDown.Value = ClampToNumericUpDown(Content.PageStart, pageStartNumericUpDown);
            pageEndNumericUpDown.Value = ClampToNumericUpDown(Content.PageEnd, pageEndNumericUpDown);

            LoadCategoriesAndSubjectsIntoControls();

            modified = false;
        }

        private decimal ClampToNumericUpDown(int value, NumericUpDown numericUpDown)
        {
            if (value < numericUpDown.Minimum)
                return numericUpDown.Minimum;

            if (value > numericUpDown.Maximum)
                return numericUpDown.Maximum;

            return value;
        }

        private void LoadCategoriesAndSubjectsIntoControls()
        {
            mainCategoryComboBox.Items.Clear();
            subjectsCheckedListBox.Items.Clear();

            mainCategoryComboBox.Items.Add(new CategoryComboItem(null, "(geen categorie)"));

            List<ContentCategory> orderedCategories = Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            foreach (ContentCategory category in orderedCategories)
            {
                mainCategoryComboBox.Items.Add(new CategoryComboItem(category.Id, category.CategoryName));
            }

            for (int i = 0; i < mainCategoryComboBox.Items.Count; i++)
            {
                if (mainCategoryComboBox.Items[i] is CategoryComboItem item &&
                    item.Id == Content.MainCategoryId)
                {
                    mainCategoryComboBox.SelectedIndex = i;
                    break;
                }
            }

            if (mainCategoryComboBox.SelectedIndex < 0)
                mainCategoryComboBox.SelectedIndex = 0;

            List<ContentSubject> orderedSubjects = Subjects
                .OrderBy(s => s.SubjectName)
                .ToList();

            foreach (ContentSubject subject in orderedSubjects)
            {
                bool isSelectedSubject = Content.SubjectIds.Contains(subject.Id);

                subjectsCheckedListBox.Items.Add(
                    new SubjectComboItem(subject.Id, subject.SubjectName),
                    isSelectedSubject
                );
            }
        }

        private void mainCategoryComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            modified = true;
        }

        private void subjectsCheckedListBox_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            modified = true;
        }

        private void addSubjectButton_Click(object? sender, EventArgs e)
        {
            if (!InputDialogBox.Show("Onderwerp toevoegen", "Naam van nieuw onderwerp:", out string subjectName))
                return;

            subjectName = subjectName.Trim();

            if (subjectName == "")
            {
                MessageBox.Show(
                    "Onderwerp naam mag niet leeg zijn!",
                    "Onderwerp toevoegen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            ContentSubject? subject = FindSubjectByName(subjectName);

            if (subject == null)
            {
                subject = new ContentSubject
                {
                    Id = Guid.NewGuid(),
                    SubjectName = subjectName
                };

                Main?.AppDatabase.Subjects.Add(subject);

                if (!Subjects.Any(s => s.Id == subject.Id))
                    Subjects.Add(subject);

                Main?.SaveDatabase();
                Main?.BroadcastRefresh();
            }
            else if (!Subjects.Any(s => s.Id == subject.Id))
            {
                Subjects.Add(subject);
            }

            AddOrCheckSubjectInList(subject);

            modified = true;
        }

        private ContentSubject? FindSubjectByName(string subjectName)
        {
            ContentSubject? localSubject = Subjects.FirstOrDefault(s =>
                string.Equals(s.SubjectName, subjectName, StringComparison.OrdinalIgnoreCase)
            );

            if (localSubject != null)
                return localSubject;

            return Main?.AppDatabase.Subjects.FirstOrDefault(s =>
                string.Equals(s.SubjectName, subjectName, StringComparison.OrdinalIgnoreCase)
            );
        }

        private void AddOrCheckSubjectInList(ContentSubject subject)
        {
            for (int i = 0; i < subjectsCheckedListBox.Items.Count; i++)
            {
                if (subjectsCheckedListBox.Items[i] is SubjectComboItem item &&
                    item.Id == subject.Id)
                {
                    subjectsCheckedListBox.SetItemChecked(i, true);
                    subjectsCheckedListBox.SelectedIndex = i;
                    return;
                }
            }

            int newIndex = subjectsCheckedListBox.Items.Add(
                new SubjectComboItem(subject.Id, subject.SubjectName),
                true
            );

            subjectsCheckedListBox.SelectedIndex = newIndex;
        }

        private Guid? GetSelectedMainCategoryId()
        {
            if (mainCategoryComboBox.SelectedItem is CategoryComboItem selectedCategory)
                return selectedCategory.Id;

            return null;
        }

        private void saveButton_Click(object? sender, EventArgs e)
        {
            if (!SaveFormIntoContent())
                return;

            ContentSaved?.Invoke(CloneEntryContent(Content));

            modified = false;
            Close();
        }

        private void closeButton_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private bool SaveFormIntoContent()
        {
            string title = titleTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show(
                    "Titel mag niet leeg zijn.",
                    "Inhoud opslaan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                titleTextBox.Focus();
                return false;
            }

            int pageStart = (int)pageStartNumericUpDown.Value;
            int pageEnd = (int)pageEndNumericUpDown.Value;

            if (pageStart > 0 && pageEnd > 0 && pageEnd < pageStart)
            {
                MessageBox.Show(
                    "Eindpagina mag niet lager zijn dan startpagina.",
                    "Inhoud opslaan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                pageEndNumericUpDown.Focus();
                return false;
            }

            Content.Title = title;
            Content.PageStart = pageStart;
            Content.PageEnd = pageEnd;
            Content.MainCategoryId = GetSelectedMainCategoryId();

            List<Guid> selectedSubjectIds = [];

            foreach (object checkedItem in subjectsCheckedListBox.CheckedItems)
            {
                if (checkedItem is SubjectComboItem subjectItem)
                {
                    selectedSubjectIds.Add(subjectItem.Id);
                }
            }

            Content.SubjectIds = selectedSubjectIds
                .Distinct()
                .ToList();

            return true;
        }

        private void MarkModified(object? sender, EventArgs e)
        {
            modified = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (modified)
            {
                DialogResult result = MessageBox.Show(
                    "Er zijn niet-opgeslagen wijzigingen. Wil je sluiten zonder op te slaan?",
                    "Wijzigingen niet opgeslagen",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        private EntryContent CloneEntryContent(EntryContent source)
        {
            return new EntryContent
            {
                Id = source.Id,
                Title = source.Title,
                PageStart = source.PageStart,
                PageEnd = source.PageEnd,
                MainCategoryId = source.MainCategoryId,
                SubjectIds = source.SubjectIds?.ToList() ?? []
            };
        }

        private class CategoryComboItem
        {
            public Guid? Id { get; }
            public string Name { get; }

            public CategoryComboItem(Guid? id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private class SubjectComboItem
        {
            public Guid Id { get; }
            public string Name { get; }

            public SubjectComboItem(Guid id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
