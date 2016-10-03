using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTools;
using System.Net;
using System.Net.Sockets;
using Encryption;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Socks
{
    public enum SocksStatus { OFFLINE, TRYCONNECT, CONNECTED, ERROR}


    

    public class XEspackSocksMessage
    {
        //private XDocument _x;
        private XElement ActionDefinition { get; set; }
        private XElement ActionData { get; set; }
        private XElement SessionNumber { get; set; }
        public XDocument XMessage
        {
            get
            {
                if (ActionDefinition == null)
                    throw new Exception("Action not defined");
                if (ActionData == null)
                    throw new Exception("Data not defined");
                if (SessionNumber == null)
                    throw new Exception("SessionNumber not defined");
                var _root = new XDocument();
                _root.Add(new XElement("message"));
                _root.Root.Add(ActionDefinition);
                _root.Root.Add(ActionData);
                _root.Root.Add(SessionNumber);
                return _root;
            }
        }
        public void SetActionDefinition(string _action)
        {
            ActionDefinition = new XElement("ActionDefinition", _action);
        }
        public string Session
        {
            get
            {
                return SessionNumber == null ? null : SessionNumber.Value;
            }
        }
        public string Action
        {
            get
            {
                return ActionDefinition == null ? null : ActionDefinition.Value;
            }
        }
        public XElement Data
        {
            get
            {
                return ActionData;
            }
        }
        public void SetActionData(XElement _data)
        {
            if (_data.Name!= "data")
                throw new Exception("Root name of element should be 'data'");
            ActionData = _data;
        }
        public void SetSession(string _sessionNumber)
        {
            SessionNumber = new XElement("SessionNumber", _sessionNumber);
        }
        public override string ToString()
        {
            return XMessage.ToString();
        }
        public XEspackSocksMessage(string _message)
        {
            XDocument _x = XDocument.Parse(_message);
            ActionDefinition = new XElement(_x.Root.Element("ActionDefinition"));
            ActionData = new XElement(_x.Root.Element("data"));
            SessionNumber = new XElement(_x.Root.Element("SessionNumber"));
        }
        public XEspackSocksMessage() { }
    }

    public static class EspackSocksServer
    {
        public const string MAINSERVERIP = "10.200.100.137";
        public static cSocks InitialServer { get; set; }
        public static string Serial
        {
            get
            {
                return Environment.MachineName;
            }
        }
        //session number
        private static string _session;
        public static string SessionNumber
        {
            get
            {
                if (_session == null)
                    getSocksConnection();
                return _session;
            }
        }

        //final connection server
        private static cSocks _connectionServer;
        public static cSocks ConnectionServer
        {
            get
            {
                if (_connectionServer == null)
                    getSocksConnection();
                return _connectionServer;
            }
        }


        public static void getSocksConnection()
        {
            if (InitialServer == null)
            {
                InitialServer = new cSocks(MAINSERVERIP);
            }
            //first phase, get the destination external IP to connect
            //create the xml message with the session information
            var _message = new XEspackSocksMessage();
            _message.SetActionDefinition("Start Session");
            _message.SetActionData(new XElement("data",Serial));
            _message.SetSession("");
            XDocument _msgOut = InitialServer.xSyncEncConversation(_message);
            var _result = _msgOut.Root;
            if (_result == null || _result.Value.Substring(0, 5) == "ERROR")
            {
                throw new Exception(_result.Value ?? "Error no result obtained");
            }
            var _parameters = _result.Element("parameters").Elements("parameter");
            
            string _IP = (from _par in _parameters where _par.Element("Name").Value.ToString() == "@ExternalIP" select _par.Element("Value").Value).First();
            var _COD3 = (from _par in _parameters where _par.Element("Name").Value.ToString() == "@COD3" select _par.Element("Value").Value).First();
            _session = (from _par in _parameters where _par.Element("Name").Value.ToString() == "@SessionNumber" select _par.Element("Value").Value).First();
            _connectionServer = new cSocks(_IP);
        }
    }

    public class cSocks
    {
        public cServer oServer { get; set; }

        public string ServerIP
        {
            get
            {
                return oServer.IP.ToString();
            }
            set
            {
                if (oServer != null)
                {
                    IPAddress _serverIP;
                    if (IPAddress.TryParse(value, out _serverIP))
                        oServer.IP = _serverIP;
                }
            }
        }

        private SocksStatus oStatus = SocksStatus.OFFLINE;

        public SocksStatus Status
        {
            get
            {
                return oStatus;
            }
            set
            {
                var _evargs = new StatusChangeEventArgs()
                {
                    OldStatus = oStatus,
                    NewStatus = value
                };
                OnStatusChange(_evargs);
                oStatus = value;
            }
        }

        public string Server
        {
            get
            {
                if (oServer != null)
                    return oServer.HostName;
                else
                    return null;
            }
            set
            {
                if (oServer == null)
                    oServer = new cServer(value);
                else
                    oServer.Server = value;
            }
        }

        public cSocks(string pServerName)
        {
            Server = pServerName;
        }

        public EventHandler<StatusChangeEventArgs> StatusChange;

        protected virtual void OnStatusChange(StatusChangeEventArgs e)
        {
            EventHandler<StatusChangeEventArgs> handler = StatusChange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

//        public static string BuildSPXML(string DataBase, string ProcedureName, string Parameters, string User="", string Password="", string Session="")
//        {
//            return string.Format(@"<procedure>
//  <DB>{0}</DB>
//  <name>{1}</name>
//  <parameters>{2}</parameters>{3}{4}{5}
//</procedure>",DataBase,ProcedureName,Parameters, User !=""?@"
//  <user>"+User+ "</user>" : "", Password != "" ? @"
//  <password>" + Password + "</password>" : "", Session != "" ? @"
//  <session>" + Session + "</session>" : "");
//        }


//        public string BuildQueryXML(string DataBase, string Query, string Parameters, string User = "", string Password = "", string Session = "")
//        {
//            return string.Format(@"<query>
//  <DB>{0}</DB>
//  <sqlQuery>{2}</sqlQuery>{3}
//  {4}
//  {5}
//</query>", DataBase, Query, Parameters, User != "" ? "<user>" + User + "</users>" : "", Password != "" ? "<password>" + Password + "</password>" : "", Session != "" ? "<session>" + Session + "</session>" : "");
//        }

        public string SyncEncConversation(string msgOut,bool compression=false)
        {
            try
            {
                var _msgIn = SyncConversation(StringCipher.Encrypt(msgOut,compression));
                return StringCipher.Decrypt(_msgIn);
            } catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            
        }

        public string SyncEncConversation(XDocument msgOut)
        {
            return (SyncEncConversation(msgOut.ToString()));
        }

        public XDocument xSyncEncConversation(object msgOut,bool compression=false)
        {
            return (XDocument.Parse(SyncEncConversation(msgOut.ToString(), compression)));
        }


        public string SyncConversation(string msgOut)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                //IPAddress ipAddress;
                //IPAddress.TryParse("10.200.90.7", out ipAddress);
                IPEndPoint remoteEP = new IPEndPoint(oServer.IP, 15000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    Status = SocksStatus.TRYCONNECT;
                    sender.Connect(remoteEP);
                    Status = SocksStatus.CONNECTED;
                    //RunOnUiThread(() => txtConsole.Text += String.Format("Socket connected to {0}\n", sender.RemoteEndPoint.ToString()));
                    //Console.WriteLine("Socket connected to {0}",
                    //    sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(msgOut);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    string _result = "";
                    int bytesRec = 0;
                    do
                    {
                        bytesRec = sender.Receive(bytes);
                        _result += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    } while (bytesRec > 0);
                    
                    
                   
                    
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    Status = SocksStatus.OFFLINE;
                    return _result;
                }
                catch (ArgumentNullException ane)
                {
                    Status = SocksStatus.ERROR;
                    _ErrorMsg= string.Format("ArgumentNullException : {0}", ane.Message);
                    return "";
                    throw (ane);
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Status = SocksStatus.ERROR;
                    _ErrorMsg= string.Format("SocketException : {0}", se.Message);
                    return "";
                    throw (se);
                    //Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Status = SocksStatus.ERROR;
                    _ErrorMsg = string.Format("Unexpected exception : {0}", e.Message);
                    return "";
                    throw (e);
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }



            catch (Exception e)
            {
                Status = SocksStatus.ERROR;
                _ErrorMsg = string.Format("General exception : {0}", e.Message);
                return "";
                throw (e);
                //Console.WriteLine(e.ToString());
            }
        }




        private string _ErrorMsg = "";



        public string ErrorMsg
        {
            get
            {
                return _ErrorMsg;
            }
        }

    }


    public class StatusChangeEventArgs: EventArgs
    {
        public SocksStatus OldStatus { get; set; }
        public SocksStatus NewStatus { get; set; }
    }
}
