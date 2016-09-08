using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Socks;
using System.Xml.Linq;
using CommonTools;
using System.Net;

namespace AccesoDatosNet
{
    public class cAccesoDatosXML : cAccesoDatos, IDisposable
    {
        public cSocks SocksCon { get; set; }
        String Serial { get; set; } = "";
        String Origin { get; set; } = "LOGON";
        cServer SecondServer { get; set; }
        string SessionNumber { get; set; }
        public override string HostName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DateTime ServerDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ConnectionState State
        {
            get
            {
                if (SocksCon.Status == SocksStatus.CONNECTED)
                    return ConnectionState.Open;
                else
                    return ConnectionState.Closed;
            }
        }
        public new string Server
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
                if (SocksCon == null)
                    SocksCon = new cSocks(value);
                else SocksCon.Server = value;
            }
        }
        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Connect()
        {
            try
            {
                string _msg;
                XDocument _msgOut;
                //string _result;

                //first phase, get the destination external IP to connect
                if (SecondServer== null)
                {
                    Serial = Serial == "" ? Environment.MachineName : Serial;
                    string _msgIn = string.Format(@"
<StartSession>
    <Serial>{0}</Serial>
</StartSession>",Serial);
                    _msg = SocksCon.SyncEncConversation(_msgIn);
                    _msgOut = XDocument.Parse(_msg);
                    var _result = _msgOut.Root.Element("Result");
                    if (_result==null || _result.Value.Substring(0,2)!="OK")
                    {
                        throw new Exception(_result.Value ?? "Error no result obtained");
                    }
                    var _IP = _msgOut.Root.Element("ExternalIP").Value;
                    var _COD3 = _msgOut.Root.Element("COD3").Value;
                    SessionNumber= _msgOut.Root.Element("SessionNumber").Value;
                    SecondServer = new cServer(_IP) { User = oServer.User, Password = oServer.Password, COD3=_COD3, Type=oServer.Type };
                }
                //try
                {
                    //_msg = SocksCon.SyncEncConversation(SocksCon.BuildSPXML(DataBase, SecondServer.User, "@Origin='RADIO LOGISTICA (VAL)'", cUser.Text, StringCipher.Encrypt(cPassword.Text)));
                    //_msgOut = XDocument.Parse(_msg);
                } 
                //second phase, connect to the destination Server



            }
            catch (Exception ex)
            {
                throw new Exception("ERROR: " + ex.Message);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    IP = null;
                    oServer = null;
                }
                string _msgIn = string.Format(@"
<StartSession>
    <Serial>{0}</Serial>
</StartSession>", Serial);

                disposedValue = true;
            }
        }


    }



}
