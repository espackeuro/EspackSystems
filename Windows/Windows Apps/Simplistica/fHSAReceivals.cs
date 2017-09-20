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
    }
}
