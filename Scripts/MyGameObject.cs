
using UnityEngine;

public class MyGameObject {
    private Vector3 pos;
    private Quaternion rot;

    public MyGameObject(Vector3 pos, Quaternion rot) {
        this.rot = rot;
        this.pos = pos;
    }

    public Vector3 Pos
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
        }
    }

    public Quaternion Rot
    {
        get
        {
            return rot;
        }

        set
        {
            rot = value;
        }
    }
}
