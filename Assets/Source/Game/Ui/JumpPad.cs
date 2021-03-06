﻿using System.Collections;
using System.Collections.Generic;
using doodle.Manager;
using UnityEngine;

namespace doodle.UI
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        private GameObject spring = null;

        [SerializeField]
        private GameObject rocket = null;

        [SerializeField]
        private float greenPadJumpVelocity = 10f;

        [SerializeField]
        private float bluePadJumpVelocity = 10f;

        [SerializeField]
        private float brownPadJumpVelocity = 2f;

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
                    changeVelocityForObject (rigidBody, brownPadJumpVelocity);
                    GameObject.Destroy (gameObject);
                    break;

                    case JumpPadType.Blue:
                    changeVelocityForObject (rigidBody, bluePadJumpVelocity);
                    GameManager.Instance.SetPlayerCurrentScore (35* jumpPadIndex);
                    GameObject.Destroy (gameObject);
                    break;

                    default:
                    changeVelocityForObject (rigidBody, greenPadJumpVelocity);
                    GameManager.Instance.SetPlayerCurrentScore (25 * jumpPadIndex);
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
            if (GameManager.Instance.GameController().
                    CheckIfPadIsReadyToDestroy (transform.position) || 
                        GameManager.Instance.IsGameOver())
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
}
