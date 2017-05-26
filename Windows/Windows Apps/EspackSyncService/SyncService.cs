using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CommonTools;
using AccesoDatosNet;

namespace EspackSyncService
{



    public partial class SyncServiceClass : ServiceBase
    {
        public SyncServiceClass(string[] args)
        {
            InitializeComponent();
        }

        private List<ISyncedService> SyncedServices = new List<ISyncedService>();
        //private bool IsRunning = false;
        private System.Timers.Timer timer;

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            //Services definition

            Values.Servers.ToList().ForEach(pair =>
            {
                switch (pair.Key)
                {
                    //case "NEXTCLOUD":
                    //    var NCService = new NextCloudService()
                    //    {
                    //        ServiceCredentials = new EspackCredentials()
                    //        {
                    //            User = "system",
                    //            Password = "*seso69*".ToSecureString()
                    //        },
                    //        ServerName = pair.Value
                    //    };
                    //    SyncedServices.Add(NCService);
                    //    EventLog.WriteEntry(string.Format("Added {0} Service to server {1}", pair.Key, pair.Value));
                    //    break;
                    case "DOMAIN":
                        SyncedServices.Add(new ADService()
                        {
                            ServiceCredentials = new EspackCredentials()
                            {
                                User = "SYSTEMS\\administrador",
                                Password = "Y?D6d#b@".ToSecureString()
                            },
                            ServerName = pair.Value
                        });
                        EventLog.WriteEntry(string.Format("Added {0} Service to server {1}", pair.Key, pair.Value));
                        break;
                }

            });


            // Timer creation
            EventLog.WriteEntry("Service Espack Sync Started.");
            // Set up a timer to trigger every minute.  
            timer = new System.Timers.Timer();
            timer.Interval = Values.PollingTime * 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            //await Synchronize();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }


        private async Task Synchronize()
        {
            using (var _RS = new DynamicRS("select UserCode, Name, Surname1,Password,Zone, MainCOD3, emailAddress, localDomain, flags, desCOD3  from vUsers where dbo.CheckFlag(flags,'CHANGED')=1", Values.gDatos))
            {
                try
                {
                    await _RS.OpenAsync();
                    //_RS.Open();
                } catch (Exception ex)
                {
                    EventLog.WriteEntry(string.Format("Error accesing database: {0}", ex.Message), EventLogEntryType.Error);
                    return;
                }
                foreach (var r in _RS.ToList())
                //_RS.ToList().ForEach(async r =>
                {
                    
                    var flags = r["flags"].ToString().Split('|');
                    int Error = 0;
                    foreach (var s in SyncedServices)
                    //SyncedServices.ForEach(async s =>
                    { 
                        if (flags.Contains(s.ServiceName))
                        {
                            try
                            {
                                var _alias = Values.DomainList.Select(d => string.Format("'smtp:{0}@{1}'", r["UserCode"].ToString(), d));
                                _alias = _alias.Concat(new string[]{ string.Format("'smtp:{0}@{1}'", r["UserCode"].ToString(), r["localDomain"].ToString())});
                                await s.Interact(r["UserCode"].ToString(), r["Name"].ToString(), r["Surname1"].ToString(), r["Zone"].ToString(), r["Password"].ToString(), r["emailAddress"].ToString(), r["MainCOD3"].ToString(), r["desCOD3"].ToString(), flags, _alias.ToArray());
                                EventLog.WriteEntry(string.Format("User {0} from {1} was modified correctly in service {2}", r["UserCode"].ToString(), r["MainCOD3"].ToString(), s.ServiceName));
                                Error = 0;
                            }
                            catch (Exception ex)
                            {
                                EventLog.WriteEntry(string.Format("User {0} from {1} was NOT modified correctly in service {2}. \nError message was {3}", r["UserCode"].ToString(), r["MainCOD3"].ToString(), s.ServiceName, ex.Message), EventLogEntryType.Error);
                                Error = 1;
                            }
                        }
                    };
                    var _SP = new SP(Values.gDatos, "pUppUserFlagCheckedClear");
                    _SP.AddParameterValue("UserCode", r["UserCode"].ToString());
                    _SP.AddParameterValue("Error", Error);
                    try
                    {
                        await _SP.ExecuteAsync();
                        if (_SP.LastMsg != "OK")
                            throw new Exception(_SP.LastMsg);
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry(string.Format("User {0} from {1} was NOT unflagged correctly. \nError message was {3}", r["UserCode"].ToString(), r["MainCOD3"].ToString(), ex.Message), EventLogEntryType.Error);
                    }
                };
            }
            using (var _RS = new StaticRS("Select address from MAIL..aliasCAB where dbo..CheckFlag(flags,'CHANGED')=1", Values.gDatos))
            {
                try
                {
                    await _RS.OpenAsync();
                    //_RS.Open();
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(string.Format("Error accesing database MAIL: {0}", ex.Message), EventLogEntryType.Error);
                    return;
                }

                foreach (var r in _RS.ToList())
                //_RS.ToList().ForEach(async r =>
                {

                }
            }
        }

        private async Task SynchronizeUser()

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            await Synchronize();
            timer.Start();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
        protected override void OnStop()
        {
            EventLog.WriteEntry("Service Espack Sync Stopped.");
        }

        protected override void OnContinue()
        {
            EventLog.WriteEntry("In OnContinue.");
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }



    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public long dwServiceType;
        public ServiceState dwCurrentState;
        public long dwControlsAccepted;
        public long dwWin32ExitCode;
        public long dwServiceSpecificExitCode;
        public long dwCheckPoint;
        public long dwWaitHint;
    };
}
