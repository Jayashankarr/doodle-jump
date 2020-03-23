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

    private PadObjectType collectibleType = PadObjectType.NONE;

    private float currentY = 0.2f;

    private int count = 0;

    void Start () 
    {

	}

    public GameObject GenerateJumpPad ()
    {
        count++;
        collectibleType = PadObjectType.NONE;
        Vector3 spawnPosition = Vector3.zero;
        int rand = Random.Range (1, 10);
        spawnPosition.x = Random.Range (-2.5f , 2.5f);
        spawnPosition.y = Random.Range(jumpPad.transform.position.y + 0.5f,jumpPad.transform.position.y + 2f);//jumpPad.transform.position.y + 2f;
        
        if (count % 3 == 0)
        {
            int randomObject = Random.Range (1,12);
            switch (randomObject)
            {
                case 3:
                    collectibleType = PadObjectType.SPRING;
                    Debug.Log ("cOLLECTIBLE COUNT : " + count);
                break;

                case 5:
                    collectibleType = PadObjectType.ROCKET;
                break;
            }
        }

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

        jumpPad.name = "JumpPad";
        jumpPad.GetComponent<JumpPad>().JumpPadIndex = count;
        
        return jumpPad;
        
    }

    // public GameObject GenearteEnemy ()
    // {
    //     GameObject enemy;

    //     return enemy;

    // }

    private GameObject padGreen (Vector3 padPosition)
    {
        GameObject padGreen = Instantiate (jumpPad, padPosition, Quaternion.identity);

        padGreen.GetComponent<SpriteRenderer>().sprite = greenPadSprite;
        padGreen.GetComponent<JumpPad>().Type = JumpPadType.Green;
        padGreen.GetComponent<JumpPad>().SetSpring (false);
        padGreen.GetComponent<JumpPad>().SetRocket (false);

        switch (collectibleType)
        {
            case PadObjectType.SPRING:
            padGreen.GetComponent<JumpPad>().SetSpring (true);
            padGreen.GetComponent<JumpPad>().SetRocket (false);
            break;

            case PadObjectType.ROCKET:
            padGreen.GetComponent<JumpPad>().SetSpring (false);
            padGreen.GetComponent<JumpPad>().SetRocket (true);
            break;
        }

        collectibleType = PadObjectType.NONE;

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
