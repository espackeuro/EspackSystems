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
    public partial class fDHCP : Form
    {
        public fDHCP()
        {
            InitializeComponent();
            cboCOD3.Source("select n.COD3 from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3",Values.gDatos);
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("This will update the DHCP server assignments and will restart the service.\n Are you sure? ", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)==DialogResult.Yes)
                {
                    string _serverDHCP = txtServerName.Text;
                    string _tmpFile = System.IO.Path.GetTempPath() + cboCOD3.Text + "_NET.conf";
                    string _configFile = "";
                    //create the temp File
                    var _RSD = new DynamicRS("Select * from vDHCPConfig where COD3='" + cboCOD3.Text + "'", Values.gDatos);
                    _RSD.Open();
                    string _group = "";
                    while (!_RSD.EOF)
                    {
                        if (_RSD["Group"].ToString() != _group)
                        {
                            if (_group != "")
                                _configFile += "\t}\n";
                            _group = _RSD["Group"].ToString();
                            _configFile += "# " + _group + '\n' + "group " + _group + "{" + '\n';
                        }
                        _configFile += "\t# " + _RSD["Comment"].ToString() + '\n';
                        _configFile += "\thost " + _RSD["Host"].ToString() + " {" + '\n';
                        _configFile += "\t\thardware ethernet " + _RSD["MAC"].ToString() + ";" + '\n';
                        _configFile += "\t\tfixed-address " + _RSD["IP"].ToString() + ";" + '\n';
                        _configFile += "\t\t}\n";
                        _RSD.MoveNext();
                    }
                    _configFile += "\t}\n";
                    _RSD.Close();
                    File.WriteAllText(_tmpFile, _configFile);
                    lblMsg.Text = "File created OK.";
                    //move the file to the server
                    using (var client = new SftpClient(_serverDHCP, txtUser.Text, txtPassword.Text))
                    {
                        lblMsg.Text = "Connecting the server.";
                        client.Connect();
                        lblMsg.Text = "Server Connected!";
                        client.ChangeDirectory("/etc/dhcp/");
                        using (var fileStream = new FileStream(_tmpFile, FileMode.Open))
                        {
                            client.BufferSize = 4 * 1024; // bypass Payload error large files
                            client.UploadFile(fileStream, Path.GetFileName(_tmpFile));
                        }
                        lblMsg.Text = "File updated!";
                        client.Disconnect();
                    }
                    //restart the dhcp service
                    using (var client = new SshClient(_serverDHCP, txtUser.Text, txtPassword.Text))
                    {
                        client.Connect();
                        client.RunCommand("service isc-dhcp-server restart");
                        lblMsg.Text = "DHCP Service restarted!";
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

        private void cboCOD3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCOD3.Value.ToString() != "")
                try
                {
                    var _RS = new DynamicRS("Select DHCP_Path,ServerShareIP from General..Sedes where COD3='" + cboCOD3.Text + "'", Values.gDatos);
                    _RS.Open();
                    if (_RS.EOF)
                    {
                        throw new Exception("Wrong COD3 code.");
                    }
                    txtServerName.Text = _RS["ServerShareIP"].ToString();
                    txtUser.Text = "root";
                    txtPassword.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }


    }
}
