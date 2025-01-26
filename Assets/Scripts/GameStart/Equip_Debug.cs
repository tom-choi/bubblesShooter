using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Equip_Debug : MonoBehaviour
{
    public Swipe[] swipes;
    public Image[] swipeImage;
    // public Color color1;
    // public Color color2;
    public UI_CharactorDebug uI_CharactorDebug;

    public bool player1;
    public bool player2;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        swipes[0].GetComponent<CanvasGroup>().DOFade(1,0);
        swipes[0].GetComponent<Swipe>().ShowInfoFirst();
        swipes[0].enabled= true;
        swipes[1].GetComponent<CanvasGroup>().DOFade(0.5f,0);
        swipes[1].GetComponent<Swipe>().ShowInfoFirst();
        swipes[1].enabled = false;
        swipes[2].GetComponent<CanvasGroup>().DOFade(0.5f,0);
        swipes[2].GetComponent<Swipe>().ShowInfoFirst();
        swipes[2].enabled = false;
        // swipeImage[0].color = color2;
    }

    // Update is called once per frame
    void Update()
    {
        if(player1 && !uI_CharactorDebug.P1R)
        {
            //WS
            Player1();
        }

        if(player2 && !uI_CharactorDebug.P2R)
        {
            //UD
            Player2();
        }
    }

    private void Player1()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            i--;
            if (i < 0)
            {
                i = swipes.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            i++;
            {
                if (i > swipes.Length - 1)
                {
                    i = 0;
                }
            }
        }

        for (int j = 0; j < swipes.Length; j++)
        {
            if (j == i)
            {
                swipes[j].GetComponent<CanvasGroup>().DOFade(1,0);
                swipes[j].enabled = true;
                // swipeImage[j].color = color2;
            }
            else
            {
                swipes[j].GetComponent<CanvasGroup>().DOFade(0.5f,0);
                swipes[j].enabled = false;
                // swipeImage[j].color = color1;
            }
        }
    }

    private void Player2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            i--;
            if (i < 0)
            {
                i = swipes.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            i++;
            {
                if (i > swipes.Length - 1)
                {
                    i = 0;
                }
            }
        }

        for (int j = 0; j < swipes.Length; j++)
        {
            if (j == i)
            {
                swipes[j].GetComponent<CanvasGroup>().DOFade(1,0);
                swipes[j].enabled = true;
                // swipeImage[j].color = color2;
            }
            else
            {
                swipes[j].GetComponent<CanvasGroup>().DOFade(0.5f,0);
                swipes[j].enabled = false;
                // swipeImage[j].color = color1;
            }
        }
    }
}
