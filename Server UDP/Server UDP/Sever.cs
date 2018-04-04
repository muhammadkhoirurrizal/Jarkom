using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server_UDP
{
    class Sever
    { 
        /*    public static int listenPort = 50;
         //static UdpClient listener;
         public static List<IPEndPoint> groupEP = new List<IPEndPoint>();
         //static IPEndPoint EP;

         private static void Main(string[] args)
         {
   
                 UdpClient listener = new UdpClient(listenPort);
                 Thread thread = new Thread(Read);
                 thread.Start(listener);
                 Console.WriteLine("Port : " + listenPort.ToString());
                 listenPort++;
             
         }
         public static void Read(object obj)
         {
             UdpClient New = (UdpClient)obj;
             IPEndPoint EP = new IPEndPoint(IPAddress.Any, listenPort);
             bool Add = false;
             try
             {
                 while (true)
                 {
                     byte[] bytes = New.Receive(ref EP);
                     string message = Encoding.ASCII.GetString(bytes);
                     if (!Add)
                     {
                         groupEP.Add(EP);
                         Console.WriteLine("Add");
                         Add = true;
                     }
                     SentBroadcast(New, bytes, EP);
                 }
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.ToString());
             }
             finally
             {
                 New.Close();
             }
         }

         static void SentBroadcast(UdpClient udpServer,byte[] data, IPEndPoint remote)
         {
             for (int i = 0; i < groupEP.Count; i++)
             {
                 IPEndPoint RemoteEP = groupEP[i];
                 if (RemoteEP != remote)
                 {
                     udpServer.Send(data, data.Length, groupEP[i]);
                 }
             }
         }*/

        static UdpClient udpServer;
        static List<IPEndPoint> listClient;
        static int Listen = 50;

        static void Main(string[] args)
        {
            udpServer = new UdpClient(Listen);

            Console.WriteLine("Waiting");
            listClient = new List<IPEndPoint>();

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, Listen);
            while (true)
            {
                byte[] data = udpServer.Receive(ref remoteEP);
                Console.WriteLine(Encoding.Default.GetString(data));
                if (!listClient.Contains(remoteEP))
                {
                    Console.WriteLine(remoteEP);
                    listClient.Add(remoteEP);
                }
                SentBroadcast(data, remoteEP);
            }
        }

        /*static void AddIdentity(byte[] data, IPEndPoint iPEndPoint)
        {
            string[] DataDiterima = Encoding.Default.GetString(data).Split('|');
            //if (DataDiterima[1] != "Connected")
            //{
                
           // }
        }*/

        static void SentBroadcast(byte[] data, IPEndPoint remote)
        {
            foreach (IPEndPoint Client in listClient)
            {
                if (Client!=remote)
                {
                    Console.WriteLine(Client + " " + remote);
                    udpServer.Send(data, data.Length, Client);
                }
            }
        }
    }
}
