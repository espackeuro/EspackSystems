using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogOn
{
    public class cAppBot
    {
        private string Code { get; set; }
        private string Description { get; set; }
        private string DataBase { get; set; }
        private IPAddress DBServerIP { get; set; }
        private IPAddress ShareServerIP { get; set; }
        private string ExeName { get; set; }
        private string DBUser { get; set; }
        private string DBPassword { get; set; }
        private string ShareUser { get; set; }
        private string SharePassword { get; set; }

        public cAppBot(string pCode,string pDescription,string pDatabase, string pDBServerIP, string pShareServerIP, string pExeName,string pDBUser,string pDBPassword,string pShareUser,string pSharePassword)
        {
            Code = pCode;
            Description = pDescription;
            DataBase = pDatabase;
            DBServerIP = IPAddress.Parse(pDBServerIP);
            ShareServerIP = IPAddress.Parse(pShareServerIP);
            ExeName = pExeName;
            DBUser = pDBUser;
            DBPassword = pDBPassword;
            ShareUser = pShareUser;
            SharePassword = pSharePassword;
        }
    }
    public class cServer
    {
        public string HostName { get; set; }
        public IPAddress IP { get; set; }
        public ServerTypes Type { get; set; }
        public string COD3 { get; set; }
    }

    public class cServerList
    {
        public List<cServer> ServerList { get; set; }
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
