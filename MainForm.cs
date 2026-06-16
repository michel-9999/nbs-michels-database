using System.Diagnostics;
using System.Reflection;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace csharp_michels_database
{
    public partial class MainForm : Form
    {
        public event EventHandler? RefreshRequested;
        public Database AppDatabase = new Database();
        private const string UPDATE_JSON_URL =
           "https://raw.githubusercontent.com/michel-9999/nbs-michels-database-public/main/update.json";
        private const int MAX_BACKUPS = 120;
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        private string? _updateUrl;
        private bool _appLockInEffect = false;
        private bool _restarting = false;

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;

            update_ToolStripStatusLabel.IsLink = false;
            update_ToolStripStatusLabel.Click += update_ToolStripStatusLabel_Click;
        }
        public void BroadcastRefresh()
        {
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }
        public void OpenEntryForm(Guid entryId)
        {
            if (_appLockInEffect)
                return;

            CollectionEntry? entry = AppDatabase.Entries.FirstOrDefault(e => e.Id == entryId);

            if (entry == null)
            {
                entry = new CollectionEntry
                {
                    Id = entryId
                };
            }

            foreach (Form child in MdiChildren)
            {
                if (child is EntryForm existingEntryForm && existingEntryForm.Entry.Id == entry.Id)
                {
                    existingEntryForm.Activate();
                    return;
                }
            }

            EntryForm entryForm = new EntryForm
            {
                Entry = entry,
                MdiParent = this
            };

            entryForm.Show();
        }
        private void ShowEntriesForm()
        {
            if (_appLockInEffect)
                return;

            foreach (Form child in this.MdiChildren)
            {
                if (child is EntriesForm)
                {
                    child.Activate(); // Bring existing form to front
                    return;
                }
            }

            EntriesForm entriesForm = new EntriesForm
            {
                MdiParent = this
            };

            entriesForm.Show();
        }
        private void ShowCategoriesForm()
        {
            if (_appLockInEffect)
                return;

            foreach (Form child in this.MdiChildren)
            {
                if (child is CategoriesForm)
                {
                    child.Activate(); // Bring existing form to front
                    return;
                }
            }

            CategoriesForm categoriesForm = new CategoriesForm
            {
                MdiParent = this
            };

            categoriesForm.Show();
        }

        private void ShowSubjectsForm()
        {
            if (_appLockInEffect)
                return;

            foreach (Form child in this.MdiChildren)
            {
                if (child is SubjectsForm)
                {
                    child.Activate(); // Bring existing form to front
                    return;
                }
            }

            SubjectsForm subjectsForm = new SubjectsForm
            {
                MdiParent = this
            };

            subjectsForm.Show();
        }
        private async void MainForm_Load(object? sender, EventArgs e)
        {
            await CheckForUpdateAsync();

            if (!_appLockInEffect)
            {
                AppDatabase = LoadDatabase();
                EnsureStandardCategoriesExist();
                SaveDatabase();
                ShowEntriesForm();
            }
        }

        private void EnsureStandardCategoriesExist()
        {
            foreach (string categoryName in Standaard_Waarden.DEFAULT_CATEGORIES)
            {
                bool exists = AppDatabase.Categories.Any(c =>
                    string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase)
                );

                if (!exists)
                {
                    AppDatabase.Categories.Add(new ContentCategory
                    {
                        CategoryName = categoryName
                    });
                }
            }
        }

        public void RemoveItem(CollectionEntry entry)
        {
            if (entry == null)
                return;

            int removedCount = AppDatabase.Entries.RemoveAll(e => e.Id == entry.Id);

            if (removedCount > 0)
            {
                SaveDatabase();
                BroadcastRefresh();
            }
        }

        private async Task CheckForUpdateAsync()
        {
            try
            {
                string json = await _httpClient.GetStringAsync(UPDATE_JSON_URL);

                UpdateInfo? updateInfo = JsonSerializer.Deserialize<UpdateInfo>(json);

                if (updateInfo == null)
                {
                    ShowUpdateCheckFailed();
                    return;
                }

                Version? currentVersion = GetCurrentAppVersion();

                if (currentVersion == null)
                {
                    ShowUpdateCheckFailed();
                    return;
                }

                if (!Version.TryParse(updateInfo.LatestVersion, out Version? latestVersion))
                {
                    ShowUpdateCheckFailed();
                    return;
                }

                bool updateAvailable = latestVersion > currentVersion;

                if (updateAvailable)
                {
                    _updateUrl = updateInfo.UpdateUrl;

                    update_ToolStripStatusLabel.Text = string.IsNullOrWhiteSpace(updateInfo.UpdateText)
                        ? "Update available"
                        : updateInfo.UpdateText;

                    update_ToolStripStatusLabel.IsLink = true;
                    update_ToolStripStatusLabel.ToolTipText = updateInfo.UpdateUrl;

                    _appLockInEffect = updateInfo.AppLock;
                    SetMainFormLocked(_appLockInEffect);
                }
                else
                {
                    _appLockInEffect = false;
                    _updateUrl = null;

                    update_ToolStripStatusLabel.Text = "Up-to-date";
                    update_ToolStripStatusLabel.IsLink = false;
                    update_ToolStripStatusLabel.ToolTipText = "";

                    SetMainFormLocked(false);
                }
            }
            catch
            {
                ShowUpdateCheckFailed();
            }
        }

        private void ShowUpdateCheckFailed()
        {
            _appLockInEffect = false;
            _updateUrl = null;

            update_ToolStripStatusLabel.Text = "UPDATE CHECK FAILED";
            update_ToolStripStatusLabel.IsLink = false;
            update_ToolStripStatusLabel.ToolTipText = "";

            // Important:
            // App lock should NOT happen when the update check fails.
            SetMainFormLocked(false);
        }

        private void update_ToolStripStatusLabel_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_updateUrl))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = _updateUrl,
                UseShellExecute = true
            });
        }

        public static Version? GetCurrentAppVersion()
        {
            string? versionText = Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            if (string.IsNullOrWhiteSpace(versionText))
                return null;

            // Handles versions like "1.0.0+abcdef"
            int plusIndex = versionText.IndexOf('+');

            if (plusIndex >= 0)
                versionText = versionText[..plusIndex];

            return Version.TryParse(versionText, out Version? version)
                ? version
                : null;
        }

        private void SetMainFormLocked(bool locked)
        {
            foreach (Control control in Controls)
            {
                SetControlLocked(control, locked);
            }

            foreach (Form mdiChild in MdiChildren)
            {
                mdiChild.Enabled = !locked;
            }
        }

        private void SetControlLocked(Control control, bool locked)
        {
            // Do not disable the StatusStrip that owns update_ToolStripStatusLabel,
            // otherwise the update label itself becomes unusable.
            if (control is StatusStrip statusStrip &&
                statusStrip.Items.Contains(update_ToolStripStatusLabel))
            {
                statusStrip.Enabled = true;

                foreach (ToolStripItem item in statusStrip.Items)
                {
                    item.Enabled = !locked || item == update_ToolStripStatusLabel;
                }

                return;
            }

            // Disable ToolStrips/MenuStrips normally unless they contain the update label.
            if (control is ToolStrip toolStrip)
            {
                bool containsUpdateLabel = toolStrip.Items.Contains(update_ToolStripStatusLabel);

                if (containsUpdateLabel)
                {
                    toolStrip.Enabled = true;

                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        item.Enabled = !locked || item == update_ToolStripStatusLabel;
                    }
                }
                else
                {
                    toolStrip.Enabled = !locked;
                }

                return;
            }

            control.Enabled = !locked;

            foreach (Control child in control.Controls)
            {
                SetControlLocked(child, locked);
            }
        }

        private void downloadPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/michel-9999/nbs-michels-database-public/releases/latest");
        }

        private void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void nieuwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenEntryForm(Guid.NewGuid());
        }

        private void alleItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEntriesForm();
        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void overToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox1 aboutBox = new AboutBox1())
            {
                aboutBox.ShowDialog(this);
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCategoriesForm();
        }

        private void categorieToevoegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputDialogBox.Show("Nieuwe categorie", "Naam van nieuwe categorie:", out string categoryName))
            {
                categoryName = categoryName.Trim();

                if (categoryName == "")
                {
                    MessageBox.Show("Categorie naam mag niet leeg zijn!");
                    return;
                }

                foreach (ContentCategory c in AppDatabase.Categories)
                {
                    if (string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(categoryName + " bestaat al als een categorie!");
                        return;
                    }
                }

                AppDatabase.Categories.Add(new ContentCategory
                {
                    CategoryName = categoryName
                });
                SaveDatabase();
                BroadcastRefresh();
            }
        }
        public void SaveDatabase()
        {
            string databaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database");
            string databasePath = Path.Combine(databaseDirectory, "database.json");

            Directory.CreateDirectory(databaseDirectory);

            // Backup existing database.json before overwriting it
            if (File.Exists(databasePath))
            {
                long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string backupPath = Path.Combine(databaseDirectory, $"database_{unixTimestamp}.bak");

                File.Copy(databasePath, backupPath, overwrite: true);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(AppDatabase, options);

            File.WriteAllText(databasePath, json);

            CleanupDatabaseBackups(databaseDirectory, MAX_BACKUPS);
        }
        public void SaveDatabase(CollectionEntry entry)
        {
            for (int i = 0; i < AppDatabase.Entries.Count; i++)
            {
                if (AppDatabase.Entries[i].Id == entry.Id)
                {
                    AppDatabase.Entries[i] = entry;
                    SaveDatabase();
                    return;
                }
            }
            AppDatabase.Entries.Add(entry);
            SaveDatabase();
        }
        public Database LoadDatabase()
        {
            string databaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database");
            string databasePath = Path.Combine(databaseDirectory, "database.json");

            if (!File.Exists(databasePath))
            {
                return new Database();
            }

            string json = File.ReadAllText(databasePath);

            Database? loadedDatabase = JsonSerializer.Deserialize<Database>(json);

            return loadedDatabase ?? new Database();
        }
        public void CleanupDatabaseBackups(string databaseDirectory, int maxBackups)
        {
            FileInfo[] backupFiles = new DirectoryInfo(databaseDirectory)
                .GetFiles("database_*.bak")
                .OrderByDescending(file => file.CreationTimeUtc)
                .ToArray();

            foreach (FileInfo backupFile in backupFiles.Skip(maxBackups))
            {
                backupFile.Delete();
            }
        }

        private void openApplicatieFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = AppDomain.CurrentDomain.BaseDirectory,
                UseShellExecute = true
            });
        }

        private void afsluitenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_restarting && !ConfirmApplicationExit())
            {
                e.Cancel = true;
            }
        }
        private bool ConfirmApplicationExit()
        {
            DialogResult result = MessageBox.Show(
                "Weet je zeker dat je de applicatie wilt afsluiten?",
                "Afsluiten",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            return result == DialogResult.Yes;
        }
        private void opnieuwOpstartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Weet je zeker dat je de applicatie opnieuw wilt opstarten?",
                "Opnieuw opstarten",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _restarting = true;
                Application.Restart();
            }
        }

        private void onderwerpToevoegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputDialogBox.Show("Nieuw onderwerp", "Naam van nieuw onderwerp:", out string subjectName))
            {
                subjectName = subjectName.Trim();

                if (subjectName == "")
                {
                    MessageBox.Show("Onderwerp naam mag niet leeg zijn!");
                    return;
                }

                foreach (ContentSubject s in AppDatabase.Subjects)
                {
                    if (string.Equals(s.SubjectName, subjectName, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(subjectName + " bestaat al als een onderwerp!");
                        return;
                    }
                }

                AppDatabase.Subjects.Add(new ContentSubject
                {
                    SubjectName = subjectName
                });
                SaveDatabase();
                BroadcastRefresh();
            }
        }

        private void lijstOnderwerpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSubjectsForm();
        }
    }



    public class UpdateInfo
    {
        [JsonPropertyName("latest_version")]
        public string LatestVersion { get; set; } = "";

        [JsonPropertyName("update_url")]
        public string UpdateUrl { get; set; } = "";

        [JsonPropertyName("update_text")]
        public string UpdateText { get; set; } = "";

        [JsonPropertyName("app_lock")]
        public bool AppLock { get; set; }
    }


}
