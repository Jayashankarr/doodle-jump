using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    void Update ()
    {
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Vector3 newPos = new Vector3 (Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = newPos;
        }
    }
}
