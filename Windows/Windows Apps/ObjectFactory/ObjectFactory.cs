using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using AccesoDatosXML;

namespace ObjectFactory
{
    public static class ObjectFactory
    {
        public static object createObject(string objectClass, string objectType, object param1 = null, object param2 = null, string serial = null)
        {

            switch (objectClass)
            {

                case "Conn":
                    object _conn;
                    if (objectType == "Socks") _conn = new cAccesoDatosXML(); else _conn = new cAccesoDatosNet();
                    EspackCommServer.Server.Serial = serial;
                    return _conn;
                case "SP": if (objectType == "Socks") return new SPXML((cAccesoDatosXML)param1, (string)param2); else return new SP((cAccesoDatosNet)param1, (string)param2);
                case "RS": if (objectType == "Socks") return new XMLRS((string)param1, (cAccesoDatosXML)param2); else return new DynamicRS((string)param1, (cAccesoDatosNet)param2);
                default:
                    return null;
            }
        }
    }

}
