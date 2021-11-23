using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Rigidbody2D playerRB;
    public Rigidbody2D cameraRB;

    // Update is called once per frame
    void Update()
    {
        cameraRB.position = playerRB.position;  
    }

}
