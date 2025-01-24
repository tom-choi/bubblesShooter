using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_CharactorDebug : MonoBehaviour
{
    public Image Weapon_1;
    public Image Weapon_1_value;
    public Color[] Color_1;
    public int i;

    public Image Weapon_2;
    public Image Weapon_2_value;
    public Color[] Color_2;
    public int j;

    public string levelname;
    // Start is called before the first frame update
    void Start()
    {
        print(Color_1.Length);
    }

    // Update is called once per frame
    void Update()
    {
        Weapon_1.color = Color_1[i];
        Weapon_1_value.color = Color_1[i];

        if(Input.GetKeyDown(KeyCode.A))
        {
          i--;
          if(i < 0)
          {
            i = Color_1.Length - 1;
          }
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            i++;
            if(i >= Color_1.Length)
            {
                i = 0;
            }
        }

        Weapon_2.color = Color_2[j];
        Weapon_2_value.color = Color_2[j];

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
          j--;
          if(j < 0)
          {
            j = Color_2.Length - 1;
          }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            j++;
            if(j >= Color_2.Length)
            {
                j = 0;
            }
        }


        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(levelname);
        }
    }
}
