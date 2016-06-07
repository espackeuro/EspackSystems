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
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Owncloud;
using LogOnObjects;
using System.Reflection;
using DiverseControls;
using CommonToolsWin;

namespace LogOn
{


    public partial class fMain : Form
    {
        
        // Definitions for dynamically create the toolbar and accessing its panels
        public StatusStrip mDefaultStatusStrip;
        public ToolStripStatusLabel Panel1;
        public ToolStripStatusLabel Panel2;
        public ToolStripStatusLabel Panel3;
        public ToolStripStatusLabel PanelName;
        public ScrollingStatusLabel PanelQOTD;
        public ToolStripStatusLabel PanelTime;
        private System.Timers.Timer _timer;
        private int _time;
        private int _zone = 0;
        public List<cUpdaterThread> UpdatingThreads = new List<cUpdaterThread>();
        public const int NUMTHREADS = 8;
        
        delegate void gbDebugCallBack(Control c);
        delegate void LogOnChangeStatusCallBack(LogOnStatus l);
        delegate void ClearListAppsCallBack();

        private LogOnStatus previousStatus { get; set; }


        private void LogOnChangeStatus(LogOnStatus pStatus)
        {
            if (this.InvokeRequired)
            {
                LogOnChangeStatusCallBack a = new LogOnChangeStatusCallBack(LogOnChangeStatus);
                this.Invoke(a, new object[] { pStatus });
            }
            else
            {
                Status=pStatus;
            }
        }

        // Main
        public fMain(string[] args)
        {

            InitializeComponent();
            this.Text=string.Format("LogOn Build {0} - ({1:yyyyMMdd})*" , Assembly.GetExecutingAssembly().GetName().Version.ToString(),CT.GetBuildDateTime(Assembly.GetExecutingAssembly()));
            // Customize the textbox controls 
            txtUser.Multiline = false;
            txtPassword.Multiline = false;
            txtNewPassword.Multiline = false;
            txtNewPasswordConfirm.Multiline = false;
            txtNewPIN.Multiline = false;
            txtNewPINConfirm.Multiline = false;
            //timer control
            _time = 0;
            _timer = new System.Timers.Timer() { Interval = 1000, Enabled = false };
            _timer.Elapsed += _timer_Elapsed;

            // Add the toolbar and set the panels texts
            AddDefaultStatusStrip();

            LogOnChangeStatus(LogOnStatus.INIT);

#if DEBUG
            Values.debugBox = new DebugTextbox();
            Values.debugBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            Values.debugBox.Location = new System.Drawing.Point(3, 411);
            Values.debugBox.Multiline = true;
            Values.debugBox.Size = new System.Drawing.Size(300, 98);
            Values.debugBox.TabIndex = 3;
            gbDebugAdd(Values.debugBox);
            txtUser.Text = "dvalles";
            txtPassword.Text = "*Kru0DMar*";
#endif

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
#else
            _pathLogonHosts = "logonHosts";
#endif
                _pathLogonHosts = Values.LOCAL_PATH +"logon/logonHosts";
                // Get logonHosts file content       
                if (File.Exists(_pathLogonHosts))
                {
                    _content = File.ReadAllLines(_pathLogonHosts).ToList<string>();
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
            var _IP = Values.gDatos.IP.GetAddressBytes();
            if (_IP[0] == 10)
                _zone = _IP[1];
            else
                _zone = _IP[2];
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


            Panel1.Text = "You are connected to "+Values.gDatos.oServer.HostName.Replace(".local","")+"!";
            Panel2.Text = "My IP: " + Values.gDatos.IP.ToString();
            Panel3.Text = "DB Server IP: " + espackArgs.Server;
            string[] FilesToUpdate = new string[] { "logonHosts", "logonloader.exe", "logonloader.exe.config" };
            // Check LogOnLoader update
#if !DEBUG 
            FilesToUpdate.ToList().ForEach(x =>
            {
                if (File.Exists(Values.LOCAL_PATH + x))
                {
                    if (File.GetLastWriteTime(Values.LOCAL_PATH + x) != File.GetLastWriteTime(Values.LOCAL_PATH + "logon/"+x))
                    {
                        File.Delete(Values.LOCAL_PATH + x);
                        File.Copy(Values.LOCAL_PATH + "logon/"+x, Values.LOCAL_PATH + x);
                    }
                }
                else
                {
                    File.Copy(Values.LOCAL_PATH + "logon/" + x, Values.LOCAL_PATH + x);
                }
            });
#endif


        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _time--;
            if (_time == 0)
            {
                LogOnChangeStatus(LogOnStatus.INIT);
                return;
            }
            var t = TimeSpan.FromSeconds(_time);
            PanelTime.Text=t.ToString(@"mm\:ss");
        }

        protected override void OnResize (EventArgs e)
        {
            base.OnResize(e);
            if (this.WindowState!= FormWindowState.Minimized)
            {
                var _numApps = Values.AppList.Count;
                if (_numApps == 0)
                    return;
                int _numColumns = gbApps.Width / cAppBot.GROUP_WIDTH;
                tlpApps.AutoScroll = false;
                tlpApps.ColumnCount = _numColumns;
                tlpApps.RowCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(_numApps) / Convert.ToDouble(_numColumns)));
                tlpApps.AutoScroll = true;
            }
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

            PanelName = new ToolStripStatusLabel("") { AutoSize = true };
            mDefaultStatusStrip.Items.Add(PanelName);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            PanelQOTD = new ScrollingStatusLabel("") {AutoSize=false, Width=350 , Interval=150};
            mDefaultStatusStrip.Items.Add(PanelQOTD);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            PanelTime = new ToolStripStatusLabel("") { AutoSize = false, Width = 100 };
            mDefaultStatusStrip.Items.Add(PanelTime);
            //mDefaultStatusStrip.Items.Add(new ToolStripSeparator());

            Controls.Add(mDefaultStatusStrip);
            KeyPreview = true;
        }

        public enum LogOnStatus { INIT, CONNECTING, CONNECTED, CHANGE_PASSWORD, CHANGING_PASSWORD, ERROR }
        private LogOnStatus _status;

        public LogOnStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                switch (value)
                {
                    case LogOnStatus.INIT:
                        _time = 300;
                        txtUser.Text = "";
                        txtPassword.Text = "";
                        txtUser.Enabled = true;
                        txtPassword.Enabled = true;
                        btnOk.Text= "Connect";
                        btnOk.Enabled = true;
                        btnChange.Visible = false;
                        txtNewPassword.Visible = false;
                        txtNewPasswordConfirm.Visible = false;
                        txtNewPIN.Visible = false;
                        txtNewPINConfirm.Visible = false;
                        btnOKChange.Visible = false;
                        btnCancelChange.Visible = false;
                        ActiveControl = txtUser;
                        _timer.Stop();
                        ClearListApps();
                        PanelName.Text = "";
                        PanelQOTD.Text = "";
                        PanelTime.Text = "";
                        
                        break;
                    case LogOnStatus.CONNECTING:
                        txtUser.Enabled = false;
                        txtPassword.Enabled = false;
                        btnOk.Text = "Connecting";
                        btnOk.Enabled = false;
                        btnChange.Visible = false;
                        txtNewPassword.Visible = false;
                        txtNewPasswordConfirm.Visible = false;
                        txtNewPIN.Visible = false;
                        txtNewPINConfirm.Visible = false;
                        btnOKChange.Visible = false;
                        btnCancelChange.Visible = false;
                        break;
                    case LogOnStatus.CONNECTED:
                        txtUser.Enabled = false;
                        txtPassword.Enabled = false;
                        btnOk.Text = "Disconnect";
                        btnOk.Enabled = true;
                        btnChange.Visible = true;
                        txtNewPassword.Visible = false;
                        txtNewPasswordConfirm.Visible = false;
                        txtNewPIN.Visible = false;
                        txtNewPINConfirm.Visible = false;
                        btnOKChange.Visible = false;
                        btnCancelChange.Visible = false;
                        _timer.Start();
                        break;
                    case LogOnStatus.CHANGE_PASSWORD:
                        txtUser.Enabled = false;
                        txtPassword.Enabled = false;
                        btnOk.Text = "Disconnect";
                        btnOk.Enabled = false;
                        btnChange.Visible = false;
                        txtNewPassword.Visible = true;
                        txtNewPasswordConfirm.Visible = true;
                        txtNewPassword.Enabled = true;
                        txtNewPasswordConfirm.Enabled = true;
                        txtNewPIN.Visible = true;
                        txtNewPINConfirm.Visible = true;
                        txtNewPIN.Enabled = true;
                        txtNewPINConfirm.Enabled = true;
                        btnOKChange.Visible = true;
                        btnCancelChange.Visible = true;
                        btnOKChange.Enabled = true;
                        btnCancelChange.Enabled = true;
                        txtNewPassword.Text = "";
                        txtNewPasswordConfirm.Text = "";
                        txtNewPIN.Text = "";
                        txtNewPINConfirm.Text = "";
                        ActiveControl = txtNewPassword;
                        break;
                    case LogOnStatus.CHANGING_PASSWORD:
                        txtUser.Enabled = false;
                        txtPassword.Enabled = false;
                        btnOk.Text = "Disconnect";
                        btnOk.Enabled = false;
                        btnChange.Visible = false;
                        txtNewPassword.Visible = true;
                        txtNewPasswordConfirm.Visible = true;
                        txtNewPassword.Enabled = false;
                        txtNewPasswordConfirm.Enabled = false;
                        txtNewPIN.Visible = true;
                        txtNewPINConfirm.Visible = true;
                        txtNewPIN.Enabled = false;
                        txtNewPINConfirm.Enabled = false;
                        btnOKChange.Visible = true;
                        btnCancelChange.Visible = true;
                        btnOKChange.Enabled = false;
                        btnCancelChange.Enabled = false;
                        break;
                    case LogOnStatus.ERROR:
                        txtUser.Enabled = false;
                        txtPassword.Enabled = false;
                        btnOk.Text = "...";
                        btnOk.Enabled = false;
                        btnChange.Visible = false;
                        txtNewPassword.Visible = false;
                        txtNewPasswordConfirm.Visible = false;
                        txtNewPIN.Visible = false;
                        txtNewPINConfirm.Visible = false;
                        btnOKChange.Visible = false;
                        btnCancelChange.Visible = false;
                        break;
                }
                _status = value;
            }
        }

        private void gbDebugAdd(Control c)
        {
            if (gbDebug.InvokeRequired)
            {
                gbDebugCallBack a = new gbDebugCallBack(gbDebugAdd);
                Invoke(a, new object[] { c });
            } else
            {
                gbDebug.Controls.Add(c);
            }
        }

        private void FillApps()
        {

            using (var _RS = new DynamicRS("select Code=ServiceCode,Description,DB,ExeName=s.app,Zone=s.location from general..Permiso_Servicios p inner join services s on s.ServiceCode=p.codigo where p.LoginSql= '" + Values.User + "' and general.dbo.checkFlag(flags,'OBS')=0", Values.gDatos))
            {
                _RS.Open();
                _RS.ToList()
                    //.Where(a => a["Code"].ToString()=="LOGISTICA").ToList()
                    .ForEach(x =>
                {
                    Values.AppList.Add(new cAppBot(x["Code"].ToString(), x["Description"].ToString(), x["DB"].ToString(), x["ExeName"].ToString(), x["Zone"].ToString(), Values.DBServerList[x["Zone"].ToString()], Values.ShareServerList[Values.COD3]));
                });
            }
            Values.AppList.Add(new cAppBot("Tools", "TOOLS", "", "", "", null, Values.ShareServerList[Values.COD3], true));
            //Values.AppList.Add(new cAppBot("lib", "lib", "", "", "", null, Values.ShareServerList[Values.COD3], true));
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
                //bool _clean = await _app.CheckUpdate();
                //bool _clean = _app.CheckUpdateSync();
                //if (_clean)
                //    _app.Activate();
                x++;
            }
            tlpApps.AutoScroll = false;
            tlpApps.ColumnCount = gbApps.Width / cAppBot.GROUP_WIDTH; 
            tlpApps.RowCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(_numApps) / Convert.ToDouble(_numColumns)));
            tlpApps.AutoScroll = true;
        }

        private void ClearListApps()
        {
            if (this.InvokeRequired)
            {
                ClearListAppsCallBack a = new ClearListAppsCallBack(ClearListApps);
                this.Invoke(a);
            }
            else
            {
                //Values.AppList.ToList().ForEach(x => x.Dispose());
                Values.AppList.Dispose();
                tlpApps.ColumnCount = 0;
                tlpApps.RowCount = 0;
            }
        }

        private async Task CheckUpdatableApps()
        {
            var task = Task.Run(() =>
            Values.AppList.ToList().ForEach(async x =>
            {
                if (await x.CheckUpdated().ConfigureAwait(false))
                //if (await x.CheckUpdate())
                    x.ChangeStatus(AppBotStatus.UPDATED);

            }));
            
            await task.ConfigureAwait(false);
        }
        private async void btnOk_Click(object sender, EventArgs e)
        {
            previousStatus = Status;
            if (Status == LogOnStatus.INIT)
            {
                LogOnChangeStatus(LogOnStatus.CONNECTING);
                SqlParameter _flags;
                SqlParameter _fullName;
                var _SP = new SP(Values.gDatos, "pLogOnUser");
                _SP.AddControlParameter("User", txtUser);
                _SP.AddControlParameter("Password", txtPassword);
                _SP.AddParameterValue("Origin", "LOGON_CS");
                _SP.AssignOutputParameterContainer("flags",out _flags);
                _SP.AssignOutputParameterContainer("FullName", out _fullName);
                try
                {
                    _SP.Execute();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Text = "";
                    Status = LogOnStatus.INIT;
                    return;
                }
                Values.userFlags = _flags.Value.ToString().Split('|').Where(x => x!="").ToList() ;
                Values.FullName = _fullName.Value.ToString();
                PanelName.Text = Values.FullName;

                var _SPQuote = new SP(Values.gDatos, "pGetQOTD");
                SqlParameter _quote;
                _SPQuote.AssignOutputParameterContainer("quote", out _quote);
                try
                {
                    _SPQuote.Execute();
                    PanelQOTD.Text = _quote.Value.ToString();
                }
                catch (Exception ex)
                {
                    PanelQOTD.Text = "";
                }
                
                if (_SP.LastMsg == "OK/CHANGE")
                {
                    this.Status = LogOnStatus.CHANGE_PASSWORD;
                    CTWin.MsgError("Your password has expired, please change it.");
                    return;
                }
                Values.User = txtUser.Text;
                Values.Password = txtPassword.Text;
                Values.FillServers(_zone);
                FillApps();
                DrawListApps();
                await CheckUpdatableApps().ConfigureAwait(false);


                while (Values.AppList.CheckingApps.Count != 0)
                {
                    System.Threading.Thread.Sleep(500);
                }

                if (Values.AppList.PendingApps.Count != 0)
                {
                    for (var i = 0; i < NUMTHREADS; i++)
                    {
                        Values.ActiveThreads++;
                        var _thread = new cUpdaterThread(Values.debugBox, Values.ActiveThreads);
                        this.UpdatingThreads.Add(_thread);
                        new Thread(new ThreadStart(_thread.Process)).Start();

                    }
                }
                LogOnChangeStatus(LogOnStatus.CONNECTED);
            }
            else
            {
                LogOnChangeStatus(LogOnStatus.INIT);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            previousStatus = Status;
            LogOnChangeStatus(LogOnStatus.CHANGE_PASSWORD);
        }

        private async void btnOKChange_Click(object sender, EventArgs e)
        {
            Status = LogOnStatus.CHANGING_PASSWORD;
            if (txtNewPassword.Text != txtNewPasswordConfirm.Text || txtNewPIN.Text != txtNewPINConfirm.Text)
            {
                MessageBox.Show("Passwords and PINs must match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Status = LogOnStatus.CHANGE_PASSWORD;
                return;
            }
            
            // Call the SP for Password/PIN change
            var _SP = new SP(Values.gDatos, "pLogOnUser");
            _SP.AddControlParameter("User", txtUser);
            _SP.AddParameterValue("Password", txtPassword.Text);
            _SP.AddParameterValue("Origin", "LOGON_CS");
            _SP.AddParameterValue("NewPassword", txtNewPassword.Text);
            _SP.AddParameterValue("NewPIN", txtNewPIN.Text);
            try
            {
                _SP.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                Status = LogOnStatus.CHANGE_PASSWORD;
                return;
            }
            if (_SP.LastMsg.Substring(0, 2) != "OK")
            {
                CTWin.MsgError(_SP.LastMsg);
                return;
            }
            //checking OWNCLOUD settings
            if (Values.userFlags.Contains("OWNCLOUD"))
            {
                string _master="";
                using (var _RS= new DynamicRS("select master=cast(dbo.fGetContextInfo() as nvarchar)", Values.gDatos))
                {
                    _RS.Open();
                    _master = _RS["master"].ToString();
                }
                bool _result= await OCCommands.CheckUser(txtUser.Text, _master);
                PanelName.Text = (_result ? "Owncloud user found" : "Owncoud user not found");
                if (!_result)
                {
                    _result = await OCCommands.AddUser(txtUser.Text, txtNewPassword.Text, Values.FullName, Values.COD3, _master);
                    PanelName.Text = (_result ? "Owncloud user created correctly" : "ERROR creating Owncloud user!!!");
                }
                else
                {
                    _result = await OCCommands.UppUser(txtUser.Text, txtNewPassword.Text, Values.FullName, "", _master);
                    PanelName.Text = (_result ? "Owncloud user updated correctly" : "ERROR updating Owncloud user!!!");
                }
            }

            MessageBox.Show("Password and PIN changed OK", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PanelName.Text = Values.FullName;
            Values.User = txtUser.Text;
            Values.Password = txtPassword.Text = txtNewPassword.Text;
            LogOnChangeStatus(previousStatus);
            txtUser.Text = Values.User;
            txtPassword.Text = Values.Password;
            if (Status==LogOnStatus.INIT)
                btnOk.PerformClick();
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            LogOnChangeStatus(previousStatus);
            //LogOnChangeStatus(LogOnStatus.CONNECTED);
        }

        // Capture some pressed key
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // ENTER Key
            if (e.KeyCode == Keys.Enter)
            {
                // Focus on Password Textbox -> Press OK button
                if (txtPassword.ContainsFocus)
                {
                    btnOk.PerformClick();
                }
                // Focus on PINConfirm Textbox -> Press OKChange button
                else if (txtNewPINConfirm.ContainsFocus)
                {
                    btnOKChange.PerformClick();
                }
                // Focus on any other control in gbCred or gbChangePassword -> Send TAB Key
                else if (gbCred.ContainsFocus || gbChangePassword.ContainsFocus)
                {
                    SendKeys.Send("{tab}");
                }
                // Any other case -> Do base OnKeyDown
                else
                    base.OnKeyDown(e);
            }

            // ESC Key
            if (e.KeyCode == Keys.Escape)
            {
                // Focus on gbCred controls (when not connected) -> Clean the credentials fields (done in LogOnStatus.INIT)
                if (Status==LogOnStatus.INIT && gbCred.ContainsFocus)
                    LogOnChangeStatus(LogOnStatus.INIT);
                // Focus on gbChangePassword controls -> Cancel the ChangePassword status
                else if (gbChangePassword.ContainsFocus)
                    LogOnChangeStatus(LogOnStatus.CONNECTED);
            }

        }
    }



   

    

}


