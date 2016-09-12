﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using AccesoDatosNet;
using CommonTools;
using System.Reflection;
using System.Xml.Linq;
using Encryption;
using System.Linq;
using System.Collections.Generic;
using Socks;

// State object for reading client data asynchronously
public class StateObject
{
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}

public class AsynchronousSocketListener
{
    //connection list
    protected static Dictionary<string, cAccesoDatosNet> Connections = new Dictionary<string, cAccesoDatosNet>();

    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {
    }



    public static void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(x => x.GetAddressBytes()[0] == 10);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 15000);

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.
        allDone.Set();

        // Get the socket that handles the client request.
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket. 
        int bytesRead = handler.EndReceive(ar);
        string _msgOut = "";
        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read 
            // more data.
            content = state.sb.ToString();

            Console.WriteLine("-ENCRYPTED-------------------------------\n- Client -> Server: {0} bytes\n-----------------------------------------\n{1}",
               content.Length, content);

            content = StringCipher.Decrypt(content);

            // All the data has been read from the 
            // client. Display it on the console.
            Console.WriteLine("-DECRYPTED-------------------------------\n- Client -> Server: {0} bytes\n-----------------------------------------\n{1}",
                content.Length, content);

            if (content.IndexOf("</StartSession>") > -1)
            {
                _msgOut = StartSession(content);
                var _x = XDocument.Parse(_msgOut);
                var _result = _x.Root.Element("Result");
                if (_result.Value.Substring(0, 2) == "OK")
                {
                    //create a new local connection to the session just opened
                    XDocument _msgIn = XDocument.Parse(content);
                    string _serial = _msgIn.Root.Element("Serial").Value;
                    //Connections.Add(_serial, Values.gDatos.Clone());
                }
            }
            else
            if (content.IndexOf("</procedure>") > -1)
            {
                //launch the procedure
                _msgOut = launchProcedure(content);
            }
            else
            if (content.IndexOf("</connection>") > -1)
            {
                _msgOut = launchConnection(content).ToString();
            }
            else
            {
                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }
        // Return result value. Display it on the console.
        Console.WriteLine("-DECRYPTED-------------------------------\n- Server -> Client: {0} bytes\n-----------------------------------------\n{1}",
        _msgOut.Length, _msgOut);
        Send(handler, StringCipher.Encrypt(_msgOut));
    }

    private static XElement launchConnection(string content)
    {
        var _x = XDocument.Parse(content);
        XElement _xServer = _x.Root.Element("server");
        var _server = new cServer();
        _server.xServer = _xServer;
        var _conn = new cAccesoDatosNet(_server, _x.Root.Element("DataBase").Value);
        try
        {
            _conn.Connect();
            return new XElement("result", "OK");
        } catch (Exception ex)
        {
            return new XElement("result", "Error: "+ex.Message);
        }
        
    }

    private static string StartSession(string content)
    {
        XDocument _msgIn = XDocument.Parse(content);
        string _serial= _msgIn.Root.Element("Serial").Value;
        string _sessionProc = cSocks.BuildSPXML("SISTEMAS", "pStartSockSession", string.Format("@Serial='{0}'", _serial));
        return launchProcedure(_sessionProc);
    }

    private static string launchProcedure(string content)
    {
        XDocument _msgIn = XDocument.Parse(content);
        var _procedureName = _msgIn.Root.Element("name").Value;

        if (_procedureName == "pLogonUser")
        {
            var _encryptPassword = _msgIn.Root.Element("password").Value;
            var _password = StringCipher.Decrypt(_encryptPassword);
            _msgIn.Root.Element("password").Value = _password;
            content = _msgIn.ToString();
        }

        string _msgExec = "";
        using (var _datos = new cAccesoDatosNet(Values.gDatos))
        {

            using (var _SP = new SP(_datos, "pLaunchCommand"))
            {
                _SP.AddParameterValue("@xmlCommand", content);
                _SP.AddParameterValue("@msgExec", "");
                try
                {
                    _SP.Execute();
                    _msgExec = _SP.ReturnValues()["@msgExec"].ToString();
                }
                catch
                {
                    throw; // acer halgo
                }

            }

        }
        XDocument _msgOut = XDocument.Parse(_msgExec);
        if (_msgOut.Root.Element("Password") != null)
        {
            _msgOut.Root.Element("Password").Value = StringCipher.Encrypt(_msgOut.Root.Element("Password").Value).ToString();
        }

        // Echo the data back to the client.

        //Console.WriteLine("-ENCRYPTED-------------------------------\n- Server -> Client: {0} bytes\n-----------------------------------------\n{1}",
        //   content.Length, content);
        return _msgOut.ToString();
        
    } 

    private static void Send(Socket handler, String data)
    {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public static int Main(String[] args)
    {

        var espackArgs = CT.LoadVars(args);
        Values.ProjectName = Assembly.GetCallingAssembly().GetName().Name;
        Values.gDatos.DataBase = espackArgs.DataBase;
        Values.gDatos.Server = espackArgs.Server;
        Values.gDatos.User = espackArgs.User;
        Values.gDatos.Password = espackArgs.Password;

        StartListening();

        return 0;
    }

    public static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static string LabelPrinterAddress = "";
        public static string COD3 = "";
        public static string ProjectName = "";
    }
}
  
 