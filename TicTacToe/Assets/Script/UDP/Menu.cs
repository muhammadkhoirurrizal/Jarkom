using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Text URL;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Btn()
    {
        if (URL.text != "" || URL.text != " ")
        {
            PlayerPrefs.SetString("URL", URL.text);
            Application.LoadLevel(1);
        }
    }
}
