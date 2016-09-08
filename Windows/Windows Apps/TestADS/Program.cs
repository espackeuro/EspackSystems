using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using Socks;

namespace TestADS
{
    class Program
    {
        static void Main(string[] args)
        {
            string _serverIP = "10.200.90.3";
            cAccesoDatosXML gDatosx = new cAccesoDatosXML();
            gDatosx.Server = _serverIP;
            gDatosx.Connect();
            
        }
    }
}
