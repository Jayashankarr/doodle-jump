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

    public int JumpPadIndex
    {
        set {jumpPadIndex = value;}
        get {return jumpPadIndex;}
    }

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
                GameController.Instance.SaveDoodleScore (35* jumpPadIndex);
                GameObject.Destroy (gameObject);
                break;

                default:
                changeVelocityForObject (rigidBody, 10f);
                GameController.Instance.SaveDoodleScore (25 * jumpPadIndex);
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

    private void Update ()
    {
        if (GameController.Instance.CheckIfPadIsReadyToDestroy (transform.position))
        {
            gameObject.SetActive (false);
            Destroy(gameObject);
        }
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}
