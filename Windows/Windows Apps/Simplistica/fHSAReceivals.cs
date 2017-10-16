using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatosNet;
using EspackClasses;
using RawPrinterHelper;
using CommonToolsWin;
using CommonTools;

namespace Simplistica
{
    public partial class fHSAReceivals : Form
    {

        public enum EnumROBOT_Status { OK, ERR, RUN, INI, NONE }

        private EnumROBOT_Status mROBOT_Status;

        public EnumROBOT_Status getROBOT_Status()
        {
            return mROBOT_Status;
        }

        public void setROBOT_Status(EnumROBOT_Status newValue)
        {
            // Set the picture.
            switch (newValue)
            {
                case EnumROBOT_Status.OK:
                    pctRobotStatus.Image = simplistica.Properties.Resources.ok_30;
                    break;
                case EnumROBOT_Status.RUN:
                case EnumROBOT_Status.INI:
                    pctRobotStatus.Image = simplistica.Properties.Resources.process_30;
                    break;
                case EnumROBOT_Status.ERR:
                    pctRobotStatus.Image = simplistica.Properties.Resources.nook_30;
                    break;
                default:
                    pctRobotStatus.Image = null;
                    break;
            }
            pctRobotStatus.Show();

            // Enable/disable timer and set the new value.
            tmrRobot.Enabled = (newValue == EnumROBOT_Status.RUN) || (newValue == EnumROBOT_Status.INI);
            mROBOT_Status = newValue;
        }


        public fHSAReceivals()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "PAddHSAReceivalsCab";
            CTLM.sSPUpp = "PUppHSAReceivalsCab";
            CTLM.sSPDel = "PDelHSAReceivalsCab";
            CTLM.DBTable = "vHSAReceivalsCabCOD3";
            //CTLM.DBTable = "(Select H.*,DescService=S.Nombre from HSAReceivalsCab H inner join Servicios S on S.codigo=H.service where s.cod3='" + Values.COD3 + "' and dbo.CheckFlag(s.flags,'HSA')=1) a";

            //Header
            CTLM.AddItem(cboService, "service", true, true, false, 0, false, true);
            CTLM.AddItem(txtReceivalCode, "recCode", false, true, true, 1, true, true);
            CTLM.AddItem(txtContainer, "container", true, true, false, 0, false, true);
            CTLM.AddItem(txtPackingSlip, "packingSlip", true, true, false, 0, false, true);
            CTLM.AddItem(txtDate, "date", true, true, false, 0,false, false);
            CTLM.AddItem(lstFlags, "flags", true, true, false, 0, false, true);
            CTLM.AddItem(txtDescService, "DescService");
            CTLM.AddItem(Values.COD3, "cod3", pSearch: true);

            //fields
            cboService.Source("Select Codigo,Nombre from Servicios where dbo.CheckFlag(flags,'HSA')=1 and cod3='" + Values.COD3 + "' order by codigo", txtDescService);
            cboService.SelectedValueChanged += CboService_SelectedValueChanged;
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='HSAReceivalsCab'");

            //VS Definitions
            VS.Conn = Values.gDatos;
            VS.sSPAdd = "PAddHSAReceivalsDet";
            VS.sSPUpp = ""; // PUppHSAReceivalsDet
            VS.sSPDel = "";
            VS.DBTable = "vHSAReceivalsDet";

            //VS Details
            VS.AddColumn("RecCode", txtReceivalCode, "@RecCode");
            VS.AddColumn("Line", "line");
            VS.AddColumn("Partnumber", "partnumber", "@partnumber", pSortable: true, pWidth: 90, aMode: AutoCompleteMode.SuggestAppend, aSource: AutoCompleteSource.CustomSource, aQuery: string.Format("select partnumber from referencias where servicio='{0}'", cboService.Value));
            VS.AddColumn("Description", "description",pSortable: true, pWidth: 200);
            VS.AddColumn("Qty", "Qty", "@qty", pWidth: 90);
            VS.AddColumn("SupplierDoc", "SupplierDoc", "@supplierDoc");
            //VS.AddColumn("Flags", "Flags", "");
            VS.CellEndEdit += VS_CellEndEdit; //VS_CellValidating; ; ;

            //Various
            CTLM.AddDefaultStatusStrip();
            CTLM.AddItem(VS);
            CTLM.Start();
            CTLM.AfterButtonClick += CTLM_AfterButtonClick;
            CTLM.BeforeButtonClick += CTLM_BeforeButtonClick;
            //toolStrip.Enabled = false;

            setROBOT_Status(EnumROBOT_Status.NONE);
        }

        private void CTLM_BeforeButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            //(new string[]{"btnOk" }).Contains(e.ButtonClick)
            if (e.ButtonClick == "btnOk" && (CTLM.Status==EnumStatus.EDIT || CTLM.Status==EnumStatus.ADDNEW ))
            {
                using (var _RS = new StaticRS(string.Format("Select 0 from servicios where codigo='{0}' and dbo.checkflag(flags,'HSA')=1 and cod3='{1}'", cboService.Value, Values.COD3), Values.gDatos))
                {
                    _RS.Open();
                    if (_RS.RecordCount == 0)
                    {
                        MessageBox.Show(string.Format("Wrong service."), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void CTLM_AfterButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            //btnACheck.Enabled = lstFlags["PALETAGS"] == false && lstFlags["RECEIVED"] == true && ServiceFlags.Contains("AUTOCHECK");
            //btnReceived.Enabled = lstFlags["RECEIVED"] == false && txtEntrada.ToString() != "";
            //btnLabelCMs.Enabled = !ServiceFlags.Contains("AUTOCHECK");

            ROBOT_GetReceivalStatus(txtReceivalCode.Text);

        }

        private void CboService_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboService.Value.ToString() != "")
            {
                using (var _RS = new StaticRS(string.Format("Select flags from servicios where codigo='{0}' and dbo.checkflag(flags,'HSA')=1 and cod3='{1}'", cboService.Value, Values.COD3), Values.gDatos))
                {
                    _RS.Open();
                    if (_RS.RecordCount != 0)
                    {
                        //toolStrip.Enabled = true;
                        /*
                        ServiceFlags = _RS["flags"].ToString().Split('|');
                        btnLabelCMs.Enabled = !ServiceFlags.Contains("AUTOCHECK");
                        btnACheck.Enabled = ServiceFlags.Contains("AUTOCHECK");
                        ((CtlVSColumn)VS.Columns["PartNumber"]).AutoCompleteQuery = string.Format("select partnumber from referencias where servicio='{0}'", cboService.Value);
                        ((CtlVSColumn)VS.Columns["PartNumber"]).ReQuery();
                        */
                    }
                    else
                    {
                        cboService.Value = "";
                        txtDescService.Value = "";
                    }

                }
            }
            else
            {
                txtDescService.Value = "";
            }
            //    toolStrip.Enabled = false;
        }
    
        private void VS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && VS.CurrentCell.Value!="")
            {
                using (var _rs = new StaticRS(string.Format("Select Description=Descripcion from Referencias where partnumber='{0}' and Servicio='{1}'", VS[e.ColumnIndex, e.RowIndex].Value, cboService.Value), Values.gDatos))
                {
                    _rs.Open();
                    if (_rs.RecordCount == 0)
                    {
                        CTWin.MsgError("Wrong partnumber");
                        VS[e.ColumnIndex, e.RowIndex].Value = "";
                        VS[e.ColumnIndex + 1, e.RowIndex].Value = "";
                        VS.CurrentCell = VS[e.ColumnIndex, e.RowIndex];
                        //e.Cancel = true;
                    }
                    else
                    {


                        VS[e.ColumnIndex+1, e.RowIndex].Value = _rs["Description"].ToString();
                        VS.CurrentCell = VS[e.ColumnIndex + 1, e.RowIndex];
                    }
                }

            }
        }

        private void btnRobotProcess_Click(object sender, EventArgs e)
        {

            if (txtContainer.Text=="")
            {
                MessageBox.Show("Wrong container number.", "SIMPLISTICA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPackingSlip.Text == "")
            {
                MessageBox.Show("Wrong packing slip.", "SIMPLISTICA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Do you really want to execute IDC_RECEP_AX HSA process for receival "+txtReceivalCode.Text+"?", "SIMPLISTICA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _numProceso;

                // Add spaces to the left of the container and get the last 7 chars.
                string _container =new String(' ', 7) + txtContainer.Text;
                _container = _container.Substring(_container.Length - 7);

                // Launch the robot process.
                using (var _sp = new SP(Values.gDatos, "AUTOMATIZACION..pAddEjecucion"))
                {
                    _sp.AddParameterValue("@Proceso", "IDC_RECEP_AX");
                    _sp.AddParameterValue("@Fase", 1);
                    _sp.AddParameterValue("@Variables", "CONTAINER="+_container+"|RECEP="+txtReceivalCode.Text+"|PKGSLIP="+txtPackingSlip.Text+"|HSA=1");
                    try
                    {
                        _sp.Execute();
                    }
                    catch (Exception ex)
                    {
                        CTWin.MsgError("Error launching robot process: "+ex.Message);
                        return;
                    }
                    if (_sp.LastMsg.Substring(0, 2) != "OK")
                    {
                        CTWin.MsgError("Error launching robot process: "+_sp.LastMsg);
                        return;
                    }
                    // Get the process number.
                    _numProceso = _sp.LastMsg.Substring(4).ToInt();
                }
                // Change the receival status.
                ROBOT_SetReceivalStatus(_numProceso, txtReceivalCode.Text, EnumROBOT_Status.INI);

                // Inform the user.
                CTLM.StatusMsg("Process launched.");
                MessageBox.Show("Process launched OK.", "SIMPLISTICA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Enable timer.
                tmrRobot.Enabled = true;

            }
        }

        private void tmrRobot_Tick(object sender, EventArgs e)
        {
            tmrRobot.Enabled = false;
            ROBOT_GetReceivalStatus(txtReceivalCode.Text);
        }

        // Changing the status of a receival.
        private void ROBOT_SetReceivalStatus(int processNumber, string receivalCode, EnumROBOT_Status newStatus)
        {
            string _status="PREC_"+newStatus.ToString();

            using (var _sp = new SP(Values.gDatos, "AUTOMATIZACION..pRECIDCAX_ChangeStatus"))
            {
                _sp.AddParameterValue("@NumProceso", processNumber);
                _sp.AddParameterValue("@receival", receivalCode);
                _sp.AddParameterValue("@status", _status);
                _sp.AddParameterValue("@hsa", 1);
                try
                {
                    _sp.Execute();
                }
                catch (Exception ex)
                {
                    CTWin.MsgError("Error updating receival status: " + ex.Message);
                    return;
                }
                if (_sp.LastMsg.Substring(0, 2) != "OK")
                {
                    CTWin.MsgError("Error updating receival status: " + _sp.LastMsg);
                    return;
                }
            }

            // Set the given status.
            setROBOT_Status(newStatus);
        }

        // Getting the status of a receival.
        private void ROBOT_GetReceivalStatus(string receivalCode)
        {
            EnumROBOT_Status _status;

            using (var _rs = new StaticRS(string.Format("Select status=case when dbo.CheckFlag(flags,'PREC_OK')=1 then 'OK' when dbo.CheckFlag(flags,'PREC_INI')=1 then 'INI' when dbo.CheckFlag(flags,'PREC_RUN')=1 then 'RUN' when dbo.CheckFlag(flags,'PREC_ERR')=1 then 'ERR' else '' end from HSAReceivalsCab where RecCode='{0}'", receivalCode), Values.gDatos))
            {
                _rs.Open();
                if (_rs.RecordCount == 0)
                {
                    CTWin.MsgError("Receival does not exist.");
                    _status = EnumROBOT_Status.NONE;
                }
                else
                {
                    switch (_rs["status"].ToString())
                    {
                        case "OK":
                            _status = EnumROBOT_Status.OK;
                            break;
                        case "RUN":
                            _status = EnumROBOT_Status.RUN;
                            break;
                        case "ERR":
                            _status = EnumROBOT_Status.ERR;
                            break;
                        case "INI":
                            _status = EnumROBOT_Status.INI;
                            break;
                        default:
                            _status = EnumROBOT_Status.NONE;
                            break;
                    }
                }
            }

            // Set the obtained status.
            setROBOT_Status(_status);
        }
    }
}
