using UnityEngine;
using UnityEngine.Networking;

public class CasseCaisse : NetworkBehaviour

{
    public GameObject floor;
    public GameObject planks;
    public GameObject scroll;
    public GameObject dust;
    public int playerInteract;

    private void Start()
    {
        floor = GameObject.Find("Decors").gameObject.transform.Find("Plane").gameObject;
        playerInteract = 0;
        activate("false");
        slice();
    }

    private void activate(string boolean)
    {
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(dust, ToDo.activate, boolean);
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(planks, ToDo.activate, boolean);
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(scroll, ToDo.activate, boolean);

    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject == floor)
        {

            dust.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            planks.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            scroll.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            activate("true");
            GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(gameObject, ToDo.destroy, "true");
        }
    }

    public void incrPlayer()
    {
        playerInteract++;
    }

    public void decrPlayer()
    {
        playerInteract--;
    }

    public void slice()
    {
        if (playerInteract == 1)
        {
            GetComponent<MouseDrag>().kinemativity = false;
        }
        else
        {
            GetComponent<MouseDrag>().kinemativity = true;
        }
    }
}

