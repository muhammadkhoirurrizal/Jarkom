using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public static Login Instance { get; set; }
    public Text[] Name;
    public static bool Ready = false;
    public static bool HasSet = false;

    public GameObject[] UIMenu;

	// Use this for initialization
	void Start () {
		if (Instance==null)
        {
            Instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Connect()
    {
        if (Name[0].text != "")
        {
            NetworkManager.Instance.LoginToServer(Name[0].text);
            NetworkManager.Instance.streamWriter.WriteLine("Ready");
            NetworkManager.Instance.streamWriter.Flush();
            UIMenu[0].SetActive(false);
        }
    }

    public void ReadyToPlay()
    {
        UIMenu[1].SetActive(true);
    }

    public void LoginButton()
    {
        if (Name[1].text != "")
        {
            PlayerPrefs.SetString("YourName", Name[1].text);
            NetworkManager.Instance.streamWriter.WriteLine("Name" + "." + Name[1].text);
            NetworkManager.Instance.streamWriter.Flush();
            Ready = true;
            if (HasSet)
            {
                Application.LoadLevel(1);
            }
            UIMenu[2].SetActive(false);
        }
    }

    public void GetEnemyName(string Name)
    {
        PlayerPrefs.SetString("EnemyName", Name);
        HasSet = true;
        if (Ready)
        {
            Application.LoadLevel(1);
        }
    }

}
