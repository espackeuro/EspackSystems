using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonTools;
using CommonToolsWin;
namespace Etiquetas_CS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static fMain fMain;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new frmSplash(args).ShowDialog();
            fMain = new fMain(args);
            Application.Run(fMain);
        }
    }

}
    