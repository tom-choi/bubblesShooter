using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public float speed = 10f;
    public GameObject PlayerModel;
    // public CanvasGroup canvasGroup;
    

    [SerializeField]
    private GameObject holo; // 光圈

    void Start()
    {
        // dotween 設定
        // canvasGroup.DOFade(0, 0.5f);
    }
    
    // 更新函数
    void Update()
    {   
        
    }

    public void AddHealth(int val)
    {
        Health += val;
        if (Health > 100)
        {
            Health = 100;
        }
    }
    
    public void SubtractHealth(int val)
    {
        Health -= val;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}