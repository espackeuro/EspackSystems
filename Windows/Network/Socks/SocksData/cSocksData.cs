using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Socks;
using AccesoDatosNet;

namespace SocksData
{
    public class cSocksData: cSocks
    {
        public cAccesoDatosNet localConn;
        public cSocksData(string pServerName):
            base(pServerName)
        { }

    }
}
