using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyType type = EnemyType.MOVING;

    public EnemyType Type
    {
        get {return type;}
        set {type = value;}
    }

    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {

    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver ())
        {
            gameObject.SetActive (false);
            Destroy(gameObject);
        }
        if (type == EnemyType.MOVING)
        {
            doEnemyMovement ();
        }
    }

    private void doEnemyMovement ()
    {
        float offset = 0f;
        if (transform.position.x < (-2))
        {
            offset = Time.deltaTime;
        }
        else if(transform.position.x > (2))
        {
            offset = -Time.deltaTime;
        }
        transform.position = new Vector3 (transform.position.x + offset, transform.position.y, transform.position.z);
    }

}
