using UnityEngine;
using UnityEngine.Networking;

public class Glowing : NetworkBehaviour {

    public int index;

    [SyncVar]
    public bool glowing;
    public GameObject owner;

    private float maxGlowIntensity = 0.7f;
    private float minGlowIntensity = 0.3f;
    private float glowingStep = 0.01f;
    private float CurrentglowingStep = 0.01f;
    
    private Material glow;
    private float glowIntensity=0;
    private string myOption;

    void Start () {
        glow = owner.GetComponent<Renderer>().materials[index];
        myOption = "_Glow";
    }

    public void activateGlowing(bool glowing)
    {
        this.glowing = glowing;
        if (glowing)
        {
            glow.SetFloat(myOption, maxGlowIntensity);
        }
        else
        {
            glow.SetFloat(myOption, 0);
        }
    }

    void Update () {
        if (glowing)
        {
            if (glowIntensity > maxGlowIntensity)
            {
                CurrentglowingStep = -glowingStep;
            }
            if (glowIntensity < minGlowIntensity)
            {
                CurrentglowingStep = glowingStep;
            }

            glowIntensity += CurrentglowingStep;
        }
        else { 
            glowIntensity = 0;
        }
        glow.SetFloat(myOption, glowIntensity);
    }
}
