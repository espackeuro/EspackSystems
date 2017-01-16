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
using CommonToolsWin;
using System.Reflection;

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
            this.Text = string.Format("Sistemas Build {0} - ({1:yyyyMMdd})*", Assembly.GetExecutingAssembly().GetName().Version.ToString(), CT.GetBuildDateTime(Assembly.GetExecutingAssembly()));
            try
            {
                Values.gDatos.Connect();
            } catch (Exception e)
            {
                MessageBox.Show("Error connecting to database: "+e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            Values.gDatos.Close();
        }


        private void mnuTowns_Click(object sender, EventArgs e)
        {
            fTown fTown = (fTown)GetChildInstance("fTown"); 
        }

        private void mnuZones_Click(object sender, EventArgs e)
        {
            fZones fZone = (fZones)GetChildInstance("fZones");
        }

        private void mnuItems_Click(object sender, EventArgs e)
        {
            //fUsers fUsers = (fUsers)GetChildInstance("fUsers");
            fItems fItems = (fItems)GetChildInstance("fItems");
        }

        private void dHCPControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDHCP fDHCP = (fDHCP)GetChildInstance("fDHCP");

        }

        //private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    fTasks fTasks = (fTasks)GetChildInstance("fTasks");
        //}

        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fTasks fTasks = (fTasks)GetFormInstance("fTasks");
            if (fTasks == null)
            {
                fTasks = new fTasks();
                fTasks.MdiParent = this;

            }
            fTasks.Show();
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

        private void btnMaster_Click(object sender, EventArgs e)
        {
            for (var i = 1; i < Application.OpenForms.Count; i++)
            {
                Application.OpenForms[i].Close();
            }
            if (Values.gDatos.context_info == null)
            {
                string lPwd = "";
#if DEBUG
                lPwd = "Y?D6d#b@";
#endif
                CTWin.InputBox("", "Enter Master Password", ref lPwd, true);
                if (lPwd != "")
                {
                    var vbresult = new byte[128];
                    cAccesoDatosNet lDatos = Values.gDatos.Clone();
                    lDatos.DataBase = "SISTEMAS";
                    SP lSP = new SP(lDatos, "pCheckContext");
                    lSP.AddParameterValue("password", lPwd);
                    lSP.AddParameterValue("Code", "MASTERPASSWORD");
                    lSP.AddParameterValue("vbpassword", null);
                    lSP.Execute();
                    if (lSP.LastMsg != "OK")
                    {
                        MessageBox.Show(lSP.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        vbresult = (byte[])lSP.ReturnValues()["@vbpassword"];
                        Values.gDatos.context_info = vbresult;
                        this.btnMaster.Image = global::Sistemas.Properties.Resources.unlock_24;
                        Values.gMasterPassword = lPwd;
                    }
                }
            } else
            {
                Values.gDatos.context_info = null;
                this.btnMaster.Image = global::Sistemas.Properties.Resources.lock_24;
            }
        }



        private void flagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fFlags fFlags = new fFlags();
            fFlags.MdiParent = this;
            fFlags.Show();
        }

        private void servicesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fServices fServices = new fServices();
            fServices.MdiParent = this;
            fServices.Show();
        }

        private void usersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fUsers fUsers = new fUsers();
            fUsers.MdiParent = this;
            fUsers.Show();
        }

        private void aliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAlias fAlias = new fAlias();
            fAlias.MdiParent = this;
            fAlias.Show();
        }

        private void securityProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fProfiles fProfiles = new fProfiles();
            fProfiles.MdiParent = this;
            fProfiles.Show();
        }

        private void dNSControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDNS fDNS = (fDNS)GetChildInstance("fDNS");
        }

        private void tESTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fTests fTests = (fTests)GetChildInstance("fTests");
        }

        private void testCarlosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fTest fTest = new fTest();
            fTest.Show();
        }
    }

    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string gMasterPassword = "";
    }

}
