using System;
using System.Windows.Forms;

namespace Lab2Project
{
    // Form 3: Bicycle rental. Reads a start hour and an end hour, shows the total.
    public class BicycleForm : Form
    {
        private TextBox txtStart;
        private TextBox txtEnd;
        private TextBox txtTotal;

        public BicycleForm()
        {
            UI.SetupForm(this, "Bicycle Rental", 360, 360);
            FormClosing += Navigator.OnUserClose;

            Label lblTitle = UI.MakeLabel("Bicycle Rental", 20, 15, 14, true);
            Label lblStart = UI.MakeLabel("Start hour (0 - 23):", 20, 62);
            Label lblEnd = UI.MakeLabel("End hour (1 - 24):", 20, 102);
            Label lblTotal = UI.MakeLabel("Total:", 20, 205);

            txtStart = UI.MakeTextBox(200, 59, 130);
            txtEnd = UI.MakeTextBox(200, 99, 130);
            txtTotal = UI.MakeTextBox(200, 202, 130);
            txtTotal.ReadOnly = true;                    // the user cannot type here

            Button btnCalculate = UI.MakeButton("Calculate", 20, 148, 310, 36);
            btnCalculate.Click += btnCalculate_Click;
            AcceptButton = btnCalculate;

            Label lblInfo = UI.MakeLabel(
                "Rates per hour:\n" +
                "500 RWF  : 0h-7h and 21h-24h\n" +
                "1000 RWF : 7h-14h and 19h-21h\n" +
                "1500 RWF : 14h-19h", 20, 240, 9);

            Button btnLogout = UI.MakeButton("Logout", 20, 305, 310, 36);
            btnLogout.Click += btnLogout_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblStart);
            Controls.Add(txtStart);
            Controls.Add(lblEnd);
            Controls.Add(txtEnd);
            Controls.Add(btnCalculate);
            Controls.Add(lblTotal);
            Controls.Add(txtTotal);
            Controls.Add(lblInfo);
            Controls.Add(btnLogout);
        }

        // Price of ONE hour that starts at 'hour'.
        private int GetRate(int hour)
        {
            if (hour < 7) return 500;        // 0h - 7h
            else if (hour < 14) return 1000; // 7h - 14h
            else if (hour < 19) return 1500; // 14h - 19h
            else if (hour < 21) return 1000; // 19h - 21h
            else return 500;                 // 21h - 24h
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int start;
            int end;

            // int.TryParse only accepts whole numbers (no letters, no decimals).
            if (!int.TryParse(txtStart.Text.Trim(), out start) || start < 0 || start > 23)
            {
                MessageBox.Show("Start hour must be a whole number between 0 and 23.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStart.Focus();
                return;
            }

            if (!int.TryParse(txtEnd.Text.Trim(), out end) || end < 1 || end > 24)
            {
                MessageBox.Show("End hour must be a whole number between 1 and 24.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnd.Focus();
                return;
            }

            if (start >= end)
            {
                MessageBox.Show("The start hour must be less than the end hour.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStart.Focus();
                return;
            }

            // Walk through the rental one hour at a time and add each hour's price.
            int total = 0;
            int hour = start;
            while (hour < end)
            {
                total = total + GetRate(hour);
                hour++;
            }

            txtTotal.Text = total + " RWF";

            MessageBox.Show("Rental duration: " + (end - start) + " hour(s)\nTotal to pay: " + total + " RWF",
                "Bicycle Rental", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the inputs, ready for the next customer.
            txtStart.Clear();
            txtEnd.Clear();
            txtStart.Focus();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Navigator.Logout();
        }
    }
}
