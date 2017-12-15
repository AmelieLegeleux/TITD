using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    public Vector3 maVelocite;
	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(0, 0, -50);
    }
    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 50);
    }

    // Update is called once per frame
    void Update () {
        maVelocite=gameObject.GetComponent<Rigidbody>().velocity;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
    }
}
