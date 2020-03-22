using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject jumbPad;

    [SerializeField]
    private GameObject jumpPadGenerator;
    
    private void Start ()
    {
        Vector3 spawnPosition = Vector3.zero;

        for (int i =0; i < 10; i++)
        {
            GameObject pad = jumpPadGenerator.GetComponent<JumpPadGenerator>().GenerateJumpPad();

            //pad.transform.parent = transform;
        }
    }

    private void Update ()
    {

    }
}
