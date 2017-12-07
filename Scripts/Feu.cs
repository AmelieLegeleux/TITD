using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feu : MonoBehaviour {

    void Update () {
        // pour que la flame pointe vers le ciel quelle que soit la position de l'objet enflâmé
        gameObject.transform.LookAt(new Vector3(-35.2f,1000f,84.8f));
    }
}
