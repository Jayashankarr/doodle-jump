using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private Text currentScore;

    [SerializeField]
    private Text highScore;

    [SerializeField]
    private Text playerName; 
    private void Start ()
    {
        currentScore.text = GameController.Instance.GetDoodleScore().ToString();

        highScore = PlayFabManager.Instance.GetHighScore();
    }

    void OnEnable ()
    {
        PlayFabManager.Instance.SetScore (GameController.Instance.GetDoodleScore());

        if (PlayFabManager.Instance.GetHighScore() < GameController.Instance.GetDoodleScore())
        {
            PlayFabManager.Instance.SetHighScore (GameController.Instance.GetDoodleScore());
        }

        //Li
    }

    public void ChangeUserName ()
    {
        PlayFabManager.Instance.UpdateName (playerName.text);
    }
}
