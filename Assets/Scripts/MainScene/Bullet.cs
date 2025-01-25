using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float expireTime = 99f;
    public int damage = 10;
    public Vector3 direction = Vector3.forward;

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
        if (isAutoChase && enemyTarget != null)
        {
            direction = (enemyTarget.transform.position - transform.position).normalized;
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void FindNearestEnemy()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.SubtractHealth(damage);
                // Return the bullet to the pool after hitting the enemy
                ObjectPool.Instance.ReturnToPool(tag, gameObject);
            }
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SubtractHealth(damage);
                // Return the bullet to the pool after hitting the enemy
                ObjectPool.Instance.ReturnToPool(tag, gameObject);
            }
        }
    }
}