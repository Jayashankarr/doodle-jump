using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    public float movementSpeed = 10f;

	Rigidbody2D rb;

	float movement = 0f;

	void Start () 
    {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
		movement = Input.GetAxis("Horizontal") * movementSpeed;
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
}
