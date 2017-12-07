using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Action : NetworkBehaviour {
    public string sens;
    public Text numero;
    [SyncVar]
    private string chiffre1Sync;
    [SyncVar]
    private string chiffre2Sync;
    [SyncVar]
    private string chiffre3Sync;
    [SyncVar]
    private string chiffre4Sync;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
        chiffre1.text = !chiffre1Sync.Equals(chiffre1.text) ? chiffre1Sync : chiffre1.text;
        chiffre2.text = !chiffre2Sync.Equals(chiffre2.text) ? chiffre2Sync : chiffre2.text;
        chiffre3.text = !chiffre3Sync.Equals(chiffre3.text) ? chiffre3Sync : chiffre3.text;
        chiffre4.text = !chiffre4Sync.Equals(chiffre4.text) ? chiffre4Sync : chiffre4.text;
        */
    }

    public void click()
    {
        switch (sens)
        {
            case ("u"):
                numero.text = ((int.Parse(numero.text) + 1) % 10).ToString();
                break;
            case ("d"):
                numero.text = (int.Parse(numero.text) - 1)<0 ? "9": (int.Parse(numero.text) - 1).ToString();
                break;
        }
    }
}
