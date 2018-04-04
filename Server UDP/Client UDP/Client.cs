using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client_UDP
{
    class Client
    {
       /* static IPEndPoint iPEndPoint;
        static UdpClient client;
        static void Main(string[] args)
        {
            client = new UdpClient();
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            int a = Int32.Parse(Console.ReadLine());
            iPEndPoint = new IPEndPoint(iPAddress, a);
            try
            {
                client.Connect(iPEndPoint);
                Thread thread = new Thread(ReceiveData);
                thread.Start(iPEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            while (true)
            {
                string Kamu = Console.ReadLine();
                byte[] sendbuf = Encoding.ASCII.GetBytes(Kamu);
                try
                {
                    client.Send(sendbuf, sendbuf.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        static void ReceiveData(object o)
        {
            while (true)
            {
                IPEndPoint ep = (IPEndPoint)o;
                byte[] receivedData = new byte[100];
                receivedData = client.Receive(ref ep);

                Console.WriteLine(Encoding.Default.GetString(receivedData));
            }
        }*/

        static UdpClient client;
        static IPAddress IPLocal;

        static void Main(string[] args)
        {
            client = new UdpClient();
            var Host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in Host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPLocal = ip;
                    Console.WriteLine(ip);
                }
            }
            Console.WriteLine("Masukkan IPServer");
            string a = Console.ReadLine();
            IPAddress IPServer = IPAddress.Parse(a);
            IPEndPoint IPEndLocal = new IPEndPoint(IPLocal, 50);
            IPEndPoint IPEndServer = new IPEndPoint(IPServer, 50); // endpoint where server is listening
            //client.Connect(IPEndLocal);

            try
            {
                string Request = IPLocal + "|" + "Connected";
                byte[] BytReq = new byte[100];
                BytReq = Encoding.Default.GetBytes(Request);
                client.Send(BytReq, BytReq.Length, IPEndServer);

            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Thread th = new Thread(ReceiveData);
            th.Start(IPEndServer);

            // send data
            while (true)
            {
                try
                {
                    string data = IPLocal + "|" + Console.ReadLine();
                    byte[] dt = new byte[100];
                    dt = Encoding.Default.GetBytes(data);
                    client.Send(dt, dt.Length, IPEndServer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        static void ReceiveData(object o)
        {
            while (true)
            {
                IPEndPoint ep = (IPEndPoint)o;
                byte[] receivedData = new byte[100];
                receivedData = client.Receive(ref ep);

                string[] DataDiterima = Encoding.Default.GetString(receivedData).Split('|');
                if (DataDiterima[0] != IPLocal.ToString())
                {
                    Console.WriteLine(DataDiterima[1]);
                }
            }
        }
    }
}
