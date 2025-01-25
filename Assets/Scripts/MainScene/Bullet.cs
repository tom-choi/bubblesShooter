using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float expireTime = 99f;
    public int damage = 10;
    public Vector3 direction = Vector3.forward;
    
    // 自動追擊子彈
    [SerializeField]
    public bool isAutoChase = false;
    [SerializeField]
    private GameObject enemyTarget;
    
    
    void OnEnable()
    {
        StartCoroutine(expireTimer());
        FindNearestEnemy();
    }

    public IEnumerator expireTimer()
    {
        yield return new WaitForSeconds(expireTime);
        ObjectPool.Instance.ReturnToPool(tag, gameObject);
    }

    void Update()
    {
        // 若有目標，朝向目標移動
        if (isAutoChase && enemyTarget != null)
        {
            direction = (enemyTarget.transform.position - transform.position).normalized;
        }
        
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void FindNearestEnemy()
    {
        // 找到帶有特定標籤的所有敵人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        enemyTarget = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                enemyTarget = enemy;
            }
        }
    }

}