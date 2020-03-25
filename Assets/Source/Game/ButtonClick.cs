using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private Text playerName = null;

    public void OnPlayButtonClicked ()
    {
        GameManager.Instance.StartGame ();
    }

    public void OnNameSubmitButtonClicked ()
    {
        GameManager.Instance.UpdatePlayerName (playerName.text);
    }

    public void OnPlayAgainButtonClicked ()
    {
        GameManager.Instance.ResetGame ();
    }

    public void OnScoreBoardbuttonClicked ()
    {
        GameManager.Instance.GetLeaderBoardData ();
    }
}
