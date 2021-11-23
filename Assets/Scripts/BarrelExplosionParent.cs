using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosionParent : MonoBehaviour
{
    public Transform thisTransform;
    public GameObject childExplosion;
    public Transform targetWall;
    public IntactWall intactWall;

    public GameObject playerObj;
    public PlayerHealth playerHealth;

    public GameObject targetEnemy;
    public EnemyAI enemy;

    public Animator childAnim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    public void Explode()
    {
        thisTransform.parent = null;
        childAnim.SetTrigger("explode");
        childExplosion.SetActive(true);
        

        float playerDistance = Mathf.Abs(Vector2.Distance(this.transform.position, playerObj.transform.position));
        playerHealth = playerObj.GetComponent<PlayerHealth>();

        if (targetEnemy != null)
        {
            float enemyDistance = Mathf.Abs(Vector2.Distance(this.transform.position, targetEnemy.transform.position));
            if(enemyDistance <= 9)
            {
                enemy = targetEnemy.gameObject.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.Death();
                }
                
            }
        }
        if (targetWall != null)
        {
            float wallDistance = Mathf.Abs(Vector2.Distance(this.transform.position, targetWall.position));
            if ((wallDistance) < 7)
            {
                intactWall.Impact();
            }
        }


        playerHealth.BarrelExplosion(playerDistance, thisTransform);

        Destroy(gameObject, 0.1f);
    }
}
