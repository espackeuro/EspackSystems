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

namespace LogOnLoader
{
    public partial class fMain : frmSplash
    {
        private string[] Args { get; set; }
        public fMain(string[] args) : base(null, "Checking Logon Updates.", true)
        {
            Args = args;
            timer1.Interval = 1;
            //this.Activated += FMain_Activated;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            LogonCheck();
            base.OnClosing(e);
        }
        //private void FMain_Activated(object sender, EventArgs e)
        //{
        //    cLogonCheck.check(this,Args);
        //}
    }
}
