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

namespace Simplistica
{
    public partial class fRackLabels : Form
    {
        public fRackLabels()
        {
            InitializeComponent();
            cboCOD3.Enabled = true;
            txtAISLE.Enabled = true;
            txtLOCATION.Enabled = true;
            cboCOD3.ParentConn = Values.gDatos;
            cboCOD3.Source("select cod3,cod3 as txt from general..sedes order by cod3", txtCOD3);
            txtCOD3.Hide();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("This will print {0} unit labels. Are you sure?", txtLOCATION.Text), "SIMPLISTICA", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string _printerAddress = "";
                int _printerResolution = 0;
                using (var _RS = new DynamicRS(string.Format("select descripcion,cmp_varchar,cmp_integer from ETIQUETAS..datosEmpresa where codigo='{0}'", Values.LabelPrinterAddress), Values.gDatos))
                {
                    _RS.Open();
                    _printerAddress = _RS["cmp_varchar"].ToString();
                    _printerResolution = Convert.ToInt32(_RS["cmp_integer"]);
                }
                var _label = new ZPLLabel(75, 150, 3, _printerResolution);
                var _racklabel = new RackLabelWOL(_label);
                //using (var _rs = new DynamicRS(string.Format("Select cp.CM,cp.Entrada,cp.Linea,cp.Partnumber,cp.QTY,cp.xfec,c.Doc_Proveedor from CMS_PALETAGS cp inner join cab_Recepcion c on c.entrada=cp.entrada where cp.Entrada='{0}' and Linea='{1}'", Convert.ToInt32(txtEntrada.Value), Convert.ToInt32(r.Cells[1].Value)), Values.gDatos))
                using (var _printer = new cRawPrinterHelper(_printerAddress))
                using (var _rs = new DynamicRS(string.Format("select cod3,ubicacion from mapa_ubicaciones where cod3='{0}' and ubicacion='{1}'", Convert.ToString(txtCOD3.Text), Convert.ToString(txtLOCATION.Text)), Values.gDatos))
                {
                    _rs.Open();
                    //iterate labels
                    _rs.ToList().ForEach(row =>
                    {
                        _racklabel.Parameters["VALUE"] = row["ubicacion"].ToString();
                        if (!_printer.SendUTF8StringToPrinter(_racklabel.ToString(), 1))
                        {
                            CTWin.MsgError(string.Format("Error printing label {}.", row["VALUE"]));
                        }
                    });
                }

            }

        }
    }
}
