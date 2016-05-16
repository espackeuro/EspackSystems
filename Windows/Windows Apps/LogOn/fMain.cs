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
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace LogOn
{
    public partial class fMain : Form
    {
        // Definitions for dynamically create the toolbar and accessing its panels
        public StatusStrip mDefaultStatusStrip;
        public ToolStripStatusLabel Panel1;
        public ToolStripStatusLabel Panel2;
        public ToolStripStatusLabel Panel3;
        public ToolStripStatusLabel Panel4;
        private int _zone = 0;
        public List<cUpdaterThread> UpdatingThreads = new List<cUpdaterThread>();

        // Main
        public fMain(string[] args)
        {
            InitializeComponent();

            // Customize the textbox controls 
            txtUser.Enabled = true;
            txtUser.Multiline = false;
            txtPassword.Enabled = true;
            txtPassword.Multiline = false;

            // Load the vars from the given args
            var espackArgs = CT.LoadVars(args);

            // If DB is not set in args, we assume any args are set
            if (espackArgs.DataBase == null)
            {
                espackArgs.DataBase = "SISTEMAS";
                espackArgs.User = "procesos";
                espackArgs.Password = "*seso69*";

                // Init _zone var (200, 210, 220, etc...), _pathLogonHosts (the path for the logonHosts file) and the list _content (that will contain logonHosts contents)
                //int _zone = 0;
                string _pathLogonHosts;
                List<string> _content = new List<string>();

                // Programmer rest (just for DEBUG time)
#if DEBUG
                _pathLogonHosts = "c:\\espack\\logonHosts";
                txtUser.Text = "vmocholi";
                txtPassword.Text = "303030";
#else
            _pathLogonHosts = ".\\logonHosts";
#endif

                // Get logonHosts file content       
                if (File.Exists(_pathLogonHosts))
                {
                    _content = File.ReadAllLines(_pathLogonHosts).ToList<string>();
                    var _IP = Values.gDatos.IP.GetAddressBytes();
                    if (_IP[0] == 10)
                        _zone = _IP[1];
                    else
                        _zone = _IP[2];
                }
                else
                {
                    throw new Exception("Can not find connection details");
                }

                // Put in _line the corresponding to the _zone (if (_zone==200) then _line="200|10.200.10.130|10.200.10.138|80.33.195.45|VAL")
                string _line = _content.FirstOrDefault(p => p.Substring(0, 3) == _zone.ToString());

                // DB Server is the 2nd element in _line
                espackArgs.Server = _line.Split('|')[1];

            }

            // Set the values of gDatos from the given args or default settings 
            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;

            // Connect (or try)
            try
            {
                Values.gDatos.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Error connecting database server: " + e.Message);
            }

            // Add the toolbar and set the panels texts
            AddDefaultStatusStrip();
            Panel1.Text = "You are connected to "+Values.gDatos.oServer.HostName.Replace(".local","")+"!";
            Panel2.Text = "My IP: " + Values.gDatos.IP.ToString();
            Panel3.Text = "DB Server IP: " + espackArgs.Server;

            //
        }

        protected override void OnResize (EventArgs e)
        {
            base.OnResize(e);
            var _numApps = Values.AppList.Count;
            if (_numApps == 0)
                return;
            int _numColumns = gbApps.Width / cAppBot.GROUP_WIDTH;
            tlpApps.AutoScroll = false;
            tlpApps.ColumnCount = _numColumns;
            tlpApps.RowCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(_numApps) / Convert.ToDouble(_numColumns)));
            tlpApps.AutoScroll = true;
        }

        // Add the toolbar dynamically: we did not find the way to add separators in design time.
        public void AddDefaultStatusStrip()
        {
            mDefaultStatusStrip = new StatusStrip();
            //SizeType lSize = new SizeType(118, 17);
            int lLabelWidth = 200;

            Panel1 = new ToolStripStatusLabel("Disconnected") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel1);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel2 = new ToolStripStatusLabel("My IP: None") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel2);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel3 = new ToolStripStatusLabel("DB Server IP: None") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel3);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel4 = new ToolStripStatusLabel("Place your Ad HERE!!") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel4);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Controls.Add(mDefaultStatusStrip);
            KeyPreview = true;
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            var _SP = new SP(Values.gDatos, "pLogOnUser");
            _SP.AddControlParameter("User", txtUser);
            _SP.AddParameterValue("Password", txtPassword.Text);
            _SP.AddParameterValue("Origin", "LOGON_CS");
            try
            {
                _SP.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                return;
            }
            Values.User = txtUser.Text;
            Values.Password = txtPassword.Text;

            FillServers();
            FillApps();
            DrawListApps();
            System.Threading.Thread.Sleep(5000);
            for (var i = 0; i < 4; i++)
            {
                var _thread = new cUpdaterThread();
                this.UpdatingThreads.Add(_thread);
                await _thread.Process();
            }
        }

        private void FillServers()
        {

            using (var _RS = new DynamicRS("select COD3,ServerDB,ServerDBIP,ServerShare,ServerShareIP,zone,UserShare,PasswordShare from general..sedes", Values.gDatos))
            {
                _RS.Open();
                while (!_RS.EOF)
                {
                    Values.DBServerList.Add(new cServer() { HostName = _RS["ServerDB"].ToString(), IP = IPAddress.Parse(_RS["ServerDBIP"].ToString()), COD3 = _RS["COD3"].ToString(), Type = ServerTypes.DATABASE });
                    Values.ShareServerList.Add(new cServer()
                    {
                        HostName = _RS["ServerShare"].ToString(),
                        IP = IPAddress.Parse(_RS["ServerShareIP"].ToString()),
                        COD3 = _RS["COD3"].ToString(),
                        Type = ServerTypes.DATABASE,
                        User = _RS["UserShare"].ToString(),
                        Password = _RS["PasswordShare"].ToString()
                    });
                    if (Convert.ToInt16(_RS["zone"]) == _zone)
                    {
                        Values.COD3 = _RS["COD3"].ToString();
                        Values.DBServerList.Add(new cServer() { HostName = _RS["ServerDB"].ToString(), IP = IPAddress.Parse(_RS["ServerDBIP"].ToString()), COD3 = "LOC", Type = ServerTypes.DATABASE });
                    }

                    _RS.MoveNext();
                }
                Values.DBServerList.Add(new cServer() { HostName = "DB01", IP = Dns.GetHostEntry("DB01").AddressList[0], COD3 = "OUT", Type = ServerTypes.DATABASE });
            }
        }

        private void FillApps()
        {
            using (var _RS = new DynamicRS("select Code=s.codigo,Description=s.descripcion,DB=s.base_datos,ExeName=s.app,Zone=s.sede from general..Permiso_Servicios p inner join general..servicios s on s.codigo=p.codigo where p.LoginSql= '" + Values.User + "' and general.dbo.checkFlag(flags,'OBS')=0", Values.gDatos))
            {
                _RS.Open();
                while (!_RS.EOF)
                {
                    Values.AppList.Add(new cAppBot(_RS["Code"].ToString(), _RS["Description"].ToString(), _RS["DB"].ToString(), _RS["ExeName"].ToString(),_RS["Zone"].ToString(), Values.DBServerList[_RS["Zone"].ToString()],Values.ShareServerList[Values.COD3] ));
                    _RS.MoveNext();
                }
            }
        }

        private void DrawListApps()
        {
            var _numApps = Values.AppList.Count;
            if (_numApps == 0)
                return;
            tlpApps.ColumnStyles.Clear();
            tlpApps.RowStyles.Clear();
            tlpApps.RowStyles.Add(new RowStyle(SizeType.Absolute, cAppBot.GROUP_HEIGHT));
            int _numColumns = _numApps;
            tlpApps.ColumnCount = _numColumns;
            int x = 0;
            foreach (cAppBot _app in Values.AppList)
            {
                tlpApps.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cAppBot.GROUP_WIDTH));
                tlpApps.Controls.Add(_app,x,0);
                x++;
            }
            tlpApps.AutoScroll = false;
            tlpApps.ColumnCount = gbApps.Width / cAppBot.GROUP_WIDTH; 
            tlpApps.RowCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(_numApps) / Convert.ToDouble(_numColumns)));
            tlpApps.AutoScroll = true;
        }

    }

    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string gMasterPassword = "";
        public static string User;
        public static string Password;
        public static cServerList DBServerList = new cServerList(ServerTypes.DATABASE);
        public static cServerList ShareServerList = new cServerList(ServerTypes.SHARE);
        public static cAppList AppList=new cAppList();
        public static string COD3="";
        public static cUpdateList UpdateList = new cUpdateList();
    }
}


