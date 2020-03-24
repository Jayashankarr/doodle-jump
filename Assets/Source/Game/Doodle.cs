using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    public float movementSpeed = 6f;

	Rigidbody2D rb;

	float movement = 0f;

	void Start () 
    {
		rb = GetComponent<Rigidbody2D>();
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
	}
}
