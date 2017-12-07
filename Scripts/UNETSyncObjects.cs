using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class prefabs
{
    public const int Candle = 0;
    public const int Chest = 1;
    public const int Ingot = 2;
    public const int Mural_Torch = 3;
    public const int Key_Hole = 4;
    public const int Torch = 5;
    public const int Torch_Holder = 6;
    public const int Crank = 7;
    public const int Hook = 8;
}

public static class ToDo
{
    public const int setFire = 0;
    public const int interract = 1;
    public const int move = 2;
    public const int slice = 3;
    public const int destroy = 4;
    public const int roll = 5;
    public const int activate = 6;
    /*
    public const int Mural_Torch = 3;
    public const int Key_Hole = 4;
    public const int Torch = 5;
    public const int Torch_Holder = 6;
    */
}

public class Modifications
{
    private GameObject objectToModify;
    private int toDo;
    private string value;
    private NetworkInstanceId hand;

    public Modifications(GameObject objectToModify, NetworkInstanceId hand, int actionToDo)
    {
        this.ObjectToModify = objectToModify;
        this.ToDo = actionToDo;
        this.Hand = hand;
    }

    public Modifications(GameObject objectToModify, int actionToDo, string value)
    {
        this.ObjectToModify = objectToModify;
        this.ToDo = actionToDo;
        this.value = value;
    }

    public Modifications(GameObject objectToModify, int actionToDo)
    {
        this.ObjectToModify = objectToModify;
        this.ToDo = actionToDo;
    }

    public GameObject ObjectToModify
    {
        get{return objectToModify;}
        set{objectToModify = value;}
    }

    public int ToDo{
        get{return toDo;}
        set{toDo = value;}
    }

    public string Value{
        get{return value;}
        set{this.value = value;}
    }

    public NetworkInstanceId Hand{
        get{return hand;}
        set{hand = value;}
    }
}

public class UNETSyncObjects : NetworkBehaviour {

    public GameObject localPlayer;
    public List<GameObject> candlesToTurnOn = new List<GameObject>();
    public List<Modifications> ObjectsToModify = new List<Modifications>();
    public GameObject[] PrefabList;
    public bool worldOpened = false;

    public void add(GameObject objectToModify, int actionToDo, string value)
    {
        ObjectsToModify.Add(new Modifications(objectToModify, actionToDo, value));
    }
    public void add(GameObject objectToModify, NetworkInstanceId hand, int actionToDo)
    {
        ObjectsToModify.Add(new Modifications(objectToModify, hand, actionToDo));
    }
    public void add(GameObject objectToModify, int actionToDo)
    {
        ObjectsToModify.Add(new Modifications(objectToModify, actionToDo));
    }
}
