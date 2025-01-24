using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public float speed = 10f;
    // public CanvasGroup canvasGroup;
    

    [SerializeField]
    private GameObject holo; // 光圈

    void Start()
    {
        // dotween 設定
        // canvasGroup.DOFade(0, 0.5f);
    }
    
    void Update()
    {   
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否有標籤 "EnemyBullet"
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player hit by enemy bullet!");
            SubtractHealth(collision.gameObject.GetComponent<Bullet>().damage); 
            // Destroy(collision.gameObject);
        }
    }

    void AddHealth(int val)
    {
        Health += val;
        if (Health > 100)
        {
            Health = 100;
        }
    }
    
    void SubtractHealth(int val)
    {
        Health -= val;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}