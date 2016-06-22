using System;
using System.Windows.Forms;
using CommonToolsWin;

namespace Simplistica
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new frmSplash(args).ShowDialog();
            Application.Run(new fMain(args));
        }
    }
}
