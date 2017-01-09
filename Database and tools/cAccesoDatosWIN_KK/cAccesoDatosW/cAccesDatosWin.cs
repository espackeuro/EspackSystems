using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using EspackControls;

namespace AccesoDatosNet_WIN
{



    public class StaticRS_Controls : StaticRS
    {
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
            if (AutoUpdate && ParamControl is Control)
            {
                ((Control)ParamControl).TextChanged += RSFrame_TextChanged;
            }
        }

        void RSFrame_TextChanged(object sender, EventArgs e)
        {
            Open();
        }

        public void AssignParameterValues()
        {
            foreach (ControlParameter lParam in ControlParameters)
            {
                if (lParam.LinkedControl is EspackControl)
                {
                    lParam.Parameter.Value = ((EspackControl)lParam.LinkedControl).Value;
                }
                else
                {
                    lParam.Parameter.Value = lParam.LinkedControl;
                }

            }
        }
        public override void Open()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            Cmd = new SqlCommand(SQL, mConn.AdoCon);
            AssignParameterValues();
            mState = RSState.Executing;
            mDR = Cmd.ExecuteReader();
            mEOF = !mDR.Read();
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            mIndex = 1;
            OnExecution(new EventArgs());
        }
    }

    public class DynamicRS_Controls: DynamicRS
    {
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
            if (AutoUpdate && ParamControl is Control)
            {
                ((Control)ParamControl).TextChanged += RSFrame_TextChanged;
            }
        }

        void RSFrame_TextChanged(object sender, EventArgs e)
        {
            Open();
        }

        public void AssignParameterValues()
        {
            foreach (ControlParameter lParam in ControlParameters)
            {
                if (lParam.LinkedControl is EspackControl)
                {
                    lParam.Parameter.Value = ((EspackControl)lParam.LinkedControl).Value;
                }
                else
                {
                    lParam.Parameter.Value = lParam.LinkedControl;
                }

            }
        }

        public override void Open()
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            AssignParameterValues();
            mDS = new DataSet();
            mState = RSState.Executing;
            mDA.Fill(mDS, "Result");
            Index = 0;
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            OnExecution(new EventArgs());
        }
        public new void Open(int pCurrentIndex, int pPageSize)
        {
            ConnectionState prevState = mConn.State;
            if (prevState != ConnectionState.Open)
            {
                mConn.Open();
            }
            AssignParameterValues();
            mDS = new DataSet();
            mState = RSState.Executing;
            mDA.Fill(mDS, pCurrentIndex, pPageSize, "Result");
            Index = 0;
            mState = RSState.Open;
            if (prevState != ConnectionState.Open)
            {
                mConn.Close();
            }
            OnExecution(new EventArgs());
        }
        public DynamicRS_Controls(string Sql, cAccesoDatosNet Conn)
            : base(Sql,Conn)
        {
        }
    }

    public class SP_Controls:SP
    {
        public SP_Controls(cAccesoDatosNet pConn, string pSPName = "") : base(pConn, pSPName)
        {
        }

        public new void AssignParameterValues()
        {
            object lValue;
            foreach (ControlParameter lParam in ControlParameters)
            {
                if (lParam.LinkedControl is EspackControl)
                {
                    AddParameterValue(lParam.Parameter.ParameterName, ((EspackControl)lParam.LinkedControl).Value);
                }
                else
                {
                    lValue = lParam.LinkedControl;
                }
            }
        }

        public new void AssignValuesParameters()
        {
            foreach (ControlParameter lParam in ControlParameters)
            {
                if (lParam.Parameter.Direction == ParameterDirection.InputOutput || lParam.Parameter.Direction == ParameterDirection.Output)
                {
                    if (lParam.LinkedControl is EspackControl)
                    {
                        ((EspackControl)lParam.LinkedControl).Value = lParam.Parameter.Value;
                    }
                }
            }
            //ObjectParameters.ToList().ForEach(x => x.Container = x.Parameter.Value);
        }

    }

    public class DA_Controls : DA
    {
        protected new DynamicRS mSelectRS { get; set; }
        public new DynamicRS SelectRS
        {
            get
            {
                return mSelectRS;
            }
            set
            {
                mSelectRS = value;
                mDA.SelectCommand = value.Cmd;
            }
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
        public string SQL
        {
            set
            {
                SelectRS = new DynamicRS_Controls(value, Conn);
                ConnectionState prevState = Conn.State;
                //if (prevState != ConnectionState.Open)
                //{
                //    Conn.Open();
                //}
                //SqlCommandBuilder.DeriveParameters(SelectRS.Cmd);
                //if (prevState != ConnectionState.Open)
                //{
                //    Conn.Close();
                //}
            }
            get
            {
                return SelectRS.Cmd.CommandText;
            }
        }
    }

}
