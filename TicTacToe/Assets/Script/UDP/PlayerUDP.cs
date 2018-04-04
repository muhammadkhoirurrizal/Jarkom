using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUDP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            MovePos("Horizontal", -5);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePos("Horizontal", 5);
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovePos("Vertical", 5);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePos("Vertical", -5);
        }
    }

    void MovePos(string To, int Speed)
    {
        if (To == "Horizontal")
        {
            transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
            UDPNetwork.Instance.SendToServer("Move", transform);
        }else if (To == "Vertical")
        {
            transform.Translate(new Vector3(0,Speed * Time.deltaTime, 0));
            UDPNetwork.Instance.SendToServer("Move", transform);
        }
    }
}
