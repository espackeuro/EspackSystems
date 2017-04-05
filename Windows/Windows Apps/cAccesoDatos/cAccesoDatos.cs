using CommonTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ControlParameter
    {
        public Object LinkedControl;
        public DbParameter Parameter;
    }

    public abstract class cAccesoDatos : ICloneable, IDisposable
    {
        public DbConnection AdoCon { get; set; }
        public string Path { get; set; }
        public string AppName { get; set; }
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

        public virtual string Server
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
                if (oServer != null)
                {
                    IPAddress _serverIP;
                    string _hostName = "";

                    if (!IPAddress.TryParse(value, out _serverIP))
                    {
                        _hostName = value;
                        try
                        {
                            var result = Dns.GetHostEntry(value);
                            _hostName = result.HostName;
                            _serverIP = result.AddressList[0];
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error trying {0}: {1}", _hostName, ex.Message));
                        }
                    }
                    else
                    {
                        _hostName = value;
                        //try
                        //{
                        //    var result = Dns.GetHostEntry(_serverIP);
                        //    _hostName = result.HostName;
                        //} catch (Exception ex)
                        //{
                        //    _hostName = value;
                        //}
                    }
                    oServer.HostName = _hostName;
                    oServer.IP = _serverIP;
                }
            }
        }
        public string Printer { get; set; }
        public string DataBase { get; set; }
        public string User
        {
            get
            {
                if (oServer != null)
                    return oServer.User;
                else return null;
            }
            set
            {
                if (oServer != null)
                    oServer.User = value;
            }
        }

        public string Password
        {
            get
            {
                if (oServer != null)
                    return oServer.Password;
                else return null;
            }
            set
            {
                if (oServer != null)
                    oServer.Password = value;
            }
        }
        public bool Silent { get; set; }
        public IPAddress IP { get; set; }
        public DateTime TimeTic { get; set; }
        public long TimeOut { get; set; }
        public string Cod3 { get; set; }
        public byte[] context_info { get; set; }
        public cServer oServer { get; set; }
        //public string DeviceSerial { get; set; } = null;

        public abstract System.Data.ConnectionState State
        {
            get;
        }

        //constructores
        public cAccesoDatos()
        {
            //AdoCon = new SqlConnection();
            //Provider = "SQLOLEDB";
            Silent = false;
            /*
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var lIP = ipHostInfo.AddressList.FirstOrDefault(x => x.GetAddressBytes()[0] == 10);
            if (lIP == null)
                lIP = ipHostInfo.AddressList.First(x => x.GetAddressBytes()[0] == 192);
            //foreach (IPAddress lIP in ipHostInfo.AddressList)
            //{
            //    if (lIP.AddressFamily.ToString() == "InterNetwork" && ((lIP.GetAddressBytes()[0] == 192 && lIP.GetAddressBytes()[1] == 168) || lIP.GetAddressBytes()[0] == 10))
            //    { //IPV4
            //        IP = lIP;
            //        break;
            //    }
            //}
            //IP = lIP;
            */
            if (oServer == null)
            {
                oServer = new cServer() { Type = ServerTypes.DATABASE };
            }

        }

        public cAccesoDatos(string pServer, string pDataBase, string pUser, string pPassword)
            : this()
        {
            //Server = pServer;
            //User = pUser;
            //Password = pPassword;
            Server = pServer;
            User = pUser;
            Password = pPassword;
            DataBase = pDataBase;

        }

        public cAccesoDatos(cServer pServer, string pDataBase)
            : this()
        {
            oServer = pServer;
            DataBase = pDataBase;
        }
        public cAccesoDatos(cAccesoDatos parent)
            : this()
        {
            Server = parent.Server;
            DataBase = parent.DataBase;
            User = parent.User;
            Password = parent.Password;
            //AdoCon.ConnectionString = parent.ConnectionString;
        }
        public cAccesoDatos(EspackParamArray pParams)
            : this()
        {
            Server = pParams.Server;
            DataBase = pParams.DataBase;
            User = pParams.User;
            Password = pParams.Password;
            Cod3 = pParams.Cod3;
            AppName = pParams.AppName;
        }
        public abstract DateTime ServerDate();
        public abstract Task<DateTime> ServerDateAsync();
        public abstract string HostName();
        public abstract Task<string> HostNameAsync();

        public abstract void Connect();
        public abstract Task ConnectAsync();

        public void Open()
        {
            Connect();
        }
        public async Task OpenAsync()
        {
            await ConnectAsync();
        }

        public abstract void Close();


        public cAccesoDatos Clone()
        {
            return (cAccesoDatos)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #region IDisposable Support
        protected bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //IP = null;
                    oServer = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~cAccesoDatosNet() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }

    public enum RSState
    {
        Closed = 0,
        Open = 1,
        Connecting = 2,
        Executing = 4,
        Fetching = 8
    }

    public abstract class RSFrame : IDisposable
    {
        //private SqlDataReader mDR = null;
        //private SqlCommand mCmd = null;
        protected cAccesoDatos mConn = null;
        protected RSState mState;
        protected DbParameterCollection _parameters;
        //events
        //Events
        public event EventHandler<EventArgs> AfterExecution; //launched when the query is executed
        public event EventHandler<EventArgs> BeforeExecution;
        //properties
        public string SQL { get; set; }
        public int Index { get; protected set; } = 0;

        public bool EOF { get; protected set; } = false;

        public bool BOF { get; protected set; } = false;
        public RSState State
        {
            get
            {
                return mState;
            }
        }
        public abstract int RecordCount { get; }
        public abstract int FieldCount { get; }

        public abstract List<string> Fields { get; }
        public abstract object this[string Idx]
        {
            get;
        }
        public abstract object this[int Idx]
        {
            get;
        }
        public abstract object DataObject
        {
            get;
        }
        public abstract List<DataRow> Rows
        {
            get;
        }
        public virtual List<DataRow> ToList()
        {
            return getList();
        }

        public bool HasRows { get; set; }
        public bool AutoUpdate { get; set; }
        //public abstract SqlCommand Cmd { get; set; }

        public virtual void MoveNext(bool silent = true)
        {
            Move(Index + 1, silent);
        }
        public virtual void MovePrevious(bool silent = true)
        {
            Move(Index - 1, silent);
        }
        public virtual void MoveFirst(bool silent = true)
        {
            Move(0, silent);
        }
        public virtual void MoveLast(bool silent = true)
        {
            Move(RecordCount - 1, silent);
        }
        public virtual void Move(int Idx, bool silent = true)
        {
            if (RecordCount == 0)
            {
                EOF = true;
                BOF = true;
                return;
            }
            if (Idx < 0)
            {
                Index = 0;
                EOF = false;
                BOF = true;
                if (!silent) throw new Exception("Current record is the first one.");
            }
            if (Idx > RecordCount - 1)
            {
                Index = RecordCount - 1;
                EOF = true;
                BOF = false;
                if (!silent) throw new Exception("Current record is the last one.");
            }
            if (Idx < RecordCount)
                EOF = false;
            mState = RSState.Fetching;
            Index = Idx;
            mState = RSState.Open;
        }
        public virtual Task ExecuteAsync() { return Task.FromResult(0); }
        public virtual void Execute() { }
        public void Open()
        {
            AssignParameterValues();
            var e = new EventArgs();
            OnBeforeExecution(e);
            Execute();
            OnAfterExecution(e);
        }
        public async Task OpenAsync()
        {
            AssignParameterValues();
            var e = new EventArgs();
            OnBeforeExecution(e);
            await ExecuteAsync();
            OnAfterExecution(e);
        }

        public void Open(string Sql, cAccesoDatos Conn)
        {
            SQL = Sql;
            mConn = Conn;
            Open();
        }
        public async Task OpenAsync(string Sql, cAccesoDatos Conn)
        {
            SQL = Sql;
            mConn = Conn;
            await OpenAsync();
        }
        public abstract void Close();

        public virtual DbParameterCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }
        public List<ControlParameter> ControlParameters { set; get; }



        public RSFrame()
        {
            ControlParameters = new List<ControlParameter>();
            AutoUpdate = false;
        }

        protected virtual void OnAfterExecution(EventArgs e)
        {
            EventHandler<EventArgs> handler = AfterExecution;
            if (handler != null)
            {
                handler(this, e);
            }

        }

        protected virtual void OnBeforeExecution(EventArgs e)
        {
            EventHandler<EventArgs> handler = BeforeExecution;
            if (handler != null)
            {
                handler(this, e);
            }

        }

        public abstract void AddControlParameter(string ParamName, Object ParamControl);

        protected void RSFrame_TextChanged(object sender, EventArgs e)
        {
            Open();
        }

        public void AssignParameterValues()
        {
            ControlParameters.Where(x => x.LinkedControl is IsValuable).ToList().ForEach(p => p.Parameter.Value = ((IsValuable)p.LinkedControl).Value);
            ControlParameters.Where(x => !(x.LinkedControl is IsValuable)).ToList().ForEach(p => p.Parameter.Value = p.LinkedControl);
        }

        public abstract List<DataRow> getList();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RSFrame() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Close();
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public abstract class SPFrame: IDisposable
    {
        protected cAccesoDatos mConn;

        public List<ControlParameter> ControlParameters { set; get; }
        //public List<ObjectParameter> ObjectParameters { get; set; }
        public DbCommand Cmd { get; set; }
        private DbParameterCollection _parameters;

        public virtual DbParameterCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }
        public virtual cAccesoDatos Conn
        {
            get
            {
                return mConn;
            }
            set
            {
               mConn = (cAccesoDatos)value;
            }
        }

        public virtual DbConnection Connection
        {
            get
            {
                return Cmd.Connection;
            }
            set
            {
                Cmd.Connection = value;
            }
        }

        public abstract CommandType CommandType { get; set; }
        public string CommandText
        {
            get
            {
                return Cmd.CommandText;
            }
            set
            {
                Cmd.CommandText = value;
            }
        }
        public abstract bool MsgOut { get; }

        public string SPName { get; set; }
        public string LastMsg { get; set; }

        //public SPFrame(cAccesoDatos pConn, string pSPName = "")
        //{
        //    //Cmd = new SqlCommand();
        //    ControlParameters = new List<ControlParameter>();
        //    //ObjectParameters = new List<ObjectParameter>();
        //    SPName = pSPName;
        //    Conn = pConn;
        //    Connection = pConn.AdoCon;
        //    CommandType = System.Data.CommandType.StoredProcedure;
        //    CommandText = pSPName;
        //    ConnectionState prevState = mConn.State;
        //    if (prevState != ConnectionState.Open)
        //    {
        //        mConn.Open();
        //    }
        //   // DbCommandBuilder.DeriveParameters(Cmd);
        //    if (MsgOut)
        //    {
        //        Parameters["@msg"].Value = "";
        //    }
        //    Cmd.CommandTimeout = 300;
        //    if (prevState != ConnectionState.Open)
        //    {
        //        mConn.Close();
        //    }
        //}

        public virtual void AddParameterValue(string pParamName, object pValue, string DBFieldName = "")
        {
            try
            {
                if (pParamName.Substring(0, 1) != "@")
                {
                    pParamName = '@' + pParamName;
                }
                DateTime res;
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

        public abstract void AddControlParameter(string pParamName, object ParamControl);

        //public abstract void AssignOutputParameter(string ParamName, DbParameter pParam);

        public void AssignParameterValues()
        {
            if (ControlParameters != null)
                ControlParameters.Where(x => x.LinkedControl is IsValuable).ToList().ForEach(p => AddParameterValue(p.Parameter.ParameterName, ((IsValuable)p.LinkedControl).Value));
        }


        public virtual void AssignOutputParameterContainer(string ParamName, out DbParameter ParamOut, object Value = null)
        {
            DbParameter _param;
            if (ParamName.Substring(0, 1) != "@")
            {
                ParamName = '@' + ParamName;
            }
            if (Parameters[ParamName] != null)
            {
                _param = Parameters[ParamName];
                ParamOut = _param;
            }
            else
                ParamOut = null;

            if (Value != null)
            {
                AddParameterValue(ParamName, Value);
            }
            
            //ObjectParameters.Add(new ObjectParameter() {Container=Container, Parameter=_param });

        }

        public virtual void AssignValuesParameters()
        {
            ControlParameters.Where(x => x.LinkedControl is IsValuable && (x.Parameter.Direction == ParameterDirection.InputOutput || x.Parameter.Direction == ParameterDirection.Output)).ToList().ForEach(p => ((IsValuable)p.LinkedControl).Value = p.Parameter.Value);
        }
        public virtual void Execute()
        {
            AssignParameterValues();
            if (Conn.State == ConnectionState.Open)
            {
                Cmd.ExecuteNonQuery();
            }
            else
            {
                Conn.Open();
                try
                {
                    Cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Conn.Close();
                }

            }
            AssignValuesParameters();
            try
            {
                LastMsg = Parameters.OfType<DbParameter>().ToList().First(x => x.ParameterName == "@msg").Value.ToString();
            }
            catch
            {
                LastMsg = "";
            }
        }
        public virtual async Task ExecuteAsync()
        {
            AssignParameterValues();
            if (Conn.State == ConnectionState.Open)
            {
                await Cmd.ExecuteNonQueryAsync();
            }
            else
            {
                await Conn.OpenAsync();
                try
                {
                    await Cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Conn.Close();
                }

            }
            AssignValuesParameters();
            try
            {
                LastMsg = Parameters.OfType<DbParameter>().ToList().First(x => x.ParameterName == "@msg").Value.ToString();
            }
            catch
            {
                LastMsg = "";
            }

        }


        public virtual Dictionary<string, object> ReturnValues()
        {
            return Parameters.OfType<DbParameter>()
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Cmd.Dispose();
                    ControlParameters.Clear();
                    ControlParameters = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }




}
