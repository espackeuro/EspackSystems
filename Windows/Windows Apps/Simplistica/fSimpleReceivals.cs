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
using CommonToolsWin;
using VSGrid;

namespace Simplistica
{
    public partial class fSimpleReceivals : Form
    {
        public fSimpleReceivals()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "PAdd_Cab_Recepcion";
            CTLM.sSPUpp = "PUpp_Cab_Recepcion";
            CTLM.sSPDel = "PDel_Cab_Recepcion";
            CTLM.DBTable = "Cab_Recepcion";

            //Header
            CTLM.AddItem(txtEntrada, "Entrada", false, true, true, 1, true, true);
            CTLM.AddItem(txtFecha, "Fecha", true, true, false, 0, false, false);
            CTLM.AddItem(cboServicio, "Servicio", true, true, false, 0, false, true);
            CTLM.AddItem(txtSuppDoc, "Doc_Proveedor", true, true, false, 0, false, true);
            CTLM.AddItem(txtNotes, "Notas", true, true, false, 0, false, false);
            CTLM.AddItem(lstFlags, "flags", false, false, false, 0, false, true);

            //empty header values
            CTLM.AddItem("", "transportista", true, true, false, 0, false, false);
            CTLM.AddItem("", "matricula", true, true, false, 0, false, false);
            CTLM.AddItem("@@@"+txtEntrada.Value.ToString(), "conductor", true, true, false, 0, false, false);
            CTLM.AddItem("", "documento_aduana", true, true, false, 0, false, false);
            CTLM.AddItem("01/01/2001 00:00", "fecha_doc_proveedor", true, true, false, 0, false, false);

            //fields
            cboServicio.Source("Select Codigo,Nombre from Servicios where dbo.CheckFlag(flags,'SIMPLE')=1 order by codigo", txtDesServicio);
            cboServicio.SelectedValueChanged += CboServicio_SelectedValueChanged;
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='Cab_Recepcion'");
            //VS Definitions
            VS.Conn = Values.gDatos;
            VS.sSPAdd = "PAdd_Det_Recepcion";
            VS.sSPUpp = "";
            VS.sSPDel = "PDel_Det_Recepcion";
            VS.DBTable = "Det_Recepcion";

            //VS Details
            VS.AddColumn("Entrada", txtEntrada, "@entrada", "", "@entrada");
            VS.AddColumn("Linea", "linea", "", "", "@linea");
            VS.AddColumn("PartNumber", "partnumber", "@partnumber", pSortable: true, pWidth: 90, aMode: AutoCompleteMode.SuggestAppend, aSource: AutoCompleteSource.CustomSource, aQuery: string.Format("select partnumber from referencias where servicio='{0}'",cboServicio.Value));
            VS.AddColumn("Descripcion", "descripcion", "@descripcion", pSortable: true, pWidth: 200);
            VS.AddColumn("Qty", "Qty", "@qty", pWidth: 90);
            VS.CellEndEdit += VS_CellEndEdit; //VS_CellValidating; ; ;
            
            //Various
            CTLM.AddDefaultStatusStrip();
            CTLM.AddItem(VS);
            CTLM.Start();
        }

        private void CboServicio_SelectedValueChanged(object sender, EventArgs e)
        {
            ((CtlVSColumn)VS.Columns["PartNumber"]).AutoCompleteQuery = string.Format("select partnumber from referencias where servicio='{0}'", cboServicio.Value);
            ((CtlVSColumn)VS.Columns["PartNumber"]).ReQuery();
        }

        private void VS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                using (var _rs = new DynamicRS(string.Format("Select Descripcion from Referencias where partnumber='{0}' and Servicio='{1}'", VS[e.ColumnIndex, e.RowIndex].Value, cboServicio.Value), Values.gDatos))
                {
                    _rs.Open();
                    if (_rs.RecordCount==0)
                    {
                        CTWin.MsgError("Wrong partnumber");
                        VS[e.ColumnIndex, e.RowIndex].Value = "";
                        VS[e.ColumnIndex + 1, e.RowIndex].Value = "";
                        //VS.CurrentCell = VS[e.ColumnIndex, e.RowIndex];
                        //e.Cancel = true;
                    }
                    else
                    {
                        VS[e.ColumnIndex + 1, e.RowIndex].Value = _rs["Descripcion"].ToString();
                        VS.CurrentCell = VS[e.ColumnIndex + 2, e.RowIndex];
                    }
                }

            }
        }

        private void btnLabelCMs_Click(object sender, EventArgs e)
        {
            using (var _sp = new SP(Values.gDatos, "PGenerar_Paletags"))
            {
                _sp.AddParameterValue("@entrada", txtEntrada.Value.ToString());
                try
                {
                    _sp.Execute();
                }
                catch (Exception ex)
                {
                    CTWin.MsgError(ex.Message);
                    return;
                }
                if (_sp.LastMsg.Substring(0, 2) != "OK")
                {
                    CTWin.MsgError(_sp.LastMsg);
                    return;
                }
                lstFlags["PALETAGS"] = true;
            }
        }

        private void btnReceived_Click(object sender, EventArgs e)
        {
            using (var _sp = new SP(Values.gDatos, "PUpp_Cab_Recepcion_Recibida"))
            {
                _sp.AddParameterValue("@conductor", "@@@" + txtEntrada.Value.ToString());
                try
                {
                    _sp.Execute();
                } catch(Exception ex)
                {
                    CTWin.MsgError(ex.Message);
                    return;
                }
                if (_sp.LastMsg.Substring(0, 2) != "OK")
                {
                    CTWin.MsgError(_sp.LastMsg);
                    return;
                }
                lstFlags["RECEIVED"]=true;
            }
        }
    }
}
