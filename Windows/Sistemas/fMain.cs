using AccesoDatosNet;
using CommonTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Security;
using static System.Convert;
using MasterClass;

namespace Sistemas
{

    public partial class fMain : Form
    {
        public fMain(string[] args)
        {
            InitializeComponent();
            var espackArgs = CT.LoadVars(args);
            //Values.gDatos.DataBase = "Sistemas";//espackArgs.DataBase;
            //Values.gDatos.Server = "192.168.200.7";//espackArgs.Server;
            //Values.gDatos.User = "sa";//espackArgs.User;
            //Values.gDatos.Password = "5380"; //espackArgs.Password;

            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;
            Values.gMasterPassword = MasterPassword.Master;
            Values.gOldMaster = "Y?D6d#b@".ToSecureString();
            Values.gDatos.context_info = MasterPassword.MasterBytes;
            this.Text = string.Format("Sistemas Build {0} - ({1:yyyyMMdd})*", Assembly.GetExecutingAssembly().GetName().Version.ToString(), CT.GetBuildDateTime(Assembly.GetExecutingAssembly()));
            try
            {
                Values.gDatos.Connect();
            } catch (Exception e)
            {
                MessageBox.Show("Error connecting to database: "+e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            //lets get user position and security level
            using (var _sp=new SP(Values.gDatos,"pUsersGetPositionAndSecurityLevel"))
            {
                try
                {
                    _sp.AddParameterValue("@UserCode", Values.gDatos.User);
                    _sp.Execute();
                    Values.SecurityLevel = ToInt32(_sp.ReturnValues()["@SecurityLevel"]);
                    Values.Position = _sp.ReturnValues()["@Position"].ToString();
                    Values.FullName = _sp.ReturnValues()["@FullName"].ToString();
                } catch (Exception ex)
                {
                    MessageBox.Show("Error getting security data: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            AddStatusStip();
            MessageBox.Show(string.Format("Welcome {0} {1}, your level access is {2}",Values.Position, Values.FullName, Values.SecurityLevel));
            Values.gDatos.Close();
        }

        public void AddStatusStip()
        {
            var mDefaultStatusStrip = new StatusStrip();
            //SizeType lSize = new SizeType(118, 17);
            var Panel1 = new ToolStripStatusLabel(Values.FullName) { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel1);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            var Panel2 = new ToolStripStatusLabel(Values.SecurityLevel.ToString()) { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel2);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            //var Panel3 = new ToolStripStatusLabel("") { AutoSize = true };
            //mDefaultStatusStrip.Items.Add(Panel3);
            //mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Controls.Add(mDefaultStatusStrip);
            KeyPreview = true;
        }
        private void mnuTowns_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void openForm(object menuOption)
        {
            var formName = ((ToolStripMenuItem)menuOption).Tag.ToString();
            var form = (Form)GetChildInstance(formName);
            form.WindowState = FormWindowState.Maximized;
        }

        private void mnuZones_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void mnuItems_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void dHCPControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);

        }

        //private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    fTasks fTasks = (fTasks)GetChildInstance("fTasks");
        //}

        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private static Dictionary<string, Form> InstancedForms= new Dictionary<string, Form>();

        private object GetChildInstance(String pFormName)
        {
            Form _Form;
            if (!InstancedForms.TryGetValue(pFormName, out _Form))
            {
                _Form = (Form)Activator.CreateInstance(Type.GetType("Sistemas."+pFormName));
                _Form.MdiParent = this;
                InstancedForms.Add(pFormName, _Form);
            }
            InstancedForms[pFormName].Show();
            InstancedForms[pFormName].WindowState= FormWindowState.Normal;
            InstancedForms[pFormName].FormClosed += delegate (object source,FormClosedEventArgs e)
             {
                 InstancedForms.Remove(pFormName);
                 //base.FormClosed(source, e);
             };
            return InstancedForms[pFormName];  //just created or created earlier.Return it+69
        }

        private object GetFormInstance(string pFormName)
        {
            return Application.OpenForms.Cast<Form>().Where(form => form.Name == pFormName).FirstOrDefault();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
        }

        private void flagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void servicesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void usersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void aliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void securityProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }

        private void dNSControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }


        private void systemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(sender);
        }
    }

    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static SecureString gMasterPassword;
        public static SecureString gOldMaster;
        public static int SecurityLevel { get; set; }
        public static string Position { get; set; }
        public static string FullName { get; set; }
    }

}
