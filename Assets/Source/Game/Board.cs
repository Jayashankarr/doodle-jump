using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Text playerName = null;

    [SerializeField]
    private Text score = null;

    public void InitializeBoard (LeadboardPlayerData data)
    {
        playerName.text = data.Name;

        score.text = data.score.ToString ();
    }
}
