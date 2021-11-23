using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPiece : MonoBehaviour
{
    public Rigidbody2D rb;
    float randX;
    float randY;


    // Start is called before the first frame update
    void Start()
    {
        randX = Random.Range(-3, 4);
        randY = Random.Range(-3, 4);
        float torque = Random.Range(-30, 30);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(randX * Random.Range(100, 150), randY * Random.Range(150, 150)));
        rb.AddTorque(torque);
    }

}
