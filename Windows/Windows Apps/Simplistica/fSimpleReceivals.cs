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
using EspackClasses;
using RawPrinterHelper;
using System.Threading;

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
            CTLM.DBTable = "(Select c.* from Cab_Recepcion c inner join Servicios s on s.codigo=c.servicio where s.cod3='"+Values.COD3+"' and dbo.CheckFlag(s.flags,'SIMPLE')=1) a";

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
            CTLM.AddItem("@@@", "conductor", true, true, false, 0, false, false);
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
        #region CM GENERATION
        private void btnLabelCMs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will generate and print all CMs. Are you sure?","SIMPLISTICA",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question) == DialogResult.Yes)
            {

                //printer preparation

                string _printerAddress = "";
                int _printerResolution = 0;
                if (Values.LabelPrinterAddress == "")
                {
                    CTWin.MsgError("Select a label printer first in preferences.");
                    return;
                }
                using (var _RS = new DynamicRS(string.Format("select descripcion,cmp_varchar,cmp_integer from ETIQUETAS..datosEmpresa where codigo='{0}'", Values.LabelPrinterAddress), Values.gDatos))
                {
                    _RS.Open();
                    _printerAddress = _RS["cmp_varchar"].ToString();
                    _printerResolution = Convert.ToInt32(_RS["cmp_integer"]);
                    //_printerType = _RS["descripcion"].ToString().Split('|')[0];
                }
                //label preparation
                var _label = new ZPLLabel(70, 31, 3, _printerResolution);
                var _CMLabel = new MicroCM(_label);
                //sp preparation
                using (var _printer = new cRawPrinterHelper(_printerAddress))
                using (var _sp = new SP(Values.gDatos, "PGenerar_Paletags_linea"))
                {
                    var _delimiterLabel = new ZPLLabel(_CMLabel.Label.width, _CMLabel.Label.height, 3, _CMLabel.Label.dpi);
                    delimiterLabel.delim(_delimiterLabel, "START RECEIVAL", txtEntrada.Text);
                    _printer.SendUTF8StringToPrinter(_delimiterLabel.ToString(), 1);
                    //we will check line by line
                    VS.ToList().Where(r => r.Cells[0].Value.ToString() != "").ToList().ForEach(r =>
                      {
                    //first we generate the cms
                    try
                          {
                              generateCM(Convert.ToInt32(txtEntrada.Value), Convert.ToInt32(r.Cells[1].Value));
                          }
                          catch (Exception ex)
                          {
                              CTWin.MsgError(ex.Message);
                              CTLM.StatusMsg(ex.Message);
                          }
                    //delimiter
                    delimiterLabel.delim(_delimiterLabel, "LINE", r.Cells[1].Value.ToString());
                          _printer.SendUTF8StringToPrinter(_delimiterLabel.ToString(), 1);
                    // then we print the labels 
                    using (var _rs = new DynamicRS(string.Format("Select cp.CM,cp.Entrada,cp.Linea,cp.Partnumber,cp.QTY,cp.xfec,c.Doc_Proveedor from CMS_PALETAGS cp inner join cab_Recepcion c on c.entrada=cp.entrada where cp.Entrada='{0}' and Linea='{1}'", Convert.ToInt32(txtEntrada.Value), Convert.ToInt32(r.Cells[1].Value)), Values.gDatos))
                          {
                              _rs.Open();
                              _rs.ToList().ForEach(row =>
                              {
                                  _CMLabel.Parameters["CM"] = row["CM"].ToString();
                                  _CMLabel.Parameters["RECEIVAL"] = row["Entrada"].ToString();
                                  _CMLabel.Parameters["RECEIVAL_DATE"] = row["xfec"].ToString();
                                  _CMLabel.Parameters["PARTNUMBER"] = row["Partnumber"].ToString();
                                  _CMLabel.Parameters["QTY"] = row["QTY"].ToString();
                                  if (!_printer.SendUTF8StringToPrinter(_CMLabel.ToString(), 1))
                                  {
                                      CTWin.MsgError(string.Format("Error printing label {}.", row["CM"]));
                                  }
                              });
                          }
                          delimiterLabel.delim(_delimiterLabel, "END RECEIVAL", txtEntrada.Text);
                          _printer.SendUTF8StringToPrinter(_delimiterLabel.ToString(), 1);
                      });

                    lstFlags["PALETAGS"] = true;
                    CTLM.StatusMsg("CMs generated OK.");
                }
            }
            MessageBox.Show("Label printing task finished.", "SIMPLISTICA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void generateCM(int pReceival, int pLine)
        {
            //sp preparation
            using (var _sp = new SP(Values.gDatos, "PGenerar_Paletags_linea"))
            {
                using (var _rs = new DynamicRS(string.Format("Select 0 from CMS_PALETAGS where Entrada='{0}' and Linea='{1}'", pReceival, pLine), Values.gDatos))
                {
                    _rs.Open();
                    //if no paletags for the line
                    if (_rs.RecordCount == 0)
                    {
                        CTLM.StatusMsg(string.Format("Generating paletags for line {0}", pLine));
                        _sp.AddParameterValue("@Entrada", pReceival);
                        _sp.AddParameterValue("@Linea", pLine);
                        try
                        {
                            //generate them
                            _sp.Execute();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        if (_sp.LastMsg.Substring(0, 2) != "OK")
                        {
                            throw new Exception(_sp.LastMsg);
                        }
                    }
                    //now we print them

                }
            }
        }
        #endregion
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
