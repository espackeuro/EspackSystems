using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace SocketClientWinForm
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("10.200.90.7", 15000);
            label1.Text = "Client Socket Program - Server Connected ...";
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, 10025);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg(returndata);
            textBox2.Text = "";
            textBox2.Focus();
        }

        public void msg(string mesg)
        {
            textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;
        }


    }
}