using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TokenAssist
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string source = string.Empty;

            // assume that if there is at least one command-line argument (other than the name of the executable),
            // that it is the path to the dnd4e file that the user wants to operate on
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                source = Environment.GetCommandLineArgs()[1];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(source));
        }
    }
}
