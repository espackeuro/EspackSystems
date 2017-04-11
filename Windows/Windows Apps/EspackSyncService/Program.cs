using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using CommonTools;
using AccesoDatosNet;


namespace EspackSyncService
{
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet()
        {
            User = "procesos",
            Password = "*seso69*",
            DataBase = "SISTEMAS"
        };
        public static byte[] MasterPassword = Encoding.Unicode.GetBytes("Y?D6d#b@");
        public static Dictionary<string, string> Servers = new Dictionary<string, string>();
        public static int PollingTime { get; set; } = 60;
    }
    static class Program
    {


        static void Main(string[] args)
        {
            if (args.Count() == 0)
                args = new string[] { "NEXTCLOUD = nextcloud.espackeuro.com", "DOMAIN = sauron.systems.espackeuro.com", "DATABASE = DB01.local" };


            args.ToList().ForEach(server =>
            {
                var tupla = server.Split('=');
                Values.Servers.Add(tupla[0], tupla[1]);
            });
            Values.gDatos.Server = Values.Servers["DATABASE"];
            Values.gDatos.context_info = Values.MasterPassword;
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
