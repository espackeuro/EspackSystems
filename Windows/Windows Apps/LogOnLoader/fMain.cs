using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonTools;
using CommonToolsWin;
namespace LogOnLoader
{
    public partial class fMain : frmSplash
    {
        public static async Task<fMain> GetForm()
        {
            fMain elForm = new fMain();
            await elForm.LogonCheck();
            return elForm;
        }
        //private string[] Args { get; set; }
        public fMain() : base(null, "Checking Logon Updates.", false)
        {
            //Args = args;

            timer1.Interval = 1;
            //LogonCheck().Wait();
            //this.Activated += FMain_Activated;
        }
        public async Task InitializeAsync()
        {
            await LogonCheck();
            TimerEnabled(true);
        }
        //private void FMain_Activated(object sender, EventArgs e)
        //{
        //    cLogonCheck.check(this,Args);
        //}
    }
}
