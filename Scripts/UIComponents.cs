using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponents : MonoBehaviour
{
    public GameObject[] images;

    public void Activate(int index, bool active)
    {
        images[index].gameObject.SetActive(active);
    }

}