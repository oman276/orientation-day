using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public float speed = 2f;
    bool movingDown = false;
    public Transform downTarget;
    public Transform upTarget;

    private void Update()
    {
        if(movingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, downTarget.position, speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, upTarget.position, speed);
        }
    }

    public void MoveDown()
    {
        FindObjectOfType<AudioController>().Play("shelf_open");
        movingDown = true;
    }

    public void MoveUp()
    {
        FindObjectOfType<AudioController>().Play("shelf_open");
        movingDown = false;
    }

}
