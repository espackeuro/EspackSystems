using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatosNet;
using System.Drawing;
using System.Collections;
using System.IO;
using FTP;
using CommonTools;
using System.Net.FtpClient;
using System.Diagnostics;
using LogonObjects.Properties;

namespace LogOnObjects
{
    public enum AppBotStatus {INIT, CHECKING, PENDING_UPDATE, UPDATED, ERROR}

    // Class cAppBot: It contains the data for each APP, some methods for the APP management (run, update, etc) and some visual controls (progress bar, button, etc).
    public class cAppBot : Control
    {
        public string Code { get; set; }
        private string Description { get; set; }
        private string DataBase { get; set; }
        private cServer DBServer { get; set; }
        public cServer ShareServer { get; set; }
        private string ExeName { get; set; }
        private cAccesoDatosNet Conn { get; set; }
        public GroupBox grpApp { get; set; }
        public Button pctApp { get; set; }
        public ProgressBar prgApp { get; set; }
        public Label lblDescriptionApp { get; set; }
        private AppBotStatus _status;
        public Bitmap AppIcon { get; set; }
        public string LocalPath { get; set; }
        public bool Special { get; set; }
        public int ProgressValue
        {
            get
            {
                return prgApp.Value;
            }
            set
            {
                prgApp.Value = value;
            }
        }
        //Events
        public event EventHandler AfterLaunch;
        protected virtual void OnAfterLaunch(EventArgs e)
        {
            EventHandler handler = AfterLaunch;
            if (handler != null)
                handler(this, e);
        }
        // For the calls from different threads
        delegate void ChangeStatusCallback(AppBotStatus pStatus);
        delegate void ChangeProgressCallback(int Value);
        // List of PENDING TO UPDATE items
        public List<cUpdateListItem> PendingItems
        {
            get
            {
                return Values.UpdateList.Where(x => x.Parent == this && x.Status == LogonItemUpdateStatus.PENDING).ToList();
            }
        }

        // List of items BEING UPDATED
        public List<cUpdateListItem> UpdatingItems
        {
            get
            {
                return Values.UpdateList.Where(x => x.Parent == this && x.Status == LogonItemUpdateStatus.UPDATING).ToList();
            }
        }
        
        // List of ALL items
        public List<cUpdateListItem> Items
        {
            get
            {
                return Values.UpdateList.Where(x => x.Parent.Code == this.Code).ToList();
            }
        }

        // List of UPDATED items
        public List<cUpdateListItem> UpdatedItems
        {
            get
            {
                return Values.UpdateList.Where(x => x.Parent == this && x.Status == LogonItemUpdateStatus.UPDATED).ToList();
            }
        }

        // Overwrite Size property (to change grpApp.Size when this.Size is changed)
        public new Size Size
        {
            get
            {
                if (grpApp != null)
                    return grpApp.Size;
                else
                    return new Size(0, 0);
            }
            set
            {
                if (grpApp != null)
                    grpApp.Size = value;

            }
        }
        
        // Status property: Sets the status value and changes the appearance of some related controls
        public AppBotStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                // Log it to the debugBox.
                if (Values.debugBox != null)
                {
                    Values.debugBox.AppendText(string.Format("App {0} status: {1}\n", Code, value));
                }
                switch (value)
                {
                    // When INIT/UPDATED -> Button enabled, ProgressBar not visible.
                    case AppBotStatus.INIT:
                    case AppBotStatus.UPDATED:
                        if (Special)
                            AppIcon = Resources.Engineering_48;
                        else
                            try
                            {
                                // First try to get the App icon from the exe file.
                                AppIcon = Icon.ExtractAssociatedIcon(LocalPath).ToBitmap();
                            }
                            catch
                            {
                                // If not possible, we use the default App icon.
                                AppIcon = Resources.Prototype_96;
                            }
                        pctApp.Image = AppIcon;
                        pctApp.Enabled = true;
                        prgApp.Visible = false;
                        break;
                    // When CHECKING -> Button disabled, ProgressBar not visible.
                    case AppBotStatus.CHECKING:
                        pctApp.Enabled = false;
                        prgApp.Visible = false;
                        break;
                    // When PENDING_UPDATE -> Button disabled, ProgressBar visible and running.
                    case AppBotStatus.PENDING_UPDATE:
                        prgApp.Style = ProgressBarStyle.Continuous;
                        prgApp.Minimum = 0;
                        prgApp.Maximum = PendingItems.Count;
                        //prgApp.MarqueeAnimationSpeed = 50;
                        prgApp.Visible = true;
                        pctApp.Enabled = false;

                        break;
                    case AppBotStatus.ERROR:
                        pctApp.Image = Resources.Forbid;
                        pctApp.Enabled = false;
                        prgApp.Visible = false;
                        break;
                }
                _status = value;
            }
        }



        // Some "constants" for the appearance of this control.
        public static int GROUP_HEIGHT = 125;
        public static int GROUP_WIDTH = 125;

        public static int PROGRESS_PADDING = 10;
        public static int PROGRESS_HEIGHT = 15;
        public static int PROGRESS_WIDTH = GROUP_WIDTH - (PROGRESS_PADDING * 2);

        public static int DESCRIPTION_PADDING = PROGRESS_PADDING;
        public static int DESCRIPTION_HEIGHT = 40;
        public static int DESCRIPTION_WIDTH = GROUP_WIDTH - (DESCRIPTION_PADDING * 2);

        public static int PICTURE_PADDING = PROGRESS_PADDING;
        public static int PICTURE_HEIGHT = GROUP_HEIGHT - DESCRIPTION_PADDING - DESCRIPTION_HEIGHT - PICTURE_PADDING * 2;
        public static int PICTURE_WIDTH = GROUP_WIDTH - PICTURE_PADDING * 2;

        // Constructor (with args) -> Calls the base constructor and sets some properties
        public cAppBot(string pCode, string pDescription, string pDatabase, string pExeName, string ServiceZone, cServer pDBServer, cServer pShareServer, string pName, bool pSpecial=false)
            : this()
        {
            Code = pCode;
            Description = pDescription;
            DataBase = pDatabase;
            ExeName = pExeName;
            LocalPath= Values.LOCAL_PATH + Code + "/" + ExeName;
            DBServer = pDBServer;
            ShareServer = pShareServer;
            lblDescriptionApp.Text = Description;
            pctApp.Image = AppIcon;
            Special = pSpecial;
            Name = pName;
            
            // Set Initial Status.
            ChangeStatus(AppBotStatus.INIT);
        }

        // Constructor (without args) -> Creates the Button, the ProgressBar, the Label and a GroupBox that will contain them. 
        public cAppBot()
        {
            grpApp = new GroupBox() { Size = new Size(GROUP_WIDTH, GROUP_HEIGHT), Location = new Point(0, 0) };
            pctApp = new Button();
            prgApp = new ProgressBar()
            {
                Size = new Size(PROGRESS_WIDTH, PROGRESS_HEIGHT),
                Location = new Point(PROGRESS_PADDING, PICTURE_HEIGHT + PICTURE_PADDING), //(GROUP_HEIGHT / 2) - (PROGRESS_HEIGHT / 2)),
                Visible = false,
                ForeColor = Color.Blue
            };
            lblDescriptionApp = new Label()
            {
                Size = new Size(DESCRIPTION_WIDTH, DESCRIPTION_HEIGHT),
                Location = new Point(DESCRIPTION_PADDING, GROUP_HEIGHT - DESCRIPTION_HEIGHT - DESCRIPTION_PADDING),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pctApp = new Button() { Size = new Size(PICTURE_WIDTH, PICTURE_HEIGHT), Location = new Point(PICTURE_PADDING, PICTURE_PADDING) };
#if DEBUG
            lblDescriptionApp.BorderStyle = BorderStyle.FixedSingle;
#else
            pctApp.FlatAppearance.BorderSize = 0;
            pctApp.FlatStyle = FlatStyle.Flat;
#endif

            // Add prgApp, pctApp and lblDescriptionApp to the grpApp GroupBox.
            grpApp.Controls.Add(prgApp);
            grpApp.Controls.Add(pctApp);
            grpApp.Controls.Add(lblDescriptionApp);
            MaximumSize = Size;
            MinimumSize = Size;
            this.Controls.Add(grpApp);

            // Add the Click event.
            pctApp.Click += PctApp_Click;
        }

        private async void PctApp_Click(object sender, EventArgs e)
        {
            if (!Special)
                await LaunchApp().ConfigureAwait(false);
        }

        // Overwrite resize event 
        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(GROUP_WIDTH, GROUP_HEIGHT);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        // CheckUpdated -> Returns true if all the files for this APP are updated.
        public Task<bool> CheckUpdated()
        {
            ChangeStatus(AppBotStatus.CHECKING);
            return Task.Run(() =>
              {
                  
                  bool _clean = true;
                  using (var client = new FtpClient())
                  {
                      client.ConnectTimeout = 120000;
                      client.Host = ShareServer.HostName;
                      client.Credentials = new NetworkCredential(ShareServer.User, ShareServer.Password);
                      client.DataConnectionType = FtpDataConnectionType.AutoActive;
                      client.Connect();
                      _clean = readDirFTP(client, "/APPS_CS/", Code.ToLower());
                  }
                  if (!_clean)
                      ChangeStatus(AppBotStatus.PENDING_UPDATE);
                  return _clean;
              });

        }
        public bool CheckUpdatedSync()
        {
            ChangeStatus(AppBotStatus.CHECKING);
            bool _clean = true;
            using (var client = new FtpClient())
            {
                client.ConnectTimeout = 60000;
                client.Host = ShareServer.HostName;
                client.Credentials = new NetworkCredential(ShareServer.User, ShareServer.Password);
                client.DataConnectionType = FtpDataConnectionType.AutoActive;
                client.Connect();
                _clean = readDirFTP(client, "/APPS_CS/", Code.ToLower());
            }
            if (!_clean)
                ChangeStatus(AppBotStatus.PENDING_UPDATE);
            return _clean;
        }
        private bool readDirFTP(FtpClient client, string basePath, string relativePath, bool _checkFiles=true)
        {
            bool _clean = true;
            //client.ChangeDirectory(basePath + relativePath);
            var _path = basePath + relativePath;




            var list = client.GetListing(_path);


            list.Where(x => x.Type == FtpFileSystemObjectType.Directory).ToList().ForEach(a => 
            {
                bool _condition = !Directory.Exists(Values.LOCAL_PATH + relativePath + "/" + a.Name);
                if (_condition == false)
                    _condition = (a.Modified != Directory.GetLastWriteTime(Values.LOCAL_PATH + relativePath + "/" + a.Name));
                if (_condition)
                {
                    Values.UpdateDir.Add(new cUpdateListItem()
                    {
                        Parent = this,
                        Item = new DirectoryItem()
                        {
                            Server = ShareServer,
                            DateCreated = a.Modified,
                            IsDirectory = true,
                            Name = ".",
                            BaseUri = new UriBuilder("ftp://" + ShareServer.HostName + "/APPS_CS/" + relativePath + "/" + a.Name).Uri
                        },
                        LocalPath = Values.LOCAL_PATH + relativePath + "/" + a.Name,
                        Status = LogonItemUpdateStatus.PENDING
                    });
                }
                _clean = readDirFTP(client, basePath, relativePath + "/" + a.Name, _condition) && _clean;
            });

            if (_checkFiles)
            {
                list.Where(x => x.Type == FtpFileSystemObjectType.File).ToList().ForEach(a =>
                {
                    if (File.Exists(Values.LOCAL_PATH + relativePath + "/" + a.Name))
                    {
                        if (File.GetLastWriteTime(Values.LOCAL_PATH + relativePath + "/" + a.Name) != a.Modified)
                        {
                            //if (Status != AppBotStatus.PENDING_UPDATE)
                            //    ChangeStatus(AppBotStatus.PENDING_UPDATE);
                            Values.UpdateList.Add(new cUpdateListItem()
                            {
                                Parent = this,
                                Item = new DirectoryItem()
                                {
                                    Server = ShareServer,
                                    DateCreated = a.Modified,
                                    IsDirectory = false,
                                    Name = a.Name,
                                    BaseUri = new UriBuilder("ftp://" + ShareServer.HostName + "/APPS_CS/" + relativePath).Uri
                                },
                                LocalPath = Values.LOCAL_PATH + relativePath + "/" + a.Name,
                                Status = LogonItemUpdateStatus.PENDING
                            });
                            _clean = false;
                        }
                    }
                    else
                    {
                        if (Status != AppBotStatus.PENDING_UPDATE)
                            ChangeStatus(AppBotStatus.PENDING_UPDATE);
                        Values.UpdateList.Add(new cUpdateListItem()
                        {
                            Parent = this,
                            Item = new DirectoryItem()
                            {
                                Server = ShareServer,
                                DateCreated = a.Modified,
                                IsDirectory = false,
                                Name = a.Name,
                                BaseUri = new UriBuilder("ftp://" + ShareServer.HostName + "/APPS_CS/" + relativePath).Uri
                            },
                            LocalPath = Values.LOCAL_PATH + relativePath + "/" + a.Name,
                            Status = LogonItemUpdateStatus.PENDING
                        });
                        _clean = false;
                    }

                });
            }
            return _clean;
        }
        //public bool CheckUpdateSync()
        //{
        //    bool _clean = true;
        //    //_clean = readDir("/APPS_CS/", Code.ToLower());
        //    using (var client = new SftpClient(ShareServer.IP.ToString(), ShareServer.User, ShareServer.Password))
        //    {
        //        client.Connect();
        //        _clean = readDirSSH(client,"/APPS_CS/", Code.ToLower(),true);
        //    }
        //    return _clean;
        //}

        private bool readDir(string basePath, string relativePath)
        {
            bool _clean = true;
            List<DirectoryItem> list;
            using (var ftp = new cFTP(ShareServer, basePath + relativePath))
            {
                list = ftp.GetDirectoryList("", getDateTimes: true);
            }
            list.Where(x => x.IsDirectory).ToList().ForEach(a =>
            {
                _clean = (readDir(basePath, relativePath + "/" + a.Name) && _clean);
            });
            list.Where(x => !x.IsDirectory).ToList().ForEach(a =>
            {
                if (File.Exists(Values.LOCAL_PATH + relativePath + "/" + a.Name))
                {
                    if (File.GetLastWriteTime(Values.LOCAL_PATH + relativePath + "/" + a.Name) != a.DateCreated)
                    {
                        if (Status != AppBotStatus.PENDING_UPDATE)
                            ChangeStatus(AppBotStatus.PENDING_UPDATE);
                        Values.UpdateList.Add(new cUpdateListItem()
                        {
                            Parent = this,
                               //ServerPath = basePath + relativePath + "/" + a.Name,
                            LocalPath = Values.LOCAL_PATH + relativePath + "/" + a.Name,
                            Item = a,
                            Status = LogonItemUpdateStatus.PENDING
                        });
                        _clean = false;
                    }
                }
                else
                {
                    if (Status != AppBotStatus.PENDING_UPDATE)
                        ChangeStatus(AppBotStatus.PENDING_UPDATE);
                    Values.UpdateList.Add(new cUpdateListItem()
                    {
                        Parent = this,
                           //ServerPath = basePath + relativePath + "/" + a.Name,
                           LocalPath = Values.LOCAL_PATH + relativePath + "/" + a.Name,
                        Item = a,
                        Status = LogonItemUpdateStatus.PENDING
                    });
                    _clean = false;
                }
            });


            return _clean;
        }


        public void ChangeStatus(AppBotStatus pStatus)
        {

            try
            {
                if (this.prgApp.InvokeRequired)
                {
                    ChangeStatusCallback a = new ChangeStatusCallback(ChangeStatus);
                    this.Invoke(a, new object[] { pStatus });
                }
                else
                {
                    Status = pStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeProgress(int Value)
        {

            try
            {
                if (this.prgApp.InvokeRequired)
                {
                    ChangeProgressCallback a = new ChangeProgressCallback(ChangeProgress);
                    this.Invoke(a, new object[] { Value });
                }
                else
                {
                    prgApp.Value=Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //ChangeProgressCallback(int Value);
        //private bool readDirSSH(SftpClient client, string basePath, string relativePath, bool checkFiles)
        //{
        //    bool _clean = true;
        //    client.ChangeDirectory(basePath + relativePath);
        //    var list = client.ListDirectory(".").Where(x => x.Name != "." && x.Name != "..");
        //    foreach (var item in list)
        //    {
        //        if (item.Attributes.IsDirectory)
        //        {
        //            var _check = false;
        //            var _condition = !Directory.Exists(Values.LOCAL_PATH + relativePath + "/" + item.Name);
        //            if (_condition == false)
        //                _condition = (item.LastWriteTime != Directory.GetLastWriteTime(Values.LOCAL_PATH + relativePath + "/" + item.Name));
        //            if (_condition)
        //            {
        //                Values.UpdateDir.Add(new cUpdateListItem()
        //                {
        //                    Parent = this,
        //                    Item = new DirectoryItem()
        //                    {
        //                        Server = ShareServer,
        //                        DateCreated = item.LastWriteTime,
        //                        IsDirectory = item.IsDirectory,
        //                        Name = item.Name,
        //                        BaseUri = new UriBuilder("ftp://" + ShareServer.IP.ToString() + "/APPS_CS/" + relativePath + "/").Uri
        //                    },
        //                    LocalPath = Values.LOCAL_PATH + relativePath + "/" + item.Name,
        //                    Status = LogonItemUpdateStatus.PENDING
        //                });
        //                _check = true;
        //            }
        //            _clean = readDirSSH(client, basePath, relativePath + "/" + item.Name,_check) && _clean;
        //        }
        //        else if (checkFiles)
        //        {
        //            if (File.Exists(Values.LOCAL_PATH + relativePath + "/" + item.Name))
        //            {
        //                if (File.GetCreationTime(Values.LOCAL_PATH + relativePath + "/" + item.Name) != item.Attributes.LastWriteTime)
        //                {
        //                    if (Status != AppBotStatus.PENDING_UPDATE)
        //                        ChangeStatus(AppBotStatus.PENDING_UPDATE);
        //                    Values.UpdateList.Add(new cUpdateListItem()
        //                    {
        //                        Parent = this,
        //                        Item = new DirectoryItem()
        //                        {
        //                            Server = ShareServer,
        //                            DateCreated = item.LastWriteTime,
        //                            IsDirectory = item.IsDirectory,
        //                            Name = item.Name,
        //                            BaseUri = new UriBuilder("ftp://" + ShareServer.IP.ToString() + "/APPS_CS/" + relativePath + "/").Uri
        //                        },
        //                        LocalPath = Values.LOCAL_PATH + relativePath + "/" + item.Name,
        //                        Status = LogonItemUpdateStatus.PENDING
        //                    });
        //                    _clean = false;
        //                }
        //            }
        //            else
        //            {
        //                if (Status != AppBotStatus.PENDING_UPDATE)
        //                    ChangeStatus(AppBotStatus.PENDING_UPDATE);
        //                Values.UpdateList.Add(new cUpdateListItem()
        //                {
        //                    Parent = this,
        //                    Item = new DirectoryItem()
        //                    {
        //                        Server = ShareServer,
        //                        DateCreated = item.LastWriteTime,
        //                        IsDirectory = item.IsDirectory,
        //                        Name = item.Name,
        //                        BaseUri = new UriBuilder("ftp://" + ShareServer.IP.ToString() + "/APPS_CS/" + relativePath + "/").Uri
        //                    },
        //                    LocalPath = Values.LOCAL_PATH + relativePath + "/" + item.Name,
        //                    Status = LogonItemUpdateStatus.PENDING
        //                });
        //                _clean = false;
        //            }

        //        }
        //    }
        //    return _clean;
        //}

        public async Task LaunchApp()
        {
            

            if (!Special)
            {
                ChangeStatus(AppBotStatus.PENDING_UPDATE);
                //if (!await CheckUpdated().ConfigureAwait(false))
                if (!await CheckUpdated())
                {
                    Application.DoEvents();
                    Values.ActiveThreads++;
                    var _thread = new cUpdaterThread(Values.debugBox, Values.ActiveThreads);
                    // launch task not async
                    _thread.Process();
                }
                ChangeStatus(AppBotStatus.UPDATED);
            }
            if (DBServer.HostName != Values.gDatos.Server && DBServer.User != "procesos")
            {
                var _datos = new cAccesoDatosNet();
                _datos.User = Values.gDatos.User;
                _datos.Password = Values.gDatos.Password;
                _datos.DataBase = Values.gDatos.DataBase;
                _datos.Server = DBServer.HostName;
                // check the password in the new server
                var _SP = new SP(_datos, "pLogOnUser");
                _SP.AddParameterValue("User", DBServer.User);
                _SP.AddParameterValue("Password", DBServer.Password);
                _SP.AddParameterValue("Origin", "LOGON_CS");
                try
                {
                    await _SP.Execute();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nIf you have recently changed your password, wait a couple of minutes before opening this app.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.FileName = LocalPath;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.Arguments = "/srv=" + DBServer.HostName + " /db=" + DataBase + " /usr=" + DBServer.User + " /pwd=" + DBServer.Password + " /loc=OUT /app=" + Name;

            try
            {
                Process exeProcess = Process.Start(startInfo);
                OnAfterLaunch(new EventArgs());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }





    // Class cAppList
    public class cAppList: IEnumerable<cAppBot>, IDisposable
    {
        private List<cAppBot> AppList { get; set; } = new List<cAppBot>();

        public List<cAppBot> CheckingApps
        {
            get
            {
                return AppList.Where(x => x.Status == AppBotStatus.CHECKING).ToList();
            }
        }

        public List<cAppBot> PendingApps
        {
            get
            {
                return AppList.Where(x => x.Status == AppBotStatus.PENDING_UPDATE).ToList();
            }
        }
        public cAppBot this[string pCode]
        {
            get
            {
                return AppList.FirstOrDefault(x => x.Code == pCode);
            }
            
        }
        public cAppBot this[int pKey]
        {
            get
            {
                return AppList[pKey];
            }

        }
        public int Count
        {
            get
            {
                return AppList.Count;
            }
        }
        public void Add(cAppBot pApp)
        {
            AppList.Add(pApp);
        }

        public void Clear()
        {
            AppList.Clear();
        }


        public IEnumerator<cAppBot> GetEnumerator()
        {
            return ((IEnumerable<cAppBot>)AppList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<cAppBot>)AppList).GetEnumerator();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    AppList.ToList().ForEach(x => x.Dispose());
                    AppList.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~cAppList() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
            disposedValue = false;
        }
        #endregion
    }



}
