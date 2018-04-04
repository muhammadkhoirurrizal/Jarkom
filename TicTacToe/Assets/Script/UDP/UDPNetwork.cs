using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

public class UDPNetwork : MonoBehaviour {

    public UdpClient client;
    public IPAddress IPLocal;
    public IPEndPoint IPEndServer;

    public string Need;

    bool Change = false;

    public GameObject PlayerPref;

    public static UDPNetwork Instance;

    // Use this for initialization
    void Start () {
       if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Login();
    }

    public void Login()
    {
        client = new UdpClient();
        var Host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in Host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                IPLocal = ip;
            }
        }
        IPAddress IPServer = IPAddress.Parse("192.168.43.225");
        IPEndPoint IPEndLocal = new IPEndPoint(IPLocal, 50);
        IPEndServer = new IPEndPoint(IPServer, 50);

        try
        {
            string Request = IPLocal + "|" + "Connected";
            byte[] BytReq = new byte[100];
            BytReq = Encoding.Default.GetBytes(Request);
            client.Send(BytReq, BytReq.Length, IPEndServer);
            //GameObject Player = Instantiate(PlayerPref, new Vector3(0, 0, 0), Quaternion.identity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Thread th = new Thread(RecieveData);
        th.Start(IPEndServer);
        
    }

    public void SendToServer(string Code, [Optional]Transform Pos)
    {
        try
        {
            string Request = IPLocal + "|" + Code  + "|" + Pos.position.x + "|" + Pos.position.y + "|" + Pos.position.z;
            byte[] BytReq = new byte[1024];
            BytReq = Encoding.Default.GetBytes(Request);
            client.Send(BytReq, BytReq.Length, IPEndServer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    void RecieveData(object obj)
    {
        while (true)
        {
            IPEndPoint ep = (IPEndPoint)obj;
            try
            {
                byte[] receivedData = new byte[100];
                receivedData = client.Receive(ref ep);

                string[] DataDiterima = Encoding.Default.GetString(receivedData).Split('|');
                if (DataDiterima[1] == "EnemyConnect")
                {
                    Debug.Log("Enemy Spawn");
                }
                else if (DataDiterima[1] == "Move")
                {
                    if (DataDiterima[0] != IPLocal.ToString())
                    {
                        PlayerPref.transform.position = Vector3.Lerp(PlayerPref.transform.position, new Vector3(float.Parse(DataDiterima[2]), float.Parse(DataDiterima[3]), float.Parse(DataDiterima[4])), 10);
                    }
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            client.Close();
            Debug.Log("Close");
        }
	}
}
