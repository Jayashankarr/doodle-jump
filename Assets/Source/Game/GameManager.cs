using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string playFabId = null;

    [SerializeField]
    private GameObject startScreen = null;

    [SerializeField]
    private GameObject gameController = null;

    [SerializeField]
    private GameObject gameOverMenu = null;


    [SerializeField]
    private GameObject hud = null;


    [SerializeField]
    private GameObject gameCanvas = null;

    [SerializeField]
    private Text playerName = null;

    [SerializeField]
    private Text hudScoreText = null; 

    [SerializeField]
    private GameObject scoreBoard = null; 

    private GameState gameState = GameState.IDLE; 

    public static GameManager Instance = null;

    private PlayFabManager dataManager = null;

    private int playerCurretScore = 0;

    private int playerHighScore = 0;

    private GameController controller = null;

    private Hud hudScript = null;
    
    void Start()
    {
        Instance = this;

        dataManager = new PlayFabManager ();
        dataManager.Login (playFabId);

        controller = gameController.GetComponent<GameController>();
        hudScript = hud.GetComponent<Hud>();
    }

    public GameController GameController ()
    {
        return controller;
    }

    public void StartGame ()
    {
        hud.SetActive (true);
        startScreen.SetActive (false);
        gameController.SetActive (true);
        controller.StartController ();
        gameState = GameState.PLAYING;
    }

    public void UpdatePlayerName (string name)
    {
        dataManager.UpdateName (name);
        hudScript.UpdateName (name);
    }

    public void GameOver ()
    {
        gameState = GameState.GAME_OVER;
        GameManager.Instance.SavePlayerHighScore ();
        playerCurretScore = 0;
        UpdateHud ();
    }

    public void OnCameraPanningComplete ()
    {
        showGameOverMenu ();
        gameState = GameState.IDLE;
    }

    public bool IsGamePlaying ()
    {
        if (gameState == GameState.PLAYING)
        {
            return true;
        }

        return false;
    }

    public bool IsGameOver ()
    {
        if (gameState == GameState.GAME_OVER)
        {
            return true;
        }

        return false;
    }

    public bool IsGameIdle ()
    {
        if (gameState == GameState.IDLE)
        {
            return true;
        }
        return false;
    }

    private void showGameOverMenu ()
    {
        gameOverMenu.SetActive (true);
    }

    public void ResetGame ()
    {
        gameState = GameState.PLAYING;
        gameOverMenu.SetActive (false);
        playerCurretScore = 0;
        controller.ResetController ();
    }

    public void SetPlayerCurrentScore (int scoreOffset)
    {
        playerCurretScore += scoreOffset;
        UpdateHud ();
    }

    private void UpdateHud ()
    {
        hudScript.UpdateScore (playerCurretScore);
    }

    public int GetPlayerCurrentScore ()
    {
        return playerCurretScore;
    }

    public int GetPlayerHighScore ()
    {
        return PlayerPrefs.GetInt ("HighScore");
    }

    public void SavePlayerHighScore ()
    {
        if (GetPlayerHighScore () < playerCurretScore)
        {
            dataManager.SetHighScore (playerCurretScore);

            playerHighScore = playerCurretScore;
        }
    }

    public void GetLeaderBoardData ()
    {
        List<LeadboardPlayerData> data = new List<LeadboardPlayerData>();

        dataManager.GetHighScoreLeaderboard (value =>
            {
                data = value;
                scoreBoard.SetActive (true);
                scoreBoard.GetComponent<ScoreBoard>().InitializeScoreBoard (data);
            }

        );
    }
}
