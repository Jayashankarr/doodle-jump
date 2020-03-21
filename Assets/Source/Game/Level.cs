using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject jumbPad;

    private void Start ()
    {
        Vector3 spawnPosition = Vector3.zero;

        for (int i =0; i < 50; i++)
        {
            if (jumbPad != null)
            {
                spawnPosition.x = Random.Range (-2f , 2f);
                spawnPosition.y = spawnPosition.y + 1.5f;

                Instantiate (jumbPad, spawnPosition, Quaternion.identity);
            }
        }
    }
}
