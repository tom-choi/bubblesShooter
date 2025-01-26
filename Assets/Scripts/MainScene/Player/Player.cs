using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public float speed = 10f;
    public GameObject PlayerModel;  

    public List<Skill> skills;

    public bool isPlayerDataLoaded = false;
    // public CanvasGroup canvasGroup;
    

    [SerializeField]
    private GameObject holo; // 光圈

    // UI
    [SerializeField]
    public PlayerIconUI playerIconUI; // 玩家UI

    void Start()
    {
        // ReadData
        initSkill();
    }
    public void initSkill()
    {


        isPlayerDataLoaded = true;
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