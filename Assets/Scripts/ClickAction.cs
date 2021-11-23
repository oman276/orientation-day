using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAction : MonoBehaviour
{
    public int clickState = 0;
    public bool keyControl = true;

    public GameObject flashlightParent;
    public GameObject flashlightObj;
    public bool flashlightOn = true;

    public Transform firepoint;
    Vector2 mousePos;
    Vector2 firepoint2D;
    public Camera cam;
    public GameObject bulletFlash;
    public float flashTime = 0.1f;
    public bool gunActive = true;
    public GameObject impactEffect;
    public GameObject muzzleFlash;
    public LineRenderer lineRenderer;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject centerBound;
    public GameObject midLeftBound;
    public GameObject midRightBound;

    Transform flashlightTransform;
    Transform leftTransform;
    Transform rightTransform;
    Transform centerTransform;
    Transform midLeftTransform;
    Transform midRightTransform;

    public LayerMask playerIgnore;

    public float flashlightRadius;
    public float flashlightAngle;

    public Animator anim;
    public PlayerMovement playerMovement;

    public float decreaseRate = 0.02f;


    public float battery = 300;
    public Slider batteryBar;
    public bool noPower = false;

    public int bulletCount = 20;
    public Text bulletText;

    public bool flashlightEnabled = false;
    public GameObject flashlightUI;

    public bool gunEnabled = false;
    public GameObject gunUI;

    public GameObject woodPiece1;
    public GameObject woodPiece2;
    public GameObject woodPiece3;
    public GameObject woodPiece4;
    public LayerMask woodPieceLayer;
    public LayerMask interactiblesLayer;

    private void Start()
    {
        //lineRenderer.enabled = false;
        bulletText.text = "" + bulletCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Q) && keyControl == true)
        {
            //print("Outer Triggered");

            if(clickState == 1 && gunEnabled == true)
            {
                clickState = 2;
                flashlightParent.SetActive(false);
                anim.SetInteger("playerState", 2);
            }
            else if(flashlightEnabled == true)
            {
                FindObjectOfType<AudioController>().Play("flashlight");
                clickState = 1;
                if(noPower == false)
                {
                    flashlightParent.SetActive(true);
                }               
                anim.SetInteger("playerState", 1);
            }
        }

        if (Input.GetMouseButtonDown(0) && keyControl == true)
        {
            if (clickState == 1 && noPower == false && flashlightEnabled == true)
            {
                FindObjectOfType<AudioController>().Play("flashlight");
                if (flashlightOn == true)
                {
                    flashlightObj.SetActive(false);
                    flashlightOn = false;
                }
                else
                {
                    flashlightObj.SetActive(true);
                    flashlightOn = true;
                }
            }

            else if (clickState == 2 && gunActive == true && bulletCount > 0 && gunEnabled == true)
            {
                gunActive = false;
                firepoint2D = new Vector2(firepoint.position.x, firepoint.position.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position,  (mousePos - firepoint2D), 10000, ~woodPieceLayer);
                StartCoroutine(bulletFlashCoroutine());
                Instantiate(muzzleFlash, firepoint2D, Quaternion.identity, firepoint);
                Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

                bulletCount = bulletCount - 1;
                bulletText.text = "" + bulletCount;
                FindObjectOfType<AudioController>().Play("bullet_fire");

                EnemyAI enemyHit = hitInfo.collider.GetComponent<EnemyAI>();
                Wood wood = hitInfo.collider.GetComponent<Wood>();
                Barrel barrel = hitInfo.collider.GetComponent<Barrel>();
                if(enemyHit != null)
                {
                    enemyHit.TakeDamage();
                }
                else if (wood != null)
                {
                    wood.Shatter();
                    Instantiate(woodPiece1, hitInfo.point, Quaternion.identity);
                    Instantiate(woodPiece2, hitInfo.point, Quaternion.identity);
                    Instantiate(woodPiece3, hitInfo.point, Quaternion.identity);
                    Instantiate(woodPiece4, hitInfo.point, Quaternion.identity);
                }
                else if(barrel != null)
                {
                    barrel.BulletHit();
                }
        
                //lineRenderer.SetPosition(0, firepoint.position);
                //lineRenderer.SetPosition(1, hitInfo.point);
                //StartCoroutine(bulletLineCounter());
            }
        }  
    }

    void FixedUpdate()
    {
        if (clickState == 1 && flashlightOn == true && flashlightEnabled == true)
        {
            if (battery > 0)
            {
                battery = battery - decreaseRate;
                batteryBar.value = battery;

                if (battery == 0)
                {
                    noPower = true;
                    flashlightOn = false;
                    flashlightObj.SetActive(false);
                }
            }

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            flashlightTransform = flashlightParent.transform;
            leftTransform = leftBound.transform;
            rightTransform = rightBound.transform;
            centerTransform = centerBound.transform;
            midLeftTransform = midLeftBound.transform;
            midRightTransform = midRightBound.transform;

            Vector2 flashlightOrigin = new Vector2(flashlightTransform.position.x, flashlightTransform.position.y);
            Vector2 centerVector = new Vector2(centerTransform.position.x, centerTransform.position.y);
            Vector2 leftVector = new Vector2(leftTransform.position.x, leftTransform.position.y);
            Vector2 rightVector = new Vector2(rightTransform.position.x, rightTransform.position.y);
            Vector2 midLeftVector = new Vector2(midLeftTransform.position.x, midLeftTransform.position.y);
            Vector2 midRightVector = new Vector2(midRightTransform.position.x, midRightTransform.position.y);

            RaycastHit2D centerHit = Physics2D.Raycast(flashlightOrigin, flashlightOrigin - centerVector, flashlightRadius, ~playerIgnore);
            RaycastHit2D leftHit = Physics2D.Raycast(flashlightOrigin, leftVector - flashlightOrigin, flashlightRadius, ~playerIgnore);
            RaycastHit2D rightHit = Physics2D.Raycast(flashlightOrigin, rightVector - flashlightOrigin, flashlightRadius, ~playerIgnore);
            RaycastHit2D midLeftHit = Physics2D.Raycast(flashlightOrigin, midLeftVector - flashlightOrigin, flashlightRadius, ~playerIgnore);
            RaycastHit2D midRightHit = Physics2D.Raycast(flashlightOrigin, midRightVector - flashlightOrigin, flashlightRadius, ~playerIgnore);

            if (centerHit)
            {
                //print(centerHit.collider.name);
                EnemyAI centerEnemy = centerHit.collider.GetComponent<EnemyAI>();
                if(centerEnemy != null && centerEnemy.chasingPlayer == false)
                {
                    FindObjectOfType<AudioController>().Play("specimen_snarl");
                    centerEnemy.chasingPlayer = true;
                }
            }
            if (leftHit)
            {
                //print(leftHit.collider.name);
                EnemyAI leftEnemy = leftHit.collider.GetComponent<EnemyAI>();
                if(leftEnemy != null && leftEnemy.chasingPlayer == false)
                {
                    FindObjectOfType<AudioController>().Play("specimen_snarl");
                    leftEnemy.chasingPlayer = true;
                }
            }
            if (rightHit)
            {
                //print(rightHit.collider.name);
                EnemyAI rightEnemy = rightHit.collider.GetComponent<EnemyAI>();
                if(rightEnemy != null && rightEnemy.chasingPlayer == false)
                {
                    FindObjectOfType<AudioController>().Play("specimen_snarl");
                    rightEnemy.chasingPlayer = true;
                }
            }
            if (midLeftHit)
            {
                //print(midLeftHit.collider.name);
                EnemyAI rightEnemy = midLeftHit.collider.GetComponent<EnemyAI>();
                if (rightEnemy != null && rightEnemy.chasingPlayer == false)
                {
                    FindObjectOfType<AudioController>().Play("specimen_snarl");
                    rightEnemy.chasingPlayer = true;
                }
            }
            if (midRightHit)
            {
                //print(midRightHit.collider.name);
                EnemyAI rightEnemy = midRightHit.collider.GetComponent<EnemyAI>();
                if (rightEnemy != null && rightEnemy.chasingPlayer == false)
                {
                    FindObjectOfType<AudioController>().Play("specimen_snarl");
                    rightEnemy.chasingPlayer = true;
                }
            }
        }
    }


    public IEnumerator bulletLineCounter()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
    }

    public IEnumerator bulletFlashCoroutine()
    {
        bulletFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        bulletFlash.SetActive(false);
        gunActive = true;
    }

    public void AddBattery()
    {
        battery = battery + 30;
        if (battery > 300)
        {
            battery = 300;
        }
        batteryBar.value = battery;
    }

    public void AddBullets()
    {
        bulletCount = bulletCount + 2;
        if (bulletCount > 99)
        {
            bulletCount = 99;
        }
        bulletText.text = "" + bulletCount;
    }

    public void PickedUpFlashlight()
    {
        clickState = 1;        
        flashlightObj.SetActive(true);                
        anim.SetInteger("playerState", 1);
    }

    public void PickedUpGun()
    {
        clickState = 2;
        flashlightObj.SetActive(false);
        anim.SetInteger("playerState", 2);
    }

}
