﻿using System.Collections;
using System.Collections.Generic;
using doodle.Manager;
using UnityEngine;

namespace doodle.UI
{
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

        private void OnCollisionEnter2D (Collision2D CollidedObject)
        {
            //if (CollidedObject.relativeVelocity.y <= 0)
            // {
            //     Rigidbody2D rigidBody = CollidedObject.collider.GetComponent<Rigidbody2D>();
            //     Vector2 velocity = rigidBody.velocity;
            //     velocity.y = 20;
            //     rigidBody.velocity = velocity;

            //     GameManager.Instance.SetPlayerCurrentScore (100);
            // }        

            GameManager.Instance.GameOver();
        } 
    }
}