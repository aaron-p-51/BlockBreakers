using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BallKillZone : NetworkBehaviour
{
    //public delegate void BallEnteredKillZone();
    //public static event BallEnteredKillZone OnBallEnteredKillZone;

    public static event Action<GameObject> OnBallEnteredKillZone;

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BallMovement>())
        {
            Debug.Log("Ball entered killzone");

            // alert ball entered killzone
            OnBallEnteredKillZone?.Invoke(gameObject);

            NetworkServer.Destroy(collision.gameObject);
        }
    }
}
