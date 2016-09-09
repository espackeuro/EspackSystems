using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Socks;
using System.Xml.Linq;
using CommonTools;
using System.Net;
using System.Collections;

namespace AccesoDatosNet
{
    public class XMLParameter : DbParameter
    {
        private ParameterDirection _paramdir = new ParameterDirection();

        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; }


        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; }


        public override int Size { get; set; }


        public override string SourceColumn { get; set; }


        public override bool SourceColumnNullMapping { get; set; }


        public override DataRowVersion SourceVersion { get; set; }

        public override object Value { get; set; }


        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }
    }

    public class XMLParameterCollection : DbParameterCollection
    {
        private List<XMLParameter> _paramList = new List<XMLParameter>();

        public new XMLParameter this[string paramName] {
            get
            {
                return _paramList.FirstOrDefault(o => o.ParameterName == paramName);
            }
            set
            {
                _paramList[this.IndexOf(paramName)] = value;
            }
        }

        public new XMLParameter this[int index]
        {
            get
            {
                return _paramList[index];
            }
            set
            {
                _paramList[index] = value;
            }
        }

        public override int Count
        {
            get
            {
                return _paramList.Count();
            }
        }

        public override bool IsFixedSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int Add(object value)
        {
            _paramList.Add((XMLParameter)value);
            return Count;
        }

        public override void AddRange(Array values)
        {
            _paramList.AddRange((IEnumerable<XMLParameter>)values);
        }

        public override void Clear()
        {
            _paramList.Clear();
        }

        public override bool Contains(string value)
        {
            try
            {
                var p = _paramList.First(o => o.ParameterName == value);
                return true;
            }catch
            {
                return false;
            }
        }

        public override bool Contains(object value)
        {
            return _paramList.Contains((XMLParameter)value);
        }

        public override void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int IndexOf(string parameterName)
        {
            try
            {
                return _paramList.IndexOf(this[parameterName]);
            } catch
            {
                return -1;
            }
        }

        public override int IndexOf(object value)
        {
            try
            {
                return _paramList.IndexOf((XMLParameter)value);
            }
            catch
            {
                return -1;
            }
        }

        public override void Insert(int index, object value)
        {
            _paramList.Insert(index, (XMLParameter)value);
        }

        public override void Remove(object value)
        {
            _paramList.Remove((XMLParameter)value);
        }

        public override void RemoveAt(string parameterName)
        {
            _paramList.Remove(this[parameterName]);
        }

        public override void RemoveAt(int index)
        {
            Remove(_paramList[index]);
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return this[parameterName];
        }

        protected override DbParameter GetParameter(int index)
        {
            return this[index];
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            this[parameterName] =(XMLParameter)value;
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            this[index] = (XMLParameter)value;
        }
    }



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
    }

    

    public class SPXML : SPFrame
    {
        private cAccesoDatosXML mConn;

        public override CommandType CommandType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool MsgOut
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void AddControlParameter(string pParamName, object ParamControl)
        {
            throw new NotImplementedException();
        }
    }

}
