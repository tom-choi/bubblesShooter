using UnityEngine;
using UnityEngine.UI;

public class UI_CharactorDebug : MonoBehaviour
{
    //玩家一
    // public Image Weapon_1;
    // public Image Weapon_1_value;
    // public Color[] Color_1;
    // public int i;
    public AudioSource Player_1_Change;
    public AudioSource Player_1_Ready;
    public bool P1R;

   //玩家二
    // public Image Weapon_2;
    // public Image Weapon_2_value;
    // public Color[] Color_2;
    // public int j;
    public AudioSource Player_2_Change;
    public AudioSource Player_2_Ready;
    public bool P2R;

    //進入戰鬥
    public AudioSource Battle_Start;
    public bool active;

    // Update is called once per frame
    void Update()
    {   
      //玩家一
        // Weapon_1.color = Color_1[i];
        // Weapon_1_value.color = Color_1[i];

        // if(Input.GetKeyDown(KeyCode.A))
        // {
        //   i--;
        //   if(i < 0)
        //   {
        //     i = Color_1.Length - 1;
        //   }

        //   if(Player_1_Change != null)
        //   {
        //     Player_1_Change.Play();
        //   }
        // }

        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     i++;
        //     if(i >= Color_1.Length)
        //     {
        //         i = 0;
        //     }

        //     if(Player_1_Change != null)
        //     {
        //       Player_1_Change.Play();
        //     }
        // }

        //玩家一準備
        if(Input.GetKeyDown(KeyCode.E) && active)
        {
          P1R = true;
          if(Player_1_Ready != null)
          {
            Player_1_Ready.Play();
          }
        }
        

        //玩家二
        // Weapon_2.color = Color_2[j];
        // Weapon_2_value.color = Color_2[j];

        // if(Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //   j--;
        //   if(j < 0)
        //   {
        //     j = Color_2.Length - 1;
        //   }

        //   if(Player_2_Change != null)
        //   {
        //     Player_2_Change.Play();
        //   }
        // }

        // if(Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //   j++;
        //   if(j >= Color_2.Length)
        //   {
        //       j = 0;
        //   }

        //   if(Player_2_Change != null)
        //   {
        //     Player_2_Change.Play();
        //   }
        // }

        //玩家二準備
        if(Input.GetKeyDown(KeyCode.Keypad0) && active)
        {
          P2R = true;
          if(Player_2_Ready != null)
          {
            Player_2_Ready.Play();
          }
        }
    }
}
