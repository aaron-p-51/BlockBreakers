using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameState : NetworkBehaviour
{
    [SerializeField]
    private BlockBreakersNetworkManager blockBreakersNetworkManager = null;

    public override void OnStartServer()
    {
        BallKillZone.OnBallEnteredKillZone += BallKillZone_OnBallEnteredKillZone;
    }

    private void BallKillZone_OnBallEnteredKillZone(GameObject obj)
    {
        SpawnBall();
    }

    [Server]
    public void SpawnBall()
    {
        GameObject ball = Instantiate(blockBreakersNetworkManager.spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        NetworkServer.Spawn(ball);
    }
}
