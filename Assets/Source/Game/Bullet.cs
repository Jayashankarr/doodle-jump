using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 target;

    [SerializeField]
    private Vector2 startPosition;

    private float moveSpeed = 7f;

    private Vector2 moveDirection = Vector2.zero;
    void Start()
    {
        target = GameManager.Instance.GameController().DoodlePosition ();
        setBulletTrajectory ();
    }

    private void setBulletTrajectory ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D> ();
        moveDirection = (target - transform.position).normalized * moveSpeed;
        moveDirection.y = Mathf.Clamp (moveDirection.y, -2, 2);
        rb.velocity= new Vector2 (moveDirection.x, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBulletOutOfViewPort ())
        {
            gameObject.SetActive (false);
            Destroy(gameObject);
            setBulletTrajectory ();
        }
    }

    private bool isBulletOutOfViewPort ()
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint (transform.position);

        if (viewPortPos.y < 0 || viewPortPos.x < 0)
        {
            return true;
        }
        return false;
    }
}
