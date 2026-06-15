namespace csharp_michels_database
{
    partial class EntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            coverPictureBox = new PictureBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            TOCPictureBox = new PictureBox();
            titelTextBox = new TextBox();
            dataGridView1 = new DataGridView();
            GUIDtextBox = new TextBox();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            opslaanToolStripMenuItem = new ToolStripMenuItem();
            opslaanToolStripMenuItem1 = new ToolStripMenuItem();
            verwijderenToolStripMenuItem = new ToolStripMenuItem();
            sluitenToolStripMenuItem = new ToolStripMenuItem();
            toevoegenWijzigenToolStripMenuItem = new ToolStripMenuItem();
            coverToolStripMenuItem = new ToolStripMenuItem();
            inhoudToolStripMenuItem = new ToolStripMenuItem();
            pDFToolStripMenuItem = new ToolStripMenuItem();
            PDFLinkLabel = new LinkLabel();
            addContentButton = new Button();
            ((System.ComponentModel.ISupportInitialize)coverPictureBox).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TOCPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(44, 25);
            label1.TabIndex = 0;
            label1.Text = "Titel";
            // 
            // coverPictureBox
            // 
            coverPictureBox.Dock = DockStyle.Fill;
            coverPictureBox.Location = new Point(3, 3);
            coverPictureBox.Name = "coverPictureBox";
            coverPictureBox.Size = new Size(365, 498);
            coverPictureBox.TabIndex = 0;
            coverPictureBox.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 81);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(379, 542);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(coverPictureBox);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(371, 504);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Cover";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(TOCPictureBox);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(371, 504);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Inhoud";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // TOCPictureBox
            // 
            TOCPictureBox.Dock = DockStyle.Fill;
            TOCPictureBox.Location = new Point(3, 3);
            TOCPictureBox.Name = "TOCPictureBox";
            TOCPictureBox.Size = new Size(365, 498);
            TOCPictureBox.TabIndex = 0;
            TOCPictureBox.TabStop = false;
            // 
            // titelTextBox
            // 
            titelTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titelTextBox.Location = new Point(62, 36);
            titelTextBox.Name = "titelTextBox";
            titelTextBox.Size = new Size(1184, 31);
            titelTextBox.TabIndex = 4;
            titelTextBox.TextChanged += titelTextBox_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(397, 118);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(849, 465);
            dataGridView1.TabIndex = 5;
            // 
            // GUIDtextBox
            // 
            GUIDtextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GUIDtextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GUIDtextBox.Location = new Point(397, 83);
            GUIDtextBox.Name = "GUIDtextBox";
            GUIDtextBox.ReadOnly = true;
            GUIDtextBox.Size = new Size(849, 29);
            GUIDtextBox.TabIndex = 6;
            GUIDtextBox.Text = "f6f6b6c3-0a91-4f8b-a4e3-9d99d83d5b91";
            GUIDtextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, 630);
            label2.Name = "label2";
            label2.Size = new Size(48, 25);
            label2.TabIndex = 8;
            label2.Text = "PDF:";
            // 
            // menuStrip1
            // 
            menuStrip1.AllowMerge = false;
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { opslaanToolStripMenuItem, toevoegenWijzigenToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1258, 33);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // opslaanToolStripMenuItem
            // 
            opslaanToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { opslaanToolStripMenuItem1, verwijderenToolStripMenuItem, sluitenToolStripMenuItem });
            opslaanToolStripMenuItem.Name = "opslaanToolStripMenuItem";
            opslaanToolStripMenuItem.Size = new Size(75, 29);
            opslaanToolStripMenuItem.Text = "Acties";
            // 
            // opslaanToolStripMenuItem1
            // 
            opslaanToolStripMenuItem1.Name = "opslaanToolStripMenuItem1";
            opslaanToolStripMenuItem1.ShortcutKeys = Keys.Control | Keys.S;
            opslaanToolStripMenuItem1.Size = new Size(240, 34);
            opslaanToolStripMenuItem1.Text = "Opslaan";
            opslaanToolStripMenuItem1.Click += opslaanToolStripMenuItem1_Click;
            // 
            // verwijderenToolStripMenuItem
            // 
            verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            verwijderenToolStripMenuItem.Size = new Size(240, 34);
            verwijderenToolStripMenuItem.Text = "Verwijderen";
            verwijderenToolStripMenuItem.Click += verwijderenToolStripMenuItem_Click;
            // 
            // sluitenToolStripMenuItem
            // 
            sluitenToolStripMenuItem.Name = "sluitenToolStripMenuItem";
            sluitenToolStripMenuItem.Size = new Size(240, 34);
            sluitenToolStripMenuItem.Text = "Sluiten";
            sluitenToolStripMenuItem.Click += sluitenToolStripMenuItem_Click;
            // 
            // toevoegenWijzigenToolStripMenuItem
            // 
            toevoegenWijzigenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { coverToolStripMenuItem, inhoudToolStripMenuItem, pDFToolStripMenuItem });
            toevoegenWijzigenToolStripMenuItem.Name = "toevoegenWijzigenToolStripMenuItem";
            toevoegenWijzigenToolStripMenuItem.Size = new Size(188, 29);
            toevoegenWijzigenToolStripMenuItem.Text = "Toevoegen/Wijzigen";
            // 
            // coverToolStripMenuItem
            // 
            coverToolStripMenuItem.Name = "coverToolStripMenuItem";
            coverToolStripMenuItem.Size = new Size(171, 34);
            coverToolStripMenuItem.Text = "Cover";
            coverToolStripMenuItem.Click += coverToolStripMenuItem_Click;
            // 
            // inhoudToolStripMenuItem
            // 
            inhoudToolStripMenuItem.Name = "inhoudToolStripMenuItem";
            inhoudToolStripMenuItem.Size = new Size(171, 34);
            inhoudToolStripMenuItem.Text = "Inhoud";
            inhoudToolStripMenuItem.Click += inhoudToolStripMenuItem_Click;
            // 
            // pDFToolStripMenuItem
            // 
            pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            pDFToolStripMenuItem.Size = new Size(171, 34);
            pDFToolStripMenuItem.Text = "PDF";
            pDFToolStripMenuItem.Click += pDFToolStripMenuItem_Click;
            // 
            // PDFLinkLabel
            // 
            PDFLinkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PDFLinkLabel.AutoSize = true;
            PDFLinkLabel.Location = new Point(62, 630);
            PDFLinkLabel.Name = "PDFLinkLabel";
            PDFLinkLabel.Size = new Size(27, 25);
            PDFLinkLabel.TabIndex = 11;
            PDFLinkLabel.TabStop = true;
            PDFLinkLabel.Text = "   ";
            // 
            // addContentButton
            // 
            addContentButton.Location = new Point(397, 589);
            addContentButton.Name = "addContentButton";
            addContentButton.Size = new Size(849, 34);
            addContentButton.TabIndex = 12;
            addContentButton.Text = "Gegevens toevoegen";
            addContentButton.UseVisualStyleBackColor = true;
            addContentButton.Click += addContentButton_Click;
            // 
            // EntryForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 664);
            Controls.Add(addContentButton);
            Controls.Add(PDFLinkLabel);
            Controls.Add(label2);
            Controls.Add(GUIDtextBox);
            Controls.Add(dataGridView1);
            Controls.Add(titelTextBox);
            Controls.Add(tabControl1);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "EntryForm";
            Text = "Nieuw item";
            Load += EntryForm_Load;
            ((System.ComponentModel.ISupportInitialize)coverPictureBox).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TOCPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox coverPictureBox;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private PictureBox TOCPictureBox;
        private TextBox titelTextBox;
        private DataGridView dataGridView1;
        private TextBox GUIDtextBox;
        private Label label2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem opslaanToolStripMenuItem;
        private ToolStripMenuItem opslaanToolStripMenuItem1;
        private ToolStripMenuItem sluitenToolStripMenuItem;
        private ToolStripMenuItem toevoegenWijzigenToolStripMenuItem;
        private ToolStripMenuItem coverToolStripMenuItem;
        private ToolStripMenuItem inhoudToolStripMenuItem;
        private ToolStripMenuItem pDFToolStripMenuItem;
        private ToolStripMenuItem verwijderenToolStripMenuItem;
        private LinkLabel PDFLinkLabel;
        private Button addContentButton;
    }
}