using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccesoDatosNet;
using System.IO;
using Renci.SshNet;

namespace Sistemas
{
    public partial class fDNS : Form
    {
        public fDNS()
        {
            InitializeComponent();
            var _RS = new DynamicRS("Select server=cmp_varchar from datosEmpresa where codigo='BIND_SERVER'",Values.gDatos);
            try
            {
                _RS.Open();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtServerName.Value = _RS["server"];
            _RS.Close();
            _RS = null;
            txtUser.Focus();

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("This will update the DNS server assignments and will restart the service.\n Are you sure? ", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string _serverDNS = txtServerName.Text;
                    var _SP = new SP(Values.gDatos, "pGetContador");
                    _SP.AddParameterValue("@Contador", 0);
                    _SP.AddParameterValue("@Serv", "");
                    _SP.AddParameterValue("@Codigo", "BIND_SERIAL");
                    _SP.Execute();
                    var _serial = _SP.Parameters["@Contador"].Value.ToString();
                    _SP = null;

                    string _tmpFile = System.IO.Path.GetTempPath() +"local.hosts";
                    string _configFile = "$ttl 38400\n";
                    _configFile += "local.	IN	SOA	valsrv02. informatica.espackeuro.com. (\n";
                    _configFile += "			" + _serial + "\n";
                    _configFile += "			10800\n";
                    _configFile += "			3600\n";
                    _configFile += "			604800\n";
                    _configFile += "			38400 )\n";
                    _configFile += "local.	IN	NS	valsrv02.\n";
                    //create the temp File
                    var _RSD = new DynamicRS("Select entry from vDNSFile order by entry", Values.gDatos);
                    _RSD.Open();
                    while (!_RSD.EOF)
                    {
                        _configFile += _RSD["entry"]+"\n";
                        _RSD.MoveNext();
                    }
                    //_configFile += "\t}\n";
                    _RSD.Close();
                    File.WriteAllText(_tmpFile, _configFile);
                    lblMsg.Text = "File created OK.";
                    //move the file to the server
                    using (var client = new SftpClient(_serverDNS, txtUser.Text, txtPassword.Text))
                    {
                        lblMsg.Text = "Connecting the server.";
                        client.Connect();
                        lblMsg.Text = "Server Connected!";
                        client.ChangeDirectory("/var/lib/bind/");
                        using (var fileStream = new FileStream(_tmpFile, FileMode.Open))
                        {
                            client.BufferSize = 4 * 1024; // bypass Payload error large files
                            client.UploadFile(fileStream, Path.GetFileName(_tmpFile));
                        }
                        lblMsg.Text = "File updated!";
                        client.Disconnect();
                    }
                    //restart the dhcp service
                    using (var client = new SshClient(_serverDNS, txtUser.Text, txtPassword.Text))
                    {
                        client.Connect();
                        client.RunCommand("service bind9 reload");
                        lblMsg.Text = "DNS Service reloaded!";
                        client.Disconnect();
                    }
                    MessageBox.Show("The process has finished correctly.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
