using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;

    public void SetHealth(float health)
    {
        //image.fillAmount = health;
        image.DOFillAmount(health, 0.5f);
    }
}