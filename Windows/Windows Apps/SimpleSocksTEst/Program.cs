
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
class Program
{
    static void Main(string[] args)
    {
        StartClient(Convert.ToInt32(17011));
        Console.ReadLine();
    }

    private static async void StartClient(int port)
    {
        TcpClient client = new TcpClient();
        await client.ConnectAsync(IPAddress.Parse("10.200.90.3"), port);
        LogMessage("Connected to Server");
        using (var networkStream = client.GetStream())
        using (var writer = new StreamWriter(networkStream))
        using (var reader = new StreamReader(networkStream))
        {
            writer.AutoFlush = true;
            for (int i = 0; i < 10; i++)
            {
                await writer.WriteLineAsync(DateTime.Now.ToLongDateString());
                var dataFromServer = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(dataFromServer))
                {
                    LogMessage(dataFromServer);
                }

            }
        }
        if (client != null)
        {
            client.Close();
        }

    }
    private static void LogMessage(string message,
            [CallerMemberName]string callername = "")
    {
        System.Console.WriteLine("{0} - Thread-{1}- {2}",
            callername, Thread.CurrentThread.ManagedThreadId, message);
    }

}