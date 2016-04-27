using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTLMantenimientoNet;

namespace Sistemas
{
    public partial class fZones : Form
    {
        private bool _changes;

        public fZones()
        {
            InitializeComponent();
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddZones";
            CTLM.sSPUpp = "pUppZones";
            CTLM.sSPDel = "pDelZones";
            CTLM.AddItem(txtCode, "Code", true, true, true, 0, true, true);
            CTLM.AddItem(txtSubNet, "SubNet", true, true, false, 1, false, true);
            CTLM.AddItem(txtMask, "Mask", true, true, false, 0, false, true);
            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "Zones";
            CTLM.Start();
            enabler(true);
            _changes = true;

            txtSubNet.TextChanged += delegate
             {
                 if (_changes)
                 {
                     _changes = false;
                     var _subnet = txtSubNet.Text.Split('.');
                     txtSubnet1.Text = _subnet.Count() > 0 ? _subnet[0] : "";
                     txtSubnet2.Text = _subnet.Count() > 1 ? _subnet[1] : "";
                     txtSubnet3.Text = _subnet.Count() > 2 ? _subnet[2] : "";
                     txtSubnet4.Text = _subnet.Count() > 3 ? _subnet[3] : "";
                     _changes = true;
                 }

             };

            txtMask.TextChanged += delegate
            {
                if (_changes)
                {
                    _changes = false;
                    var _mask = txtMask.Text.Split('.');
                    txtMask1.Text = _mask[0];
                    txtMask2.Text = _mask.Count() > 1 ? _mask[1] : "";
                    txtMask3.Text = _mask.Count() > 2 ? _mask[2] : "";
                    txtMask4.Text = _mask.Count() > 3 ? _mask[3] : "";
                    _changes = true;
                }
            };
            CTLM.AfterButtonClick += delegate (object source, CTLMEventArgs e)
            {
                switch (e.ButtonClick) 
                {
                    case "btnCancel":
                    case "btnSearch":
                    case "btnAdd":
                    case "btnUpp":
                        enabler(true);
                        break;
                    default:
                        enabler(false);
                        break;
                }
            };
            txtMask1.TextChanged += ipChecker;
            txtMask2.TextChanged += ipChecker;
            txtMask3.TextChanged += ipChecker;
            txtMask4.TextChanged += ipChecker;
            txtSubnet1.TextChanged += ipChecker;
            txtSubnet2.TextChanged += ipChecker;
            txtSubnet3.TextChanged += ipChecker;
            txtSubnet4.TextChanged += ipChecker;
        }

        private void enabler(bool value)
        {
            txtSubnet1.Enabled = value;
            txtSubnet2.Enabled = value;
            txtSubnet3.Enabled = value;
            txtSubnet4.Enabled = value;
            txtMask1.Enabled = value;
            txtMask2.Enabled = value;
            txtMask3.Enabled = value;
            txtMask4.Enabled = value;
        }
        private void ipChecker(object sender, EventArgs e)
        {
            if (_changes)
            {
                _changes = false;
                var text = (MaskedTextBox)sender;
                try
                {
                    Convert.ToByte(text.Text);
                }
                catch
                {
                    CTLM.StatusMsg("Wrong IP block data.");
                    text.Text = "";
                }
                txtSubNet.Text = txtSubnet1.Text + "." + txtSubnet2.Text + "." + txtSubnet3.Text + "." + txtSubnet4.Text;
                if (txtSubNet.Text == "...")
                    txtSubNet.Text = "";
                txtMask.Text = txtMask1.Text + "." + txtMask2.Text + "." + txtMask3.Text + "." + txtMask4.Text;
                if (txtMask.Text == "...")
                    txtMask.Text = "";
                if (text.Text.Length == 3)
                    SendKeys.Send("{TAB}");
                _changes = true;
            }
        } 
        private static fZones m_fRange;
        public static fZones GetChildInstance()
        {
            if (m_fRange == null) //if not created yet, Create an instance
                m_fRange = new fZones();
            return m_fRange;  //just created or created earlier.Return it+69
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            m_fRange = null;
            base.OnFormClosed(e);
        }
    }
}
