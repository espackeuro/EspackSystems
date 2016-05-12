using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatosNet;

namespace LogOn
{
    public class cAppBot: Control
    {
        public string Code { get; set; }
        private string Description { get; set; }
        private string DataBase { get; set; }
        private cServer DBServer { get; set; }
        private cServer ShareServer { get; set; }
        private string ExeName { get; set; }
        private cAccesoDatosNet Conn { get; set; }
        public cAppBot(string pCode,string pDescription,string pDatabase, string pExeName, string ServiceZone)
        {
            Code = pCode;
            Description = pDescription;
            DataBase = pDatabase;
            ExeName = pExeName;

            //
            DBServer = Values.DBServerList[ServiceZone];
            ShareServer = Values.ShareServerList[Values.COD3];
        }

        public cAppBot()
        {
        }
    }

    // Class cAppList
    public class cAppList
    {
        public List<cAppBot> AppList { get; set; } = new List<cAppBot>();

        public cAppBot this[string Code]
        {
            get
            {
                return AppList.FirstOrDefault(x => x.Code== Code);
            }
        }

        public void Add(cAppBot pApp)
        {
            AppList.Add(pApp);
        }
    }

}
