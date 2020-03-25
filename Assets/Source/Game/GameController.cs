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
    private GameObject GameOverMenu;

    private Camera mainCamera;

    private Vector3 topLeft = Vector3.zero;

    private float cachedCameraY = 0f;

    private Coroutine createLevelCoroutine = null;

    private Vector3 lastCreatedLevelPosition = Vector3.zero;

    private int currentScore = 0;

    private int totalGeneratedLevels = 0;

    private void Start ()
    {
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        cachedCameraY = mainCamera.transform.position.y;
    }

    public void StartController ()
    {
        mainCamera = Camera.main;
        Vector3 startPos = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,0));   
        doodle.SetActive (true);
        lastCreatedLevelPosition = startPos;
        Vector3 spawnPosition = Vector3.zero;

        for (int i =0; i < 10; i++)
        {
            ++totalGeneratedLevels;
            GameObject pad = levelGenerator.GetComponent<LevelSpawner>().GenerateJumpPad(lastCreatedLevelPosition);
            lastCreatedLevelPosition = pad.transform.position;
        }

        createLevelCoroutine = StartCoroutine (createLevels());

    }

    public Vector2 DoodlePosition ()
    {
        return doodle.transform.position;
    }

    private IEnumerator createLevels ()
    {
        if (CheckIfNewPadGenerationIsNeeded (lastCreatedLevelPosition))
        {
            ++totalGeneratedLevels;
            cachedCameraY = mainCamera.transform.position.y;
            int enemyRand = Random.Range (1,6);
            GameObject level = null;

            if (enemyRand == 4 && totalGeneratedLevels % 9 == 0)
            {
                level = levelGenerator.GetComponent<LevelSpawner>().GenearteEnemy(lastCreatedLevelPosition);
            }
            else
            {
                level = levelGenerator.GetComponent<LevelSpawner>().GenerateJumpPad(lastCreatedLevelPosition);
            }
        
            lastCreatedLevelPosition = level.transform.position;
        }

        yield return new WaitForEndOfFrame();
        createLevelCoroutine = null;
    }

    private void Update ()
    {
        if (createLevelCoroutine == null && GameManager.Instance.IsGamePlaying())
        {
            createLevelCoroutine = StartCoroutine (createLevels());
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

    public bool CheckIfDoodleIsBelowgameView (Vector3 position)
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint (position);

        Debug.Log ("Doodle pos :" + viewPortPos.y);

        if (viewPortPos.y < 0f)
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
        doodle.SetActive (false);
        levelGenerator.GetComponent<LevelSpawner>().ResetGenerator ();
        StartController ();
    }
}
