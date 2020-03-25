using System.Collections;
using System.Collections.Generic;
using doodle.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace doodle.UI
{
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
            
        }

        private void OnEnable ()
        {
            currentScore.text = GameManager.Instance.GetPlayerCurrentScore().ToString();

            highScore.text = GameManager.Instance.GetPlayerHighScore().ToString();
        }

        public void ChangeUserName ()
        {
            
        }
    }
}
