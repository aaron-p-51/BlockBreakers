using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BallMovement : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 30f;

    [SerializeField]
    private Rigidbody2D _rigidBody2D = null;


    public override void OnStartServer()
    {
        base.OnStartServer();

        _rigidBody2D.simulated = true;

        _rigidBody2D.velocity = Vector2.up * _speed;
    }

    private static float HitFactor(float ballXPos, float playerXPos, float playerWidth)
    {
        // Taken from Mirror Pong demo

        //
        //
        //      =======================
        //      -1         0          1
        //      left     middle     right
        return (ballXPos - playerXPos) / playerWidth;
    }


    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit the player
        if (collision.transform.GetComponent<PlayerInputHandler>())
        {
            float x = HitFactor(transform.position.x, collision.transform.position.x, collision.collider.bounds.size.x);
            float y = collision.relativeVelocity.y > 0 ? 1 : 0;

            Vector2 dir = new Vector2(x, y).normalized;

            _rigidBody2D.velocity = dir * _speed;
        }
    }
}
