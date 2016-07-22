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
    public partial class fRackLabels : Form
    {
        public fRackLabels()
        {
            InitializeComponent();
            cboCOD3.Enabled = true;
            txtAISLE.Enabled = true;
            txtLOCATION.Enabled = true;
            cboCOD3.ParentConn = Values.gDatos;

        }
    }
}
