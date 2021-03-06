﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using CommonTools;
using System.Net;

namespace LogOnObjects
{
    public static class Values
    {
        public const string LOCAL_PATH = "C:/ESPACK_CS/";
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string gMasterPassword = "";
        public static string User;
        public static string Password;
        public static cServerList DBServerList = new cServerList(ServerTypes.DATABASE);
        public static cServerList ShareServerList = new cServerList(ServerTypes.SHARE);
        public static cAppList AppList = new cAppList();
        public static string COD3 = "";
        public static cUpdateList UpdateList = new cUpdateList();
        public static cUpdateList UpdateDir = new cUpdateList();
        public static int ActiveThreads = 0;
        public static DebugTextbox debugBox;
        public static List<string> userFlags;
        public static string FullName;
        public static void FillServers(string pCOD3)
        {

            using (var _RS = new DynamicRS("select COD3,ServerDB,ServerShare,zone,UserShare,PasswordShare,servershareip,serverdbip from general..sedes", Values.gDatos))
            {
                _RS.Open();
                while (!_RS.EOF)
                {
                    Values.DBServerList.Add(new cServer() { Resolve=false, HostName = _RS[pCOD3 == "OUT" ? "ServerDBIP" : "ServerDB"].ToString(), COD3 = _RS["COD3"].ToString(), Type = ServerTypes.DATABASE, User = Values.User, Password = Values.Password });
                    Values.ShareServerList.Add(new cServer()
                    {
                        Resolve=false,
                        HostName = _RS[pCOD3 == "OUT"? "ServerShareIP":"ServerShare"].ToString(),
                        //IP = IPAddress.Parse(_RS["ServerShareIP"].ToString()),
                        COD3 = _RS["COD3"].ToString(),
                        Type = ServerTypes.DATABASE,
                        User = _RS["UserShare"].ToString(),
                        Password = _RS["PasswordShare"].ToString()
                    });
                    if (_RS["COD3"].ToString() == pCOD3)
                    {
                        Values.COD3 = _RS["COD3"].ToString();
                        Values.DBServerList.Add(new cServer() { HostName = _RS["ServerDB"].ToString(), COD3 = "LOC", Type = ServerTypes.DATABASE, User = Values.User, Password = Values.Password });
                        Values.DBServerList.Add(new cServer() { HostName = "DB01.local", COD3 = "OUT", Type = ServerTypes.DATABASE, User = Values.User, Password = Values.Password });
                    }
                    _RS.MoveNext();
                }
                
            }
           
        }
        public static bool External = false;
    }
}
