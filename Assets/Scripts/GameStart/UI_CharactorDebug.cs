using UnityEngine;
using UnityEngine.UI;

public class UI_CharactorDebug : MonoBehaviour
{
    //玩家一
    // public Image Weapon_1;
    // public Image Weapon_1_value;
    // public Color[] Color_1;
    // public int i;
    // public AudioSource Player_1_Change;
    public AudioSource Player_1_Ready;
    public Image Player_1;
    public Sprite Player_1_Idle;
    public Sprite Player_1_OK;
    public GameObject Player_1_notReady;
    public GameObject Player_1_isReady;
    public bool P1R; //玩家一準備

   //玩家二
    // public Image Weapon_2;
    // public Image Weapon_2_value;
    // public Color[] Color_2;
    // public int j;
    // public AudioSource Player_2_Change;
    public AudioSource Player_2_Ready;
    public Image Player_2;
    public Sprite Player_2_Idle;
    public Sprite Player_2_OK;
    public GameObject Player_2_notReady;
    public GameObject Player_2_isReady;

    public bool P2R; //玩家二準備

    //進入戰鬥
    public AudioSource Battle_Start;
    public bool active;

    private void Start() 
    {
      Player_1.sprite = Player_1_Idle;
      Player_2.sprite = Player_2_Idle;
      Player_1_notReady.SetActive(true);
      Player_1_isReady.SetActive(false);
      Player_2_notReady.SetActive(true);
      Player_2_isReady.SetActive(false);
    }

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
          if(Player_1_Ready != null && !P1R)
          {
            P1R = true;
            Player_1.sprite = Player_1_OK;
            Player_1_isReady.SetActive(true);
            Player_1_notReady.SetActive(false);
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
          if(Player_2_Ready != null && !P2R)
          {
            P2R = true;
            Player_2.sprite = Player_2_OK;
            Player_2_isReady.SetActive(true);
            Player_2_notReady.SetActive(false);
            Player_2_Ready.Play();
          }
        }
    }
}
