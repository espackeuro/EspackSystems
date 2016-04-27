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
using System.Reflection;

namespace Sistemas
{
    public partial class frmSplash : Form
    {
        public frmSplash(string[] args)
        {
            InitializeComponent();
            lblCredit.Text = "© Espack Eurologistica Systems Area " + CT.GetBuildDateTime(Assembly.GetExecutingAssembly()).Year.ToString() + " Build " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblCommandLine.Text = string.Join(" ", args);
            MouseClick += delegate
             {
                 this.Close();
             };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
        }


    }
}
