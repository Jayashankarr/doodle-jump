using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {
        if (CollidedObject.relativeVelocity.y <= 0)
        {
            Rigidbody2D rigidBody = CollidedObject.collider.GetComponent<Rigidbody2D>();
            Vector2 velocity = rigidBody.velocity;
            velocity.y = 20;
            rigidBody.velocity = velocity;

            GameController.Instance.SaveDoodleScore (100);
        }        
    } 
}
