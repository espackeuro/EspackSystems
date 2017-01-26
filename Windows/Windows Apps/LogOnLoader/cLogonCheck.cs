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

namespace LogOnLoader
{
    public partial class fMain : frmSplash
    {
        private static int _zone = 0;
        public static EspackParamArray espackArgs { get; set; }


        public async Task LogonCheck()
        {
            string _pathLogonHosts;
            ServicePointManager.DnsRefreshTimeout = 0;

            _pathLogonHosts = Directory.GetCurrentDirectory() + "\\logonHosts";
            List<string> _content = new List<string>();
#if DEBUG
                _pathLogonHosts = "C:/ESPACK_CS/logonHosts";
        
#endif
            // Get logonHosts file content       
            var _IP = Values.gDatos.IP.GetAddressBytes();
            if (_IP[0] == 10)
                _zone = _IP[1];
            else
                _zone = _IP[2];
            try
            {
                if (File.Exists(_pathLogonHosts))
                {
                    _content = File.ReadAllLines(_pathLogonHosts).ToList<string>();
                }
                else
                {


                    throw new Exception(string.Format("Can not find connection details for {1}: {2}",_IP.ToString(), _pathLogonHosts));
                }
            } catch (Exception ex)
            {
                throw new Exception(string.Format("Can not find connection details for {1}: {2} - {3}", _IP.ToString(), _pathLogonHosts, ex.Message));
            }
                // Put in _line the corresponding to the _zone (if (_zone==200) then _line="200|10.200.10.130|10.200.10.138|80.33.195.45|VAL")
                string _line = _content.FirstOrDefault(p => p.Substring(0, 3) == _zone.ToString());

            // DB Server is the 2nd element in _line
            var _serverDB = new cServer()
            {
                HostName = _line.Split('|')[1],
                COD3 = _line.Split('|')[4],
                Type = ServerTypes.DATABASE,
                User = "procesos",
                Password = "*seso69*"
            };

            var _serverShare = new cServer()
            {
                HostName = _line.Split('|')[2],
                COD3 = _line.Split('|')[4],
                Type = ServerTypes.DATABASE,
                User = "logon",
                Password = "*logon*"
            };
            Values.ShareServerList.Add(_serverShare);
            Values.COD3 = _line.Split('|')[4];
            Values.AppList.Add(new cAppBot("logon", "LOGON", "SISTEMAS", "logon.exe", "LOC", _serverDB, _serverShare, "", true));
            Values.AppList[0].CheckUpdatedSync();
            if (Values.AppList.PendingApps.Count != 0)
            {

                this.Message = "Updating LogOn.";
                Application.DoEvents();
                System.Threading.Thread.Sleep(250);
                Values.ActiveThreads++;
                var _thread = new cUpdaterThread(Values.debugBox, Values.ActiveThreads);
                // launch task not async
                _thread.Process();
            }
            this.Message = "LogOn updated. Launching.";
            System.Threading.Thread.Sleep(250);
            await Values.AppList[0].LaunchApp(true);
            //
        }

    }

}
