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
    public partial class fServices : Form
    {
        public fServices()
        {
            InitializeComponent();

            CTLM.Conn = Values.gDatos;

            CTLM.sSPAdd = "pAddServices";
            CTLM.sSPUpp = "pUppServices";
            CTLM.sSPDel = "pDelServices";
            CTLM.AddItem(txtServiceCode, "ServiceCode", true, true, true, 1, true, true);
            CTLM.AddItem(txtDescription, "Description", true, true, false, 0, false, true);
            CTLM.AddItem(txtDB, "DB", true, true,false, 0, false, true);
            CTLM.AddItem(txtLocation, "Location", true, true, false, 0, false, true);
            CTLM.AddItem(txtApp, "App", true, true, false, 0, false, true);
            CTLM.AddItem(txtServer1, "Server1", true, true, false, 0, false, true);
            CTLM.AddItem(txtServer2, "Server2", true, true, false, 0, false, true);
            CTLM.AddItem(txtActiveServer, "ActiveServer", true, true, false, 0, false, true);
            CTLM.AddItem(lstFlags, "flags", true, true, false, 0, false, true);
            CTLM.AddItem(lstCOD3, "COD3", true, true, false, 0, false, true);

            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "Services";

            lstFlags.Source("select codigo,DescFlagEng from flags where tabla='Services' order by codigo");
            lstCOD3.Source("select n.COD3,s.descripcion from NetworkSedes n inner join general..sedes s on n.cod3=s.cod3 order by n.cod3");

            CTLM.Start();
           

        }
    }
}
