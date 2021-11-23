using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntactWall : MonoBehaviour
{
    public GameObject rubble;

    public float rotation;

    public void Impact()
    {
        Instantiate(rubble, this.transform.position, Quaternion.Euler(0, 0, rotation));
        Destroy(this.gameObject);
    }

}
