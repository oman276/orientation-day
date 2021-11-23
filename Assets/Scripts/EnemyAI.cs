using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float targetAngle;

    public bool chasingPlayer = false;
    public bool playerDead = false;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public GameObject enemyGFX;

    float currentAngle = 0;

    Seeker seeker;
    Rigidbody2D rb;

    public CircleCollider2D triggerCircle;
    public CircleCollider2D colliderCircle;

    public Animator anim;

    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);     
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerDead)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().inertia = 0;
            anim.SetBool("isMoving", false);
        }

        else if (chasingPlayer)
        {

            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            if (targetAngle < 0)
            {
                targetAngle = targetAngle + 360;
            }


            if (targetAngle > currentAngle)
            {
                currentAngle = currentAngle + 4;
            }
            else if (targetAngle < currentAngle)
            {
                currentAngle = currentAngle - 4;
            }

            enemyGFX.transform.rotation = Quaternion.Euler(0, 0, currentAngle - 180);

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;

            }

            if (rb.velocity.x != 0 || rb.velocity.y != 0)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    public void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);

        float distance = Vector2.Distance(rb.position, target.position);
        if (distance < 15 && chasingPlayer == false)
        {
            FindObjectOfType<AudioController>().Play("specimen_snarl");
            chasingPlayer = true;
        }
    }


    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            FindObjectOfType<AudioController>().Play("specimen_death");
            anim.SetBool("dead", true);
            Destroy(colliderCircle);
            Destroy(triggerCircle);
            Destroy(this);
        }
    }

    public void Death()
    {
        FindObjectOfType<AudioController>().Play("specimen_death");
        anim.SetBool("dead", true);
        Destroy(colliderCircle);
        Destroy(triggerCircle);
        Destroy(this);
    }

}
