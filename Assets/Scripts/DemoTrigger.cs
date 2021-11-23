using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTrigger : MonoBehaviour
{
    public DemoEnemy demoEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        demoEnemy.anim.SetBool("isMoving", true);
        FindObjectOfType<AudioController>().Play("specimen_snarl");
        demoEnemy.movementActive = true;
        Destroy(gameObject);
    }


}
