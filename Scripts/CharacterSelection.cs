using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public GameObject networkManager;
    public GameObject spawnPoint;
    public CharacterInfo[] characters;
    public InputField myName;
    public RectTransform characterSelectUI;

    private CharacterInfo currentCharacter;
    private int currentIndex;

	void Start ()
    {
        currentCharacter = null;
        currentIndex = 0;
        setCharacter();
	}

    void setCharacter()
    {
        if(currentCharacter != null)
        {
            Destroy(currentCharacter.gameObject);
        }
        currentCharacter = Instantiate(characters[currentIndex], spawnPoint.transform.position, spawnPoint.transform.rotation);
        networkManager.GetComponent<NetworkController>().playerPrefab = networkManager.GetComponent<NetworkController>().spawnPrefabs[currentIndex];
        networkManager.GetComponent<NetworkController>().curPlayer = currentIndex;
    }

    public void setNextCharacter()
    {
        int nbCharacters = characters.Length;
        if (currentIndex == nbCharacters-1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex ++;
        }
        setCharacter();
    }

    public void setPreviousCharacter()
    {
        int nbCharacters = characters.Length;
        if (currentIndex == 0)
        {
            currentIndex = nbCharacters-1;
        }
        else
        {
            currentIndex--;
        }
        setCharacter();
    }

    public void setName()
    {
        currentCharacter.myName = myName.text;
        networkManager.GetComponent<NetworkController>().playerName = myName.text;
    }

    public void startGame()
    {
        characterSelectUI.GetComponent<Canvas>().enabled = false;
        setName();
        networkManager.GetComponent<NetworkManagerHUD>().enabled = true;
    }
}
