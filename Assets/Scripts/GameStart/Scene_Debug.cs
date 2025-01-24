using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class Scene_Debug : MonoBehaviour
{
    public CanvasGroup MainScene;
    public CanvasGroup CharactorScene;
    public float WaitTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            // MainScene.DOFade(0,0.5f);
            // CharactorScene.DOFade(1,0.5f);
            StartCoroutine(GameStart(WaitTime));
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
    }
}
