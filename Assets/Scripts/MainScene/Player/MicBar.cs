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

    private float stPx1 = 1321.0f, stPy1 = 118.08f, edPx1 = 1611.0f, edPy1 = 118.08f;
    private float stPx2 = 597.90f, stPy2 = 110.99f, edPx2 = 307.90f, edPy2 = 110.99f;

    private RectTransform judgeRect;
    private bool isCharging = false;
    private float chargeDuration = 2f; // 充能持續時間
    private Tweener judgeTween;

    public void Start()
    {
        judgeRect = QTEJudge.GetComponent<RectTransform>();
        StartCoroutine(readyforupdateQTE());
    }
    
    public IEnumerator readyforupdateQTE()
    {
        yield return new WaitUntil(() => player.isPlayerDataLoaded);
        SetUpQTE();
    }

    void Update()
    {
        // 監聽充能開始和結束
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
    }

    private void StartCharging()
    {
        isCharging = true;
        int playerType = player.playerController.weapon.weaponType == WeaponType.SingleShot ? 1 : 2;

        // 根據玩家類型設置終點
        Vector2 endPosition = playerType == 1 
            ? new Vector2(edPx1, edPy1) 
            : new Vector2(edPx2, edPy2);

        // 創建移動動畫
        judgeTween = judgeRect.DOAnchorPos(endPosition, chargeDuration)
            .SetEase(Ease.Linear);
    }

    private void StopCharging()
    {
        isCharging = false;
        if (judgeTween != null)
        {
            judgeTween.Kill();
        }

        // 重置位置
        int playerType = player.playerController.weapon.weaponType == WeaponType.SingleShot ? 1 : 2;
        ResetJudgePosition(playerType);
    }
}