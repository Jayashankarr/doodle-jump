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
		if ((target.position.y > transform.position.y) && (GameController.Instance.CurrentState != GameState.GAME_OVER))
		{
			Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
			transform.position = newPos;
		}
		else if (GameController.Instance.CurrentState == GameState.GAME_OVER)
		{
			if (timeDown < 3f)
			{
				transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
				timeDown += Time.fixedDeltaTime;
			}
			else
			{
				GameController.Instance.ActivateGameOverMenu ();
			}
		}
	}
}
