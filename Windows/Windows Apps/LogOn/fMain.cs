﻿using AccesoDatosNet;
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

namespace LogOn
{
    public partial class fMain : Form
    {
        // Definitions for dynamically create the Toolbar 
        public StatusStrip mDefaultStatusStrip;
        public ToolStripStatusLabel Panel1;
        public ToolStripStatusLabel Panel2;
        public ToolStripStatusLabel Panel3;
        public ToolStripStatusLabel Panel4;

        public fMain(string[] args)
        {
            InitializeComponent();

            // Customize the textbox controls 
            txtUser.Enabled = true;
            txtUser.Multiline = false;
            txtPassword.Enabled = true;
            txtPassword.Multiline = false;

            var espackArgs = CT.LoadVars(args);

            if (espackArgs.DataBase == null)
            {
                espackArgs.DataBase = "SISTEMAS";
                espackArgs.User = "procesos";
                espackArgs.Password = "*seso69*";


                int _zone = 0;
                string _pathLogonHosts;

#if DEBUG
                _pathLogonHosts = "c:\\espack\\logonHosts";
                txtUser.Text = "dvalles";
                txtPassword.Text = "*Kru0DMar*";
#else
            _pathLogonHosts = ".\\logonHosts";
#endif

                List<string> _content = new List<string>();

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

                string _line = _content.FirstOrDefault(p => p.Substring(0, 3) == _zone.ToString());


                // "200|10.200.10.130|10.200.10.138|80.33.195.45|VAL"

                espackArgs.Server = _line.Split('|')[1];

            }

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
            catch(Exception e)
            {
                throw new Exception("Error connecting database server: "+e.Message);
            }
            AddDefaultStatusStrip();

            Panel1.Text = "Connected!";
            Panel2.Text = "My IP: " + Values.gDatos.IP.ToString();
            Panel3.Text = "DB Server IP: " + espackArgs.Server;


        }

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

            Panel3 = new ToolStripStatusLabel("DB Server IP: None") { AutoSize =true };
            mDefaultStatusStrip.Items.Add(Panel3);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Panel4 = new ToolStripStatusLabel("Place your Ad HERE!!") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(Panel4);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Controls.Add(mDefaultStatusStrip);
            KeyPreview = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var _SP = new SP(Values.gDatos, "pLogOnUser");
            _SP.AddControlParameter("User", txtUser);
            _SP.AddParameterValue("Password", txtPassword.Text);
            _SP.AddParameterValue("Origin", "LOGON_CS");
            try
            {
                _SP.Execute();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                return;
            }
            Values.User = txtUser.Text;
            Values.Password = txtPassword.Text;

            FillServers();

            //var _SQL= "select * from general..Permiso_Servicios p inner join general..servicios s on s.codigo=p.codigo where p.LoginSql= '" + + "' and general.dbo.checkFlag(flags,'OBS')=0"
        }

        private void FillServers()
        {

            using (var _RS = new DynamicRS("select COD3,ServerDB,ServerDBIP,ServerShare,ServerShareIP from general..sedes", Values.gDatos))
            {
                _RS.Open();
                while (!_RS.EOF)
                {
                    Values.DBServerList.Add(new cServer() { HostName = _RS["ServerDB"].ToString(), IP = IPAddress.Parse(_RS["ServerDBIP"].ToString()), COD3 = _RS["COD3"].ToString(), Type = ServerTypes.DATABASE });
                    Values.ShareServerList.Add(new cServer() { HostName = _RS["ServerShare"].ToString(), IP = IPAddress.Parse(_RS["ServerShareIP"].ToString()), COD3 = _RS["COD3"].ToString(), Type = ServerTypes.DATABASE });
                    _RS.MoveNext();
                }
            }
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
    }
}


