using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public ClickAction clickAction;

    // Start is called before the first frame update
    void Start()
    {
        clickAction = FindObjectOfType<ClickAction>();    
    }

    public void Interact()
    {
        clickAction.AddBullets();
        Destroy(gameObject);
    }


}
