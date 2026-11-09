using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab2Project
{
    // Small helper methods so we don't repeat the same "create a label / button" code everywhere.
    internal static class UI
    {
        public static Label MakeLabel(string text, int x, int y, float size = 10, bool bold = false)
        {
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;
            label.Location = new Point(x, y);
            label.Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular);
            return label;
        }

        public static Button MakeButton(string text, int x, int y, int width, int height)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Font = new Font("Segoe UI", 10);
            return button;
        }

        public static TextBox MakeTextBox(int x, int y, int width)
        {
            TextBox box = new TextBox();
            box.Location = new Point(x, y);
            box.Width = width;
            box.Font = new Font("Segoe UI", 10);
            return box;
        }

        // Common settings used by every form (centered, fixed size, no maximize).
        public static void SetupForm(Form form, string title, int width, int height)
        {
            form.Text = title;
            form.ClientSize = new Size(width, height);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
        }
    }

    // Handles moving between forms.
    internal static class Navigator
    {
        // Logout: close every form except Login, then show the Login form again.
        public static void Logout()
        {
            LoginForm login = null;
            List<Form> others = new List<Form>();

            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                    login = (LoginForm)f;
                else
                    others.Add(f);
            }

            foreach (Form f in others)
            {
                f.Close();
            }

            if (login != null)
            {
                login.ResetAndShow();
            }
        }

        // If the user clicks the X button on any form, the whole program closes.
        public static void OnUserClose(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
