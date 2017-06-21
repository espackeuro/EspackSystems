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
using BaseService;
using ADService;
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
                        SyncedServices.Add(new ADServiceClass()
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
            //get the recordset from database where status = 
            Values.gDatos.DataBase = "SISTEMAS";
            Values.gDatos.Connect();
            using (var _RS = new DynamicRS("select UserCode, Name, Surname1,Password,Position, MainCOD3, emailAddress, localDomain, flags, desCOD3  from vUsers where dbo.CheckFlag(flags,'CHANGED')=1", Values.gDatos))
            {
                try
                {
                    await _RS.OpenAsync();
                } catch (Exception ex)
                {
                    EventLog.WriteEntry(string.Format("Error accesing database: {0}", ex.Message), EventLogEntryType.Error);
                    return;
                }
                foreach (var r in _RS.ToList())
                {
                    Dictionary<string, string> _flagsDefs;
                    using (var _frs = new DynamicRS("Select Codigo, DescFlagEng from flags where Servicio='SYNC' and Tabla='Users'", Values.gDatos))
                    {
                        _frs.Open();
                        _flagsDefs = _frs.ToList().ToDictionary(dr => dr["Codigo"].ToString(),dr => dr["DescFlagEng"].ToString());//.ToList().Select(fr => new KeyValuePair<string,string>(fr["Codigo"].ToString(), fr["DescFlagEng"].ToString())).ToDictionary<string,string>;
                    }
                    var _flags = r["flags"].ToString().Split('|').ToList().Where(fr => _flagsDefs.Keys.Contains(fr) ).ToList();
                    int _error = 0;
                    //create the user object
                    var _user = new EspackUser()
                    {
                        UserCode = r["UserCode"].ToString(),
                        Name = r["Name"].ToString(),
                        Surname = r["Surname1"].ToString(),
                        Group = "ESPACK_"+r["Position"].ToString(),
                        Password = r["Password"].ToString(),
                        Email = r["emailAddress"].ToString(),
                        Sede = new EspackSede()
                        {
                            COD3 = r["MainCOD3"].ToString(),
                            COD3Description = r["desCOD3"].ToString()
                        },
                        Flags = _flags,
                    };
                    // lets cycle each defined service
                    foreach (var s in SyncedServices)
                    { 
                        if (_flags.Contains(s.ServiceName))
                        {
                            try
                            {
                                s.Flags = _flagsDefs;
                                _flags.Remove(s.ServiceName);
                                //define the alias list
                                var _alias = Values.DomainList.Select(d => string.Format("'smtp:{0}@{1}'", r["UserCode"].ToString(), d));
                                _user.Aliases = _alias.Concat(new string[]{ string.Format("'smtp:{0}@{1}'", r["UserCode"].ToString(), r["localDomain"].ToString())}).ToList(); //we add the @COD3.espackeuro.com domain
                                //update or create the user in the service
                                await s.Interact(_user);
                                EventLog.WriteEntry(string.Format("User {0} from {1} was modified correctly in service {2}", _user.UserCode, _user.Sede.COD3, s.ServiceName));
                                _error = 0;
                            }
                            catch (Exception ex)
                            {
                                EventLog.WriteEntry(string.Format("User {0} from {1} was NOT modified correctly in service {2}. \nError message was {3}", r["UserCode"].ToString(), r["MainCOD3"].ToString(), s.ServiceName, ex.Message), EventLogEntryType.Error);
                                _error = 1;
                            }
                        }
                    };
                    // clear the CHANGED flag
                    var _SP = new SP(Values.gDatos, "pUppUserFlagCheckedClear");
                    _SP.AddParameterValue("UserCode", r["UserCode"].ToString());
                    _SP.AddParameterValue("Error", _error);
                    try
                    {
                        await _SP.ExecuteAsync();
                        if (_SP.LastMsg != "OK")
                            throw new Exception(_SP.LastMsg);
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry(string.Format("User {0} from {1} was NOT unflagged correctly. \nError message was {2}", r["UserCode"].ToString(), r["MainCOD3"].ToString(), ex.Message), EventLogEntryType.Error);
                    }
                };
            }
            //now lets sync the groups
            Values.gDatos.DataBase = "MAIL";
            Values.gDatos.Connect();
            using (var _RS = new DynamicRS("Select local_part,address,flags from aliasCAB where dbo.CheckFlag(flags,'CHANGED')=1", Values.gDatos))
            {
                try
                {
                    await _RS.OpenAsync();
                    //_RS.Open();
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(string.Format("Error accesing database {0}", ex.Message), EventLogEntryType.Error);
                    return;
                }

                foreach (var r in _RS.ToList())
                //_RS.ToList().ForEach(async r =>
                {
                    var _flags = r["flags"].ToString().Split('|');
                    int _error = 0;
                    //lets get the group members
                    using (var _aliases = new DynamicRS(string.Format("Select local_part_goto,domain_goto from aliasDET where address='{0}'", r["address"].ToString()), Values.gDatos))
                    {
                        try
                        {
                            await _aliases.OpenAsync();
                            //_RS.Open();
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry(string.Format("Error accesing database {0}", ex.Message), EventLogEntryType.Error);
                            return;
                        }
                        var _alList = _aliases.ToList().Select(a => string.Format("{0}@{1}", a["local_part_goto"], CleanDomain(a["domain_goto"].ToString()))).ToArray();
                        //create the group object
                        var _group = new EspackGroup()
                        {
                            GroupCode = r["local_part"].ToString()!="" ? r["local_part"].ToString() : r["address"].ToString().Replace('@','_').ToUpper(),
                            GroupMail = r["address"].ToString(),
                            GroupMembers = _alList
                        };
                        foreach (var s in SyncedServices)
                        {
                            if (_flags.Contains(s.ServiceName))
                            {
                                try
                                {
                                    await s.InteractGroup(_group);
                                    EventLog.WriteEntry(string.Format("Group {0} was modified correctly in service {1}", _group.GroupCode, s.ServiceName));
                                    _error = 0;
                                }
                                catch (Exception ex)
                                {
                                    EventLog.WriteEntry(string.Format("Group {0} was NOT modified correctly in service {1}. \nError message was {2}", _group.GroupCode, s.ServiceName, ex.Message), EventLogEntryType.Error);
                                    _error = 1;
                                }
                            }
                        }
                        // clear the CHANGED flag
                        var _SP = new SP(Values.gDatos, "pUppAliasFlagCheckedClear");
                        _SP.AddParameterValue("address", _group.GroupMail);
                        _SP.AddParameterValue("Error", _error);
                        try
                        {
                            await _SP.ExecuteAsync();
                            if (_SP.LastMsg != "OK")
                                throw new Exception(_SP.LastMsg);
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry(string.Format("Group {0} was NOT unflagged correctly. \nError message was {1}", _group.GroupCode,  ex.Message), EventLogEntryType.Error);
                        }
                    }
                }
            }
        }

        private string CleanDomain(string domain)
        {
            var _dom = domain.Split('.');
            if (_dom.Contains("espackeuro"))
                return "espackeuro.com";
            if (_dom.Contains("grupointerpack") && !_dom.Contains("gapps"))
                return "grupointerpack.com";
            return domain;
        }

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
