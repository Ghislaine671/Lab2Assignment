using System;
using System.Windows.Forms;

namespace Lab2Project
{
    // Form 1: Login. Checks that username and password are not empty.
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        public LoginForm()
        {
            UI.SetupForm(this, "Login", 340, 230);

            Label lblTitle = UI.MakeLabel("Login", 20, 15, 16, true);
            Label lblUser = UI.MakeLabel("Username:", 20, 70);
            Label lblPass = UI.MakeLabel("Password:", 20, 110);

            txtUsername = UI.MakeTextBox(120, 67, 190);
            txtPassword = UI.MakeTextBox(120, 107, 190);
            txtPassword.UseSystemPasswordChar = true;   // shows dots instead of letters

            btnLogin = UI.MakeButton("Login", 20, 160, 290, 38);
            btnLogin.Click += btnLogin_Click;
            AcceptButton = btnLogin;                    // pressing Enter clicks Login

            Controls.Add(lblTitle);
            Controls.Add(lblUser);
            Controls.Add(lblPass);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validation: both fields are required.
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter your username and password.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Success: hide Login and open the Choose form.
            Hide();
            new ChooseForm().Show();
        }

        // Called after logout: clear the boxes and show this form again.
        public void ResetAndShow()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            Show();
            txtUsername.Focus();
        }
    }
}
