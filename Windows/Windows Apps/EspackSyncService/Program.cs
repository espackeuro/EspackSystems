using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using CommonTools;
using AccesoDatosNet;
using MasterClass;

namespace EspackSyncService
{
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet()
        {
            User = "SSPI",
            Password = "*seso69*",
            DataBase = "SISTEMAS"
        };
        public static byte[] MasterPassword = MasterClass.MasterPassword.MasterBytes;
        public static Dictionary<string, string> Servers = new Dictionary<string, string>();
        public static int PollingTime { get; set; } = 10;
        public static List<string> DomainList { get; set; }
    }
    static class Program
    {


        static void Main(string[] args)
        {
            if (args.Count() == 0)
                args = new string[] { /*"NEXTCLOUD=nextcloud.espackeuro.com", */"DOMAIN=sauron.systems.espackeuro.com", "DATABASE=DB01.local" };


            args.ToList().ForEach(server =>
            {
                var tupla = server.Split('=');
                Values.Servers.Add(tupla[0], tupla[1]);
            });
            Values.gDatos.Server = Values.Servers["DATABASE"];
            Values.gDatos.context_info = MasterPassword.MasterBytes;
            Values.gDatos.Connect();
            using (var _domains = new StaticRS("select domain from MAIL..domain where dbo.CheckFlag(flags,'FORWARD')=1 ", Values.gDatos))
            {
                _domains.Open();
                Values.DomainList = _domains.ToList().Select(r => r["domain"].ToString()).ToList();
                //_domains.Close();
            }
            if (Environment.UserInteractive)
            {
                SyncServiceClass service1 = new SyncServiceClass(args);
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.  



                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new SyncServiceClass(args)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
