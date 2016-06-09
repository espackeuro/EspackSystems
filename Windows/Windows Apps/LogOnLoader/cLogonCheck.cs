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

namespace LogOnLoader
{
    public partial class fMain : frmSplash
    {
        private static int _zone = 0;
        public static EspackParamArray espackArgs { get; set; }
        public void LogonCheck()
        {
            //System.Threading.Thread.Sleep(5000);
            // Load the vars from the given args
            espackArgs = CT.LoadVars(Args);

            // If DB is not set in args, we assume any args are set
            if (espackArgs.DataBase == null)
            {
                espackArgs.DataBase = "SISTEMAS";
                espackArgs.User = "procesos";
                espackArgs.Password = "*seso69*";

                // Init _zone var (200, 210, 220, etc...), _pathLogonHosts (the path for the logonHosts file) and the list _content (that will contain logonHosts contents)
                //int _zone = 0;
                string _pathLogonHosts;
                _pathLogonHosts = Directory.GetCurrentDirectory() + "\\logonHosts";
                List<string> _content = new List<string>();
#if DEBUG
                _pathLogonHosts = "D:/APPS_CS/logon/logonHosts";
        
#endif
                // Get logonHosts file content       
                var _IP = Values.gDatos.IP.GetAddressBytes();
                if (_IP[0] == 10)
                    _zone = _IP[1];
                else
                    _zone = _IP[2];

                if (File.Exists(_pathLogonHosts))
                {
                    _content = File.ReadAllLines(_pathLogonHosts).ToList<string>();
                }
                else
                {


                    throw new Exception("Can not find connection details:"+ _pathLogonHosts);
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
            Values.User = "procesos";
            Values.Password = "*seso69*";
            // Connect (or try)
            try
            {
                Values.gDatos.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Error connecting database server: " + e.Message);
            }
            Values.User = espackArgs.User;
            Values.Password= espackArgs.Password;
            Values.FillServers(_zone);
            Values.AppList.Add(new cAppBot("logon", "LOGON", "SISTEMAS", "logon.exe", "LOC", Values.DBServerList[Values.COD3], Values.ShareServerList[Values.COD3],"",true));
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
            Values.AppList[0].LaunchApp();
            //
        }

    }

}
