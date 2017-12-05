using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
/*
using System.Windows.Forms;
using EspackControls;
*/
using CommonTools;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using AccesoDatos;

namespace AccesoDatosNet
{


    public class ObjectParameter
    {
        public object Container;
        public SqlParameter Parameter;
    }
    

    public class cAccesoDatosNet : cAccesoDatos, IDisposable
    {
        public new SqlConnection AdoCon { get; set; }
        public override System.Data.ConnectionState State
        {
            get
            {
                return AdoCon.State;
            }
        }

        public string ConnectionString
        {
            get
            {
                return AdoCon.ConnectionString;
            }
            set
            {
                AdoCon.ConnectionString = value;
            }
        }
           

        //constructores
        public cAccesoDatosNet():
            base()
        {
            AdoCon = new SqlConnection();
            oServer.Resolve = true;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var lIP = ipHostInfo.AddressList.FirstOrDefault(x => x.GetAddressBytes()[0] == 10);
            if (lIP == null)
                lIP = ipHostInfo.AddressList.First(x => x.GetAddressBytes()[0] == 192);
            IP = lIP;
            
        }

        public cAccesoDatosNet(string pServer, string pDataBase, string pUser, string pPassword)
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

        public cAccesoDatosNet(cServer pServer, string pDataBase)
            : this()
        {
            oServer = pServer;
            DataBase = pDataBase;
        }
        public cAccesoDatosNet(cAccesoDatosNet parent)
            :this()
        {
            Server = parent.Server;
            DataBase = parent.DataBase;
            User = parent.User;
            Password = parent.Password;
            //AdoCon.ConnectionString = parent.ConnectionString;
        }
        public cAccesoDatosNet(EspackParamArray pParams)
            : this()
        {
            Server = pParams.Server;
            DataBase = pParams.DataBase;
            User = pParams.User;
            Password = pParams.Password;
            Cod3 = pParams.Cod3;
            AppName = pParams.AppName;
        }
        public override DateTime ServerDate()
        {
                var lRs = new DynamicRS();
                lRs.Open("Select Date=convert(varchar,Getdate(),120)", this);
                if (!lRs.HasRows)
                {
                    throw new Exception("Server not available");
                }
                string[] lDateTot = lRs[0].ToString().Split(' ');
                lRs.Close();
                string[] lDate = lDateTot[0].Split('-');
                string[] lTime = lDateTot[1].Split(':');
                DateTime lRes = new DateTime(Convert.ToInt32(lDate[0]), Convert.ToInt32(lDate[1]), Convert.ToInt32(lDate[2]), Convert.ToInt32(lTime[0]), Convert.ToInt32(lTime[1]), Convert.ToInt32(lTime[2]));
                return lRes;
        }
        public override async Task<DateTime> ServerDateAsync()
        {
            var lRs = new DynamicRS();
            await lRs.OpenAsync("Select Date=convert(varchar,Getdate(),120)", this);
            if (!lRs.HasRows)
            {
                throw new Exception("Server not available");
            }
            string[] lDateTot = lRs[0].ToString().Split(' ');
            lRs.Close();
            string[] lDate = lDateTot[0].Split('-');
            string[] lTime = lDateTot[1].Split(':');
            DateTime lRes = new DateTime(Convert.ToInt32(lDate[0]), Convert.ToInt32(lDate[1]), Convert.ToInt32(lDate[2]), Convert.ToInt32(lTime[0]), Convert.ToInt32(lTime[1]), Convert.ToInt32(lTime[2]));
            return lRes;
        }
        public override string HostName()
        {
                var lRs = new DynamicRS();
                lRs.Open("Select HostName=host_name()", this);
                if (lRs.EOF)
                {
                    throw new Exception("Server not available");
                }
                string lRes = lRs[0].ToString();
                lRs.Close();
                return lRes;
        }
        public async override Task<string> HostNameAsync()
        {
            var lRs = new DynamicRS();
            await lRs.OpenAsync("Select HostName=host_name()", this);
            if (lRs.EOF)
            {
                throw new Exception("Server not available");
            }
            string lRes = lRs[0].ToString();
            lRs.Close();
            return lRes;
        }
        public override void Connect()
        {
            try
            {
                if (AdoCon.State == ConnectionState.Open)
                    Close();
                string newConnectionString = "";
                if (User != "SSPI")
                    newConnectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", Server, DataBase, User, Password);
                else
                    newConnectionString = string.Format("Server={0};Initial Catalog={1};Integrated Security=SSPI;MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", Server, DataBase);
                if (newConnectionString != AdoCon.ConnectionString)
                    AdoCon.ConnectionString = newConnectionString;
                AdoCon.Open();
                if (context_info != null)
                {
                    SqlCommand cmd = AdoCon.CreateCommand();
                    string lSql = "set CONTEXT_INFO @C";
                    cmd.CommandText = lSql;
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@C";
                    param.DbType = DbType.Binary;
                    param.Size = 128;
                    param.Value = context_info;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async override Task ConnectAsync()
        {
            try
            {
                if (AdoCon.State == ConnectionState.Open)
                    Close();
                string newConnectionString = "";
                if (User != "SSPI")
                    newConnectionString = "Server=" + Server.Replace(".local", "") + ";Initial Catalog=" + DataBase + ";User Id=" + User + ";Password=" + Password + ";MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3";
                else
                    newConnectionString = "Server=" + Server.Replace(".local", "") + ";Initial Catalog=" + DataBase + ";Integrated Security=SSPI;MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3";
                if (newConnectionString != AdoCon.ConnectionString)
                    AdoCon.ConnectionString = newConnectionString;
                await AdoCon.OpenAsync();
                if (context_info != null)
                {
                    SqlCommand cmd = AdoCon.CreateCommand();
                    string lSql = "set CONTEXT_INFO @C";
                    cmd.CommandText = lSql;
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@C";
                    param.DbType = DbType.Binary;
                    param.Size = 127;
                    param.Value = context_info;
                    cmd.Parameters.Add(param);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                throw;
            }

        }
        public override void Close()
        {
            AdoCon.Close();
        }

        public new cAccesoDatosNet Clone()
        {
            return (cAccesoDatosNet)this.MemberwiseClone();
        }


        #region IDisposable Support
        private new bool disposedValue = false; // To detect redundant calls

        protected new virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    AdoCon.Close();
                    AdoCon.Dispose();
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


    

    public class StaticRS :RSFrame
    {
        protected new cAccesoDatosNet mConn = null;
        protected SqlDataReader mDR = null;
        protected List<DataRow> Result = null;
        //private cAccesoDatosNet mConn = null;
        //Events
        //public new event EventHandler<EventArgs> AfterExecution; //launched when the query is executed
//properties

        public SqlCommand Cmd { get; set; }

        public override object this[string Idx] => RecordCount!=0? Result[Index][Idx]:null; //Field<string>(Idx):null;
        public override object this[int Idx] => Result[Idx];

        public new bool HasRows
        {
            get
            {
                return mDR.HasRows;
            }
        }

        public override object DataObject
        {
            get
            {
                return mDR;
            }
        }

        public override List<DataRow> Rows
        {
            get
            {
                return Result;
            }
        }

        public override int RecordCount
        {
            get
            {
                return Result.Count;
            }
        }

        public override int FieldCount
        {
            get
            {
                return Result[0].Table.Columns.Count;
            }
        }

        public override List<string> Fields
        {
            get
            {
                return mDR.GetSchemaTable().Rows.OfType<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();
            }
        }

        public StaticRS() 
            :base()
        {
            mDR=null;
            Cmd=new SqlCommand();
            mState = RSState.Closed;

        }

        public StaticRS(string Sql, cAccesoDatosNet Conn)
            : base()
        {
            SQL = Sql; 
            mConn = Conn;
            mState = RSState.Closed;
        }
        public override void Execute()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            Cmd = new SqlCommand(SQL, mConn.AdoCon);
            mState = RSState.Executing;
            try
            {
                mDR = Cmd.ExecuteReader();
                //EOF = ! mDR.Read();
                var _dt = new DataTable();
                _dt.Load(mDR);
                Result = _dt.Rows.OfType<DataRow>().ToList();
                EOF = Result.Count() == 0;
                Index = 0;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            Index = 0;
        }
        public override async Task ExecuteAsync()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                await mConn.OpenAsync();
            }
            Cmd = new SqlCommand(SQL, mConn.AdoCon);
            mState = RSState.Executing;
            try
            {
                mDR = await Cmd.ExecuteReaderAsync();
                var _dt = new DataTable();
                await Task.Run(() => _dt.Load(mDR));
                Result = _dt.Rows.OfType<DataRow>().ToList();
                EOF = Result.Count() == 0;
                Index = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            Index = 0;
        }
        public async Task MoveNextAsync()
        {
            await Task.Run(() => MoveNext());
        }

        //public override void MoveNext(bool silent=true)
        //{
        //    mState = RSState.Fetching;
        //    if (mDR.Read())
        //    {
        //        EOF = false;
        //    }
        //    else
        //        EOF = true;
        //    mState = RSState.Open;
        //}
        //public override void Move(int Idx) { }

        public override void Close()
        {
            mDR.Close();
            mDR = null;
            Cmd = null;
            Result = null;
            mState = RSState.Closed;
        }
        ~StaticRS()
        {
            if (mDR!= null)
                mDR.Close();
            mDR = null;
            Cmd = null;
            Result = null;
        }


        public override List<DataRow> getList() 
        {
            return Result;
        }

        public override void AddControlParameter(string ParamName, object ParamControl)
        {
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
        }
    }

    public class DynamicRS : RSFrame
    {
        protected new cAccesoDatosNet mConn = null;
        protected DataSet mDS;
        protected SqlDataAdapter mDA = new SqlDataAdapter();
        //Events
        //new public event EventHandler<EventArgs> AfterExecution; //launched when the query is executed     
        //new public event EventHandler<EventArgs> BeforeExecution;
        protected new SqlParameterCollection _parameters;

        public override DbParameterCollection Parameters
        {
            get
            {
                return Cmd.Parameters;
            }
        }

        //private cAccesoDatosNet mConn;
        public override int RecordCount
        {
            get
            {
                return mDS.Tables["Result"].Rows.Count;
            }
        }

        public override object this[string Idx]
        {
            get
            {
                return mDS.Tables["Result"].Rows[Index][Idx];
            }
        }
        public override object this[int Idx]
        {
            get
            {
                return mDS.Tables["Result"].Rows[Index][Idx];
            }
        }
        public override int FieldCount
        {
            get
            {
                return mDS.Tables["Result"].Columns.Count;
            }
        }

        public override List<string> Fields
        {
            get
            {
                return mDS.Tables["Result"].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            }
        }

        public SqlDataAdapter DA
        {
            get
            {
                return mDA;
            }
            set
            {
                mDA = value;
            }
        }

        public DataSet DS
        {
            get
            {
                return mDS;
            }
        }

        public SqlCommand Cmd
        {
            get
            {
                return mDA.SelectCommand;
            }
            set
            {
                mDA.SelectCommand = value;
            }
        }

        public XDocument XMLData
        {
            get
            {
                using (var _stream = new MemoryStream())
                {
                    mDS.Tables["Result"].WriteXml(_stream);
                    _stream.Position = 0;
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.CheckCharacters = false;
                    XmlReader reader = XmlReader.Create(_stream, settings);
                    reader.MoveToContent();
                    if (reader.IsEmptyElement) { reader.Read(); return null; }
                    try
                    {
                        var _res = XDocument.Load(reader);
                        return _res;
                    } catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
            }
        }

        public override List<DataRow> ToList()
        {
            return mDS.Tables["Result"].Rows.Cast<DataRow>().ToList();
        }
        public override object DataObject
        {
            get
            {
                if (mDS == null)
                    Open();
                return mDS.Tables["Result"];
            }
        }

        public override List<DataRow> Rows
        {
            get
            {
                return mDS.Tables["Result"].Rows.OfType<DataRow>().ToList();
            }
        }

        public DynamicRS()
            : base()
        {
            //mDS = new DataSet();
            
        }
        public DynamicRS(string Sql, cAccesoDatosNet Conn)
            : base()
        {
            SQL = Sql; 
            mConn = Conn;
            //mDS = new DataSet();
            mDA.SelectCommand = new SqlCommand(SQL, mConn.AdoCon);
            
        }
        
        public override void Execute()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            if (mDA.SelectCommand== null)
            {
                mDA.SelectCommand = new SqlCommand(SQL, mConn.AdoCon);
            }
            mDS = new DataSet();
            mState = RSState.Executing;
            //mDA.Fill(mDS, "Result");
            mDA.Fill(mDS, "Result");
            //await Task.Run(()=> mDA.Fill(mDS, "Result"));
            MoveFirst();
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
        }
        public override async Task ExecuteAsync()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                await mConn.OpenAsync();
            }
            if (mDA.SelectCommand == null)
            {
                mDA.SelectCommand = new SqlCommand(SQL, mConn.AdoCon);
            }
            mDS = new DataSet();
            mState = RSState.Executing;
            //mDA.Fill(mDS, "Result");
            Task t = Task.Run(() => mDA.Fill(mDS, "Result"));

            t.Wait();
            //await Task.Run(()=> mDA.Fill(mDS, "Result"));
            MoveFirst();
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
        }
        public void Open(string Sql, cAccesoDatosNet Conn)
        {
            SQL = Sql;
            mConn = Conn;
            Open();
        }

        public async Task OpenAsync(string Sql, cAccesoDatosNet Conn)
        {
            SQL = Sql;
            mConn = Conn;
            await OpenAsync();
        }

        public void Open(int pCurrentIndex, int pPageSize)
        {
            AssignParameterValues();
            var e = new EventArgs();
            OnBeforeExecution(e);

            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            mDS = new DataSet();
            mState = RSState.Executing;
            mDA.Fill(mDS, pCurrentIndex, pPageSize, "Result");
            MoveFirst();
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            OnAfterExecution(e);
        }
        public async Task OpenAsync(int pCurrentIndex, int pPageSize)
        {
            AssignParameterValues();
            var e = new EventArgs();
            OnBeforeExecution(e);

            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                await mConn.OpenAsync();
            }
            mDS = new DataSet();
            mState = RSState.Executing;
            await Task.Run(() => mDA.Fill(mDS, pCurrentIndex, pPageSize, "Result"));
            MoveFirst();
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            OnAfterExecution(e);
        }
        public void FillSchema()
        {
            mDS = new DataSet();
            mDA.FillSchema(DS, SchemaType.Mapped);
        }



        public override void Close()
        {
            mDS = null;
            mDA = null;
        }

        ~DynamicRS()
        {
            mDS = null;
            mDA = null;
        }
        public override List<DataRow> getList()
        {
            //var _list = new List<DbDataRecord>();
            return mDS.Tables["Result"].Rows.OfType<DataRow>().ToList();
        }


        public string GetXml()
        {
            return mDS.GetXml();
        }

        public override void AddControlParameter(string ParamName, object ParamControl)
        {
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
        }
    }


    public class SP:SPFrame
    {
        private new cAccesoDatosNet mConn;
        
        //public List<ObjectParameter> ObjectParameters { get; set; }
        public new SqlCommand Cmd { get; set; }

        public new SqlParameterCollection Parameters
        {
            get 
            {
                return Cmd.Parameters;
            }
        }
        public new SqlConnection Connection
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

        public new cAccesoDatosNet Conn
        {
            get
            {
                return mConn;
            }
            set
            {
                mConn = value;
                Cmd.Connection = mConn.AdoCon;
            }
        }
        public override CommandType CommandType
        {
            get
            {
                return Cmd.CommandType;
            }
            set
            {
                Cmd.CommandType = value;
            }
        }
        public new string CommandText
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
        public SP(cAccesoDatosNet pConn, string pSPName = "")
        {
            Cmd = new SqlCommand();
            ControlParameters = new List<ControlParameter>();
            //ObjectParameters = new List<ObjectParameter>();
            SPName = pSPName;
            Conn = (cAccesoDatosNet)pConn;
            Connection = Conn.AdoCon;
            CommandType = System.Data.CommandType.StoredProcedure;
            CommandText = pSPName;
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Connect();
            }
            SqlCommandBuilder.DeriveParameters(Cmd);
            if (MsgOut)
            {
                Parameters["@msg"].Value = "";
            }
            Cmd.CommandTimeout = 300;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
        }
        public SP(cAccesoDatos pConn, string pSPName="")
            : this((cAccesoDatosNet)pConn, pSPName)
        {
        }
        public override void AddParameterValue(string pParamName, object pValue, string DBFieldName="" )
        {
            try
            {
                pParamName = pParamName.Replace("[", "").Replace("]", "");
                if (pParamName.Substring(0, 1) != "@")
                {
                    pParamName = '@' + pParamName;
                }
                DateTime res;
                if (Parameters[pParamName].SqlDbType.IsNumericType())
                {
                    if (pValue != null && pValue.ToString()=="")
                    {
                        pValue = null;
                    }
                }

                if (Parameters[pParamName].SqlDbType == SqlDbType.Timestamp)
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
            pParamName = pParamName.Replace("[", "").Replace("]", "");
            if (pParamName.Substring(0, 1) != "@")
            {
                pParamName = '@' + pParamName;
            }
            SqlParameter lParam;
            if (Parameters[pParamName] != null)
            {
                lParam = Parameters[pParamName];
            }
            else
            {
                lParam = new SqlParameter()
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
        //public override void AssignOutputParameter(string ParamName, DbParameter pParam)
        //{
        //    if (Parameters[ParamName] != null)
        //    {
        //        pParam = Parameters[ParamName];
        //    }
        //    else
        //    {
        //        pParam = new SqlParameter()
        //        {
        //            ParameterName = ParamName
        //        };
        //        Parameters.Add(pParam);
        //    }

        //    OutputParameters.Add(pParam);
        //}

        public void AssignOutputParameterContainer(string ParamName, out SqlParameter ParamOut, object Value=null)
        {
            SqlParameter _param;
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
                _param = new SqlParameter()
                {
                    ParameterName = ParamName
                };
                Parameters.Add(_param);
            }
            if (Value!= null)
            {
                AddParameterValue(ParamName, Value);
            }
            ParamOut = _param;
            //ObjectParameters.Add(new ObjectParameter() {Container=Container, Parameter=_param });
            
        }
        public override void AssignValuesParameters()
        {
            ControlParameters.Where(x => x.LinkedControl is IsValuable && (x.Parameter.Direction == ParameterDirection.InputOutput || x.Parameter.Direction == ParameterDirection.Output)).ToList().ForEach(p => ((IsValuable)p.LinkedControl).Value= p.Parameter.Value);
        }

        public override void Execute()
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
                LastMsg = Parameters.OfType<SqlParameter>().ToList().First(x => x.ParameterName == "@msg").Value.ToString();
            }
            catch
            {
                LastMsg = "";
            }

        }
        public override async Task ExecuteAsync()
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
                LastMsg = Parameters.OfType<SqlParameter>().ToList().First(x => x.ParameterName == "@msg").Value.ToString();
            }
            catch
            {
                LastMsg = "";
            }

        }
        public override Dictionary<string, object> ReturnValues()
        {
            return Parameters.OfType<SqlParameter>()
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

        public XDocument XParameters
        {
            get
            {
                var _x = new XElement("parameters");
                Parameters.OfType<SqlParameter>().ToList().ForEach(p => {
                    var _p = new XElement("parameter");
                    _p.Add(new XElement("Name", p.ParameterName));
                    _p.Add(new XElement("Direction", p.Direction));
                    _p.Add(new XElement("Value", p.Value));
                    _p.Add(new XElement("Type", p.SqlDbType));
                    _x.Add(_p);
                });
                return new XDocument(_x);
            }
        }
        public XDocument XOutParameters
        {
            get
            {
                var _x = new XElement("parameters");
                Parameters.OfType<SqlParameter>().Where(o => o.Direction== ParameterDirection.InputOutput || o.Direction== ParameterDirection.Output).ToList().ForEach(p => {
                    var _p = new XElement("parameter");
                    _p.Add(new XElement("Name", p.ParameterName));
                    _p.Add(new XElement("Direction", p.Direction));
                    _p.Add(new XElement("Value", p.Value));
                    _p.Add(new XElement("Type", p.SqlDbType));
                    _x.Add(_p);
                });
                return new XDocument(_x);
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
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

        #endregion
    }

    public class DA
    {
        protected SqlDataAdapter mDA;
        //private DataSet mDS;
        protected string msSPAdd = "";
        protected string msSPUpp = "";
        protected string msSPDel = "";
        protected DynamicRS mSelectRS { get; set; }
        protected SP mInsertCommand;
        protected SP mUpdateCommand;
        protected SP mDeleteCommand;
        public cAccesoDatosNet Conn { get;set; }


        public virtual DynamicRS SelectRS
        {
            get
            {
                return mSelectRS;
            }
            set 
            {
                mSelectRS=value;
                mDA.SelectCommand = value.Cmd;
            }
        }

        public DataTable Table
        {
            get
            {
                return SelectRS.DS.Tables["Result"];
            }
        }

        public SP InsertCommand
        {
            get
            {
                return mInsertCommand;
            }
            set
            {
                mInsertCommand = value;
                mDA.InsertCommand = value.Cmd;
            }
        }
        public SP UpdateCommand
        {
            get
            {
                return mUpdateCommand;
            }
            set
            {
                mUpdateCommand = value;
                mDA.UpdateCommand = value.Cmd;
            }
        }
        public SP DeleteCommand
        {
            get
            {
                return mDeleteCommand;
            }
            set
            {
                mDeleteCommand = value;
                mDA.DeleteCommand = value.Cmd;
            }
        }


        //Properties with SPs but in string format, we will use this when assigning a SP by its name
        public string sSPAdd
        {
            set
            {
                if (value != "") InsertCommand = new SP(Conn, value);
                msSPAdd = value;
            }
            get
            {
                return msSPAdd;
            }
        }

        public string sSPUpp
        {
            set
            {
                if (value != "") UpdateCommand = new SP(Conn, value);
                msSPUpp = value;
            }
            get
            {
                return msSPUpp;
            }
        }

        public string sSPDel
        {
            set
            {
                if (value != "") DeleteCommand = new SP(Conn, value);
                msSPDel = value;
            }
            get
            {
                return msSPDel;
            }
        }

        public string SQL
        {
            set
            {
                SelectRS = new DynamicRS(value, Conn);
                ConnectionState prevState = Conn.State;
            }
            get
            {
                return SelectRS.Cmd.CommandText;
            }
        }
        public SqlParameterCollection Parameters
        {
            get
            {
                return (SqlParameterCollection)SelectRS.Parameters;
            }
        }
        
        public DataColumnCollection Schema
        {
            get
            {
                if (mSelectRS.DS == null || mSelectRS.DS.Tables["Table"]==null) 
                {
                    try
                    {
                        FillSchema();
                    }
                    catch 
                    {
                        return null;
                    }

                }
                return mSelectRS.DS.Tables["Table"].Columns;
            }
        }

        public void Update()
        {
            mDA.Update(SelectRS.DS,"Result");
        }

        public void Open()
        {
            SelectRS.Open();
        }
        public async Task OpenAsync()
        {
            await SelectRS.OpenAsync();
        }
        public void Open(int pCurrentIndex, int pPageSize)
        {
            SelectRS.Open(pCurrentIndex, pPageSize);
        }
        public async Task OpenAsync(int pCurrentIndex, int pPageSize)
        {
            await SelectRS.OpenAsync(pCurrentIndex, pPageSize);
        }
        public void FillSchema()
        {
            SelectRS.FillSchema();
        }
        public DA(cAccesoDatosNet pConn)
        {
            Conn = pConn;
            mDA = new SqlDataAdapter();
        }
        public DA(cAccesoDatosNet pConn, string pSql)
        {
            Conn = pConn;
            mDA = new SqlDataAdapter();
            SQL = pSql;
        }
        public DA()
        {
            mDA = new SqlDataAdapter();
        }

        public void AddParameter(string pParameterName, Object LinkedControl = null)
        {
            if (LinkedControl != null)
            {
                mSelectRS.AddControlParameter(pParameterName, LinkedControl);
            }
            else
            {
                SqlParameter lParam = new SqlParameter();
                lParam.ParameterName = pParameterName;
                Parameters.Add(lParam);
            }
        }
    }






}
