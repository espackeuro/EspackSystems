using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using Socks;
using Encryption;
using Compression;

namespace TestADS
{
    class Program
    {
        static void Main(string[] args)
        {
            //string data = @"";
            //var comp = StringCipher.Encrypt(data, true);
            //var dcomp = StringCipher.Decrypt(comp);

            //var encrypted = StringCipher.Encrypt(data, false);
            //var comp2 = cComp.Zip(encrypted);


            cAccesoDatosXML gDatosx = new cAccesoDatosXML();
            gDatosx.Server = "DB01";
            gDatosx.User = "sa";
            gDatosx.Password = "5380";
            gDatosx.DataBase = "Sistemas";
            gDatosx.Compression = true;
            gDatosx.Connect();

            using (var _RS = new XMLRS("select top 10 * from itemsCab", gDatosx))
            {
                _RS.Compression = true;

                _RS.Open();

                _RS.MoveNext();

                _RS.MoveLast();

                _RS.MovePrevious();

                _RS.MoveFirst();

            }
            //using (var _SP = new SPXML(gDatosx, "pLogonUser"))
            //{
            //    _SP.Compression = true;
            //    _SP.AddParameterValue("User", "restelles");
            //    _SP.AddParameterValue("Password", "G8npi3rc");
            //    _SP.AddParameterValue("Origin", "LOGON_CS");
            //    _SP.Execute();
            //}


        }
    }
}
