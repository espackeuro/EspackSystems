﻿using System;
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

namespace Sistemas
{
    public partial class fUsers : Form
    {
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
            txtSurname1.Validating += delegate//(object sender, KeyEventArgs e)
            {
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                if (CTLM.Status==EnumStatus.ADDNEW)
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
            
            //cboCOD3.SelectedValue = "";
            //CTLM.AfterButtonClick += delegate (object source, CTLMEventArgs e)
            //{
            //    if (e.ButtonClick == "btnCancel")
            //    {
            //        txtCOD3Name.Text = "";
            //    }
            //};
            //KeyDown += delegate(object sender, KeyEventArgs e)
            // {
            //     MessageBox.Show("Patata");
            // };
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            //var ocCommand = new OCAddUser("restelles", "patatita");
            //var ocCommand = new OCGetUsers("restelles");
            // var ocCommand = new OCEditUser("restelles", "display", "Rafa Estellés");
            var ocCommand = new OCGetGroups("fnastasie");
            ocCommand.setCredentials("admin", "*hwLD8e*");
            await ocCommand.sendRequest();
            var result = ocCommand.responseX;




       //     using (var client = new HttpClient())
       //     {
       //         var byteArray = Encoding.ASCII.GetBytes("admin:*mJ7goY*");
       //         var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
       //         client.DefaultRequestHeaders.Authorization = header;
       //         var values = new Dictionary<string, string>
       //         {
       //{ "key", "password" },
       //{ "value", "*hwLD8e*" }
       //         };
       //         var content = new FormUrlEncodedContent(values);

       //         //var response = await client.PostAsync("https://owncloud.espackeuro.com/ocs/v1.php/cloud/users", content);
       //         //var response = await client.GetAsync("https://owncloud.espackeuro.com/ocs/v1.php/cloud/users");
       //         var response = await client.PutAsync("https://owncloud.espackeuro.com/ocs/v1.php/cloud/users/admin", content);
       //         var responseString = await response.Content.ReadAsStringAsync();
       //     }


        }
    }
}
