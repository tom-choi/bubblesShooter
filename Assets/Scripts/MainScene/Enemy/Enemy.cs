using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int Health = 30;
    public float speed = 10f;
    public GameObject PlayerTarget;
    public Vector3 direction = Vector3.forward;


    private void FindNearestPlayer()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        PlayerTarget = null;

        foreach (GameObject p in Players)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                PlayerTarget = p;
            }
        }
    }

    private void Update()
    {
        if (PlayerTarget == null)
        {
            FindNearestPlayer();
        }
        if (PlayerTarget != null)
        {
            direction = (PlayerTarget.transform.position - transform.position).normalized;
        }
        transform.Translate(direction * speed * Time.deltaTime);
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