using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    
    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {
        Rigidbody2D rigidBody = CollidedObject.collider.GetComponent<Rigidbody2D>();

        Vector2 velocity = rigidBody.velocity;
        velocity.y = 10f;
        rigidBody.velocity = velocity;

    }
}
