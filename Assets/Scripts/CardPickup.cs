using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    public Terminal terminal;

    private void Start()
    {
        terminal = FindObjectOfType<Terminal>();
    }

    public void Interact()
    {
        terminal.CardCollected();
        Destroy(gameObject);
    }

}
