using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool firstSpawn = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public float batteryPower;
    public int bullet;
    public int health;

    public ClickAction clickAction;
    public PlayerHealth playerHealth;
    public LevelLoadObject levelLoadObject;

    public GameObject bulletUI;
    public GameObject batteryUI;

    public void Start()
    {
        clickAction = FindObjectOfType<ClickAction>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        levelLoadObject = FindObjectOfType<LevelLoadObject>();
       
    }    


    public void LoadNextLevel(int sceneNum)
    {
        bullet = clickAction.bulletCount;
        health = playerHealth.health;
        batteryPower = clickAction.battery;
        firstSpawn = false;
        SceneManager.LoadScene(sceneNum + 1);
    }

    public void NewLevelLoad()
    {
        if (firstSpawn == false)
        {
            print("New Level Load Triggered");

            playerHealth = FindObjectOfType<PlayerHealth>();
            clickAction = FindObjectOfType<ClickAction>();
            levelLoadObject = FindObjectOfType<LevelLoadObject>();

            clickAction.bulletCount = bullet;
            playerHealth.health = health;
            clickAction.battery = batteryPower;

            clickAction.gunEnabled = true;
            clickAction.flashlightEnabled = true;
            clickAction.PickedUpGun();
            clickAction.PickedUpFlashlight();

            bulletUI = levelLoadObject.bulletUI;
            batteryUI = levelLoadObject.batteryUI;
            levelLoadObject.bulletText.text = "" + bullet;
            bulletUI.SetActive(true);
            batteryUI.SetActive(true);
        }
        else
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            clickAction = FindObjectOfType<ClickAction>();
            levelLoadObject = FindObjectOfType<LevelLoadObject>();

            clickAction.bulletCount = 25;
            playerHealth.health = 50;
            clickAction.battery = 300;

            clickAction.gunEnabled = true;
            clickAction.flashlightEnabled = true;
            clickAction.PickedUpGun();
            clickAction.PickedUpFlashlight();

            bulletUI = levelLoadObject.bulletUI;
            batteryUI = levelLoadObject.batteryUI;
            levelLoadObject.bulletText.text = "" + clickAction.bulletCount;
            bulletUI.SetActive(true);
            batteryUI.SetActive(true);
        }
        
    }

}
