using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 50;
    public Rigidbody2D rb;

    public float damageThrust = 15;
    public ForceMode2D forceMode = ForceMode2D.Impulse;

    public Slider slider;
    public Animator anim;

    Vector2 enemyPos;
    Vector2 playerPos;
    Vector2 thrustVector;

    EnemyAI enemy;

    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;
    public GameObject flashlightObject;
    public ClickAction clickAction;

    public float knockbackDenom = 30f;
    public float damageDemom = 100f;

    private void Start()
    {
        InvokeRepeating("AddHealth", 0f, 1f);
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            FindObjectOfType<AudioController>().Play("impact");
            enemy = collision.gameObject.GetComponent<EnemyAI>();

            //playerPos = new Vector2(rb.position.x, rb.position.y);
            //enemyPos = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            //thrustVector = enemyPos - playerPos;

            //rb.AddForce(-thrustVector * damageThrust);

            health = health - 10;
            slider.value = health;
            if (health < 1)
            {
                Death();
            }
            else
            {
                Invoke("RestrictMovement", 0.3f);

                ContactPoint2D contactPoint = collision.GetContact(0);
                Vector2 playerPosition = transform.position;
                Vector2 dir = contactPoint.point - playerPosition;

                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;

                playerMovement.movementActive = false;

                rb.velocity = new Vector2(0, 0);
                rb.inertia = 0;
                rb.AddForce(dir * damageThrust, forceMode);
                enemy = null;
            }

        }
    }

    public void RestrictMovement()
    {
        playerMovement.movementActive = true;
    }

    public void AddHealth()
    {
        if(health != 50)
        {
            health = health + 1;
        }
        slider.value = health;       
    }

    public void BarrelExplosion(float distance, Transform barrelTransform)
    {
        Invoke("RestrictMovement", 0.2f);

        Vector2 playerPosition = transform.position;
        Vector2 barrelVector = barrelTransform.position;

        Vector2 dir = barrelVector - playerPosition;
        dir = -dir.normalized;

        playerMovement.movementActive = false;

        rb.velocity = new Vector2(0, 0);
        rb.inertia = 0;

        rb.AddForce(dir * (knockbackDenom/distance), forceMode);
        health = health - ((int)(damageDemom / distance));
        slider.value = health;
        if(health <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        if (enemy != null)
        {
            enemy.playerDead = true;
        }
        //rb.velocity = new Vector2(0, 0);
        //rb.inertia = 0;
        anim.SetTrigger("dead");
        playerMovement.movementActive = false;
        clickAction.keyControl = false;
        playerRotation.rotationEnabled = false;
        flashlightObject.SetActive(false);
        playerMovement.StartRespawn();
        Destroy(this);
    }



}
