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
            cboService.SelectedValueChanged += CboService_SelectedValueChanged;
        }

        private void CboService_SelectedValueChanged(object sender, EventArgs e)
        {
            cboPrinters.Enabled = true;
            cboPrinters.Source("select Codigo from  ETIQUETAS..datosEmpresa where descripcion like '%" + cboService.Text + "%' order by cmp_integer", Values.gDatos);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int _labelInit = 0;
            using (var _conn = new cAccesoDatosNet(Values.gDatos.Server,"REPAIRS",Values.gDatos.User,Values.gDatos.Password))
            using (var _sp = new SP(_conn, "pGetContador"))
            {

                _sp.AddParameterValue("@Contador", "");
                _sp.AddParameterValue("@Serv", "");
                _sp.AddParameterValue("@Codigo", cboService.Value + "_UNIT_ETIQ");
                _sp.AddParameterValue("@Incremento",Convert.ToInt32(txtQty.Value));
                _sp.Execute();
                _labelInit = Convert.ToInt32(_sp.ReturnValues()["@Contador"]);
            }
            string _printerAddress = "";
            using (var _RS = new DynamicRS(string.Format("select descripcion,cmp_varchar from ETIQUETAS..datosEmpresa where codigo='{0}'", cboPrinters.Value), Values.gDatos))
            {
                _RS.Open();
                _printerAddress = _RS["cmp_varchar"].ToString();
                //_printerType = _RS["descripcion"].ToString().Split('|')[0];
            }
            var _label = new ZPLLabel(70, 32, 3, 203);
            var _unitLabel = new SingleBarcode(_label);
            //_label.addLine(35, 3, 0, "C", "", "[BC][UNITNUMBER]", 0, 2.5F, 1,true);
            //var _param = new Dictionary<string, string>();
            using (var _printer = new cRawPrinterHelper(_printerAddress))
            {
                for (var i = _labelInit; i < _labelInit + Convert.ToInt32(txtQty.Value); i++)
                {
                    _unitLabel.Parameters["VALUE"] = "U" + i.ToString().PadLeft(9, '0');
                    _printer.SendUTF8StringToPrinter(_unitLabel.ToString(), 1);
                }
            }
        }

    }
}
