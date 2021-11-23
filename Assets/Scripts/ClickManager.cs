using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    BatteryPickup batteryPickup;
    BulletPickup bulletPickup;
    CardPickup cardPickup;
    GoButton goButton;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePos2D = new Vector3(mousePos.x, mousePos.y, -10);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            

            if (hit.collider != null)
            {
                //print(hit.collider.name);
                batteryPickup = hit.collider.gameObject.GetComponent<BatteryPickup>();
                bulletPickup = hit.collider.gameObject.GetComponent<BulletPickup>();
                cardPickup = hit.collider.gameObject.GetComponent<CardPickup>();
                goButton = hit.collider.gameObject.GetComponent<GoButton>();

                if(batteryPickup != null)
                {
                    FindObjectOfType<AudioController>().Play("pickup");
                    batteryPickup.Interact();
                }
                else if(bulletPickup != null)
                {
                    FindObjectOfType<AudioController>().Play("pickup");
                    bulletPickup.Interact();
                }
                else if(cardPickup != null)
                {
                    FindObjectOfType<AudioController>().Play("pickup");
                    cardPickup.Interact();
                }
                else if(goButton != null)
                {
                    //FindObjectOfType<AudioController>().Play("pickup");
                    goButton.Interact();
                }
            }

        }
    }

}
