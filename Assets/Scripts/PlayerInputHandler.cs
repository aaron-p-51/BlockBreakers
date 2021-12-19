using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerInputHandler : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 30f;

    [SerializeField]
    private Rigidbody2D _rigidBody2D;

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            _rigidBody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f) * _speed * Time.fixedDeltaTime;
        }
    }


}
