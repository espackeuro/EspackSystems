using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistemas
{
    public partial class fProfiles : Form
    {
        public fProfiles()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddProfilesCab";
            CTLM.sSPUpp = "pUppProfilesCab";
            CTLM.sSPDel = "pDelProfilesCab";
            CTLM.DBTable = "ProfilesCab";

            //Header
            CTLM.AddItem(txtProfileCode, "ProfileCode", true, true, true, 1, true, true);
            CTLM.AddItem(txtDescription, "Description", true, true, false, 0, false, true);
            CTLM.AddItem(lstCOD3, "COD3", true, true, false, 0, false, true);
            CTLM.AddItem(lstFlags, "flags", true, true, false, 0, false, true);

            lstFlags.Source("select codigo,descFlagEng from flags where tabla='ProfilesCab' order by DescFlagEng");
            lstCOD3.Source("select n.COD3,g.Descripcion from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3");

            //VS Definitions
            VS.Conn = Values.gDatos;
            VS.sSPAdd = "pAddProfilesDet";
            VS.sSPUpp = "pUppProfilesDet";
            VS.sSPDel = "pDelProfilesDet";
            VS.DBTable = "ProfilesDet";

            //Details
            //VS.AddColumn("ProfileCode", txtProfileCode, "@ProfileCode", "@ProfileCode", "@ProfileCode");
            //VS.AddColumn("Service","Service","@Service", "@Service", "@Service",aMode: AutoCompleteMode.Append,aSource: AutoCompleteSource.CustomSource,aQuery: "Select ServiceCode from Services order by ServiceCode");
            //VS.AddColumn("Default Flags", "DefaultFlags", "@DefaultFlags", "@DefaultFlags", "");

            //Various
            CTLM.AddDefaultStatusStrip();
            CTLM.AddItem(VS);
            CTLM.Start();
        }

    }
}
