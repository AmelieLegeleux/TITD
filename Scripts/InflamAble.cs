using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InflamAble : NetworkBehaviour {

    public GameObject feu;

    [SyncVar]
    private bool onFire = false;
    public float hauteurFlame;

    public bool OnFire{
        get{return onFire;}
        set{onFire = value;}
    }


    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.collider is SphereCollider)
        {
            Interractable itOther = collision.collider.GetComponentInParent<Interractable>();
            if (itOther != null)
            {
                if (itOther.isFire)
                {
                    Interractable it = gameObject.GetComponent<Interractable>();
                    if (it != null)
                    {
                        if (it.inflamAble)
                        {
                            if (collision.gameObject.GetComponent<InflamAble>().OnFire)
                            {
                                if (!OnFire)
                                {
                                    GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(gameObject, ToDo.setFire, "true");
                                    //OnFire = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void Update () {
        if (OnFire != feu.activeInHierarchy)
        {
            feu.SetActive(true);
        }
    }
}
