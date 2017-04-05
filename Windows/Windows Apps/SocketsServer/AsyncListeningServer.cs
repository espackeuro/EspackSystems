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

namespace SocketsServer
{
    public class AsyncListeningServer
    {
        private int ServerPort;
        private int ControlPort;
        public AsyncListeningServer(int serverPort, int controlPort)
        {
            ServerPort = serverPort;
            ControlPort = controlPort;
        }
        public AsyncListeningServer(int serverPort)
        {
            ServerPort = serverPort;
            ControlPort = 0;
        }

        public async Task StartServer(int port)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(x => x.GetAddressBytes()[0] == 10);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();
            LogMessage("Server is running");
            LogMessage("Listening on port " + port);

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

        public void Start()
        {
            Task.Run(() => StartServer(ServerPort));
            if (ControlPort != 0)
                Task.Run(() => StartServer(ControlPort));
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

                        var x = SXMessage.Parse(mec.MsgOut);

                        var xec = new EncryptMessageOutput(new CompressMessageOutput(x));
                        xec.MsgIn = mec.MsgOut;
                        await xec.process();
                        Console.WriteLine(xec.Debug);
                        LogMessage("SENT:" + xec.MsgOut);
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
}
