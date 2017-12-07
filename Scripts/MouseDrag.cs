using System;
using UnityEngine;
using UnityEngine.Networking;

public class MouseDrag : NetworkBehaviour {

    [SyncVar]
    public bool gravityUsed = true;
    [SyncVar]
    public bool kinemativity;
    private GameObject hand;
    public bool inclinAble;
    public string uniqueID = Guid.NewGuid().ToString();

    public int constraintNumber;

    private void Start()
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }

    public void DragSourie(NetworkInstanceId playerHand){
        if(playerHand!= NetworkInstanceId.Invalid)
        {
            hand = ClientScene.FindLocalObject(playerHand).transform.Find(name: "Head").Find("Hand").gameObject;
        }
        else
        {
            hand = null;
        }
        gravityUsed = (hand == null);
        kinemativity = !(hand == null);
    }

    void Update()
    {
        base.gameObject.GetComponent<Rigidbody>().useGravity = gravityUsed;
        base.gameObject.GetComponent<Rigidbody>().isKinematic = kinemativity;
        base.gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)constraintNumber;
        if (hand != null)
        {
            Transform t = hand.transform;
            gameObject.transform.position = new Vector3(t.position.x, t.position.y, t.position.z);
            gameObject.transform.rotation = Quaternion.Euler(t.eulerAngles.x * (inclinAble ? t.rotation.x > 0 ? 1 : 1 : 1), t.parent.eulerAngles.y, t.parent.eulerAngles.z);
        }
    }
}
