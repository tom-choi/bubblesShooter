using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public GameObject scrollbar;
    public float scroll_pos = 0;
    float[] pos;
    public float MaxSize;
    public float MinSize;
    public GameObject[] Weapon;
    public TMP_Text Gear_name;
    public TMP_Text Gear_Description;

    public bool player1;
    public bool player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1)
        {
            Player1();
        }

        if(player2)
        {
            Player2();
        }
    }

    private void Player1()
    {
        pos = new float[transform.childCount];//子數量
        float distance = 1f / (pos.Length - 1f);//子比例 0.25f
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }


        // if(Input.GetMouseButton(0))
        // {
        //     scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        // }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (scroll_pos > 0)
            {
                scroll_pos -= distance;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (scroll_pos < 1)
            {
                scroll_pos += distance;
            }
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(MaxSize, MaxSize), 0.1f);

                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(MinSize, MinSize), 0.1f);
                    }
                }
            }
        }

        // //獲取當前物件
        // int num = (int)(scroll_pos / distance);
        // print(Weapon[num].name);
        ShowInfo(distance);
    }
    private void Player2()
    {
        pos = new float[transform.childCount];//子數量
        float distance = 1f / (pos.Length - 1f);//子比例 0.25f
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }


        // if(Input.GetMouseButton(0))
        // {
        //     scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        // }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (scroll_pos > 0)
            {
                scroll_pos -= distance;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (scroll_pos < 1)
            {
                scroll_pos += distance;
            }
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - distance / 2)
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(MaxSize, MaxSize), 0.1f);

                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(MinSize, MinSize), 0.1f);
                    }
                }
            }
        }

        ShowInfo(distance);
    }

    private void ShowInfo(float distance)
    {
        //獲取當前物件
        int num = (int)(scroll_pos / distance);
        //print(Weapon[num].GetComponent<Gear>().gear_Name);
        Gear_name.text = Weapon[num].GetComponent<Gear>().gear_Name.ToString();
        Gear_Description.text = Weapon[num].GetComponent<Gear>().gear_Description.ToString();
        
    }
}
