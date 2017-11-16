using System.Windows.Forms;
using AccesoDatosNet;
using System.IO;
using System.Diagnostics;
using System.Security;
using System;
using System.Drawing;
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
            CTLM.AddItem(txtVersion, "version", true, true, false, 0, false, false);
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
            CTLM.AfterButtonClick += CTLM_AfterButtonClick;
            CTLM.BeforeButtonClick += CTLM_BeforeButtonClick;
        }

        private void CTLM_BeforeButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            switch (CTLM.Status)
            {
                case EnumStatus.ADDNEW:
                case EnumStatus.EDIT:
                    {
                        if (txtVersion != txtNetVersion)
                        {
                            txtVersion.Text = txtNetVersion.Text;
                        }
                    }
                    break;
            }
        }

        private void CTLM_AfterButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            string _COD3="";
            FileVersionInfo _localFileVersion;
            switch (CTLM.Status)
            {
                case EnumStatus.NAVIGATE:
                case EnumStatus.SEARCH:
                    {
                        if (isEspackIP(ref _COD3) && txtServiceCode.Text != "")
                        {
                            using (var _RS = new StaticRS(string.Format("Select APPServer from sistemas..NetworkSedes where COD3='{0}'", _COD3), Values.gDatos))
                            {
                                _RS.Open();
                                if (!_RS.EOF)
                                {
                                    string _pathToExe = string.Format(@"\\{0}\raiz\media\shares\APPS_CS\{1}\{2}", _RS["AppServer"].ToString(), txtServiceCode.Text.ToLower(),txtApp.Text.ToString().ToLower());
                                    //_pathToExe = @"\\valsrv02\raiz\media\shares\APPS_CS\almacenaje\almacenaje.exe";
                                    try
                                    {
                                        _localFileVersion = FileVersionInfo.GetVersionInfo(_pathToExe);
                                        txtNetVersion.Text = _localFileVersion.FileVersion.ToString();
                                        if (txtNetVersion.Text != txtVersion.Text)
                                            txtNetVersion.BackColor = Color.LightSalmon;
                                        else
                                            txtNetVersion.BackColor = txtVersion.BackColor;
                                    }
                                    catch (SecurityException se)
                                    {
                                        MessageBox.Show(se.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    catch (Exception ex)
                                    {
                                        // MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtNetVersion.Text = "";
                                        txtNetVersion.BackColor = txtVersion.BackColor;
                                    }
                                }

                            }
                        }
                    }
                    break;
            }
        }

        private bool isEspackIP(ref string COD3)
        {
            //#if DEBUG
            //            COD3 = "OUT";
            //            return false;
            //#else
            int _zone;
            var _IP = Values.gDatos.IP.GetAddressBytes();
            if (_IP[0] == 10)
                _zone = _IP[1];
            else
                _zone = _IP[2];
            using (var _RS = new StaticRS(string.Format("Select COD3 from GENERAL..Sedes where zone='{0}'", _zone), Values.gDatos))
            {
                _RS.Open();
                if (_RS.RecordCount == 0)
                {
                    COD3 = "OUT";
                    return false;
                }
                COD3 = _RS["COD3"].ToString();
            }
            return true;
            //#endif
        }

        private void btnReloadVersions_Click(object sender, EventArgs e)
        {
            CTLM.ClearValues();
            txtNetVersion.Text = "";
            CTLM.SetStatus(EnumStatus.SEARCH);
            CTLM.Button_Click("btnOk");
            Application.DoEvents();
            while (!CTLM.EOF)
            {
                CTLM.ShowRSValues();
                Application.DoEvents();
                if (txtVersion.Text !=txtNetVersion.Text)
                {
                    int i = CTLM.RSPosition;
                    CTLM.SetStatus(EnumStatus.EDIT);
                    txtVersion.Text = txtNetVersion.Text;
                    CTLM.Button_Click("btnOk");
                    Application.DoEvents();
                    CTLM.ClearValues();
                    txtNetVersion.Text = "";
                    CTLM.SetStatus(EnumStatus.SEARCH);
                    CTLM.Button_Click("btnOk");
                    Application.DoEvents();
                    CTLM.setRSPosition(i);
                }
                txtNetVersion.Text = "";
                CTLM.Button_Click("btnNext");
            }

        }
    }
}
