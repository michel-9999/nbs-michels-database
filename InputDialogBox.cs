using System;
using System.Drawing;
using System.Windows.Forms;

namespace csharp_michels_database
{
    public static class InputDialogBox
    {
        public static bool Show(
            string title,
            string prompt,
            out string input,
            string defaultValue = "")
        {
            input = defaultValue;

            using Form form = new Form();
            using Label label = new Label();
            using TextBox textBox = new TextBox();
            using Button okButton = new Button();
            using Button cancelButton = new Button();

            form.Text = title;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.ClientSize = new Size(400, 140);
            form.ShowInTaskbar = false;

            label.Text = prompt;
            label.AutoSize = false;
            label.Location = new Point(12, 12);
            label.Size = new Size(376, 30);

            textBox.Location = new Point(12, 48);
            textBox.Size = new Size(376, 23);
            textBox.Text = defaultValue;
            textBox.SelectAll();

            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(232, 95);
            okButton.Size = new Size(75, 28);

            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(313, 95);
            cancelButton.Size = new Size(75, 28);

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);

            form.AcceptButton = okButton;
            form.CancelButton = cancelButton;

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                input = textBox.Text;
                return true;
            }

            input = "";
            return false;
        }
    }
}