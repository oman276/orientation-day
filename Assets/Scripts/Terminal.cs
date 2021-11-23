using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public Animator anim;

    public GameObject icon1;
    public GameObject icon2;
    public GameObject icon3;

    public int cardsInDeck = 0;
    public int cardsTotal = 0;

    public int targetNum;

    public void CardCollected()
    {
        //print("loopTriggered");
        cardsInDeck++;
        cardsTotal++;

        anim.SetInteger("cards", cardsTotal);

        
        icon1.SetActive(true);
        //print("icon1");
        if (cardsTotal >= 2)
        {
            icon2.SetActive(true);
            //print("icon2");
            if(cardsTotal >= 3)
            {
                //print("icon3");
                icon3.SetActive(true);
            }
        }
    }

}
