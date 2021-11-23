using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    public LevelManager levelManager;

    public Animator anim;
    public bool isMoving = false;

    public bool movementActive = true;

    Vector2 movement;

    public bool inCabinetRange = false;
    public bool drawerActive = false;
    Cabinet currentCabinet;

    public ClickAction clickAction;
    public PlayerRotation playerRotation;
    public TextController textController;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        textController = FindObjectOfType<TextController>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("isMoving", true);
            isMoving = true;          
        }
        else
        {
            anim.SetBool("isMoving", false);
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (inCabinetRange == true)
            {
                //textController.ItemCollectCheck();
                currentCabinet.drawer.MoveDown();
                drawerActive = true;
                inCabinetRange = false;
                movementActive = false;
                clickAction.keyControl = false;
                playerRotation.rotationEnabled = false;
                rb.velocity = new Vector2(0, 0);
                rb.inertia = 0;
            }
            else if(drawerActive == true)
            {
                currentCabinet.drawer.MoveUp();
                drawerActive = false;
                inCabinetRange = true;
                movementActive = true;
                clickAction.keyControl = true;
                playerRotation.rotationEnabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.firstSpawn = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void FixedUpdate()
    {
        if (movementActive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            gameObject.transform.position = rb.position;
        }


    }

    public void ChangeState(int state)
    {
        anim.SetInteger("playerState", state);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cabinet")
        {
            inCabinetRange = true;
            currentCabinet = collision.gameObject.GetComponent<Cabinet>();

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cabinet")
        {
            inCabinetRange = false;
        }
    }

    public void StartRespawn()
    {
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {
        levelManager.firstSpawn = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
