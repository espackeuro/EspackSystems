//using System;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Support.V7.App;
//using Android.Views;
//using Android.Widget;
//using DataWedge;
//using AccesoDatosNet;
//using System.Net.Sockets;
//using System.Net;
//using System.Text;
//using System.Threading;

//namespace RadioLogisticaDeliveries
//{

//    [Activity(Label = "Radio LOGISTICA deliveries", WindowSoftInputMode = SoftInput.AdjustResize)]
//    public class MainScreen2 : AppCompatActivity
//    {
//        protected EditText txtReading;
//        protected TextView txtPartnumber;
//        protected TextView txtQty;
//        protected TextView txtLabelRack;
//        protected TextView txtCM;
//        public TextView txtConsole;
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);
//            this.Title = "Block " + Values.gBlock;
//            SetContentView(Resource.Layout.Deliveries);
//            txtReading = FindViewById<EditText>(Resource.Id.txtReading);
//            txtPartnumber = FindViewById<TextView>(Resource.Id.txtPartnumber);
//            txtQty = FindViewById<TextView>(Resource.Id.txtQty);
//            txtLabelRack = FindViewById<TextView>(Resource.Id.txtLabelRack);
//            var btnCloseSession = FindViewById<Button>(Resource.Id.btnCloseSession);
//            txtConsole = FindViewById<TextView>(Resource.Id.txtConsole);
//            btnCloseSession.Click += BtnCloseSession_Click;
//            txtReading.KeyPress += TxtReading_KeyPress;

//        }

//        private void BtnCloseSession_Click(object sender, EventArgs e)
//        {
//            ThreadPool.QueueUserWorkItem(o =>  StartClient());
//        }

//        private void TxtReading_KeyPress(object sender, View.KeyEventArgs e)
//        {
//            if (e.KeyCode== Keycode.Enter || e.KeyCode== Keycode.Tab)
//            {
//                Toast.MakeText(this, txtReading.Text, ToastLength.Long).Show();
//            }
//        }

//        private void TxtReading_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        public void StartClient()
//        {
//             Data buffer for incoming data.
//            byte[] bytes = new byte[1024];

//             Connect to a remote device.
//            try
//            {
//                 Establish the remote endpoint for the socket.
//                 This example uses port 11000 on the local computer.
//                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
//                IPAddress ipAddress;
//                IPAddress.TryParse("10.200.90.3", out ipAddress);
//                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 15000);

//                 Create a TCP/IP  socket.
//                Socket sender = new Socket(AddressFamily.InterNetwork,
//                    SocketType.Stream, ProtocolType.Tcp);

//                 Connect the socket to the remote endpoint. Catch any errors.
//                try
//                {
//                    sender.Connect(remoteEP);
//                    RunOnUiThread(() => txtConsole.Text += String.Format("Socket connected to {0}\n", sender.RemoteEndPoint.ToString()));
//                    Console.WriteLine("Socket connected to {0}",
//                        sender.RemoteEndPoint.ToString());

//                     Encode the data string into a byte array.
//                    byte[] msg = Encoding.ASCII.GetBytes(@"<procedure> 
//  <DB>TEST</DB>
//  <name>pAddTest</name>
//  <parameters>@Value='patatita huevos 3'</parameters>
//</procedure>");

//                     Send the data through the socket.
//                    int bytesSent = sender.Send(msg);

//                     Receive the response from the remote device.
//                    int bytesRec = sender.Receive(bytes);
//                    RunOnUiThread(() => txtConsole.Text += String.Format("Echoed test = {0}\n",Encoding.ASCII.GetString(bytes, 0, bytesRec)));
//                    Console.WriteLine("Echoed test = {0}",
//                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

//                     Release the socket.
//                    sender.Shutdown(SocketShutdown.Both);
//                    sender.Close();

//                }
//                catch (ArgumentNullException ane)
//                {
//                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
//                }
//                catch (SocketException se)
//                {
//                    Console.WriteLine("SocketException : {0}", se.ToString());
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
//                }

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//    }

//}