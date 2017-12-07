using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkController : NetworkManager
{

    public int curPlayer;
    public string playerName;

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        IntegerMessage msg = new IntegerMessage(curPlayer);
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(conn, 0, msg);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        int idPlayer = 0;
        if (extraMessageReader != null)
        {
            IntegerMessage stream = extraMessageReader.ReadMessage<IntegerMessage>();
            idPlayer = stream.value;
        }
        GameObject playerPrefab = spawnPrefabs[idPlayer];

        GameObject player = Instantiate(playerPrefab, GetStartPosition().position, GetStartPosition().rotation) as GameObject;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
