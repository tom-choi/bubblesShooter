using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateBubble : MonoBehaviour
{
    public UI_CharactorDebug uI_CharactorDebug;
    public GameObject bubblePrefab;
    public Transform ui;
    public float Randomtime;
    // public bool canCreate;
    public CanvasGroup bu;
    public CanvasGroup bb;
    public CanvasGroup l;
    public CanvasGroup e;
    public CanvasGroup[] bubblegroup;

    public CanvasGroup startGame;
    public bool canStartNow; //可以開始遊戲
    public AudioSource openning;
    public float openningTime;
    // Start is called before the first frame update
    void Start()
    {
        //BGM與開場白
        StartCoroutine(StartAfterAudio());
    }

    IEnumerator StartAfterAudio()
    {
        //開場白結束後
        yield return new WaitForSeconds(openningTime);

        //生成大量泡泡
        for (int i = 0; i < 15; i++)
        {
            GameObject newbubble = Instantiate(bubblePrefab, transform.position, transform.rotation);
            int Size = Random.Range(1, 6);
            newbubble.transform.localScale = new Vector3(Size, Size, transform.localScale.z);
            newbubble.transform.SetParent(ui);
            newbubble.transform.position = new Vector3(Random.Range(100, 1800), transform.position.y, transform.position.z);
            Destroy(newbubble, 5f);
        }

        //主題
        StartCoroutine(Openning());
    }

    // Update is called once per frame
    void Update()
    {   
        //持續生成泡泡 隨機大小位置
        if(uI_CharactorDebug.active)
        {
            return;
        }
        if(Randomtime > 0)
        {
            Randomtime -= Time.deltaTime;
        }
        else if(Randomtime <= 0)
        {
            Randomtime = Random.Range(1,2f);
            GameObject newbubble = Instantiate(bubblePrefab,transform.position,transform.rotation);
            int Size = Random.Range(1,6);
            newbubble.transform.localScale = new Vector3(Size,Size,transform.localScale.z);
            newbubble.transform.SetParent(ui);
            newbubble.transform.position = new Vector3(Random.Range(300,1800),transform.position.y,transform.position.z);
            Destroy(newbubble,5f);
        }
    }

    IEnumerator Openning()
    {
        // canCreate = true;

        //主題延遲顯示
        yield return new WaitForSeconds(2f);
        bu.DOFade(1,0.5f);

        yield return new WaitForSeconds(0.5f);
        bb.DOFade(1,0.5f);   

        yield return new WaitForSeconds(0.5f);
        l.DOFade(1,0.5f);        

        yield return new WaitForSeconds(0.5f);
        e.DOFade(1,0.5f);

        for(int i = 0 ; i < bubblegroup.Length ; i++)
        {
            yield return new WaitForSeconds(0.5f);
            bubblegroup[i].DOFade(1,0.5f);
        }

        startGame.DOFade(1,0.5f);

        yield return new WaitForSeconds(0.5f);

        //可以開始遊戲
        canStartNow = true;
    }
}
