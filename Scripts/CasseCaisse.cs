using UnityEngine;
using UnityEngine.Networking;

public class CasseCaisse : NetworkBehaviour

{
    public GameObject floor;
    public GameObject planks;
    public GameObject scroll;
    public GameObject dust;

    private void Start()
    {
        activate(false);
    }

    private void activate(bool active)
    {
        string boolean = (active ? "true" : "false");
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(dust.GetComponent<NetworkIdentity>().netId, ToDo.activate, boolean);
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(planks, ToDo.activate, boolean);
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(scroll, ToDo.activate, boolean);
        /*dust.SetActive(active);
        planks.SetActive(active);
        scroll.SetActive(active);*/

    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject == floor)
        {

            dust.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            planks.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            scroll.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
            activate(true);
            GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(gameObject, ToDo.destroy, "true");
        }
    }
}

