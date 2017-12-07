using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coffre : MonoBehaviour {
    public Text chiffre1;
    public Text chiffre2;
    public Text chiffre3;
    public Text chiffre4;

    public string code;
    public Transform rabat;
    public List<GameObject> objectToActivate = new List<GameObject>();

    private bool ouvert = false;
    private float debut;

    void Start () {
        activateObject(false);
    }

	private void activateObject(bool activate)
    {
        foreach (GameObject obj in objectToActivate)
        {
            obj.GetComponent<Interractable>().pickable=activate;
        }
    }

    void FixedUpdate () {
        if (!ouvert)
        {
            ouvert = (code == (chiffre1.text + chiffre2.text + chiffre3.text + chiffre4.text));
            debut = Time.time;
        }
        else
        {
            if (Time.time < debut+2f)
            {
                rabat.Rotate(-40*Time.deltaTime,0,0);
                activateObject(true);
            }
            else
            {
                rabat.Rotate(0, 0, 0);
            }
        }
    }

}
