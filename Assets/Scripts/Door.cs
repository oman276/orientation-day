using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public Transform destination1;
    public Transform destination2;

    public float speed = 5;
    public bool movementTrue = false;

    public void Update()
    {
        if (movementTrue)
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, destination1.position, speed);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, destination2.position, speed);
        }
    }


}
