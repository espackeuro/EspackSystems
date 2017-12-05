using AccesoDatosNet;
using CommonToolsWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplistica
{
    public partial class fProductionOrders : Form
    {
        public fProductionOrders()
        {
            InitializeComponent();
            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "PAdd_Cab_Ordenes_Produccion";
            CTLM.sSPUpp = "PUpp_Cab_Ordenes_Produccion";
            CTLM.sSPDel = "PDel_Cab_Ordenes_Produccion";
            CTLM.DBTable = "(Select c.* from Cab_Ordenes_Produccion c inner join Servicios s on s.codigo=c.servicio where s.cod3='" + Values.COD3 + "' and dbo.CheckFlag(s.flags,'SIMPLE')=1) a";

            //Header
            CTLM.AddItem(txtNumero, "Numero", false, true, true, 1, true, true);
            CTLM.AddItem(cboServicio, "Servicio", true, true, false, 0, false, true);
            CTLM.AddItem(txtFecha, "Fecha", true, false, false, 0, false, false, DateTime.Now);
            CTLM.AddItem(cboRuta, "Ruta", true, true, false, 0, false, false);
            CTLM.AddItem(txtExpedicion, "Expedicion", pSearch: true);
            //empty header values
            CTLM.AddItem(Values.gDatos.User, "Usuario_Proceso", true, false, false, pDefValue: Values.gDatos.User, pSPAddParamName: "usuario");
            CTLM.AddItem(DateTime.Now.ToString(), "Start_time", true, false, false, pDefValue: DateTime.Now.ToString(), pSPAddParamName: "start");
            CTLM.AddItem(DateTime.Now.ToString(), "End_time", true, false, false, pDefValue: DateTime.Now.ToString(), pSPAddParamName: "end");

            //fields
            cboServicio.Source("Select Codigo,Nombre from Servicios where dbo.CheckFlag(flags,'SIMPLE')=1 and cod3='" + Values.COD3 + "' order by codigo", txtDesServicio);
            cboServicio.SelectedValueChanged += CboServicio_SelectedValueChanged;

            //VS Definitions
            VS.Conn = Values.gDatos;
            VS.sSPAdd = "PAdd_Det_Ordenes_Produccion";
            VS.sSPUpp = "PUpp_Det_Ordenes_Produccion";
            VS.sSPDel = "PDel_Det_Ordenes_Produccion";
            VS.DBTable = "Det_Ordenes_Produccion";

            //VS Details
            VS.AddColumn("Numero", txtNumero, "@numero", "@numero", "@numero", pVisible: false);
            VS.AddColumn("Linea", "Linea", "", "@linea", "@linea");
            VS.AddColumn("PartNumber", "partnumber", "@partnumber", pSortable: true, pWidth: 90, aMode: AutoCompleteMode.SuggestAppend, aSource: AutoCompleteSource.CustomSource, aQuery: string.Format("select partnumber from referencias where servicio='{0}'", cboServicio.Value));
            VS.AddColumn("Descripcion", "descripcion", "@descripcion", pSortable: true, pWidth: 200);
            VS.AddColumn("Servicio", cboServicio, "@Servicio", pVisible: false);
            VS.AddColumn("Qty", "Qty", "@qty", "@qty", pWidth: 90);
            VS.AddColumn("Qty_ord", "Qty_Ord", "@Qty_ord", pVisible: false);
            VS.AddColumn("QTY_Embalajes", "QTY_Embalajes", "@QTY_Embalajes", "@QTY_Embalajes", pVisible: false);
            VS.AddColumn("PPM", "Qty_pzas_modulo", "@ppm", "@ppm", pVisible: false);
            VS.AddColumn("PPB", "Qty_pzas_pallet", "@ppb", "@ppb", pVisible: false);
            VS.AddColumn("BB2", "qty_pzas_block2", "@bb2", "@bb2", pVisible: false);
            VS.AddColumn("MOD", "mod", "@mod", "@mod", pVisible: false);
            VS.AddColumn("BLOCK", "block", "@block", "@block", pVisible: false);
            VS.AddColumn("BLOCK2", "block2", "@block2", "@block2", pVisible: false);

            //Various
            VS.CellEndEdit += VS_CellEndEdit;
            CTLM.AddDefaultStatusStrip();
            CTLM.AddItem(VS);
            CTLM.Start();
        }



        private void VS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                using (var _rs = new StaticRS(string.Format("Select Descripcion from Referencias where partnumber='{0}' and Servicio='{1}'", VS[e.ColumnIndex, e.RowIndex].Value, cboServicio.Value), Values.gDatos))
                {
                    _rs.Open();
                    if (_rs.RecordCount == 0)
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
                        VS.CurrentCell = VS[e.ColumnIndex + 3, e.RowIndex];
                    }
                }

            }
        }

        private void CboServicio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboServicio.Value.ToString() != "")
                cboRuta.Source(string.Format("Select Ruta, Descripcion1 from Servicios_Destinos where Servicio='{0}'", cboServicio.Value), txtDesRoute);
            else
                cboRuta.Source("Select source='',Description='---'", txtDesRoute);
        }
    }
}
