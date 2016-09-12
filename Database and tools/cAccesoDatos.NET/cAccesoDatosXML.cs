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

        public XElement XParameter
        {
            get
            {
                XElement _root = new XElement("parameter");
                _root.Add(new XElement("parameterName", ParameterName));
                _root.Add(new XElement("parameterValue", this.Value));
                return _root;
            }


        }

        public string XMLParameterText
        {
            get
            {
                return XParameter.ToString();
            }
        }

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

        public XMLParameter()
        {

        }


    }

    public class XMLParameterCollection : DbParameterCollection
    {
        private List<XMLParameter> _paramList = new List<XMLParameter>();

        public XElement XParameterCollection
        {
            get
            {
                XElement _root = new XElement("parameterCollection");
                _paramList.ForEach(o => { _root.Add((XElement)o.XParameter); });
                return _root;
            }
        }

        public string XMLParameterCollectionText
        {
            get
            {
                return XParameterCollection.ToString();
            }
        }

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
        
        String Origin { get; set; } = "LOGON";
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
                if (EspackSocksServer.ConnectionServer.Status == SocksStatus.CONNECTED)
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
            }
        }

        public XElement xConn
        {
            get
            {
                XElement _root = new XElement("connection");
                _root.Add(oServer.xServer);
                _root.Add(new XElement("DataBase", DataBase));
                return _root;
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
                XDocument _msgOut = EspackSocksServer.ConnectionServer.xSyncEncConversation(xConn.ToString());
                if (_msgOut.Element("result").Value != "OK")
                    throw new Exception(_msgOut.Element("result").Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }



    public class SPXML : SPFrame
    {
        protected new cAccesoDatosXML mConn;
        //private string SPName {get;set;}
        private XMLParameterCollection _parameters = new XMLParameterCollection();

        public new XMLParameterCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }
        public new cAccesoDatosXML Conn
        {
            get
            {
                return mConn;
            }
            set
            {
                mConn = (cAccesoDatosXML)value;
            }
        }

        public XElement xSP
        {
            get
            {
                var _x = new XElement("procedurex");
                _x.Add(Conn.xConn);
                _x.Add(Parameters.XParameterCollection);
                _x.Add(new XElement("procedureName", SPName));
                return _x;
            }
        }

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
                try
                {
                    return (Parameters[1].ParameterName == "@msg");
                }
                catch
                {
                    return false;
                }
            }
        }
        
        public SPXML(cAccesoDatosXML pConn, string pSPNAme = "")
        {
            SPName = pSPNAme;
            Conn = pConn;
        }

        public override void AddParameterValue(string pParamName, object pValue, string DBFieldName = "")
        {
            try
            {
                if (pParamName.Substring(0, 1) != "@")
                {
                    pParamName = '@' + pParamName;
                }
                DateTime res;
                if (Parameters[pParamName] == null)
                    Parameters.Add(new XMLParameter() { ParameterName = pParamName, DbType= DbType.String });
                if (Parameters[pParamName].DbType.IsNumericType())
                {
                    if (pValue != null && pValue.ToString() == "")
                    {
                        pValue = null;
                    }
                }

                if (Parameters[pParamName].DbType == DbType.DateTime)
                {
                    if (pValue is DateTime)
                    {
                        Parameters[pParamName].Value = (DateTime)pValue;
                        return;
                    }
                    if (!DateTime.TryParse(pValue.ToString(), out res))
                    {
                        throw new Exception("Wrong DateTime parameter " + pParamName);
                    }
                    else
                    {
                        Parameters[pParamName].Value = res;
                    }
                }
                if (pValue != null && pValue is System.String)
                {
                    Parameters[pParamName].Value = pValue.ToString();
                }
                else if (pValue == null)
                {
                    Parameters[pParamName].Value = DBNull.Value;
                }
                else
                {
                    Parameters[pParamName].Value = pValue;
                }
                if (DBFieldName != "")
                {
                    Parameters[pParamName].SourceColumn = DBFieldName;
                }
            }
            catch
            {
                //AdoPar = null;
                throw;
            }

        }
        public override void AddControlParameter(string pParamName, object ParamControl)
        {
            if (pParamName.Substring(0, 1) != "@")
            {
                pParamName = '@' + pParamName;
            }
            XMLParameter lParam;
            if (Parameters[pParamName] != null)
            {
                lParam = Parameters[pParamName];
            }
            else
            {
                lParam = new XMLParameter()
                {
                    ParameterName = pParamName
                };
                Parameters.Add(lParam);
            }

            ControlParameters.Add(new ControlParameter()
            {
                Parameter = lParam,
                LinkedControl = ParamControl
            });
        }
        public void AssignOutputParameterContainer(string ParamName, out XMLParameter ParamOut, object Value = null)
        {
            XMLParameter _param;
            if (ParamName.Substring(0, 1) != "@")
            {
                ParamName = '@' + ParamName;
            }
            if (Parameters[ParamName] != null)
            {
                _param = Parameters[ParamName];
            }
            else
            {
                _param = new XMLParameter()
                {
                    ParameterName = ParamName
                };
                Parameters.Add(_param);
            }
            if (Value != null)
            {
                AddParameterValue(ParamName, Value);
            }
            ParamOut = _param;
            //ObjectParameters.Add(new ObjectParameter() {Container=Container, Parameter=_param });

        }
        public override void AssignValuesParameters()
        {
            ControlParameters.Where(x => x.LinkedControl is IsValuable && (x.Parameter.Direction == ParameterDirection.InputOutput || x.Parameter.Direction == ParameterDirection.Output)).ToList().ForEach(p => ((IsValuable)p.LinkedControl).Value = p.Parameter.Value);
        }
        public override void Execute()
        {
            AssignParameterValues();

            XDocument _msgOut = EspackSocksServer.ConnectionServer.xSyncEncConversation(xSP.ToString());
            if (_msgOut.Element("result").Value != "OK")
                throw new Exception(_msgOut.Element("result").Value);


            AssignValuesParameters();
            try
            {
                LastMsg = Parameters.OfType<XMLParameter>().ToList().First(x => x.ParameterName == "@msg").Value.ToString();
            }
            catch
            {
                LastMsg = "";
            }

        }

        public override Dictionary<string, object> ReturnValues()
        {
            return Parameters.OfType<XMLParameter>()
                .Where(p => p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                .ToDictionary(i => i.ParameterName, i => i.Value);

            //Dictionary<string, object> Result = new Dictionary<string, object>();
            //foreach (SqlParameter Param in Parameters)
            //{
            //    if (Param.Direction == ParameterDirection.InputOutput || Param.Direction == ParameterDirection.Output)
            //    {
            //        Result.Add(Param.ParameterName, Param.Value);
            //    }
            //}
            //return Result;
        }

    }

}
