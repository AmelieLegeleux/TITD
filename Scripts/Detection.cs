using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Detection : NetworkBehaviour {

    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        if (!owner.GetComponent<Interraction>().inInterraction)
        {
            if (other.gameObject.transform.tag == "Interractable")
            {
                if (!other.gameObject.transform.GetComponent<Interractable>().interract && other.gameObject.transform.GetComponent<Interractable>().pickable)
                {
                    owner.GetComponent<Interraction>().toucheObjet(other.gameObject);
                    /*
                    owner.GetComponent<Interraction>().objetTouche = other.gameObject;
                    owner.GetComponent<Interraction>().afficheNom(other.gameObject.transform.GetComponent<Interractable>().displayName, 2);
                    */
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (owner.GetComponent<Interraction>() != null)
        {
            if (!owner.GetComponent<Interraction>().inInterraction)
            {
                if (owner.GetComponent<Interraction>().objetTouche == other.gameObject)
                {
                    owner.GetComponent<Interraction>().free();
                    //                owner.GetComponent<Interraction>().objetTouche = null;
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
