using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private GameObject spring = null;

    [SerializeField]
    private GameObject rocket = null;

    private JumpPadType type = JumpPadType.Green;

    public JumpPadType Type
    {
        set { type = value; }
        get { return type; }
    }

    private PadObjectType collectibleType = PadObjectType.NONE;

    private int jumpPadIndex = 0;

    private void Start ()
    {

    }

    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {
        if (CollidedObject.relativeVelocity.y <= 0)
        {
            Rigidbody2D rigidBody = CollidedObject.collider.GetComponent<Rigidbody2D>();
            switch (type)
            {
                case JumpPadType.Brown:
                changeVelocityForObject (rigidBody, 0f);
                GameObject.Destroy (gameObject);
                break;

                case JumpPadType.Blue:
                changeVelocityForObject (rigidBody, 10f);
                GameObject.Destroy (gameObject);
                break;

                default:
                changeVelocityForObject (rigidBody, 10f);
                break;
            }
        }        
    }

    private void changeVelocityForObject (Rigidbody2D rigidBody, float offset)
    {
        Vector2 velocity = rigidBody.velocity;
        velocity.y = offset;
        rigidBody.velocity = velocity;
    }

    public void SetRocket (bool value)
    {
        rocket.SetActive (value);
    }

    public void SetSpring (bool value)
    {
        spring.SetActive (value);
    }
}
