using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpPad;

    [SerializeField]
    private Sprite greenPadSprite;

    [SerializeField]
    private Sprite bluePadSprite;

    [SerializeField]
    private Sprite brownPadSprite;

    private float currentY = 0.2f;

    void Start () 
    {

	}

    public GameObject GenerateJumpPad ()
    {
        Vector3 spawnPosition = Vector3.zero;
        int rand = Random.Range (1, 10);
        spawnPosition.x = Random.Range (-2f , 2f);
        spawnPosition.y = jumpPad.transform.position.y + 2f;

        switch (rand)
        {
            case 3:
            jumpPad = padBrown (spawnPosition);
            break;

            case 7:
            jumpPad = padBlue (spawnPosition);
            break;

            default:
            jumpPad = padGreen (spawnPosition);
            break;

        }

        return jumpPad;
        
    }

    private GameObject padGreen (Vector3 padPosition)
    {
        GameObject padGreen = Instantiate (jumpPad, padPosition, Quaternion.identity);
        padGreen.GetComponent<SpriteRenderer>().sprite = greenPadSprite;
        padGreen.GetComponent<JumpPad>().Type = JumpPadType.Green;

        return padGreen;
    }

    private GameObject padBrown (Vector3 padPosition)
    {
        GameObject padBrown = Instantiate (jumpPad, padPosition, Quaternion.identity);
        padBrown.GetComponent<SpriteRenderer>().sprite = brownPadSprite;
        padBrown.GetComponent<JumpPad>().Type = JumpPadType.Brown;

        return padBrown;
    }

    private GameObject padBlue (Vector3 padPosition)
    {
        GameObject padBlue = Instantiate (jumpPad, padPosition, Quaternion.identity);
        padBlue.GetComponent<SpriteRenderer>().sprite = bluePadSprite;
        padBlue.GetComponent<JumpPad>().Type = JumpPadType.Blue;

        return padBlue;
    }
}
