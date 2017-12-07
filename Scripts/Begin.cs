using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(sceneName: "characterSelection");        
    }
}
