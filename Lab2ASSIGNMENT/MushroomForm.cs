using System;
using System.Windows.Forms;

namespace Lab2Project
{
    // Form 4: Mushroom identifier ("20 Questions" style).
    // The program asks ONE question at a time and never more than three.
    //
    //   Ring?  yes -> Forest?  yes -> Amanite tue-mouche
    //                          no  -> Convex cup? yes -> Agaric jaunissant / no -> Coprin chevelu
    //          no  -> Gills?   no  -> Cepe de bordeaux
    //                          yes -> Convex cup? yes -> Pied bleu / no -> Girolle
    public class MushroomForm : Form
    {
        private Label lblStep;
        private Label lblQuestion;

        private string current;   // which question is on screen: "ring", "forest", "gills" or "cup"
        private bool hasRing;     // remembers the answer to the ring question
        private int count;        // how many questions have been asked so far

        public MushroomForm()
        {
            UI.SetupForm(this, "Mushroom Identifier", 420, 300);
            FormClosing += Navigator.OnUserClose;

            Label lblTitle = UI.MakeLabel("Mushroom Identifier", 20, 15, 14, true);
            Label lblHelp = UI.MakeLabel("Think of one of the six mushrooms and answer Yes or No.", 20, 50, 9);

            lblStep = UI.MakeLabel("", 20, 90, 9);
            lblQuestion = UI.MakeLabel("", 20, 115, 12, true);

            Button btnYes = UI.MakeButton("Yes", 20, 170, 180, 45);
            Button btnNo = UI.MakeButton("No", 220, 170, 180, 45);
            btnYes.Click += btnYes_Click;
            btnNo.Click += btnNo_Click;

            Button btnLogout = UI.MakeButton("Logout", 20, 240, 380, 36);
            btnLogout.Click += btnLogout_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblHelp);
            Controls.Add(lblStep);
            Controls.Add(lblQuestion);
            Controls.Add(btnYes);
            Controls.Add(btnNo);
            Controls.Add(btnLogout);

            StartOver();
        }

        // Go back to the first question.
        private void StartOver()
        {
            current = "ring";
            hasRing = false;
            count = 1;
            ShowQuestion();
        }

        // Display the question that matches 'current'.
        private void ShowQuestion()
        {
            lblStep.Text = "Question " + count + " of at most 3";

            if (current == "ring")
                lblQuestion.Text = "Does your mushroom have a ring?";
            else if (current == "forest")
                lblQuestion.Text = "Does your mushroom grow in a forest?";
            else if (current == "gills")
                lblQuestion.Text = "Does your mushroom have gills?";
            else
                lblQuestion.Text = "Does your mushroom have a convex cup?";
        }

        // Decide what to do with the answer (yes = true, no = false).
        private void HandleAnswer(bool yes)
        {
            string result = null;   // stays null until we know the mushroom

            if (current == "ring")
            {
                hasRing = yes;
                current = yes ? "forest" : "gills";
            }
            else if (current == "forest")
            {
                if (yes)
                    result = "Amanite tue-mouche";
                else
                    current = "cup";
            }
            else if (current == "gills")
            {
                if (!yes)
                    result = "Cepe de bordeaux";
                else
                    current = "cup";
            }
            else // current == "cup"
            {
                if (hasRing)
                    result = yes ? "Agaric jaunissant" : "Coprin chevelu";
                else
                    result = yes ? "Pied bleu" : "Girolle";
            }

            if (result != null)
            {
                MessageBox.Show("Your mushroom is: " + result, "Result",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartOver();   // ready for the next user
            }
            else
            {
                count++;
                ShowQuestion();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            HandleAnswer(true);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            HandleAnswer(false);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Navigator.Logout();
        }
    }
}
