using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;
using UnityEngine.UI;

public class MicBar : MonoBehaviour
{
    public Image image;
    public Player player;
    public GameObject QTEJudge;

    public GameObject qte1;
    public GameObject qte2;

    private float stPx2 = 1321.0f, stPy2 = 0.08f, edPx2 = 1611.0f, edPy2 = 0.08f;
    private float stPx1 = 597.90f, stPy1 = 0.99f, edPx1 = 307.90f, edPy1 = 0.99f;

    private RectTransform judgeRect;
    private bool isCharging = false;
    private float chargeDuration = 2f; // 充能持續時間
    private Tweener judgeTween;
    private Tweener fillTween;

    public void Start()
    {
        judgeRect = QTEJudge.GetComponent<RectTransform>();
        image.fillAmount = 0;
        StartCoroutine(readyforupdateQTE());
    }
    
    public IEnumerator readyforupdateQTE()
    {
        yield return new WaitUntil(() => player.isPlayerDataLoaded);
        SetUpQTE();
    }

    void Update()
    {
        if (player.playerController.weapon.isCharging && !isCharging)
        {
            StartCharging();
        }
        else if (!player.playerController.weapon.isCharging && isCharging)
        {
            StopCharging();
        }
    }

    public void SetUpQTE()
    {
        switch(player.playerController.weapon.weaponType)
        {
            case WeaponType.Pistol:
                qte1.SetActive(false);
                qte2.SetActive(true);
                ResetJudgePosition(2);
                break;
            case WeaponType.SingleShot:
                qte1.SetActive(true);
                qte2.SetActive(false);
                ResetJudgePosition(1);
                break;
            default:
                break;
        }
    }

    private void ResetJudgePosition(int playerType)
    {
        if (playerType == 1)
        {
            judgeRect.anchoredPosition = new Vector2(stPx1, stPy1);
        }
        else
        {
            judgeRect.anchoredPosition = new Vector2(stPx2, stPy2);
        }
        image.fillAmount = 0;
    }

    private void StartCharging()
    {
        isCharging = true;
        int playerType = player.PlayerID;

        // 根據玩家類型設置終點
        Vector2 endPosition = playerType == 1 
            ? new Vector2(edPx1, edPy1) 
            : new Vector2(edPx2, edPy2);

        // QTEJudge移動動畫
        judgeTween = judgeRect.DOAnchorPos(endPosition, chargeDuration)
            .SetEase(Ease.Linear);

        // Image fillAmount動畫
        fillTween = DOTween.To(() => image.fillAmount, x => image.fillAmount = x, 1f, chargeDuration)
            .SetEase(Ease.Linear);
    }

    private void StopCharging()
    {
        isCharging = false;
        if (judgeTween != null)
        {
            judgeTween.Kill();
        }
        if (fillTween != null)
        {
            fillTween.Kill();
        }

        // 重置位置和fillAmount
        int playerType = player.PlayerID;
        ResetJudgePosition(playerType);
    }
}