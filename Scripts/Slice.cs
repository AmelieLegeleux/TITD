using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Slice : NetworkBehaviour {

    [SyncVar]
    private bool isSlicing;
    private GameObject hand;
    public float posYStart;
    public float posYStartHand;

    public void slice(NetworkInstanceId playerHand)
    {
        if (playerHand != NetworkInstanceId.Invalid)
        {
            this.hand = ClientScene.FindLocalObject(playerHand).transform.Find(name: "Head").Find("Hand").gameObject;
        }
        else
        {
            this.hand = null;
        }
        isSlicing = (playerHand != NetworkInstanceId.Invalid);
        if (hand != null)
        {
            posYStartHand = hand.transform.position.y;
            posYStart = gameObject.transform.position.y;
        }
    }

    void Start () {
        initialisation();
	}
	
	void Update () {
        if (isSlicing)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, posYStart+(hand.transform.position.y- posYStartHand), gameObject.transform.position.z);
        }
        else
        {
            if (gameObject.transform.position.y < 3.65 && !base.gameObject.GetComponent<Interractable>().interract)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y+(2f*Time.deltaTime)), gameObject.transform.position.z);
            }
        }
	}

    private void initialisation()
    {
        isSlicing = false;
    }

}
