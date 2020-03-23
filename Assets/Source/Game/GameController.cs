﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpPadGenerator;

    private int jumpPadIndex = 0;

    private void Start ()
    {
        Vector3 spawnPosition = Vector3.zero;
        for (int i =0; i < 100; i++)
        {
            Debug.Log ("Jumppad :" + jumpPadIndex);
            GameObject pad = jumpPadGenerator.GetComponent<JumpPadGenerator>().GenerateJumpPad();

            //pad.transform.parent = transform;
        }
    }

    private void Update ()
    {

    }
}