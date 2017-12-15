using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Interraction : NetworkBehaviour
{

//    private float tailleRayon = 2.5f;
    private UNETSyncObjects modifications;

    [SyncVar]
    public bool inInterraction;
    public GameObject viseur;
    public GameObject ObjstartPosition;
    public GameObject objetTouche;
    private GameObject heavyBox;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("LocalGameSystem") != null)
        {
            modifications = GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>();
        }
        heavyBox = GameObject.Find("BoxToBreak");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (!inInterraction)
            {
                /*
                if (objetTouche != null)
                {
                    if (!objetTouche.GetComponent<Interractable>().interract)
                    {
                        free();
                    }
                }
                Vector3 startPosition = ObjstartPosition.transform.position;
                Vector3 direction = viseur.transform.TransformDirection(Vector3.forward) * tailleRayon;
                Debug.DrawRay(startPosition, direction, Color.green);

                RaycastHit hit;
                if (Physics.Raycast(startPosition, direction, out hit))
                {
                    if (hit.transform.tag == "Interractable" && hit.distance < tailleRayon)
                    {
                        if (!hit.transform.GetComponent<Interractable>().interract)
                        {
                            afficheNom(hit.transform.GetComponent<Interractable>().displayName, 2);
                            objetTouche = hit.transform.gameObject;
                        }
                    }
                }
                */
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (objetTouche != null)
                {
                    if (objetTouche.GetComponent<Interractable>().pickable && objetTouche == heavyBox)
                    {
                        GetComponent<PlayerController>().anim.SetFloat("Idle", 3);
                        GetComponent<PlayerController>().anim.SetFloat("Walk", 2);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objetTouche != null)
                {
                    Interractable inter = objetTouche.GetComponent<Interractable>();
                    NetworkInstanceId myNetID = GetComponent<NetworkIdentity>().netId;
                    if (inter.pickable && objetTouche == heavyBox)
                    {
                        heavyBox.GetComponent<CasseCaisse>().incrPlayer();
                        heavyBox.GetComponent<CasseCaisse>().slice();
                    }

                    if (inter.moveAble && inter.pickable)
                    {
                        GameObject.Find("Obj_transporte").GetComponent<Text>().text = inter.interract ? "" : inter.displayName;
                        if (!inter.interract) afficheNom("", 0);
                        inter.player = inter.interract ? null : gameObject;
                        modifications.add(objetTouche, inter.interract ? NetworkInstanceId.Invalid : myNetID, ToDo.move);
                        inInterraction = !inter.interract;
                    }
                    if (inter.sliceAble)
                    {
                        modifications.add(objetTouche, myNetID, ToDo.slice);
                        afficheNom("", 0);
                        inter.player = gameObject;
                        inInterraction = true;
                        
                    }
                    if (inter.clickAble) { 
                        objetTouche.GetComponent<Action>().click();
                    }

                    if (inter.rollAble)
                    {
                        modifications.add(objetTouche, myNetID, ToDo.roll);
                        afficheNom("", 0);
                        inter.player = gameObject;
                        inInterraction = true;
                    }

                    if (inter.readAble)
                    {
                        objetTouche.GetComponent<ReadAble>().Activate(objetTouche);
                        objetTouche.GetComponent<ReadAble>().Activate(GetComponent<PlayerComponents>().obj[0]);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                GetComponent<PlayerController>().anim.SetFloat("Idle", 0);
                GetComponent<PlayerController>().anim.SetFloat("Walk", 0);
                if (objetTouche != null)
                {
                    Interractable inter = objetTouche.GetComponent<Interractable>();
                    if (objetTouche == heavyBox)
                    {
                        heavyBox.GetComponent<CasseCaisse>().decrPlayer();
                        heavyBox.GetComponent<CasseCaisse>().slice();
                    }
                
                    if (inter.sliceAble)
                    {

                        objetTouche.GetComponent<Interractable>().player = null;
                        modifications.add(objetTouche, NetworkInstanceId.Invalid,ToDo.slice);
                        objetTouche = null;
                        inInterraction = false;
                    }

                    if(inter.rollAble)
                    {
                        objetTouche.GetComponent<Interractable>().player = null;
                        modifications.add(objetTouche, NetworkInstanceId.Invalid, ToDo.roll);
                        objetTouche = null;
                        inInterraction = false;
                    }
                }
            }
        }
    }
    public void free(){
        if (isLocalPlayer)
        {
            afficheNom("", 0);
            GameObject.Find("Obj_transporte").GetComponent<Text>().text = "";
            //objetTouche.GetComponent<ChangeCouleur>().changeCouleur(0);
            inInterraction = false;
            objetTouche = null;
        }
    }
    public void afficheNom(string nom, int typeColor)
    {
        objetTouche.GetComponent<ChangeCouleur>().changeCouleur(typeColor);
        GameObject.Find("Obj_vise").GetComponent<Text>().text = nom;
        GameObject.Find("Obj_vise_fond").GetComponent<Text>().text = nom;

    }
    public void toucheObjet(GameObject obj)
    {
        if (isLocalPlayer)
        {
            objetTouche = obj;
            afficheNom(obj.transform.GetComponent<Interractable>().displayName, 2);
        }
    }
}