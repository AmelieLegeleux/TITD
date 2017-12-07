using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Jeu : NetworkBehaviour
{

    public List<short> uniqueCanal = new List<short>();
    public int connextionCount;
    public GameObject PoleTorchPrefab;
    public GameObject WallTorchPrefab;
    public GameObject TorchPrefab;
    public GameObject IngotPrefab;
    public GameObject CandlePrefab;
    public GameObject Crank;
    public GameObject Hook;

    private List<GameObject> candlesToTurnOn;

    public short getUniqueNumber()
    {
        short uniqueNumber = 150;
        while (uniqueCanal.Contains(uniqueNumber))
        {
            uniqueNumber++;
        }
        uniqueCanal.Add(uniqueNumber);
        return uniqueNumber;
    }

    public override void OnStartServer()
    {
        instanciation();
    }

    void Start()
    {
    }

    private void activation(bool activate)
    {
        NetworkManager.singleton.playerPrefab.GetComponent<PlayerController>().enabled = activate;
        NetworkManager.singleton.playerPrefab.GetComponent<Health>().enabled = activate;
        NetworkManager.singleton.playerPrefab.GetComponent<Interraction>().enabled = activate;
        NetworkManager.singleton.playerPrefab.GetComponent<Collision>().enabled = activate;
        NetworkManager.singleton.playerPrefab.GetComponent<UNETObjectHandler>().enabled = activate;
    }

    private void instanciation()
    {
        candlesToTurnOn = new List<GameObject>();

        SpawnObject(PoleTorchPrefab, -75.132f, 3.65f, 84.943f, 0f, 0f, 0f);
        SpawnObject(PoleTorchPrefab, -75.132f, 3.65f, 103.032f, 0f, 0f, 0f);
        SpawnObject(PoleTorchPrefab, -41.962f, 3.65f, 63.854f, 0f, -90f, 0f);
        SpawnObject(PoleTorchPrefab, -8.968f, 3.65f, 96.968f, 0f, 180f, 0f);
        SpawnObject(PoleTorchPrefab, -8.968f, 3.65f, 84.99f, 0f, 180f, 0f);
        SpawnObject(PoleTorchPrefab, -39.188f, 3.65f, 86.812f, 0f, 90f, 0f);
        SpawnObject(PoleTorchPrefab, -51.872f, 3.65f, 63.868f, 0f, -90f, 0f);

        SpawnObject(TorchPrefab, -64.684f, 1.159f, 106.158f, 0f, 65.9f, 90f);

        SpawnObject(CandlePrefab, -20.77344f, 1.085168f, 99.18661f, 0f, 0f, 0f, false);
        SpawnObject(CandlePrefab, -19.59971f, 0.7336634f, 105.8812f, 0f, 0f, 0f, true);
        SpawnObject(CandlePrefab, -67.95668f, 1.263587f, 71.69641f, 0f, 0f, 0f, false);
        SpawnObject(CandlePrefab, -12.21261f, 1.29871f, 70.35531f, 0f, 0f, 0f, false);
        SpawnObject(CandlePrefab, -64.39392f, 1.085168f, 107.7573f, 0f, 0f, 0f, true);

        SpawnObject(WallTorchPrefab, -20.12f, 0.146f, 104.555f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -20.15396f, 0.146f, 104.656f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -20.023f, 0.146f, 104.692f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -19.899f, 0.146f, 104.661f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -19.899f, 0.146f, 104.507f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -20.034f, 0.146f, 104.507f, 0f, 0f, 0f, false);
        SpawnObject(WallTorchPrefab, -20.173f, 0.146f, 104.507f, 0f, 0f, 0f, false);

        /*
        SpawnObject(Crank, -54.857f, 1.451504f, 84.183f, 0f, -40f, 0f, false);
        SpawnObject(Hook, -50.395f, 5.115001f, 87.0286f, 0f, 0f, 0f, false);
        */



    }

    private void SpawnObject(GameObject prefab, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
    {
        Vector3 position = new Vector3(posX, posY, posZ);
        Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);
        GameObject obj = Instantiate(prefab, position, rotation);
        NetworkServer.Spawn(obj);
    }

    private void SpawnObject(GameObject prefab, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, bool isOnFire)
    {
        Vector3 position = new Vector3(posX, posY, posZ);
        Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);
        GameObject obj = Instantiate(prefab, position, rotation);
        NetworkServer.Spawn(obj);
        if (isOnFire)
        {
            candlesToTurnOn.Add(obj);
        }
    }
    private void Update()
    {
        UNETSyncObjects modifications = GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>();
        if (!NetworkManager.singleton.playerPrefab.GetComponent<PlayerController>().enabled)
        {
            activation(SceneManager.GetActiveScene().name == "online");
        }


        if (candlesToTurnOn != null)
        {
            foreach (GameObject g in candlesToTurnOn.ToArray())
            {
                if (!g.GetComponent<InflamAble>().OnFire)
                {
                    modifications.add(g, ToDo.setFire, "true");
//                    candlesToTurnOn.Remove(g);
                }
            }
        }
    }
}
