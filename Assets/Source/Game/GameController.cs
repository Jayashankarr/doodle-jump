using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpPadGenerator;

    [SerializeField]
    private Transform jumpPadOriginTransform;

    private Camera mainCamera;

    private Vector3 topLeft = Vector3.zero;

    public static GameController Instance;

    private float cachedCameraY = 0f;

    private Coroutine createPadCoroutine = null;

    private Vector3 lastCratedPadPosition = Vector3.zero;

    private void Awake ()
    {
        Instance = this;
    }

    private void Start ()
    {
        mainCamera = Camera.main;
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        cachedCameraY = mainCamera.transform.position.y;
        lastCratedPadPosition = jumpPadOriginTransform.position;

        Vector3 spawnPosition = Vector3.zero;
        for (int i =0; i < 10; i++)
        {
            GameObject pad = jumpPadGenerator.GetComponent<JumpPadGenerator>().GenerateJumpPad(lastCratedPadPosition);
            lastCratedPadPosition = pad.transform.position;
        }

        createPadCoroutine = StartCoroutine (createJumpPads());
    }

    private IEnumerator createJumpPads ()
    {
        if (CheckIfNewPadGenerationIsNeeded (lastCratedPadPosition))
        {
            cachedCameraY = mainCamera.transform.position.y;
            GameObject pad = jumpPadGenerator.GetComponent<JumpPadGenerator>().GenerateJumpPad(lastCratedPadPosition);
            lastCratedPadPosition = pad.transform.position;
        }
        yield return new WaitForEndOfFrame();

        createPadCoroutine = null;
    }

    private void Update ()
    {
        if (createPadCoroutine == null)
        {
            createPadCoroutine = StartCoroutine (createJumpPads());
        }
    }

    public Vector3 GetTopLeftPosition ()
    {
        return topLeft;
    }

    public bool CheckIfPadIsReadyToDestroy (Vector3 position)
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint (position);

        if (viewPortPos.y < 0)
        {
            return true;
        }
        return false;
    }

    private bool CheckIfNewPadGenerationIsNeeded (Vector3 position)
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint (position);

        if (viewPortPos.y < 1)
        {
            return true;
        }
        return false;
    }
}
