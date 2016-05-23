using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogOn;
using System.Windows.Forms;

namespace LogOnLoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cLogonCheck.check(args);
            //Application.Run(new fMain());
        }
    }
}
