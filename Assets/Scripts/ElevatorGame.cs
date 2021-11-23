using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorGame : MonoBehaviour
{
    public Door door;
    public PlayerMovement playerMovement;
    public ClickAction clickAction;
    public PlayerRotation playerRotation;
    public TextController textController;

    public Animator fadeAnim;

    private void Start()
    {
        door = GetComponent<Door>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        clickAction = FindObjectOfType<ClickAction>();
        playerRotation = FindObjectOfType<PlayerRotation>();
        textController = FindObjectOfType<TextController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.movementTrue = true;
        playerMovement.rb.velocity = new Vector2(0, 0);
        playerMovement.rb.inertia = 0;
        playerMovement.movementActive = false;
        clickAction.keyControl = false;
        playerRotation.rotationEnabled = false;
        clickAction.flashlightObj.SetActive(false);
        //textController.UpdateText(6);
        fadeAnim.SetTrigger("fade");
        Invoke("NextLevel", 1f);
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(4);
    }
}
