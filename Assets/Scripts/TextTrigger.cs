using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public int triggerInt;
    public TextController textController;

    private void Start()
    {
        textController = FindObjectOfType<TextController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            textController.UpdateText(triggerInt);
            Destroy(gameObject);
        }
    }

}
