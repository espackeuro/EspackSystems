using Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonTools;
using AccesoDatosNet;
using System.Reflection;

namespace AsyncEchoServer
{
    public class AsyncEchoServer
    {
        private int _listeningPort;
        public AsyncEchoServer(int port)
        {
            _listeningPort = port;
        }
        ///<summary>
        /// Start listening for connection
        /// </summary>
        public async void Start()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(x => x.GetAddressBytes()[0] == 10);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 17011);
            TcpListener listener = new TcpListener(ipAddress, _listeningPort);
            listener.Start();
            LogMessage("Server is running");
            LogMessage("Listening on port " + _listeningPort);

            while (true)
            {
                LogMessage("Waiting for connections...");
                try
                {
                    var tcpClient = await listener.AcceptTcpClientAsync();
                    HandleConnectionAsync(tcpClient);
                }
                catch (Exception exp)
                {
                    LogMessage(exp.ToString());
                }

            }

        }
        ///<summary>
        /// Process Individual client
        /// </summary>
        ///
        ///
        private async void HandleConnectionAsync(TcpClient tcpClient)
        {
            string clientInfo = tcpClient.Client.RemoteEndPoint.ToString();
            LogMessage(string.Format("Got connection request from {0}", clientInfo));
            try
            {
                using (var networkStream = tcpClient.GetStream())
                using (var reader = new StreamReader(networkStream))
                using (var writer = new StreamWriter(networkStream))
                {
                    writer.AutoFlush = true;
                    while (true)
                    {
                        var dataFromServer = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(dataFromServer))
                        {
                            break;
                        }
                        LogMessage("RECEIVED:" + dataFromServer);
                        

                        var m = new genericXMessage();
                        var mec = new DecryptMessageInput(new DecompressMessageInput(m));
                        mec.MsgIn = dataFromServer;
                        await mec.process();
                        Console.WriteLine(mec.Debug);

                        var x = ADatabaseMessage.Parse(mec.MsgOut);

                        var xec = new EncryptMessageOutput(new CompressMessageOutput(x));
                        xec.MsgIn = mec.MsgOut;
                        await xec.process();
                        Console.WriteLine(xec.Debug);
                        LogMessage("SENT:"+xec.MsgOut);
                        await writer.WriteLineAsync(xec.MsgOut);
                        //await writer.WriteLineAsync(dataFromServer.ToUpper());
                        //LogMessage("SENT:" + dataFromServer.ToUpper());
                    }
                }
            }
            catch (Exception exp)
            {
                LogMessage(exp.Message);
            }
            finally
            {
                LogMessage(string.Format("Closing the client connection - {0}",
                            clientInfo));
                tcpClient.Close();
            }

        }
        private void LogMessage(string message,
                                [CallerMemberName]string callername = "")
        {
            System.Console.WriteLine("[{0}] - Thread-{1}- {2}",
                    callername, Thread.CurrentThread.ManagedThreadId, message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var espackArgs = CT.LoadVars(args);
            Values.ProjectName = Assembly.GetCallingAssembly().GetName().Name;
            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;
            InitialConnection.gDatos = Values.gDatos;
            AsyncEchoServer async = new AsyncEchoServer(17011);
            async.Start();
            Console.ReadLine();
        }
    }
    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string LabelPrinterAddress = "";
        public static string COD3 = "";
        public static string ProjectName = "";
    }
}