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
    private GameObject gameCanvas = null;

    [SerializeField]
    private Text hudScoreText = null; 

    private GameState gameState = GameState.IDLE; 

    public static GameManager Instance = null;

    private PlayFabManager dataManager = null;

    private int playerCurretScore = 0;

    private GameController controller = null;
    
    void Start()
    {
        Instance = this;

        dataManager = new PlayFabManager ();
        dataManager.Login (playFabId);

        controller = gameController.GetComponent<GameController>();
    }

    public GameController GameController ()
    {
        return controller;
    }

    public void StartGame ()
    {
        startScreen.SetActive (false);
        gameController.SetActive (true);
        controller.StartController ();
        gameState = GameState.PLAYING;
    }

    public void UpdatePlayerName (string name)
    {
        dataManager.UpdateName (name);
    }

    public void GameOver ()
    {
        gameState = GameState.GAME_OVER;
        GameManager.Instance.SavePlayerHighScore ();
    }

    public void OnCameraPanningComplete ()
    {
        showGameOverMenu ();
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
        hudScoreText.text = playerCurretScore.ToString();
    }

    public int GetPlayerCurrentScore ()
    {
        return playerCurretScore;
    }

    public int GetPlayerHighScore ()
    {
        return dataManager.GetHighScore ();
    }

    public void SavePlayerHighScore ()
    {
        if (GetPlayerHighScore () < playerCurretScore)
        {
            dataManager.SetHighScore (playerCurretScore);
        }
    }
}
