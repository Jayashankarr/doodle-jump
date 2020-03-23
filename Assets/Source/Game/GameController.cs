using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpPadGenerator;

    [SerializeField]
    private Transform jumpPadOriginTransform;

    [SerializeField]
    private GameObject scoreBoard;

    [SerializeField]
    private GameObject GameOverMenu;

    private Camera mainCamera;

    private Vector3 topLeft = Vector3.zero;

    public static GameController Instance;

    private float cachedCameraY = 0f;

    private Coroutine createPadCoroutine = null;

    private Vector3 lastCratedPadPosition = Vector3.zero;

    private int score = 0;

    private GameState currentState = GameState.PLAYING;

    public GameState CurrentState
    {
        set {currentState = value;}
        get {return currentState;}
    }

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

    public void SaveDoodleScore (int value)
    {
        score += value;
        scoreBoard.GetComponent<Text>().text = score.ToString();
    }

    public int GetDoodleScore ()
    {
        return score;
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

    public bool CheckIfGameOver (Vector3 position)
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
}
