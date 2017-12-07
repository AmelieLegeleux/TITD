using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RollAble : NetworkBehaviour {

    [SyncVar]
    public bool isRolling;
    [SyncVar]
    public float rollValue;

    public GameObject hook;

    public Transform center;
    private float maxRopeSize;
    private float minRopeSize;
    public float ropeSize;
    private GameObject hand;
    private float deltaZ;
    private float ConvertionRate;
    private float oldValue;
    public Transform handle;
    Quaternion lastRotation;
    public float speed;
    private Vector3 angularVelocity;

    // Use this for initialization
    void Start () {
        maxRopeSize = 1000;
        minRopeSize = 1;
        ropeSize = 1;
        deltaZ = 180;
        ConvertionRate = Mathf.Rad2Deg;
	}

    public void Roll(NetworkInstanceId playerHand){
        if (playerHand != NetworkInstanceId.Invalid)
        {
            hand = ClientScene.FindLocalObject(playerHand).transform.Find(name: "Head").Find("Hand").gameObject;
        }
        else
        {
            hand = null;
        }
        isRolling = (playerHand != NetworkInstanceId.Invalid);
        if (hand != null)
        {

        }
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (isRolling)
        {
            float angleZ = calculAngle(center.position, hand.transform.position);
            float step = 0.03f;
            if (ropeSize <= maxRopeSize && rollValue >= 0) {
                ropeSize += step;
            }
             if (ropeSize >= minRopeSize && rollValue <= 0)
            {
                ropeSize -=step;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angleZ);
        }
        else if (ropeSize > minRopeSize)
        {
            ropeSize -= 0.1f;
            transform.Rotate(new Vector3(0, 0, -10));
        }

        hook.GetComponent<Scalable>().updateSize(ropeSize);

        var deltaRot = transform.rotation * Quaternion.Inverse(lastRotation);
        var eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));
        lastRotation = transform.rotation;
        angularVelocity = eulerRot / Time.fixedDeltaTime;
        rollValue = Mathf.Round(angularVelocity.magnitude/10)*(angularVelocity.z >0 ? 1: -1);
    }


    private float calculAngle(Vector3 firtsPoint, Vector3 secondPoint)
    {
        return deltaZ + Mathf.Atan2(secondPoint.y - firtsPoint.y, secondPoint.x - firtsPoint.x) * ConvertionRate;
    }
}
