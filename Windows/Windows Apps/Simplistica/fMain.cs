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
    public partial class fMain : Form
    {
        public fMain(string[] args)
        {
            InitializeComponent();
            this.Text = string.Format("Simplistica Build {0} - ({1:yyyyMMdd})*", Assembly.GetExecutingAssembly().GetName().Version.ToString(), CT.GetBuildDateTime(Assembly.GetExecutingAssembly()));
            var espackArgs = CT.LoadVars(args);
            //Values.gDatos.DataBase = "Sistemas";//espackArgs.DataBase;
            //Values.gDatos.Server = "192.168.200.7";//espackArgs.Server;
            //Values.gDatos.User = "sa";//espackArgs.User;
            //Values.gDatos.Password = "5380"; //espackArgs.Password;

            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;
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
            fSimpleReceivals fSimpleReceivals = new fSimpleReceivals();
            fSimpleReceivals.MdiParent = this;
            fSimpleReceivals.Show();
        }

        private void printRepairsUnitLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPrintUnitLabels fPrintUnitLabels = new fPrintUnitLabels();
            fPrintUnitLabels.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSettings fSettings = new fSettings();
            fSettings.ShowDialog();
        }
    }
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string LabelPrinterAddress = "";
        public static string COD3 = "";

    }
}
