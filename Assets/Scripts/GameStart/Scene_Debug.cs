using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_Debug : MonoBehaviour
{
    public CreateBubble createBubble;
    public UI_CharactorDebug uI_CharactorDebug;
    public CanvasGroup MainScene;
    public CanvasGroup CharactorScene;
    public AudioSource GameStartAudio;
    public AudioSource BGM;
    public AudioSource Dialog;
    public bool gamestart; //進入遊戲音效判定
    public bool battlestart; //進入戰鬥音效判定
    public bool canchoosecharactor; //可以選擇角色
    public float WaitTime;

    public string levelname;

    // Start is called before the first frame update
    void Start()
    {
        MainScene.alpha = 1f;
        CharactorScene.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            if(!createBubble.canStartNow)
            {
                return;
            }
            // MainScene.DOFade(0,0.5f);
            // CharactorScene.DOFade(1,0.5f);
            if(!gamestart && GameStartAudio != null)
            {
                gamestart = true;
                GameStartAudio.Play();
                Dialog.Stop();
                BGM.Stop();
            }
            StartCoroutine(GameStart(WaitTime));
        }

        
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {   
            //未進入選擇畫面及玩家一未準備及玩家二未準備
            if(!canchoosecharactor || !uI_CharactorDebug.P1R || !uI_CharactorDebug.P2R)
            {
                return;
            }

            if(!battlestart && uI_CharactorDebug.Battle_Start != null)
            {
                battlestart = true;
                uI_CharactorDebug.Battle_Start.Play();

                StartCoroutine(BattleStart(8f));
            }
        }
    }

    // public void gamestart()
    // {
    //     SceneManager.LoadScene(levelname);
    // }

    IEnumerator GameStart(float waitTime)
    {
        MainScene.DOFade(0,0.5f);

        yield return new WaitForSeconds(waitTime);

        CharactorScene.DOFade(1,0.5f);

        canchoosecharactor = true;

        uI_CharactorDebug.active = true;
    }

    IEnumerator BattleStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        SceneManager.LoadScene(levelname);
    }
    
}
