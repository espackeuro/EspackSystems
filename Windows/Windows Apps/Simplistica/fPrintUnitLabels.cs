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

namespace Simplistica
{
    public partial class fPrintUnitLabels : Form
    {
        public fPrintUnitLabels()
        {
            InitializeComponent();
            cboService.Enabled = true;
            txtDesService.Enabled = true;
            txtQty.Enabled = true;
            cboService.ParentConn = Values.gDatos;
            cboService.Source("Select Codigo,Nombre from Servicios where dbo.CheckFlag(flags,'REPAIRS')=1 order by codigo", txtDesService);
            cboService.SelectedIndexChanged += CboService_SelectedIndexChanged;
            AcceptButton = btnPrint;
        }

        private async void CboService_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var _rs = new DynamicRS(string.Format("Select cmp_integer from REPAIRS..datosEmpresa where codigo='{0}_UNIT_QTY'", cboService.Value), Values.gDatos))
            {
                await _rs.Open();
                txtQtyLabel.Text = _rs.EOF ? "1" : _rs["cmp_integer"].ToString();
            }
        }

        private async void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("This will print {0} unit labels. Are you sure?",txtQty.Text), "SIMPLISTICA", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _labelInit = 0;
                using (var _conn = new cAccesoDatosNet(Values.gDatos.Server, "REPAIRS", Values.gDatos.User, Values.gDatos.Password))
                using (var _sp = new SP(_conn, "pGetContador"))
                {

                    _sp.AddParameterValue("@Contador", "");
                    _sp.AddParameterValue("@Serv", "");
                    _sp.AddParameterValue("@Codigo", cboService.Value + "_UNIT_ETIQ");
                    _sp.AddParameterValue("@Incremento", Convert.ToInt32(txtQty.Value));
                    await _sp.Execute();
                    _labelInit = Convert.ToInt32(_sp.ReturnValues()["@Contador"]);
                }
                string _printerAddress = "";
                int _printerResolution = 0;
                using (var _RS = new DynamicRS(string.Format("select descripcion,cmp_varchar,cmp_integer from ETIQUETAS..datosEmpresa where codigo='{0}'", Values.LabelPrinterAddress), Values.gDatos))
                {
                    await _RS.Open();
                    _printerAddress = _RS["cmp_varchar"].ToString();
                    _printerResolution = Convert.ToInt32(_RS["cmp_integer"]);
                    //_printerType = _RS["descripcion"].ToString().Split('|')[0];
                }
                var _label = new ZPLLabel(70, 32, 3, _printerResolution);
                var _unitLabel = new SingleBarcode(_label);

                //_label.addLine(35, 3, 0, "C", "", "[BC][UNITNUMBER]", 0, 2.5F, 1,true);
                //var _param = new Dictionary<string, string>();
                using (var _printer = new cRawPrinterHelper(_printerAddress))
                {
                    var _delimiterLabel = new ZPLLabel(_unitLabel.Label.width, _unitLabel.Label.height, 3, _unitLabel.Label.dpi);
                    delimiterLabel.delim(_delimiterLabel, "START UNIT LABELS", "-");
                    _printer.SendUTF8StringToPrinter(_delimiterLabel.ToString(), 1);
                    for (var i = _labelInit; i < _labelInit + Convert.ToInt32(txtQty.Value); i++)
                    {
                        _unitLabel.Parameters["VALUE"] = "U" + i.ToString().PadLeft(8, '0');
                        for (var j = 0; j < Convert.ToInt32(txtQtyLabel.Text); j++)
                            _printer.SendUTF8StringToPrinter(_unitLabel.ToString(), 1);
                    }
                    delimiterLabel.delim(_delimiterLabel, "END UNIT LABLES","-");
                    _printer.SendUTF8StringToPrinter(_delimiterLabel.ToString(), 1);
                }
            }
        }

    }
}
