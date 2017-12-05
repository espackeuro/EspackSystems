using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatosNet;
using CommonTools;
using System.IO;
using System.IO.IsolatedStorage;
using CommonToolsWin;
using System.Reflection;

namespace Simplistica
{
    public partial class fMainSimplistica : Form
    {
        public fMainSimplistica(string[] args)
        {
            InitializeComponent();
            
            var espackArgs = CT.LoadVars(args);
            //Values.gDatos.DataBase = "Sistemas";//espackArgs.DataBase;
            //Values.gDatos.Server = "192.168.200.7";//espackArgs.Server;
            //Values.gDatos.User = "sa";//espackArgs.User;
            //Values.gDatos.Password = "5380"; //espackArgs.Password;
            Values.ProjectName = "Simplistica";//Assembly.GetCallingAssembly().GetName().Name;
            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;

            this.Text = string.Format("{0} Build {1} - ({2:yyyyMMdd})*", Values.ProjectName, Assembly.GetExecutingAssembly().GetName().Version.ToString(), CT.GetBuildDateTime(Assembly.GetExecutingAssembly()));
            try
            {
                Values.gDatos.Connect();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error connecting to database: " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            Values.gDatos.Close();
            //check settings file
            if (!cSettings.SettingFileNameExists)
            {
                fSettings fSettings = new fSettings();
                fSettings.ShowDialog();
            }
            Values.LabelPrinterAddress = cSettings.readSetting("labelPrinter");
            Values.COD3 = cSettings.readSetting("COD3");
        }

        private void simpleReceivalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fSimpleReceivals fSimpleReceivals = new fSimpleReceivals();
            //fSimpleReceivals.MdiParent = this;
            //fSimpleReceivals.Show();
            fSimpleReceivals fSimpleReceivals = (fSimpleReceivals)GetChildInstance("fSimpleReceivals");
        }

        private void printRepairsUnitLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fPrintUnitLabels fPrintUnitLabels = new fPrintUnitLabels();
            //fPrintUnitLabels.Show();
            fPrintUnitLabels fPrintUnitLabels = (fPrintUnitLabels)GetChildInstance("fPrintUnitLabels");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fSettings fSettings = new fSettings();
            //fSettings.ShowDialog();
            fSettings fSettings = (fSettings)GetChildInstance("fSettings");
        }

        private void printRackLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fRackLabels fRackLabels = (fRackLabels)GetChildInstance("fRackLabels");
            //fRackLabels fRackLabels = new fRackLabels();
            //fRackLabels.MdiParent = this;
            //fRackLabels.Show();
        }


        //form opening control
        private static Dictionary<string, Form> InstancedForms = new Dictionary<string, Form>();

        private object GetChildInstance(String pFormName)
        {
            Form _Form;
            if (!InstancedForms.TryGetValue(pFormName, out _Form))
            {
                _Form = (Form)Activator.CreateInstance(Type.GetType(Values.ProjectName+"." + pFormName));
                _Form.MdiParent = this;
                InstancedForms.Add(pFormName, _Form);
            }
            InstancedForms[pFormName].Show();
            InstancedForms[pFormName].WindowState = FormWindowState.Normal;
            InstancedForms[pFormName].FormClosed += delegate (object source, FormClosedEventArgs e)
            {
                InstancedForms.Remove(pFormName);
                //base.FormClosed(source, e);
            };
            return InstancedForms[pFormName];  //just created or created earlier.Return it+69
        }

        private void referencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSimpleReferences fSimpleReferences = (fSimpleReferences)GetChildInstance("fSimpleReferences");
        }

        private void simpleProductionOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fProductionOrders fProductionOrders = (fProductionOrders)GetChildInstance("fProductionOrders");
        }

        private void simpleExpeditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSimpleExpeditions fSimpleExpeditions = (fSimpleExpeditions)GetChildInstance("fSimpleExpeditions");
        }
        private void hSAReceivalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHSAReceivals fHSAReceivals = (fHSAReceivals)GetChildInstance("fHSAReceivals");
        }
    }
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string LabelPrinterAddress = "";
        public static string COD3 = "";
        public static string ProjectName = "";
    }
}
