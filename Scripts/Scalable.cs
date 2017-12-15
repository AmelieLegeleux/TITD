using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scalable : NetworkBehaviour {

    [SyncVar]
    private float ropeSize;

    public GameObject rope;
    public GameObject hook;
    private float oldSize;
    private Transform startHookPosition;
    // Use this for initialization
    void Start () {
        oldSize =1;
        startHookPosition = hook.transform;
    }

    public void updateSize(float value)
    {
        ropeSize = value;
    }

	// Update is called once per frame
	void Update () {
        if (ropeSize != oldSize)
        {
            rope.transform.localScale = new Vector3(1, ropeSize, 1);
            rope.transform.localPosition = new Vector3(0, -ropeSize+1,0);
            //7.259269f
            hook.transform.localPosition = new Vector3(startHookPosition.localPosition.x, (rope.transform.localPosition.y*2)-2 , startHookPosition.localPosition.z );
            oldSize = ropeSize;
        }
	}
}
