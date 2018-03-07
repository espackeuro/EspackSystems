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
using static CommonToolsWin.CTWin;
using VSGrid;
using CTLMantenimientoNet;

namespace Simplistica
{
    public partial class fSimpleDeliveriesEPC: Form
    {
        public fSimpleDeliveriesEPC()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pSimpleDeliveriesCabAdd";
            CTLM.sSPUpp = "pSimpleDeliveriesCabUpp";
            CTLM.sSPDel = "pSimpleDeliveriesCabDel";
            CTLM.DBTable = "(Select v.* from vSimpleDeliveriesCab v inner join Servicios s on s.codigo=v.service where s.cod3='" + Values.COD3 + "' and dbo.CheckFlag(s.flags,'SIMPLE')=1) a";

            //Header
            CTLM.AddItem(txtDeliveryN, "DeliveryNumber", false, true, true, 1, true, true);
            CTLM.AddItem(cboService, "Service", true, true, true, 0, false, true);
            CTLM.AddItem(txtPlate, "TruckPlate", true, true, false, 0, false,true);
            CTLM.AddItem(txtUser, "UserProc", true, true, false, 0, false, true);
            CTLM.AddItem(cboShift, "Shift", true, true, false, 0, false, true);
            CTLM.AddItem(cboDock, "Dock", true, true, false, 0, false, true);
            CTLM.AddItem(cboDestination, "Destination", true, true, false, 0, false, true);
            CTLM.AddItem(dateStart, "StartDate", true, true, false, 0, false, true);
            CTLM.AddItem(dateEnd, "EndDate", true, true, false, 0, false, true);
            CTLM.AddItem(lstFlags, "flags", false, false, false, 0, false, true);

            //fields
            cboService.Source("Select Codigo from Servicios where dbo.CheckFlag(flags,'SIMPLE')=1 and cod3='" + Values.COD3 + "' order by codigo");
            cboService.SelectedValueChanged += CboService_SelectedValueChanged;
            cboDock.Source("Select DockCode from SedesDocks where cod3='" + Values.COD3 + "' order by DockCode");
            cboDestination.Source("Select Destination=planta from Servicios_Destinos where servicio='" + cboService.Value.ToString() + "' order by planta");
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='SimpleDeliveriesCab'");

            //VS Definitions
            VS.Conn = Values.gDatos;
            VS.sSPAdd = "pSimpleDeliveriesDetAdd";
            VS.sSPUpp = "pSimpleDeliveriesDetUpp";
            VS.sSPDel = "pSimpleDeliveriesDetDel";
            VS.DBTable = "vSimpleDeliveriesDet";

            //VS Details
            VS.AddColumn("DeliveryNumber", txtDeliveryN, "@DeliveryNumber", "@DeliveryNumber", "@DeliveryNumber",pVisible:false);
            VS.AddColumn("Service", cboService, "@Service", "@Service", "@Service", pVisible: false);
            VS.AddColumn("Line", "Line","","@Line", "@Line",pSortable:true,pLocked:true);
            VS.AddColumn("PartNumber", "partnumber", "@partnumber", pSortable: true, pWidth: 90, aMode: AutoCompleteMode.SuggestAppend, aSource: AutoCompleteSource.CustomSource, aQuery: string.Format("select partnumber from referencias where servicio='{0}'", cboService.Value));
            VS.AddColumn("Description","Description", pWidth: 160);
            VS.AddColumn("OrderedQty", "OrderedQty", "@OrderedQty", "@OrderedQty", pWidth: 90);
            VS.AddColumn("SentQty", "SentQty", "@SentQty", "@SentQty", pWidth: 90);
            VS.CellEndEdit += VS_CellEndEdit; //VS_CellValidating; ; ;

            //Various
            CTLM.AddDefaultStatusStrip();
            CTLM.AddItem(VS);
            CTLM.Start();
            CTLM.AfterButtonClick += CTLM_AfterButtonClick;
            toolStrip.Enabled = false;
        }

        private void CTLM_AfterButtonClick(object sender, CTLMEventArgs e)
        {

            toolStrip.Enabled = (CTLM.Status == CommonTools.EnumStatus.NAVIGATE);
            toolStrip.Enabled = true; // The event that changes the status happens after this AfterButtonClick, so we left this enabled until I decide what to do.

        }

        private void CboService_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboService.Value.ToString() != "")
            { 
                ((CtlVSColumn)VS.Columns["PartNumber"]).AutoCompleteQuery = string.Format("select partnumber from referencias where servicio='{0}'", cboService.Value);
                ((CtlVSColumn)VS.Columns["PartNumber"]).ReQuery();

                cboDestination.Source("Select Destination=planta from Servicios_Destinos where servicio='" + cboService.Value.ToString() + "' order by planta");
            }
               
        }

        private void VS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (VS.Columns["PartNumber"].Index)) 
            {
                using (var _rs = new StaticRS(string.Format("Select Descripcion from Referencias where partnumber='{0}' and Servicio='{1}'", VS[e.ColumnIndex, e.RowIndex].Value, cboService.Value), Values.gDatos))
                {
                    _rs.Open();
                    if (_rs.RecordCount == 0)
                    {
                        MsgError("Wrong partnumber");
                        VS[e.ColumnIndex, e.RowIndex].Value = "";
                        VS[VS.Columns["Description"].Index, e.RowIndex].Value = "";
                    }
                    else
                    {
                        VS[VS.Columns["Description"].Index, e.RowIndex].Value = _rs["Descripcion"].ToString();
                        VS.CurrentCell = VS[VS.Columns["Description"].Index, e.RowIndex];
                    }
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            // Launch the robot process.
            using (var _sp = new SP(Values.gDatos, "pSimpleDeliveriesChangeStatus"))
            {
                _sp.AddParameterValue("@DeliveryNumber", txtDeliveryN.Text);
                _sp.AddParameterValue("@Service",cboService.Value.ToString());
                _sp.AddParameterValue("@Action", "CLOSE");
                try
                {
                    _sp.Execute();
                }
                catch (Exception ex)
                {
                    MsgError("Error launching process: " + ex.Message);
                    return;
                }
                if (_sp.LastMsg.Substring(0, 2) != "OK")
                {
                    MsgError(_sp.LastMsg);
                    return;
                }
                MessageBox.Show(string.Format("Delivery closed OK."), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
