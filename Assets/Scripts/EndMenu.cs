using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public Animator fadeAnim;

    public GameObject elevator;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, target.position, 2 * Time.deltaTime);
    }

    public void BackToMenu()
    {
        fadeAnim.SetTrigger("fade");
        Invoke("LevelLoad", 1f);
    }

    public void LevelLoad()
    {
        SceneManager.LoadScene(0);
    }
}
