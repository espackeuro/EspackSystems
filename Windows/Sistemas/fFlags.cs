using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccesoDatosNet;
using CommonTools;

namespace Sistemas
{
    public partial class fFlags : Form
    {
        private string _prevStatus;
        public fFlags()
        {
            InitializeComponent();
            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddFlags";
            CTLM.sSPUpp = "";
            CTLM.sSPDel = "pDelFlags";
            CTLM.AddItem(txtIdReg, "IdReg", false, false, false, 1, true, true);
            CTLM.AddItem(cboTable,"Tabla", true, false, true, 1, false, true);
            CTLM.AddItem(txtLetter, "Letra", true, false, false, 1, false, true);
            CTLM.AddItem(txtCode, "Codigo", true, false, true, 1, false, true);
            CTLM.AddItem(txtDescFlag, "DescFlag", true, false, false, 0, false, true);
            CTLM.AddItem(txtDesFlagEng, "DescFlagEng", true, false, false, 0, false, true);
            CTLM.AddItem(lstServices, "Servicio", true, false, false, 0, false, true);

            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "flags";

            lstServices.Source("select ServiceCode,ServiceCode as 'DESC' from services order by servicecode, Description");
            cboTable.Source("select name from sistemas.sys.tables where is_ms_shipped=0 order by name");

            CTLM.Start();

            

        }


    }
}
