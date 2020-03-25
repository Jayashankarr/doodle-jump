using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private Text playerName = null;

    private void Start ()
    {
        scoreText.text = "0";
        //playerName.text = "Player";
    }

    public void UpdateName (string name)
    {
        playerName.text = name;
    }

    public void UpdateScore (int value)
    {
        scoreText.text = value.ToString();
    }
}
