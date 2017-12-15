/*using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPosition : MonoBehaviour
{

    private Transform myTransform;
//    [SerializeField] float lerpRate = 5;
//    [SyncVar] private Vector3 syncPos;
    //    private NetworkIdentity theNetID;
    public GameObject objectToMove;

    private Vector3 lastPos;
    private float threshold = 0.5f;

    /*
    void Start()
    {
        myTransform = objectToMove.GetComponent<Transform>();
        syncPos = objectToMove.GetComponent<Transform>().position;
    }


    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!hasAuthority)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void Cmd_ProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
//            gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToClient);
            if (Vector3.Distance(myTransform.position, lastPos) > threshold)
            {
                Cmd_ProvidePositionToServer(myTransform.position);
                lastPos = myTransform.position;
            }
        }
    }
}
    */
