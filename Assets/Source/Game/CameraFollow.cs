using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

	[SerializeField]
	private Transform background;

	private float TimeDown = 0;

	private Coroutine cameraFollow = null;

	void LateUpdate () 
    {
		if ((target.position.y > transform.position.y) && (GameController.Instance.CurrentState != GameState.GAME_OVER))
		{
			Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
			transform.position = newPos;
			background.position = new Vector3(background.position.x, transform.position.y, background.position.z);
		}
		else if (GameController.Instance.CurrentState == GameState.GAME_OVER)
		{
			if (TimeDown < 3f)
			{
				transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
				background.position = new Vector3(background.position.x, target.position.y, background.position.z);
				TimeDown += Time.fixedDeltaTime;
			}
		}
	}

	void Update ()
	{
		// if (GameController.Instance.CurrentState == GameState.GAME_OVER)
		// {
		// 	if (cameraFollow == null)
		// 	{
		// 		cameraFollow = StartCoroutine (cameraDown ());
		// 	}
		// }
	}

	private IEnumerator cameraDown ()
	{
		transform.position = new Vector3(transform.position.x , transform.position.y - 0.1f, transform.position.z);
		yield return new WaitForSeconds(0.05f);

		cameraFollow = null;
	}
}
