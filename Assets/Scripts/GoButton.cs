using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{
    public Door door;
    public Terminal terminal;

    public void Start()
    {
        //door = FindObjectOfType<Door>();
        terminal = FindObjectOfType<Terminal>();
    }

    public void Interact()
    {
        if (terminal.cardsTotal == terminal.targetNum)
        {
            FindObjectOfType<AudioController>().Play("correct");
            door.movementTrue = true;
        }
        else
        {
            FindObjectOfType<AudioController>().Play("error");
        }
    }

}
