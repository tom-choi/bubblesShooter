using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int Health = 30;
    public float speed = 10f;
    void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否有標籤 "Bullet"
        if (collision.gameObject.CompareTag("Barrage_Big"))
        {
            Debug.Log("Player hit by enemy bullet!");
            SubtractHealth(collision.gameObject.GetComponent<Bullet>().damage); 
            // Destroy(collision.gameObject);
        }
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
        if (Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);
        }   
    }

}