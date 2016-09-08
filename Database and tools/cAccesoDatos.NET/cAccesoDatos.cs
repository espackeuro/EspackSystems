using CommonTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;

namespace AccesoDatosNet
{
    public abstract class cAccesoDatos : ICloneable, IDisposable
    {
        //public SqlConnection AdoCon { get; set; }
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
            IP = lIP;
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
        public cAccesoDatos(cAccesoDatosNet parent)
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
        public abstract DateTime ServerDate
        {
            get;
        }
        public abstract string HostName
        {
            get;
        }

        public abstract void Connect();

        public void Open()
        {
            Connect();
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
                    IP = null;
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
        protected bool mEOF;
        protected bool mBOF;
        protected RSState mState;
        protected int mIndex = 0;
        protected SqlParameterCollection _parameters;
        //events
        //Events
        public event EventHandler<EventArgs> AfterExecution; //launched when the query is executed
        public event EventHandler<EventArgs> BeforeExecution;
        //properties
        public string SQL { get; set; }
        public int Index
        {
            get
            {
                return mIndex;
            }
        }
        public bool EOF
        {
            get
            {
                return mEOF;
            }
        }
        public bool BOF
        {
            get
            {
                return mBOF;
            }
        }
        public RSState State
        {
            get
            {
                return mState;
            }
        }
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

        public List<object> ToList()
        {
            return getList();
        }

        public bool HasRows { get; set; }
        public bool AutoUpdate { get; set; }
        //public abstract SqlCommand Cmd { get; set; }

        public abstract void MoveNext();
        public abstract void MovePrevious();
        public abstract void MoveLast();
        public abstract void MoveFirst();
        public abstract void Move(int Idx);
        public abstract void Execute();
        public void Open()
        {
            AssignParameterValues();
            var e = new EventArgs();
            OnBeforeExecution(e);
            Execute();
            OnAfterExecution(e);
        }
        public void Open(string Sql, cAccesoDatosNet Conn)
        {
            SQL = Sql;
            mConn = Conn;
            Open();
        }

        public abstract void Close();

        public SqlParameterCollection Parameters
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

        public void AddControlParameter(string ParamName, Object ParamControl)
        {
            SqlParameter lParam = new SqlParameter()
            {
                ParameterName = ParamName
            };
            ControlParameters.Add(new ControlParameter()
            {
                Parameter = lParam,
                LinkedControl = ParamControl
            });
            Parameters.Add(lParam);
            if (AutoUpdate && ParamControl is IsValuable)
            {
                ((IsValuable)ParamControl).TextChanged += RSFrame_TextChanged;
            }
        }
        void RSFrame_TextChanged(object sender, EventArgs e)
        {
            Open();
        }

        public void AssignParameterValues()
        {
            ControlParameters.Where(x => x.LinkedControl is IsValuable).ToList().ForEach(p => p.Parameter.Value = ((IsValuable)p.LinkedControl).Value);
            ControlParameters.Where(x => !(x.LinkedControl is IsValuable)).ToList().ForEach(p => p.Parameter.Value = p.LinkedControl);
        }

        public abstract List<object> getList();

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

}
