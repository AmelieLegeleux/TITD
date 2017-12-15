using UnityEngine;
using UnityEngine.Networking;

public class DisplayNetwork : NetworkBehaviour
{
    public void Activate(string ObjectName, bool active)
    {
        string acti = active.ToString();
        GameObject obj = GameObject.Find(ObjectName).gameObject;
        if (obj != null)
        {
            GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>().add(obj, ToDo.activate, acti);
        }
    }
}

