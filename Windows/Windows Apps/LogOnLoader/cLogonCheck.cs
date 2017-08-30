using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTools;
using CommonToolsWin;
using System.IO;
using System.Windows.Forms;
using LogOnObjects;
using System.Net;
using AccesoDatosNet;
using static LogOnObjects.Values;
namespace LogOnLoader
{
    public partial class fMain : frmSplash
    {



        public async Task LogonCheck()
        {
            string _dbserver="";
            string _shareserver="";
            string _cod3 = "";


            try
            {
                var _dnsDB = Dns.GetHostEntry("appdb.local");
                _dbserver = _dnsDB.HostName;
                var _dnsShare = Dns.GetHostEntry("appshare.local");
                _shareserver = _dnsShare.HostName;
                _cod3 = _dbserver.Substring(0, 3);
            }
            catch
            {
                //CTWin.InputBox("", "Enter database server name or IP.", ref _dbserver);
                External = true;

            }
#if DEBUG
            _dbserver = "db02.cr2.local";
            _shareserver = "net01.cr2.local";
#endif
            // string _pathLogonHosts;
            ServicePointManager.DnsRefreshTimeout = 0;


            var _serverDB = new cServer()
            {
                HostName = _dbserver,// _line.Split('|')[1],
                COD3 = _cod3,//_line.Split('|')[4],
                Type = ServerTypes.DATABASE,
                User = "procesos",
                Password = "*seso69*"
            };

            var _serverShare = new cServer()
            {
                HostName = _shareserver,//_line.Split('|')[2],
                COD3 = _cod3,//_line.Split('|')[4],
                Type = ServerTypes.DATABASE,
                User = "logon",
                Password = "*logon*"
            };
            ShareServerList.Add(_serverShare);
            COD3 = _cod3;// _line.Split('|')[4];
            AppList.Add(new cAppBot("logon", "LOGON", "SISTEMAS", "logon.exe", "LOC", _serverDB, _serverShare, "", true));
            if (!External)
            {
                var _clean = await Values.AppList[0].CheckUpdated();
                if (!_clean)
                    AppList[0].Status = AppBotStatus.PENDING_UPDATE;
                if (Values.AppList.PendingApps.Count != 0)
                {

                    Message = "Updating LogOn.";
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(250);
                    ActiveThreads++;
                    var _thread = new cUpdaterThread(Values.debugBox, Values.ActiveThreads);
                    // launch task not async
                    _thread.Process();
                }
                this.Message = "LogOn updated. Launching.";
                System.Threading.Thread.Sleep(250);
            }
            await AppList[0].LaunchApp(true);
            //
        }

    }

}
