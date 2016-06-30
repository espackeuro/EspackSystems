using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using CommonToolsWin;

namespace Simplistica
{
    public partial class fSettings : Form
    {
        public fSettings()
        {
            InitializeComponent();
            cboPrinters.Enabled = true;
            cboPrinters.Source("select Codigo from  ETIQUETAS..datosEmpresa where descripcion like '%COO%' order by cmp_integer", Values.gDatos);
            cboPrinters.Value = cSettings.readSetting("labelPrinter");
            Values.LabelPrinterAddress = cboPrinters.Value.ToString();
            cboPrinters.SelectedIndexChanged += CboPrinters_SelectedIndexChanged;
            cboWarehouse.Enabled = true;
            cboWarehouse.Source("select cod3,Descripcion from general..sedes_servicios S inner join general..Sedes se on se.Codigo = S.CodigoSede where codigoServicio = 'LOGISTICA' order by Descripcion",Values.gDatos);
            cboWarehouse.Value = cSettings.readSetting("COD3");
            Values.COD3 = cboWarehouse.Value.ToString();
            cboWarehouse.SelectedIndexChanged += CboWarehouse_SelectedIndexChanged;
        }

        private void CboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cSettings.writeSetting("COD3", cboWarehouse.Value.ToString());
            Values.COD3 = cboWarehouse.Value.ToString();
        }

        private void CboPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            cSettings.writeSetting("labelPrinter", cboPrinters.Value.ToString());
            Values.LabelPrinterAddress = cboPrinters.Value.ToString();
        }
    }
}
