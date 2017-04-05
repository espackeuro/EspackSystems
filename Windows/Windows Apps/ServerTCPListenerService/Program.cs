using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;

namespace ServerTCPListenerService
{
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet()
        {
            User = "procesos",
            Password = "*seso69*",
            DataBase = "SISTEMAS",
            Server = "net.espackeuro.com"
        };
        public static int ServerPort { get; } = 17011;
        public static int ControlPort { get; } = 17012;
    }
    static class Program
    {


        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                ServerTCPListenerService service1 = new ServerTCPListenerService(args);
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.  



                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ServerTCPListenerService(args)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}

