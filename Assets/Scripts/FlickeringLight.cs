using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public float minValue = 0.3f;
    public float maxValue = 0.7f;

    private float startTime;
    private float timer;

    public float targetTime;

    public GameObject wallLight;
    public bool lightOn = true;

    private void Start()
    {
        startTime = Time.time;
        targetTime = Random.Range(minValue, maxValue);
    }

    private void Update()
    {
        timer = Time.time - startTime;
        if(timer >= targetTime)
        {
            if(lightOn)
            {
                lightOn = false;
                wallLight.SetActive(false);
                targetTime = Random.Range(minValue, maxValue);
                startTime = Time.time;
            }
            else
            {
                lightOn = true;
                wallLight.SetActive(true);
                targetTime = Random.Range(minValue, maxValue);
                startTime = Time.time;
            }

        }


    }


}
