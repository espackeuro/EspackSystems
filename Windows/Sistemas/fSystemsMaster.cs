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
    public partial class fSystemsMaster : Form
    {
        public fSystemsMaster()
        {
            InitializeComponent();

            //CTLM Definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pMasterSystemsAdd";
            CTLM.sSPUpp = "pMasterSystemsUpp";
            CTLM.sSPDel = "pMasterSystemsDel";
            CTLM.DBTable = "MasterSystems";

            //Header
            CTLM.AddItem(txtSystemCode, "SystemCode", true, true, true, 1, true, true);
            CTLM.AddItem(txtDescription, "Description", true, true, false, 0, false);
            CTLM.AddItem(cboDatabase, "DB", true, true, false, 0, false, true);
            CTLM.AddItem(txtApp, "App", true, true, false, 0, false, true);
            CTLM.AddItem(txtVersion, "Version", true, true, false, 0, false, true);
            CTLM.AddItem(lstLocations, "Locations", true, true, false, 0, false, true);
            CTLM.AddItem(lstUserPositions, "UserPositions", true, true, false, 0, false, true);
            CTLM.AddItem(lstAreas, "Areas", true, true, false, 0, false, true);
            CTLM.AddItem(lstRequiredUserFlags, "RequiredUserFlags", true, true, false, 0, false, true);

            CTLM.AddItem(lstFlags, "flags", true, true, false, 0, false, true);

            cboDatabase.Source("Select name from sys.databases order by name");
            lstLocations.Source("Select cod3,nombre from vSedes order by nombre");
            lstUserPositions.Source("Select PositionCode, Convert(varchar,MinSecurityLevel)+'-'+PositionDescription from MasterUserPositions order by MinSecurityLevel");
            lstAreas.Source("Select idArea,Description from MasterAreas order by idArea");
            lstRequiredUserFlags.Source("select codigo,descFlagEng from flags where tabla='Users' order by DescFlagEng");
            lstFlags.Source("select codigo,descFlagEng from flags where tabla='MasterSystems' order by DescFlagEng");


            CTLM.AddDefaultStatusStrip();
            CTLM.Start();
        }
    }
}
