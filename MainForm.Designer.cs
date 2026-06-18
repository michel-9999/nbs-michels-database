namespace csharp_michels_database
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            updateToolStripStatusLabel = new ToolStripStatusLabel();
            update_ToolStripStatusLabel = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            programmaToolStripMenuItem = new ToolStripMenuItem();
            opnieuwOpstartenToolStripMenuItem = new ToolStripMenuItem();
            afsluitenToolStripMenuItem = new ToolStripMenuItem();
            itemsToolStripMenuItem1 = new ToolStripMenuItem();
            nieuwToolStripMenuItem = new ToolStripMenuItem();
            alleItemsToolStripMenuItem = new ToolStripMenuItem();
            toevoegenToolStripMenuItem = new ToolStripMenuItem();
            categorieToevoegenToolStripMenuItem = new ToolStripMenuItem();
            categoryToolStripMenuItem = new ToolStripMenuItem();
            onderwerpenToolStripMenuItem = new ToolStripMenuItem();
            onderwerpToevoegenToolStripMenuItem = new ToolStripMenuItem();
            lijstOnderwerpenToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            downloadPaginaToolStripMenuItem = new ToolStripMenuItem();
            openApplicatieFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            michelsWebsiteToolStripMenuItem = new ToolStripMenuItem();
            overToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, updateToolStripStatusLabel, update_ToolStripStatusLabel });
            statusStrip1.Location = new Point(0, 522);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(878, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 15);
            // 
            // updateToolStripStatusLabel
            // 
            updateToolStripStatusLabel.Name = "updateToolStripStatusLabel";
            updateToolStripStatusLabel.Size = new Size(0, 15);
            // 
            // update_ToolStripStatusLabel
            // 
            update_ToolStripStatusLabel.Name = "update_ToolStripStatusLabel";
            update_ToolStripStatusLabel.Size = new Size(0, 15);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { programmaToolStripMenuItem, itemsToolStripMenuItem1, toevoegenToolStripMenuItem, onderwerpenToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(878, 33);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // programmaToolStripMenuItem
            // 
            programmaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { opnieuwOpstartenToolStripMenuItem, afsluitenToolStripMenuItem });
            programmaToolStripMenuItem.Name = "programmaToolStripMenuItem";
            programmaToolStripMenuItem.Size = new Size(122, 29);
            programmaToolStripMenuItem.Text = "Programma";
            // 
            // opnieuwOpstartenToolStripMenuItem
            // 
            opnieuwOpstartenToolStripMenuItem.Name = "opnieuwOpstartenToolStripMenuItem";
            opnieuwOpstartenToolStripMenuItem.Size = new Size(266, 34);
            opnieuwOpstartenToolStripMenuItem.Text = "Opnieuw opstarten";
            opnieuwOpstartenToolStripMenuItem.Click += opnieuwOpstartenToolStripMenuItem_Click;
            // 
            // afsluitenToolStripMenuItem
            // 
            afsluitenToolStripMenuItem.Name = "afsluitenToolStripMenuItem";
            afsluitenToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            afsluitenToolStripMenuItem.Size = new Size(266, 34);
            afsluitenToolStripMenuItem.Text = "Afsluiten";
            afsluitenToolStripMenuItem.Click += afsluitenToolStripMenuItem_Click_1;
            // 
            // itemsToolStripMenuItem1
            // 
            itemsToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { nieuwToolStripMenuItem, alleItemsToolStripMenuItem });
            itemsToolStripMenuItem1.Name = "itemsToolStripMenuItem1";
            itemsToolStripMenuItem1.Size = new Size(72, 29);
            itemsToolStripMenuItem1.Text = "Items";
            // 
            // nieuwToolStripMenuItem
            // 
            nieuwToolStripMenuItem.Name = "nieuwToolStripMenuItem";
            nieuwToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            nieuwToolStripMenuItem.Size = new Size(268, 34);
            nieuwToolStripMenuItem.Text = "Nieuw Item";
            nieuwToolStripMenuItem.Click += nieuwToolStripMenuItem_Click;
            // 
            // alleItemsToolStripMenuItem
            // 
            alleItemsToolStripMenuItem.Name = "alleItemsToolStripMenuItem";
            alleItemsToolStripMenuItem.ShortcutKeys = Keys.F5;
            alleItemsToolStripMenuItem.Size = new Size(268, 34);
            alleItemsToolStripMenuItem.Text = "Alle Items";
            alleItemsToolStripMenuItem.Click += alleItemsToolStripMenuItem_Click;
            // 
            // toevoegenToolStripMenuItem
            // 
            toevoegenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { categorieToevoegenToolStripMenuItem, categoryToolStripMenuItem });
            toevoegenToolStripMenuItem.Name = "toevoegenToolStripMenuItem";
            toevoegenToolStripMenuItem.Size = new Size(123, 29);
            toevoegenToolStripMenuItem.Text = "Categorieën";
            // 
            // categorieToevoegenToolStripMenuItem
            // 
            categorieToevoegenToolStripMenuItem.Name = "categorieToevoegenToolStripMenuItem";
            categorieToevoegenToolStripMenuItem.Size = new Size(281, 34);
            categorieToevoegenToolStripMenuItem.Text = "Categorie Toevoegen";
            categorieToevoegenToolStripMenuItem.Click += categorieToevoegenToolStripMenuItem_Click;
            // 
            // categoryToolStripMenuItem
            // 
            categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            categoryToolStripMenuItem.Size = new Size(281, 34);
            categoryToolStripMenuItem.Text = "Lijst Categorieën";
            categoryToolStripMenuItem.Click += categoryToolStripMenuItem_Click;
            // 
            // onderwerpenToolStripMenuItem
            // 
            onderwerpenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { onderwerpToevoegenToolStripMenuItem, lijstOnderwerpenToolStripMenuItem });
            onderwerpenToolStripMenuItem.Name = "onderwerpenToolStripMenuItem";
            onderwerpenToolStripMenuItem.Size = new Size(136, 29);
            onderwerpenToolStripMenuItem.Text = "Onderwerpen";
            // 
            // onderwerpToevoegenToolStripMenuItem
            // 
            onderwerpToevoegenToolStripMenuItem.Name = "onderwerpToevoegenToolStripMenuItem";
            onderwerpToevoegenToolStripMenuItem.Size = new Size(294, 34);
            onderwerpToevoegenToolStripMenuItem.Text = "Onderwerp Toevoegen";
            onderwerpToevoegenToolStripMenuItem.Click += onderwerpToevoegenToolStripMenuItem_Click;
            // 
            // lijstOnderwerpenToolStripMenuItem
            // 
            lijstOnderwerpenToolStripMenuItem.Name = "lijstOnderwerpenToolStripMenuItem";
            lijstOnderwerpenToolStripMenuItem.Size = new Size(294, 34);
            lijstOnderwerpenToolStripMenuItem.Text = "Lijst Onderwerpen";
            lijstOnderwerpenToolStripMenuItem.Click += lijstOnderwerpenToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { downloadPaginaToolStripMenuItem, openApplicatieFolderToolStripMenuItem, toolStripSeparator2, michelsWebsiteToolStripMenuItem, overToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(65, 29);
            helpToolStripMenuItem.Text = "Help";
            // 
            // downloadPaginaToolStripMenuItem
            // 
            downloadPaginaToolStripMenuItem.Name = "downloadPaginaToolStripMenuItem";
            downloadPaginaToolStripMenuItem.Size = new Size(296, 34);
            downloadPaginaToolStripMenuItem.Text = "Download Pagina";
            downloadPaginaToolStripMenuItem.Click += downloadPaginaToolStripMenuItem_Click;
            // 
            // openApplicatieFolderToolStripMenuItem
            // 
            openApplicatieFolderToolStripMenuItem.Name = "openApplicatieFolderToolStripMenuItem";
            openApplicatieFolderToolStripMenuItem.Size = new Size(296, 34);
            openApplicatieFolderToolStripMenuItem.Text = "Open Applicatie Folder";
            openApplicatieFolderToolStripMenuItem.Click += openApplicatieFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(293, 6);
            // 
            // michelsWebsiteToolStripMenuItem
            // 
            michelsWebsiteToolStripMenuItem.Name = "michelsWebsiteToolStripMenuItem";
            michelsWebsiteToolStripMenuItem.Size = new Size(296, 34);
            michelsWebsiteToolStripMenuItem.Text = "Michel's Website";
            michelsWebsiteToolStripMenuItem.Click += michelsWebsiteToolStripMenuItem_Click;
            // 
            // overToolStripMenuItem
            // 
            overToolStripMenuItem.Name = "overToolStripMenuItem";
            overToolStripMenuItem.Size = new Size(296, 34);
            overToolStripMenuItem.Text = "Over...";
            overToolStripMenuItem.Click += overToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 544);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Michel's Database";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripStatusLabel updateToolStripStatusLabel;
        private ToolStripStatusLabel update_ToolStripStatusLabel;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem downloadPaginaToolStripMenuItem;
        private ToolStripMenuItem itemsToolStripMenuItem1;
        private ToolStripMenuItem nieuwToolStripMenuItem;
        private ToolStripMenuItem alleItemsToolStripMenuItem;
        private ToolStripMenuItem overToolStripMenuItem;
        private ToolStripMenuItem toevoegenToolStripMenuItem;
        private ToolStripMenuItem categoryToolStripMenuItem;
        private ToolStripMenuItem categorieToevoegenToolStripMenuItem;
        private ToolStripMenuItem openApplicatieFolderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem programmaToolStripMenuItem;
        private ToolStripMenuItem afsluitenToolStripMenuItem;
        private ToolStripMenuItem opnieuwOpstartenToolStripMenuItem;
        private ToolStripMenuItem onderwerpenToolStripMenuItem;
        private ToolStripMenuItem onderwerpToevoegenToolStripMenuItem;
        private ToolStripMenuItem lijstOnderwerpenToolStripMenuItem;
        private ToolStripMenuItem michelsWebsiteToolStripMenuItem;
    }
}
