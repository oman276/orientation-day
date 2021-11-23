using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnemy : MonoBehaviour
{
    public Animator anim;

    public bool movementActive = false;
    public bool movingTo1 = true;

    public Transform target1;
    public Transform target2;

    public float speed = 2;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movementActive)
        { 

            if (movingTo1 == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target1.position, speed * Time.deltaTime);
                if((transform.position.x - target1.position.x > -0.02) && (transform.position.y - target1.position.y > -0.02))
                {
                    movingTo1 = false;
                }
            }
            else
            {
                //print("Interior Trigger");
                transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
                if((transform.position.x - target2.position.x > -0.02) && (transform.position.y - target2.position.y > -0.02))
                {
                    Destroy(gameObject);
                }
            }
        }   
    }


}
