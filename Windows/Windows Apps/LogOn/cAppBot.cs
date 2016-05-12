using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogOn
{
    public class cAppBot: Control
    {
        private string Code { get; set; }
        private string Description { get; set; }
        private string DataBase { get; set; }
        private cServer DBServer { get; set; }
        private cServer ShareServer { get; set; }
        private string ExeName { get; set; }

        public cAppBot(string pCode,string pDescription,string pDatabase, string pExeName, string LocalCOD3, string ServiceZone)
        {
            Code = pCode;
            Description = pDescription;
            DataBase = pDatabase;
            ExeName = pExeName;

            //
            DBServer = Values.DBServerList[ServiceZone];
            ShareServer = Values.ShareServerList[LocalCOD3];
        }
    }

    // Class cServer -> there are two types: DATABASE and SHARE
    public class cServer
    {
        public string HostName { get; set; }
        public IPAddress IP { get; set; }
        public ServerTypes Type { get; set; }
        public string COD3 { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    // Class cServerList
    public class cServerList
    {
        public List<cServer> ServerList { get; set; } = new List<cServer>();
        public ServerTypes ListType { get; set; }

        public cServer this[string COD3]
        {
            get
            {
                return ServerList.FirstOrDefault(x => x.COD3 == COD3);
            }
        }

        public cServerList(ServerTypes pServerType)
        {
            ListType = pServerType;
        }

        public void Add(cServer pServer)
        {
            ServerList.Add(pServer);
        }
    }

    public enum ServerTypes { SHARE, DATABASE }
}
