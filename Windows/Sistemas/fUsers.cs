using System;
using System.Windows.Forms;
using AccesoDatosNet;
using CommonTools;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Owncloud;
using System.Threading.Tasks;
using System.Linq;

namespace Sistemas
{
    public partial class fUsers : Form
    {
        private List<Task> _backgroundTasks = new List<Task>();
        private string _prevStatus;
        public fUsers()
        {
            InitializeComponent();
            
            //CTLM definitions
            //Who
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddUsers";
            CTLM.sSPUpp = "pUppUsers";
            CTLM.sSPDel = "";
            CTLM.AddItem(txtUserCode, "UserCode", true, true, false, 1, true, true);
            CTLM.AddItem(txtName, "Name", true, true, false, 0, false, true);
            CTLM.AddItem(txtSurname1, "Surname1", true, true, false, 0, false, true);
            CTLM.AddItem(txtSurname2, "Surname2", true, true, false, 0, false, true);
            CTLM.AddItem(txtUserNumber, "UserNumber", false, true, false, 0, false, true);
            //Where
            CTLM.AddItem(cboCOD3, "MainCOD3", true, true, false, 1,false, true);
            CTLM.AddItem(txtDesCod3, "desCOD3");
            CTLM.AddItem(listCOD3, "COD3", true, true, false, 1, false, true);
            CTLM.AddItem(cboZone, "Zone", true, true, false, 1, false, true);
            //Systems
            CTLM.AddItem(txtPWD, "Password", false, true, false, 0, false, false);
            CTLM.AddItem(txtPasswordEXP, "PasswordEXP", false, true, false, 0, false, false);
            CTLM.AddItem(txtPIN, "PIN", false, true, false, 0, false, false);
            CTLM.AddItem(cboDomain, "domain", true, false, false, 0, false, true);
            CTLM.AddItem(txtEmail, "emailAddress", false, false, false, 0, false, true);
            CTLM.AddItem(txtQuota, "EmailQuota", false, true, false, 0, false, false);
            CTLM.AddItem(lstFlags, "Flags", true, true, false, 0, false, true);
            CTLM.AddItem(lstEmailAliases, "Aliases",true,true,false,0,false,true,pSPAddParamName: "alias", pSPUppParamName: "alias");
            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "vUsers";
            CTLM.ReQuery = true;
            cboCOD3.Source("select n.COD3,g.Descripcion from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3", txtDesCod3);
            listCOD3.Source("select n.COD3,g.Descripcion from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3");
            listCOD3.Changed += delegate
            {

                //var _alias = lstEmailAliases.Value;
                lstEmailAliases.Source("select Address,a2=Address from mail..aliasCAB a where exists( select 0 from dbo.Split(a.COD3,'|') where valor in (select valor from dbo.Split('" + listCOD3.Value + "','|')))");
                //lstEmailAliases.Value = _alias;
            };
            cboDomain.Source("Select domain from domain where domain<>'ALL' order by domain");
            cboZone.Source("Select Code from Zones order by Code");
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='Users'");
            CTLM.AfterButtonClick += CTLM_AfterButtonClick;
            CTLM.Start();
            _prevStatus = listCOD3.Text;
            cboCOD3.SelectedValueChanged += delegate
            {
                if (cboCOD3.SelectedValue != null && (CTLM.Status==EnumStatus.EDIT || CTLM.Status==EnumStatus.ADDNEW))
                    listCOD3.CheckItem(cboCOD3.Text);
            };
            
            listCOD3.ItemCheck += delegate (object sender, ItemCheckEventArgs e)
            {
                if ((e.NewValue==CheckState.Unchecked) && (listCOD3.keyItem(e.Index)==cboCOD3.Text) && (CTLM.Status==EnumStatus.EDIT || CTLM.Status==EnumStatus.ADDNEW))
                {
                    MessageBox.Show("Cannot remove Main COD3 from COD3 list", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.NewValue = CheckState.Checked;
                }
            };
            txtSurname1.Validating += TxtSurname1_Validating;
            //this.FormClosed += FUsers_FormClosed;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Task.WaitAll(_backgroundTasks.Where(p => !p.IsCompleted).ToArray(), 30000);
            base.OnFormClosed(e);
        }

        private void TxtSurname1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            {
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                if (CTLM.Status == EnumStatus.ADDNEW)
                {
                    if (txtUserCode.Text == "" && txtSurname1.Text != "")
                    {
                        var _possible = CT.ToASCII(txtName.Text.Substring(0, 1) + txtSurname1.Text).ToLower();
                        Values.gDatos.AdoCon.Open();
                        SqlCommand query = new SqlCommand("SELECT 0 from Users where UserCode=@UserCode; ", Values.gDatos.AdoCon);
                        query.Parameters.AddWithValue("@UserCode", "");
                        for (var i = 2; i < 100; i++)
                        {
                            query.Parameters["@UserCode"].Value = _possible;
                            var reader = query.ExecuteReader();
                            if (!reader.HasRows)
                                break;
                            _possible = _possible + i.ToString();
                            reader.Close();
                        }
                        Values.gDatos.AdoCon.Close();
                        txtUserCode.Text = _possible;
                    }
                }
            };
        }

        private async void CTLM_AfterButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            if (lstFlags.Value.ToString().IndexOf("|EMAIL|") == -1)
            {
                lstEmailAliases.Source("Select 0,0 where 0=1");
            }
            switch (e.ButtonClick)
            {
                //case "btnAdd":
                //case "btnUpp":
                //case "btnNext":
                //case "btnPrev":
                //case "btnFirst":
                //case "btnLast":
                case "btnOk":
                    if (lstFlags.Value.ToString().IndexOf("|OWNCLOUD|") != -1 && Values.gMasterPassword!="" && (CTLM.Status== EnumStatus.ADDNEW || CTLM.Status== EnumStatus.EDIT))
                    {
                        
                        CTLM.Enabled = false;
                        CTLM.StatusBarProgressMarqueeStart();
                        var task0 = OCCommands.CheckUser(txtUserCode.Text, Values.gMasterPassword);
                        _backgroundTasks.Add(task0);
                        bool result = await task0;
                        _backgroundTasks.Remove(task0);
                        CTLM.StatusMsg( result ? "Owncloud user found" : "Owncoud user not found");
                        if (!result)
                        {
                            var task1 = OCCommands.AddUser(txtUserCode.Text, txtPWD.Text, txtName.Text + " " + txtSurname1.Text + " " + txtSurname2.Text, cboZone.Value + "|" + cboCOD3.Value, Values.gMasterPassword);
                            _backgroundTasks.Add(task1);
                            bool res2 = await task1;
                            CTLM.StatusMsg(res2 ? "Owncloud user created correctly" : "ERROR creating Owncloud user!!!");
                            _backgroundTasks.Remove(task1);
                        } else
                        {
                            var task1 = OCCommands.UppUser(txtUserCode.Text, txtPWD.Text, txtName.Text + " " + txtSurname1.Text + " " + txtSurname2.Text, cboZone.Value + "|" + cboCOD3.Value, Values.gMasterPassword);
                            _backgroundTasks.Add(task1);
                            bool res2 = await task1;
                            CTLM.StatusMsg(res2 ? "Owncloud user updated correctly" : "ERROR updating Owncloud user!!!");
                            _backgroundTasks.Remove(task1);
                        }
                        
                        CTLM.StatusBarProgressStop();
                        CTLM.Enabled = true;
                    }
                    break;
            }
        }
    }
}
