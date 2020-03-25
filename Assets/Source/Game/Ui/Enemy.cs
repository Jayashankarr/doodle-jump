using System.Collections;
using System.Collections.Generic;
using doodle.Manager;
using UnityEngine;

namespace doodle.UI
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private GameObject bullet;
        private EnemyType type = EnemyType.MOVING;

        private float moveOffset = 0.015f;

        private Coroutine shootCoroutine = null;

        public EnemyType Type
        {
            get {return type;}
            set {type = value;}
        }

        private void OnCollisionEnter2D (Collision2D CollidedObject)
        {
            GameManager.Instance.GameOver ();
            gameObject.SetActive (false);
            Destroy(gameObject);
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

            if (type == EnemyType.SHOOTING)
            {
                if (shootCoroutine == null )
                shootCoroutine = StartCoroutine (shoot());
            }
        }

        private IEnumerator shoot ()
        {
            Instantiate (bullet, transform.position, Quaternion.identity, transform);

            yield return new WaitForSeconds (2f);

            shootCoroutine = null;
        }

        private void doEnemyMovement ()
        {
            if (transform.position.x < -2f)
            {
                moveOffset *= -1;
            }
            else if(transform.position.x > 2f)
            {
                moveOffset *= -1;
            }
            transform.position = new Vector3 (transform.position.x + moveOffset, transform.position.y, transform.position.z);
        }

    }
}
