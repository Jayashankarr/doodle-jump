using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace doodle.UI
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField]
        private GameObject board;

        [SerializeField]
        private Transform boardContainer;

        private float yOffset = 0;

        private List<GameObject> boards;

        private void Start ()
        {
            

        }

        public void InitializeScoreBoard (List<LeadboardPlayerData> playerDatas)
        {
            boards = new List<GameObject>();
            for (int i = 0; i < playerDatas.Count; i++)
            {
                yOffset -= board.GetComponent<RectTransform>().rect.height + 1f;
                Vector3 newPos = Vector3.zero;
                //newPos.y = yOffset;
                GameObject newBoard = Instantiate (board, newPos, Quaternion.identity, boardContainer);
                // newBoard.transform.localPosition = Vector3.zero;
                // newBoard.transform.Translate (0, yOffset, 0) ;//= transform.TransformPoint (newPos);
                newBoard.GetComponent<Board>().InitializeBoard (playerDatas[i]);
                boards.Add (newBoard);
            }
        }

        public void OnCloseClick ()
        {
            for (int i = 0; i < boards.Count; i++)
            {
                Destroy(boards[i]);
            }
            boards.Clear ();
            yOffset = 0f;
            gameObject.SetActive (false);
        }
    }
}
