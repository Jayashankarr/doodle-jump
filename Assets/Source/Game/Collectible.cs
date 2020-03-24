using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectibleType type = CollectibleType.SPRING;

    public CollectibleType Type
    {
        set {type = value;}
        get {return type;}
    }
    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {
        if (CollidedObject.relativeVelocity.y <= 0)
        {
            Rigidbody2D rigidBody = CollidedObject.collider.GetComponent<Rigidbody2D>();
            Vector2 velocity = rigidBody.velocity;
            velocity.y = 20;
            rigidBody.velocity = velocity;

            GameManager.Instance.SetPlayerCurrentScore (100);
        }        
    } 
}
