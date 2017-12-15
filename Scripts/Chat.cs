
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private Text container;
    [SerializeField]
    private ScrollRect rect;

    public GameObject textZone;

    internal void AddMessage(string chatText)
    {
        container.text += "\n" +chatText;
        //slide le texte pour toujours voir le dernier message
        Debug.Log(container.text);
        Invoke("ScrollDown", .1f);
        container.gameObject.SetActive(true);
    }

    public string chatList = "";

    internal void fadeOut(bool activate)
    {
        /*
        float t;
        float duration = 2;
        for (t = 0.0f; t < duration; t += Time.deltaTime)
        {
            textZone.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, t / duration);
//            Yield;
        }
        */
        textZone.SetActive(activate);
        GetComponent<Image>().enabled = activate;
    }

    private void ScrollDown()
    {
        if (rect != null)
            rect.verticalScrollbar.value = 0;
    }

    public virtual void SendMessage(InputField input){}
}

