using System;
using System.Windows.Forms;

namespace Lab2Project
{
    // Form 2: the menu. The user picks Bicycle or Mushroom.
    public class ChooseForm : Form
    {
        public ChooseForm()
        {
            UI.SetupForm(this, "Choose", 340, 190);
            FormClosing += Navigator.OnUserClose;

            Label lblTitle = UI.MakeLabel("Choose an application", 20, 15, 14, true);

            Button btnBicycle = UI.MakeButton("Bicycle", 20, 70, 140, 50);
            Button btnMushroom = UI.MakeButton("Mushroom", 180, 70, 140, 50);

            btnBicycle.Click += btnBicycle_Click;
            btnMushroom.Click += btnMushroom_Click;

            Controls.Add(lblTitle);
            Controls.Add(btnBicycle);
            Controls.Add(btnMushroom);
        }

        private void btnBicycle_Click(object sender, EventArgs e)
        {
            Hide();
            new BicycleForm().Show();
        }

        private void btnMushroom_Click(object sender, EventArgs e)
        {
            Hide();
            new MushroomForm().Show();
        }
    }
}
