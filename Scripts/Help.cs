using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{

    private UIComponents canvas;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "online")
        {
            canvas = GameObject.Find("UI").gameObject.GetComponent<UIComponents>();
        }
    }

    public void DisplayHelp()
    {
        canvas.Activate(4, true);
        canvas.Activate(0, false);
        canvas.Activate(1, false);
        canvas.Activate(2, false);
        canvas.Activate(3, false);
    }

    public void CloseHelp()
    {
        canvas.Activate(4, false);
        canvas.Activate(0, true);
        canvas.Activate(1, true);
        canvas.Activate(2, true);
        canvas.Activate(3, true);
    }


}
