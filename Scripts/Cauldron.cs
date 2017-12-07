using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cauldron : NetworkBehaviour {

    public GameObject fire;
    public GameObject smoke;
    public GameObject meltedGold;
    public GameObject ingot;
    public GameObject hook;
    public GameObject key;

    [SyncVar]
    public float meltedGoldElevation = 0.64f;
    [SyncVar]
    public bool goldMelted;
    [SyncVar]
    public bool isOnFire;
    [SyncVar]
    public bool ingotPresent;

    void Start () {
        fire.SetActive(false);
        smoke.SetActive(false);
        goldMelted = false;
        isOnFire = false;
        meltedGold.transform.localPosition = new Vector3(0,0.13f,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (isOnFire && !fire.activeInHierarchy)
        {
            setFire();
        }
        if(ingotPresent && !smoke.activeInHierarchy && isOnFire)
        {
            meltingGold();
        }
        if (isOnFire && goldMelted && meltedGold.transform.localPosition.y<meltedGoldElevation && ingotPresent)
        {
            meltedGold.transform.localPosition = new Vector3(0, meltedGold.transform.localPosition.y+0.003f, 0);
            ingot.transform.localScale *= 0.99f;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ingot)
        {
            ingotPresent = true;
        }
        if (other.gameObject == hook && isOnFire && goldMelted)
        {
            key.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ingot)
        {
            ingotPresent = false;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject == hook && isOnFire && goldMelted)
        {
            hook.transform.Find("Key1").gameObject.SetActive(true);
        }
    }

    public void setFire()
    {
        fire.SetActive(true);
        isOnFire = true;
    }

    public void meltingGold()
    {
        goldMelted = true;
        smoke.SetActive(true);
    }
}
