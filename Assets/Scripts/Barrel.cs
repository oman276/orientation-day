using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public BarrelExplosionParent explosionParent;
    public Animator anim;
    public BoxCollider2D boxCollider2D;

    public void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void BulletHit()
    {
        FindObjectOfType<AudioController>().Play("barrel");
        explosionParent.Explode();
        anim.SetTrigger("boom");
        Destroy(boxCollider2D);
        Destroy(this);
    }

}
