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
    public GameObject QTEJudge; //QTE判定用

    public GameObject qte1;
    public GameObject qte2;

    private float stPx1 = 1321.0f, stPy1 = 118.08f , edPx1 = 1611.0f, edPy1 = 118.08f;
    private float stPx2 = 597.90f, stPy2 = 110.99f, edPx2 = 307.90f, edPy2 = 110.99f;
    public void Start()
    {
        StartCoroutine(readyforupdateQTE());
    }
    
    public IEnumerator readyforupdateQTE()
    {
        yield return new WaitUntil(() => player.isPlayerDataLoaded);
        SetUpQTE();
    }

    public void SetUpQTE()
    {
        switch(player.playerController.weapon.weaponType)
        {
            case WeaponType.Pistol:
                qte1.SetActive(false);
                qte2.SetActive(true);
                break;
            case WeaponType.SingleShot:
                qte1.SetActive(true);
                qte2.SetActive(false);
                break;
            default:
                break;
        }
    }

}