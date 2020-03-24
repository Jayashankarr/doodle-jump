using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Coroutine movementCoroutine = null;

    private EnemyType type = EnemyType.MOVING;

    public EnemyType Type
    {
        get {return type;}
        set {type = value;}
    }

    float offset = 0.5f;

    private void OnCollisionEnter2D (Collision2D CollidedObject)
    {

    }
    
    void Update()
    {
        if (GameController.Instance.CurrentState == GameState.GAME_OVER)
        {
            gameObject.SetActive (false);
            Destroy(gameObject);
        }
        if (type == EnemyType.MOVING &&movementCoroutine == null)
        {
            movementCoroutine = StartCoroutine (enemyMovement());
        }
        else
        {

        }
    }

    private IEnumerator enemyMovement ()
    {

        if (transform.position.x < (-2))
        {
            offset = 0.25f;
        }
        else if(transform.position.x > (2))
        {
            offset = -0.25f;
        }

        transform.position = new Vector3 (transform.position.x + offset, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.1f); 

        movementCoroutine = null;
    }

}
