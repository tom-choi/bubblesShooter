using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public AudioSource countdownSound_4m; // 倒計時聲效
    public AudioSource countdownSound_3m; // 倒計時聲效
    public AudioSource countdownSound_2m; // 倒計時聲效
    public AudioSource countdownSound_1m; // 倒計時聲效
    public AudioSource countdownSound_20s; // 倒計時聲效
    public float countdownTime = 255f; // 5分鐘的倒計時
    private bool isFlashing = false;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            UpdateTimerText(currentTime);
            yield return new WaitForSeconds(0.01f);
            currentTime -= 0.01f; // 每次減少0.01秒

            if (Mathf.Floor(currentTime % 60) == 0 && currentTime > 0) // 每經過1分鐘
            {
                StartCoroutine(FlashAndPlaySound());
            }
        }

        // 倒計時結束時的處理
        timerText.text = "00:00.000";
    }
    void UpdateTimerText(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
    }

    IEnumerator FlashAndPlaySound()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            // 播放倒計時聲效
            // 4m
            Debug.Log("Player Sound");
            if (countdownTime <= 250 && countdownTime > 180)
            {
                countdownSound_4m.Play();
            }
            // 3m
            else if (countdownTime <= 190 && countdownTime > 120)
            {
                countdownSound_3m.Play();

            }
            // 2m
            else if (countdownTime <= 130 && countdownTime > 60)
            {
                countdownSound_2m.Play();
            }
            // 1m
            else if (countdownTime <= 70 && countdownTime > 40)
            {
                countdownSound_1m.Play();
            }

            // 文字顏色閃爍
            for (int i = 0; i < 6; i++) // 閃爍6次
            {
                timerText.DOColor(Color.red, 0.2f).OnComplete(() =>
                {
                    timerText.DOColor(Color.black, 0.2f);
                });
                yield return new WaitForSeconds(0.5f);
            }

            // 縮放效果
            timerText.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 1);

            isFlashing = false;
        }
    }
}