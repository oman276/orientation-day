using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public ClickAction clickAction;

    private void Start()
    {
        clickAction = FindObjectOfType<ClickAction>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        clickAction.gunEnabled = true;
        clickAction.gunUI.SetActive(true);
        clickAction.PickedUpGun();
        Destroy(gameObject);
    }
}
