using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlaceAble : MonoBehaviour {
    public bool occuped;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name== "Mural_Torch(Clone)")
        {
            if (!occuped)
            {
                if (other.gameObject.GetComponent<InflamAble>().OnFire)
                {
                    gameObject.transform.Find("Mural_Torch").gameObject.SetActive(true);
                    occuped = true;
                    other.gameObject.GetComponent<Interractable>().player.GetComponent<Interraction>().free();
                    GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(other.gameObject, ToDo.destroy);
                }
            }
        }
    }
}
