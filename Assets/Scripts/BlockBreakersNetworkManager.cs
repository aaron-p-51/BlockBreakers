using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BlockBreakersNetworkManager : NetworkManager
{
    [SerializeField]
    private Transform _player1Start = null;

    [SerializeField]
    private Transform _player2Start = null;

    private GameObject _ball = null;

    [SerializeField]
    private BallKillZone _ballKillZone = null;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startTransform;

        if (numPlayers == 0)
            startTransform = _player1Start;
        else
            startTransform = _player2Start;

        GameObject player = Instantiate(playerPrefab, startTransform.position, startTransform.rotation);

        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 1)
        {
            _ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            NetworkServer.Spawn(_ball);
        }
    }

    public override void OnStartServer()
    {
        BallKillZone.OnBallEnteredKillZone += BallKillZone_OnBallEnteredKillZone;
    }

    private void BallKillZone_OnBallEnteredKillZone(GameObject obj)
    {
        Debug.Log("Server Ball enterned kill zone");
        
        
    }
}
