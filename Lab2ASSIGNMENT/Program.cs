using System;
using System.Windows.Forms;

namespace Lab2Project
{
    internal static class Program
    {
        // The program starts here. The first form shown is the Login form.
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
