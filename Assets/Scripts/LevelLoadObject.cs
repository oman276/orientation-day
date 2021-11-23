using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadObject : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject batteryUI;
    public GameObject bulletUI;
    public Text bulletText;


    private void Update()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if(levelManager != null)
        {
            levelManager.NewLevelLoad();
            Destroy(this);
        }
    }

}
