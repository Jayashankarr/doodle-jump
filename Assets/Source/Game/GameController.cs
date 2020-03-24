using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject doodle;

    [SerializeField]
    private GameObject levelGenerator;

    [SerializeField]
    private Transform jumpPadOriginTransform;

    [SerializeField]
    private GameObject GameOverMenu;

    private Camera mainCamera;

    private Vector3 topLeft = Vector3.zero;

    private float cachedCameraY = 0f;

    private Coroutine createLevelCoroutine = null;

    private Vector3 lastCratedLevelPosition = Vector3.zero;

    private int currentScore = 0;

    private int totalGeneratedLevels = 0;

    private void Start ()
    {
        mainCamera = Camera.main;
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        cachedCameraY = mainCamera.transform.position.y;
    }

    private void startController ()
    {
        doodle.transform.position = jumpPadOriginTransform.position;
        lastCratedLevelPosition = jumpPadOriginTransform.position;

        Vector3 spawnPosition = Vector3.zero;
        for (int i =0; i < 10; i++)
        {
            ++totalGeneratedLevels;
            GameObject pad = levelGenerator.GetComponent<LevelGenerator>().GenerateJumpPad(lastCratedLevelPosition);
            lastCratedLevelPosition = pad.transform.position;
        }

        createLevelCoroutine = StartCoroutine (createLevels());

    }

    private IEnumerator createLevels ()
    {
        if (CheckIfNewPadGenerationIsNeeded (lastCratedLevelPosition))
        {
            ++totalGeneratedLevels;
            cachedCameraY = mainCamera.transform.position.y;
            int enemyRand = Random.Range (1,6);
            GameObject level = null;

            if (enemyRand == 4 && totalGeneratedLevels % 3 == 0)
            {
                level = levelGenerator.GetComponent<LevelGenerator>().GenearteEnemy(lastCratedLevelPosition);
            }
            else
            {
                level = levelGenerator.GetComponent<LevelGenerator>().GenerateJumpPad(lastCratedLevelPosition);
            }
        
            lastCratedLevelPosition = level.transform.position;
        }

        yield return new WaitForEndOfFrame();
        createLevelCoroutine = null;
    }

    private void Update ()
    {
        if (createLevelCoroutine == null)
        {
            createLevelCoroutine = StartCoroutine (createLevels());
        }

        transform.position = new Vector3 (transform.position.x , mainCamera.transform.position.y, transform.position.z);
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

    public bool CheckIfDoodleIsBelowgameView (Vector3 position)
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint (position);

        if (viewPortPos.y < 0.1f)
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

    public void ActivateGameOverMenu ()
    {
        GameOverMenu.SetActive (true);
    }

    public void ResetController ()
    {
        startController ();
        levelGenerator.GetComponent<LevelGenerator>().ResetGenerator ();
    }
}
