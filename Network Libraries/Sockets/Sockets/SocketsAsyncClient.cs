using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;
using Dawn.Net.Sockets;
using System.Linq;

namespace SocketsClient
{


// State object for receiving data from remote device.
public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketClient
    {
        public IPAddress ServerIP { get; set; }
        public int ServerPort { get; set; }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private SocketsStatus oStatus = SocketsStatus.OFFLINE;

        public SocketsStatus Status
        {
            get
            {
                return oStatus;
            }
            set
            {
                if (value != oStatus)
                {
                    oStatus = value;
                    OnPropertyChanged("Status");
                }

            }
        }
        public enum SocketsStatus { OFFLINE}

        public async Task<string> AsyncConversation(string msgOut)
        {
            try
            {
                string dataFromServer = "";
                IPAddress ipAddress = ServerIP;
                var port = ServerPort;
                if (ipAddress == null)
                    throw new Exception("No IPv4 address for server");
                using (TcpClient client = new TcpClient())
                {
                    
                    await client.ConnectAsync(ipAddress, port);
                    //LogMessage("Connected to Server");
                    using (var networkStream = client.GetStream())
                    using (var writer = new StreamWriter(networkStream))
                    using (var reader = new StreamReader(networkStream))
                    {
                        writer.AutoFlush = true;
                        await writer.WriteLineAsync(msgOut);
                        try
                        {
                            dataFromServer = await reader.ReadLineAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (string.IsNullOrEmpty(dataFromServer))
                        {
                            return "";
                        }

                    }

                }



                return dataFromServer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SyncConversation(string msgOut)
        {
            try
            {
                string dataFromServer = "";
                IPAddress ipAddress = ServerIP;
                var port = ServerPort;
                if (ipAddress == null)
                    throw new Exception("No IPv4 address for server");
                TcpClient client = new TcpClient();
                

                    client.Connect(ipAddress, port);
                //LogMessage("Connected to Server");
                using (var networkStream = client.GetStream())
                using (var writer = new StreamWriter(networkStream))
                using (var reader = new StreamReader(networkStream))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(msgOut);
                    try
                    {
                        dataFromServer = reader.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    client.Close();
                    if (string.IsNullOrEmpty(dataFromServer))
                    {
                        return "";
                    }
                }
                return dataFromServer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //#region AsyncSockets
        //private static Socket listener;
        //private static SocketAwaitablePool awaitables = new SocketAwaitablePool(100);
        //private static BlockingBufferManager buffers = new BlockingBufferManager(1024, 100);
        //public async Task<string> AsyncConversation(string msgOut)
        //{
        //    // Data buffer for incoming data.
        //    byte[] bytes = new byte[1024];

        //    // Connect to a remote device.
        //    try
        //    {
        //        // Establish the remote endpoint for the socket.
        //        // This example uses port 11000 on the local computer.
        //        //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //        //IPAddress ipAddress;
        //        //IPAddress.TryParse("10.200.90.7", out ipAddress);
        //        IPEndPoint remoteEP = new IPEndPoint(ServerIP, ServerPort);

        //        // Create a TCP/IP  socket.
        //        Socket sender = new Socket(AddressFamily.InterNetwork,
        //            SocketType.Stream, ProtocolType.Tcp);

        //        // Connect the socket to the remote endpoint. Catch any errors.
        //        try
        //        {
        //            sender.Connect(remoteEP);
        //            //RunOnUiThread(() => txtConsole.Text += String.Format("Socket connected to {0}\n", sender.RemoteEndPoint.ToString()));
        //            //Console.WriteLine("Socket connected to {0}",
        //            //    sender.RemoteEndPoint.ToString());

        //            // Encode the data string into a byte array.

        //            var awaitable = awaitables.Take();
        //            awaitable.Buffer = new ArraySegment<byte>(Encoding.ASCII.GetBytes(msgOut));

        //            while (true)
        //            {
        //                if (await sender.SendAsync(awaitable) != SocketError.Success)
        //                    throw (new Exception("Error lala")); // or log and throw an exception.

        //                if (awaitable.Buffer.Count == awaitable.Transferred.Count)
        //                    break; // Break if all the data is sent.

        //                // Set the buffer to send the remaining data.
        //                awaitable.Buffer = new ArraySegment<byte>(
        //                    awaitable.Buffer.Array,
        //                    awaitable.Buffer.Offset + awaitable.Transferred.Count,
        //                    awaitable.Buffer.Count - awaitable.Transferred.Count);
        //            }
        //            awaitable.Clear();
        //            // Receive the response from the remote device.
        //            string _result = "";
        //            // Get a buffer from the pool.
        //            var buffer = buffers.GetBuffer();
        //            while (true)
        //            {
        //                awaitable.Buffer = buffer;

        //                // Receive data from the client.
        //                var result = await sender.ReceiveAsync(awaitable);

        //                if (result != SocketError.Success)
        //                {
        //                    // Something went wrong.
        //                    // Check `result` and break the loop.
        //                    break;
        //                }
        //                _result += Encoding.ASCII.GetString(buffer.ToArray<byte>(), 0, awaitable.Transferred.Count);
        //                // Received data is now in `awaitable.Transferred`.
        //                if (awaitable.Transferred.Count == 0)
        //                {
        //                    // The client "gracefully" closed the connection.
        //                    break;
        //                }

        //            }





        //            sender.Shutdown(SocketShutdown.Both);
        //            sender.Close();
        //            return _result;
        //        }
        //        catch (ArgumentNullException ane)
        //        {
        //            _ErrorMsg = string.Format("ArgumentNullException : {0}", ane.Message);
        //            throw (ane);

        //            //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        //        }
        //        catch (SocketException se)
        //        {
        //            _ErrorMsg = string.Format("SocketException : {0}", se.Message);
        //            throw (se);

        //            //Console.WriteLine("SocketException : {0}", se.ToString());
        //        }
        //        catch (Exception e)
        //        {
        //            _ErrorMsg = string.Format("Unexpected exception : {0}", e.Message);
        //            throw (e);
        //            //Console.WriteLine("Unexpected exception : {0}", e.ToString());
        //        }

        //    }



        //    catch (Exception e)
        //    {
        //        _ErrorMsg = string.Format("General exception : {0}", e.Message);
        //        return "";
        //        throw (e);
        //        //Console.WriteLine(e.ToString());
        //    }
        //}
        //#endregion
        private string _ErrorMsg = "";



        public string ErrorMsg
        {
            get
            {
                return _ErrorMsg;
            }
        }

    }
}
