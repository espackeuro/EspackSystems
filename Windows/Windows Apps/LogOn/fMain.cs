using AccesoDatosNet;
using CommonTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EspackControls;
using EspackFormControls;

namespace LogOn
{
    public partial class fMain : Form
    {
        public StatusStrip mDefaultStatusStrip;
        public ToolStripStatusLabel Panel1;
        public ToolStripStatusLabel Panel2;
        public ToolStripStatusLabel Panel3;
        public ToolStripStatusLabel Panel4;

        public fMain(string[] args)
        {
            InitializeComponent();

            // sdjfh
            txtUser.Enabled = true;
            txtUser.Multiline = false;
            txtPassword.Enabled = true;
            txtPassword.Multiline = false;

            var espackArgs = CT.LoadVars(args);
            //Values.gDatos.DataBase = "Sistemas";//espackArgs.DataBase;
            //Values.gDatos.Server = "192.168.200.7";//espackArgs.Server;
            //Values.gDatos.User = "sa";//espackArgs.User;
            //Values.gDatos.Password = "5380"; //espackArgs.Password;

            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;

            AddDefaultStatusStrip();

        }

        public void AddDefaultStatusStrip()
        {
            mDefaultStatusStrip = new StatusStrip();
            //SizeType lSize = new SizeType(118, 17);
            int lLabelWidth = 75;

            Panel1 = new ToolStripStatusLabel("Panel1") { Width = lLabelWidth, AutoSize = false };
            mDefaultStatusStrip.Items.Add(Panel1);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel2 = new ToolStripStatusLabel("Panel2") { Width = lLabelWidth, AutoSize = false };
            mDefaultStatusStrip.Items.Add(Panel2);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel3 = new ToolStripStatusLabel("Panel3") { Width = lLabelWidth, AutoSize = false };
            mDefaultStatusStrip.Items.Add(Panel3);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel4 = new ToolStripStatusLabel("Panel4") { Width = lLabelWidth, AutoSize = false };
            mDefaultStatusStrip.Items.Add(Panel4);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Controls.Add(mDefaultStatusStrip);
            KeyPreview = true;
        }
    }
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string gMasterPassword = "";
    }
}


