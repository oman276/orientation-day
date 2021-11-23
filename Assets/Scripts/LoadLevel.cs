using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public LevelManager levelManager;
    public Animator fadeAnim;


    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int levelNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fadeAnim.SetTrigger("fade");
        Invoke("NextLoad", 1f);
    }

    public void NextLoad()
    {
        levelManager.LoadNextLevel(levelNum);
    }


}
