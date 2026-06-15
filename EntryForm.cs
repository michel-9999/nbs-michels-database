using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace csharp_michels_database
{
    public partial class EntryForm : Form
    {
        private bool modified = false;

        private string draftCoverPath = "";
        private string draftTOCPath = "";
        private string draftPDFPath = "";
        private List<EntryContent> draftContents = [];

        private MainForm? Main => MdiParent as MainForm;

        public CollectionEntry Entry = new CollectionEntry();

        public EntryForm()
        {
            InitializeComponent();

            coverPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            TOCPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            coverPictureBox.DoubleClick += coverPictureBox_DoubleClick;
            TOCPictureBox.DoubleClick += TOCPictureBox_DoubleClick;

            PDFLinkLabel.Click += PDFLinkLabel_Click;
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            LoadEntryIntoForm();
        }

        private void LoadEntryIntoForm()
        {
            draftCoverPath = Entry.Cover;
            draftTOCPath = Entry.TOC;
            draftPDFPath = Entry.PDF;

            draftContents = Entry.Contents
                .Select(CloneEntryContent)
                .ToList();

            GUIDtextBox.Text = Entry.Id.ToString();
            titelTextBox.Text = Entry.Title;
            PDFLinkLabel.Text = draftPDFPath;

            LoadImageIntoPictureBox(coverPictureBox, draftCoverPath, showErrors: false);
            LoadImageIntoPictureBox(TOCPictureBox, draftTOCPath, showErrors: false);

            SetupContentsGrid();
            RefreshContentsGrid();

            Text = string.IsNullOrWhiteSpace(Entry.Title) ? "Nieuw item" : Entry.Title;

            modified = false;
        }

        private void SaveFormIntoEntry()
        {
            Entry.Title = titelTextBox.Text.Trim();

            Entry.Cover = draftCoverPath;
            Entry.TOC = draftTOCPath;
            Entry.PDF = draftPDFPath;

            Entry.Contents = draftContents
                .Select(CloneEntryContent)
                .ToList();
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
                SubCategoryIds = source.SubCategoryIds.ToList()
            };
        }

        private void SetupContentsGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdColumn",
                HeaderText = "Id",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TitleColumn",
                HeaderText = "Titel",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PageStartColumn",
                HeaderText = "Startpagina",
                Width = 110
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PageEndColumn",
                HeaderText = "Eindpagina",
                Width = 110
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MainCategoryColumn",
                HeaderText = "Hoofdcategorie",
                Width = 180
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "EditColumn",
                HeaderText = "",
                Text = "Bewerken",
                UseColumnTextForButtonValue = true,
                Width = 110
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "DeleteColumn",
                HeaderText = "",
                Text = "Verwijderen",
                UseColumnTextForButtonValue = true,
                Width = 110
            });

            dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void RefreshContentsGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (EntryContent content in draftContents)
            {
                string mainCategoryName = Main?.AppDatabase.GetCategoryName(content.MainCategoryId) ?? "";

                dataGridView1.Rows.Add(
                    content.Id,
                    content.Title,
                    content.PageStart,
                    content.PageEnd,
                    mainCategoryName
                );
            }
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName != "EditColumn" && columnName != "DeleteColumn")
                return;

            Guid contentId = (Guid)dataGridView1.Rows[e.RowIndex].Cells["IdColumn"].Value;

            EntryContent? content = draftContents.FirstOrDefault(c => c.Id == contentId);

            if (content == null)
                return;

            if (columnName == "EditColumn")
            {
                OpenEntryContentForm(content);
                return;
            }

            if (columnName == "DeleteColumn")
            {
                DeleteEntryContent(content);
                return;
            }
        }

        private void DeleteEntryContent(EntryContent content)
        {
            DialogResult result = MessageBox.Show(
                $"Wil je '{content.Title}' verwijderen?",
                "Inhoud verwijderen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            // If the edit form for this content is already open, close it first.
            // Otherwise it could later save the deleted item back into draftContents.
            if (Main != null)
            {
                foreach (Form child in Main.MdiChildren)
                {
                    if (child is EntryContentForm existingForm &&
                        existingForm.ParentEntryId == Entry.Id &&
                        existingForm.Content.Id == content.Id)
                    {
                        existingForm.Close();

                        if (!existingForm.IsDisposed)
                            return;

                        break;
                    }
                }
            }

            draftContents.RemoveAll(c => c.Id == content.Id);

            RefreshContentsGrid();

            modified = true;
        }

        private void SelectTabIfExists(int tabIndex)
        {
            if (tabControl1.TabPages.Count > tabIndex)
            {
                tabControl1.SelectedIndex = tabIndex;
            }
        }

        private void titelTextBox_TextChanged(object sender, EventArgs e)
        {
            Text = string.IsNullOrWhiteSpace(titelTextBox.Text) ? "Nieuw item" : titelTextBox.Text.Trim();
            modified = true;
        }

        private void opslaanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save();

            MessageBox.Show(
                "Item opgeslagen.",
                "Opslaan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (modified)
            {
                DialogResult result = MessageBox.Show(
                    "Er zijn niet-opgeslagen wijzigingen. Wil je deze opslaan voordat je sluit?",
                    "Wijzigingen opslaan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    save();
                }
            }

            base.OnFormClosing(e);
        }

        private void save()
        {
            SaveFormIntoEntry();

            Main?.SaveDatabase(Entry);
            Main?.BroadcastRefresh();

            modified = false;
        }

        private void sluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Wil je '{Entry.Title}' verwijderen?",
                "Item verwijderen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            Main?.RemoveItem(Entry);
            modified = false;
            Close();
        }

        private void coverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectImage(
                title: "Selecteer cover afbeelding",
                currentPath: draftCoverPath,
                onSelected: selectedPath =>
                {
                    draftCoverPath = selectedPath;

                    LoadImageIntoPictureBox(coverPictureBox, draftCoverPath, showErrors: true);
                    SelectTabIfExists(0);

                    modified = true;
                }
            );
        }

        private void inhoudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectImage(
                title: "Selecteer inhoud afbeelding",
                currentPath: draftTOCPath,
                onSelected: selectedPath =>
                {
                    draftTOCPath = selectedPath;

                    LoadImageIntoPictureBox(TOCPictureBox, draftTOCPath, showErrors: true);
                    SelectTabIfExists(1);

                    modified = true;
                }
            );
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectPDF(
                title: "Selecteer PDF",
                currentPath: draftPDFPath,
                onSelected: selectedPath =>
                {
                    draftPDFPath = selectedPath;
                    PDFLinkLabel.Text = draftPDFPath;

                    modified = true;
                }
            );
        }

        private void coverPictureBox_DoubleClick(object? sender, EventArgs e)
        {
            OpenImageIfValid(draftCoverPath);
        }

        private void TOCPictureBox_DoubleClick(object? sender, EventArgs e)
        {
            OpenImageIfValid(draftTOCPath);
        }

        private void PDFLinkLabel_Click(object? sender, EventArgs e)
        {
            OpenPDFIfValid(draftPDFPath);
        }

        private void OpenImageIfValid(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            if (!File.Exists(imagePath))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = imagePath,
                UseShellExecute = true
            });
        }

        private void OpenPDFIfValid(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            {
                MessageBox.Show(
                    "Het PDF-pad is niet geldig of het bestand bestaat niet.",
                    "PDF openen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = pdfPath,
                UseShellExecute = true
            });
        }

        private void SelectImage(string title, string currentPath, Action<string> onSelected)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp;*.tif;*.tiff|Alle bestanden|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            SetInitialDirectoryAndFileName(openFileDialog, currentPath);

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            onSelected(openFileDialog.FileName);
        }

        private void SelectPDF(string title, string currentPath, Action<string> onSelected)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "PDF-bestanden|*.pdf|Alle bestanden|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            SetInitialDirectoryAndFileName(openFileDialog, currentPath);

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            onSelected(openFileDialog.FileName);
        }

        private void SetInitialDirectoryAndFileName(OpenFileDialog openFileDialog, string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath) || !File.Exists(currentPath))
                return;

            string? directory = Path.GetDirectoryName(currentPath);

            if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
            {
                openFileDialog.InitialDirectory = directory;
                openFileDialog.FileName = Path.GetFileName(currentPath);
            }
        }

        private void LoadImageIntoPictureBox(PictureBox pictureBox, string imagePath, bool showErrors)
        {
            ClearPictureBoxImage(pictureBox);

            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            if (!File.Exists(imagePath))
                return;

            try
            {
                using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                using Image loadedImage = Image.FromStream(stream);

                pictureBox.Image = new Bitmap(loadedImage);
            }
            catch (Exception ex)
            {
                ClearPictureBoxImage(pictureBox);

                if (showErrors)
                {
                    MessageBox.Show(
                        $"Afbeelding kon niet geladen worden:\n\n{ex.Message}",
                        "Afbeelding laden",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void ClearPictureBoxImage(PictureBox pictureBox)
        {
            if (pictureBox.Image == null)
                return;

            Image oldImage = pictureBox.Image;
            pictureBox.Image = null;
            oldImage.Dispose();
        }

        private void addContentButton_Click(object sender, EventArgs e)
        {
            EntryContent newContent = new EntryContent();

            OpenEntryContentForm(newContent);
        }

        private void OpenEntryContentForm(EntryContent content)
        {
            if (Main == null)
                return;

            foreach (Form child in Main.MdiChildren)
            {
                if (child is EntryContentForm existingForm &&
                    existingForm.ParentEntryId == Entry.Id &&
                    existingForm.Content.Id == content.Id)
                {
                    if (existingForm.WindowState == FormWindowState.Minimized)
                    {
                        existingForm.WindowState = FormWindowState.Normal;
                    }

                    existingForm.Activate();
                    return;
                }
            }

            EntryContentForm form = new EntryContentForm
            {
                MdiParent = Main,
                ParentEntryId = Entry.Id,
                Content = CloneEntryContent(content),
                Categories = Main.AppDatabase.Categories.ToList()
            };

            form.ContentSaved += EntryContentForm_ContentSaved;

            form.Show();
        }

        private void EntryContentForm_ContentSaved(EntryContent savedContent)
        {
            EntryContent clonedContent = CloneEntryContent(savedContent);

            int existingIndex = draftContents.FindIndex(c => c.Id == clonedContent.Id);

            if (existingIndex >= 0)
            {
                draftContents[existingIndex] = clonedContent;
            }
            else
            {
                draftContents.Add(clonedContent);
            }

            draftContents = draftContents
                .OrderBy(c => c.PageStart)
                .ThenBy(c => c.PageEnd)
                .ThenBy(c => c.Title)
                .ToList();

            RefreshContentsGrid();

            modified = true;
        }
    }
}