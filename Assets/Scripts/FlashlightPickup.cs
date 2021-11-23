using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public ClickAction clickAction;

    private void Start()
    {
        clickAction = FindObjectOfType<ClickAction>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioController>().Play("flashlight");
        clickAction.flashlightEnabled = true;
        clickAction.PickedUpFlashlight();
        clickAction.flashlightUI.SetActive(true);
        Destroy(gameObject);

    }


}
