using System;
using UnityEngine;
using UnityEngine.Networking;

public class Animations : NetworkBehaviour
{
    public void AnimMove(Animator anim, float xTranslation, float zTranslation, bool speed)
    {
        if (speed)
        {
            anim.speed = 2;
        }
        else
        {
            anim.speed = 1;
        }
        anim.SetFloat("Forward", zTranslation);
        anim.SetFloat("Strafe", xTranslation);
        if (xTranslation == 0 && zTranslation == 0)
        {
            anim.SetFloat("Idle", 0);
        }

        if (xTranslation > 0)
        {
            anim.SetFloat("Walk", 0);
        }
    }
}
