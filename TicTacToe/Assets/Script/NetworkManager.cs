using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public TcpClient tcpClient;
    public StreamWriter streamWriter;
    public static NetworkManager Instance;

    public string Need;

    bool Change = false;

    // Use this for initialization
    void Start () {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        
    }

    public void LoginToServer(string IP)
    {
        try
        {
            tcpClient = new TcpClient(IP, 4444);
            Debug.Log("Connect To Server");
            Thread thread = new Thread(Read);
            thread.Start(tcpClient);
            streamWriter = new StreamWriter(tcpClient.GetStream());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Update()
    {
        if (Change)
        {
            string[] msgPart = Need.Split('.');
            if (msgPart[0] == "Name")
            {
                Login.Instance.GetEnemyName(msgPart[1]);
            }
            else if (msgPart[0] == "Pilihan")
            {
                int No;
                Int32.TryParse(msgPart[1], out No);
                GameManager.Instance.Change(No);
            }
            else if (msgPart[0] == "Turn")
            {
                if (msgPart[1] =="Ready")
                {
                    GameManager.Instance.UIChange();
                }
                {
                    int No;
                    Int32.TryParse(msgPart[1], out No);
                    GameManager.Instance.DependTurn(No);
                }
                
            }
            else if (msgPart[0] == "Request")
            {
                GameManager.Instance.RequestRand();
            }
            else if (msgPart[0] == "Hasil")
            {
                GameManager.Instance.Win();
            }
            else if (msgPart[0] == "Ready")
            {
                Login.Instance.ReadyToPlay();
            }
            else if (msgPart[0] == "Draw")
            {
                GameManager.Instance.Draw();
            }
            Change = false;
        }
    }

    void Read(object obj)
    {
        TcpClient tcpClient = (TcpClient)obj;
        StreamReader streamReader = new StreamReader(tcpClient.GetStream());

        while (true)
        {
            try
            {
                Need = streamReader.ReadLine();

                Change = true;

            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                break;
            }
        }
    }
}
