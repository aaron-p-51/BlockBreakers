using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LevelManager : NetworkBehaviour
{

    [SerializeField]
    private BlockBreakersNetworkManager blockBreakersNetworkManager = null;

    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("LevelManager is null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public override void OnStartServer()
    {
        BallKillZone.OnBallEnteredKillZone += BallKillZone_OnBallEnteredKillZone;
    }

    public override void OnStopServer()
    {
        BallKillZone.OnBallEnteredKillZone -= BallKillZone_OnBallEnteredKillZone;
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
