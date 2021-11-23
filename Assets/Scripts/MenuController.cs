using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject floor;
    public AudioController audioController;

    public GameObject fade;

    public Animator fadeAnim;

    public void StartGame()
    {
        Destroy(floor);
        Scrape();
        Invoke("FadeOut", 0.5f);
        Invoke("Crash", 0.7f);
        Invoke("LoadNextLevel", 1.5f);
    }

    void Scrape()
    {
        FindObjectOfType<AudioController>().Play("scrape");
    }

    void Crash()
    {
        FindObjectOfType<AudioController>().Play("crash");
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    void FadeOut()
    {
        fadeAnim.SetTrigger("fade");
    }


}
