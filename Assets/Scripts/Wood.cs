using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public void Shatter()
    {
        FindObjectOfType<AudioController>().Play("wood");
        Destroy(gameObject);
    }
    

}
