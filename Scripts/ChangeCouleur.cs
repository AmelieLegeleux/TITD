using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCouleur : MonoBehaviour {

    private Shader SaveMat;
    // Use this for initialization
    void Start()
    {
    }

    public void changeCouleur(int valeur)
    {
        GetComponent<Glowing>().activateGlowing((valeur == 2));
    }
}
