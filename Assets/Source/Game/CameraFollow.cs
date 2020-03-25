using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
    private Transform target;

	private float timeDown = 0;

	private Coroutine cameraFollow = null;

	void LateUpdate () 
    {
		if (!GameManager.Instance.IsGameIdle())
		{
			if ((target.position.y > transform.position.y) && !(GameManager.Instance.IsGameOver()))
			{
				Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
				transform.position = newPos;
			}
			else if (GameManager.Instance.IsGameOver())
			{
				if (timeDown < 3f)
				{
					transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
					timeDown += Time.fixedDeltaTime;
				}
				else
				{
					GameManager.Instance.OnCameraPanningComplete ();
					target.gameObject.SetActive (false);
				}
			}
		}
	}
}
