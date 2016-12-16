using AccesoDatosNet;
using CommonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Messages
{
    public static class InitialConnection
    {
        public static cAccesoDatosNet gDatos { get; set; }
    }
    //for database
    public abstract class ADatabaseMessage : IMessage
    {
        protected string _msgIn = "";
        protected string _msgOut;
        public string Debug { get; protected set; } = "";
        public string MsgIn
        {
            get
            {
                return _msgIn;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("-{0}-------------------------------\nMsgIn={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgIn = value;
                }
                else _msgIn = "";
            }

        }
        public string MsgOut
        {
            get
            {
                return _msgOut;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("Process {0}\nMsgOut={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgOut = value;
                }
                else _msgOut = "";
            }

        }

        public abstract string ProcessName { get;}
        public abstract Task<bool> process();

        public XDataMessageRequest xmlMsgIn { get; set; }
        public XDocument xmlMsgOut { get; protected set; }
        public ADatabaseMessage(string s)
        {
            this.MsgIn = s;
            this.xmlMsgIn = new XDataMessageRequest(s);
        }
        public ADatabaseMessage(XDataMessageRequest x)
        {
            xmlMsgIn = x;
            MsgIn = x.ToString();
        }
        public ADatabaseMessage() { }
        public static ADatabaseMessage Parse(string s)
        {
            var x = new XDataMessageRequest(s);
            return Parse(x);
        }

        public static ADatabaseMessage Parse(XDataMessageRequest x)
        {
            switch (x.Action)
            {
                case "Start Session":
                    return new xStartSession(x);
                case "Database Connection":
                    return new xDatabaseConnection(x);
                case "Stored Procedure":
                    return new xStoredProcedure(x);
                case "Recordset":
                    return new xRecordSet(x);
            }
            return null;
        }
    }
    public class genericXMessage : ADatabaseMessage
    {
        public override string ProcessName { get { return "GENERIC"; } }
        public override async Task<bool> process()
        {
            MsgOut = await Task.Run(() =>
            {
                return MsgIn;
            });
            xmlMsgOut = XDocument.Parse(MsgOut);
            return true;
        }
    }

    public class xRecordSet : ADatabaseMessage
    {
        public override string ProcessName { get { return "RECORDSET"; } }

        public override async Task<bool> process()
        {
            xmlMsgOut = await Task.Run(() =>
            {
                var _xconn = xmlMsgIn.Data.Element("connection"); //get the connection data
                string _dataBase = _xconn.Element("DataBase").Value.ToString(); //get the database
                var _server = new cServer(_xconn.Element("server")); //create the server object from the connection data
                string _sql = xmlMsgIn.Data.Element("sql").Value.ToString(); //get the sql
                using (var _conn = new cAccesoDatosNet(_server, _dataBase)) //create the normal connection

                using (var _rs = new DynamicRS(_sql, _conn)) // create the normal recordset
                {
                    try
                    {
                        _rs.Open(); //execute the recordset
                        var x = _rs.XMLData; //create the msgout xml data
                        x.Root.Name = "result";
                        return x; // returns
                    }
                    catch (Exception ex)
                    {
                        return XDocument.Parse((new XElement("result", "ERROR: " + ex.Message)).ToString()); //returns error
                    }

                }
            });
            MsgOut = xmlMsgOut.ToString();
            return true;
        }
        public xRecordSet(string s) : base(s) { }
        public xRecordSet(XDataMessageRequest x) : base(x) { }
        public xRecordSet() { }
    }

    public class xStartSession : ADatabaseMessage
    {
        public override string ProcessName { get { return "SESSION START"; } }

        public override async Task<bool> process()
        {
            xmlMsgOut = await Task.Run(() =>
            {
                string _serial = xmlMsgIn.Data.Value;
                using (SP _sessionProc = new SP(InitialConnection.gDatos, "pStartSockSession"))
                {
                    _sessionProc.AddParameterValue("Serial", _serial);
                    try
                    {
                        _sessionProc.Execute();
                        if (_sessionProc.LastMsg != "OK")
                            throw new Exception(_sessionProc.LastMsg);
                        XDocument _msgOut = new XDocument();
                        _msgOut.Add(new XElement("result"));
                        _msgOut.Root.Add(_sessionProc.XOutParameters.Root);
                        //_msgOut.Root.Name = "result";
                        return _msgOut;
                    }
                    catch (Exception ex)
                    {
                        return XDocument.Parse((new XElement("result", "ERROR: " + ex.Message)).ToString()); //returns error
                    }

                }
            });
            MsgOut = xmlMsgOut.ToString();
            return true;
        }
        public xStartSession(string s) : base(s) { }
        public xStartSession(XDataMessageRequest x) : base(x) { }
        public xStartSession() { }
    }
    public class xDatabaseConnection : ADatabaseMessage
    {
        public override string ProcessName { get { return "DATABASE CONNECTION"; } }

        public override async Task<bool> process()
        {
            xmlMsgOut = await Task.Run(() =>
            {

                XElement _xServer = xmlMsgIn.Data.Element("server");
                var _server = new cServer();
                _server.xServer = _xServer;
                var _conn = new cAccesoDatosNet(_server, xmlMsgIn.Data.Element("DataBase").Value);
                try
                {
                    _conn.Connect();
                    return new XDocument(new XElement("result", "OK"));
                }
                catch (Exception ex)
                {
                    return new XDocument(new XElement("result", "Error: " + ex.Message));
                }

            });
            MsgOut = xmlMsgOut.ToString();
            return true;
        }
        public xDatabaseConnection(string s) : base(s) { }
        public xDatabaseConnection(XDataMessageRequest x) : base(x) { }
        public xDatabaseConnection() { }
    }
    public class xStoredProcedure : ADatabaseMessage
    {
        public override string ProcessName { get { return "STORED PROCEDURE"; } }

        public override async Task<bool> process()
        {
            xmlMsgOut = await Task.Run(() =>
            {

                var _xconn = xmlMsgIn.Data.Element("connection"); //get the connection data
                var _parameters = xmlMsgIn.Data.Element("parameterCollection").Elements("parameter"); //get the parameters list
                string _dataBase = _xconn.Element("DataBase").Value.ToString(); //get the database
                string _procedureName = xmlMsgIn.Data.Element("procedureName").Value.ToString(); // get the procedure name
                var _server = new cServer(_xconn.Element("server")); //create the server object from the connection data
                using (var _conn = new cAccesoDatosNet(_server, _dataBase)) //create the normal connection
                using (var _sp = new SP(_conn, _procedureName)) // create the normal SP
                {
                    try
                    {
                        _parameters.ToList().ForEach(p =>
                        {
                            _sp.AddParameterValue(p.Element("parameterName").Value.ToString(), p.Element("parameterValue").Value.ToString());
                        }); //adds the parameters and the values to the sp from the parameter list
                        _sp.Execute(); //execute the sp
                        if (_sp.LastMsg.Substring(0, 2) != "OK")
                            throw new Exception(_sp.LastMsg); //error message
                        XElement _msgOut = new XElement("result"); //create the msgout xml data
                        _msgOut.Add(_sp.XOutParameters.Root); //adds the out parameters to the data
                        return new XDocument(_msgOut); // returns
                    }
                    catch (Exception ex)
                    {
                        return new XDocument(new XElement("result", "Error: " + ex.Message));
                    }
                }
            });
            MsgOut = xmlMsgOut.ToString();
            return true;
        }
        public xStoredProcedure(string s) : base(s) { }
        public xStoredProcedure(XDataMessageRequest x) : base(x) { }
        public xStoredProcedure() { }
    }
}
