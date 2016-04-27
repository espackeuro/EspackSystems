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

using System.Data.SqlClient;
using CTLMantenimientoNet;

namespace Sistemas
{
    public partial class fTown : Form
    {
        public fTown()
        {
            InitializeComponent();
            //CTLM definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddNetworkSedes";
            CTLM.sSPUpp = "pUppNetworkSedes";
            CTLM.sSPDel = "pDelNetworkSedes";
            CTLM.AddItem(cboCOD3, "COD3", true, true, true, 1, true, true);
            CTLM.AddItem(txtSubNet, "SubNet", true, true, false, 0, false, true);
            CTLM.AddItem(txtMask, "Mask", true, true,false, 0, false, false);
            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "NetworkSedes";
            CTLM.Start();
            cboCOD3.Source("Select COD3,Nombre from vSedes", txtCOD3Name);
            //cboCOD3.SelectedValue = "";
            //CTLM.AfterButtonClick += delegate(object source, CTLMEventArgs e)
            // {
            //     if (e.ButtonClick== "btnCancel")
            //     {
            //         txtCOD3Name.Text = "";
            //     }
            // };
                
        }
        private static fTown m_fTown;
        public static fTown GetChildInstance()
        {
            if (m_fTown == null) //if not created yet, Create an instance
                m_fTown = new fTown();
            return m_fTown;  //just created or created earlier.Return it+69
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            m_fTown = null;
            base.OnFormClosed(e);
        }
    }
}
