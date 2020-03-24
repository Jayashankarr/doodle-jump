using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    public float movementSpeed = 6f;

	Rigidbody2D rb;

	float movement = 0f;

	private Camera camera = null;

	void Start () 
    {
		rb = GetComponent<Rigidbody2D>();
		camera = Camera.main;
	}
	
	void Update () 
    {
		movement = Input.GetAxis("Horizontal") * movementSpeed;
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;

		if (GameManager.Instance.GameController().
				CheckIfDoodleIsBelowgameView (transform.position))
		{
			GameManager.Instance.GameOver ();
		}
		
		doodleWarp ();
	}

	private void doodleWarp ()
	{
		Vector3 topLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        float Offset = 0.5f;

        if (transform.position.x > -topLeft.x + Offset)
		{
			transform.position = new Vector3(topLeft.x - Offset, transform.position.y, transform.position.z);
		}
        else if(transform.position.x < topLeft.x - Offset)
		{
            transform.position = new Vector3(-topLeft.x + Offset, transform.position.y, transform.position.z);
		}
	}
}
