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
    private GameObject scoreBoard = null; 

    private GameState gameState = GameState.PLAYING; 

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

    public bool IsGameOver ()
    {
        if (gameState == GameState.GAME_OVER)
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
    }

    public void SetPlayerCurrentScore (int scoreOffset)
    {
        playerCurretScore += scoreOffset;
        scoreBoard.GetComponent<Text>().text = playerCurretScore.ToString();
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
