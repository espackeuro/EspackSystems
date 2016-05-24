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

namespace CommonTools
{
    public partial class frmSplash : Form
    {
        public string Message
        {
            get
            {
                return lblMessage.Text;
            }
            set
            {
                lblMessage.Text = value;
            }
        }
        public frmSplash(string[] args, string pMessage="", bool pTimer=true)
        {
            InitializeComponent();
            TimerEnabled(pTimer);
            lblCredit.Text = "© Espack Eurologistica Systems Area " + CT.GetBuildDateTime(Assembly.GetExecutingAssembly()).Year.ToString() + " Build " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Message = pMessage=="" ? string.Join(" ", args): pMessage;
            //MouseClick += delegate
            // {
            //     this.Close();
            // };
        }
        public void TimerEnabled(bool pStatus)
        {
            timer1.Enabled = pStatus;
        }
        public void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
        }


    }
}
