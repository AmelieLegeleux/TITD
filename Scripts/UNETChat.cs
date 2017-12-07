using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class UNETChat : Chat
{
    //just a random number
    private short canal=0;
    public float delay=10;

    private float timeReference;
    private Color colorStart;
    private Color colorEnd;

    private void Start()
    {
        if (canal == 0)
        {
            canal = 40;// GameObject.Find("GameSystem").GetComponent<Jeu>().getUniqueNumber();
        }

        //if the client is also the server
        if (NetworkServer.active)
        {
            //registering the server handler
            NetworkServer.RegisterHandler(canal, ServerReceiveMessage);
        }

        //registering the client handler
        if (NetworkManager.singleton != null)
        {
            NetworkManager.singleton.client.RegisterHandler(canal, ReceiveMessage);
        }
        /*
        colorStart = textZone.GetComponent<Renderer>().material.color;
        colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
        timeReference = Time.time;
        */
    }

    private void Update()
    {
        if(timeReference+delay < Time.time)
        {
            fadeOut(false);
        }
    }




    private void ReceiveMessage(NetworkMessage message)
    {
        //reading message
        string text = message.ReadMessage<StringMessage>().value;

        AddMessage(text);
        fadeOut(true);
        timeReference = Time.time;
    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage myMessage = new StringMessage();
        //we are using the connectionId as player name only to exemplify
//        myMessage.value = message.conn.connectionId + ": " + message.ReadMessage<StringMessage>().value;
        myMessage.value = message.ReadMessage<StringMessage>().value;
        //sending to all connected clients
        NetworkServer.SendToAll(canal, myMessage);
    }

    public override void SendMessage(UnityEngine.UI.InputField input)
    {
        StringMessage myMessage = new StringMessage();
        //getting the value of the input

        myMessage.value = GameObject.Find("Network Manager").GetComponent<NetworkController>().playerName;
        myMessage.value += " : "+ input.text;

        //sending to server
        NetworkManager.singleton.client.Send(canal, myMessage);
    }
}