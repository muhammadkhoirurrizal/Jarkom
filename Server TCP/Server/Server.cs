using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;
namespace Server
{
    public class Server
    {
        private static TcpListener tcpListener;
        private static List<TcpClient> tcpClientsList = new List<TcpClient>();

        static int Mulai = 0;

        static void Main(string[] args)
        {
            tcpListener = new TcpListener(IPAddress.Any, 4444);
            tcpListener.Start();

            Console.WriteLine("Server started");

            while (true)
            {
                if (Mulai <= 2)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    tcpClientsList.Add(tcpClient);
                    Thread thread = new Thread(ClientListener);
                    thread.Start(tcpClient);
                    Mulai++;
                }
            }
        }

        public static void ClientListener(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            StreamReader reader = new StreamReader(tcpClient.GetStream());

            Console.WriteLine("Client connected");

            while (true)
            {
                if (Mulai == 2)
                {
                    Console.WriteLine("%d", Mulai);
                    string message = reader.ReadLine();
                    if (message == "Ready")
                    {
                        BroadCastAll(message, tcpClient);
                        Console.WriteLine(message + "all");
                    }
                    else if (message == "Exit")
                    {
                        foreach (TcpClient client in tcpClientsList)
                        {
                            client.Close();
                        }
                        Mulai = 0;
                    }
                    else
                    {
                        BroadCast(message, tcpClient);
                        Console.WriteLine(message);
                    }
                    
                }
            }
        }

        public static void BroadCastAll(string msg, TcpClient Client)
        {
            foreach (TcpClient client in tcpClientsList)
            {
                StreamWriter sWriter = new StreamWriter(client.GetStream());
                sWriter.WriteLine(msg);
                sWriter.Flush();
            }
        }

        public static void BroadCast(string msg, TcpClient Client)
        {
            foreach (TcpClient client in tcpClientsList)
            {
                if (client != Client)
                {
                    StreamWriter sWriter = new StreamWriter(client.GetStream());
                    sWriter.WriteLine(msg);
                    sWriter.Flush();
                }
            }
        }
    }
}
