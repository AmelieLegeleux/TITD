using UnityEngine;
using UnityEngine.Networking;

public class ReadAble : NetworkBehaviour
{

    private bool active;

    private void Start()
    {
        active = false;
    }

    public void Activate(GameObject toRead)
    {
        //toRead.transform.SetParent(head.transform);
        //toRead.transform.Translate (0, 3, 0);
        toRead.SetActive(active);
        GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(toRead, ToDo.activate, active.ToString());
        active = !active;
    }
}
