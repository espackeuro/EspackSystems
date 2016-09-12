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
            cAccesoDatosXML gDatosx = new cAccesoDatosXML();
            gDatosx.Server = "DB01";
            gDatosx.User = "sa";
            gDatosx.Password = "5380";
            gDatosx.DataBase = "Sistemas";
            gDatosx.Connect();

            using (var _SP= new SPXML(gDatosx, "pLogonUser"))
            {
                _SP.AddParameterValue("User", "restelles");
                _SP.AddParameterValue("Password", "G8npi3rc");
                _SP.Execute();
            }
            
            
        }
    }
}
