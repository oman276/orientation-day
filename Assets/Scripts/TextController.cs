using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    public GameObject textObject;
    public Text text;

    public bool textVisible = false;
    public float textAlpha;
    public float textDelta;

    public bool collectTextTriggered = false;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            UpdateText(1);
        }       
    }

    public void UpdateText(int textNum)
    {
        if(textNum == 1)
        {
            text.text = "use WASD to move";
        }
        else if (textNum == 2)
        {
            text.text = "point using MOUSE, turn off and on with LEFT CLICK";
        }
        else if (textNum == 3)
        {
            text.text = "point using MOUSE, fire using LEFT CLICK. switch items using Q";
        }
        else if (textNum == 4)
        {
            text.text = "open and close cabinets and terminals you are close to with E. LEFT CLICK items in cabinets to collect them";
        }
        else if (textNum == 5)
        {
            text.text = "";
        }
        else if(textNum == 6)
        {
            text.text = "That's it, sorry. Haven't made that part yet. Thanks for helping play the demo! Let me know anything and everything you thought!";
        }
        else if(textNum == 7)
        {
            text.text = "LEFT CLICK items to collect them";
        }
        else if(textNum == 8)
        {
            text.text = "SHOOT wood to shatter it";
        }
        else if (textNum == 9)
        {
            text.text = "press ESC to reset level if needed";
        }
        else if(textNum == 10)
        {
            text.text = "shoot EXPLOSIVE BARRELS to destroy walls and hurt enemies. stand back to avoid damage";
        }

        StartCoroutine(FadeTextToFullAlpha(1f, text));
        Invoke("FadeOut", 6f);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, text));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public void ItemCollectCheck()
    {
        if(collectTextTriggered == false)
        {
            collectTextTriggered = true;
            UpdateText(7);
        }
    }

}
