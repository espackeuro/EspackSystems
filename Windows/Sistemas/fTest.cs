using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistemas
{
    public partial class fTest : Form
    {
        public fTest()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddTestCarlos";
            CTLM.sSPUpp = "";
            CTLM.sSPDel = "";
            CTLM.DBTable = "TestCarlos";

            //fields
            CTLM.AddItem(txtIdreg, "idreg", false, false, false, 1, true, true);
            CTLM.AddItem(txtCampo1, "Campo1", true, false, false, 0, false, true);
            CTLM.AddItem(txtCampo2, "Campo2", true, false, false, 0, false, true);

            CTLM.AddDefaultStatusStrip();
            CTLM.Start();

        }
    }
}
