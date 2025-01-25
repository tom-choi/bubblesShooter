using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float expireTime = 99f;
    public int damage = 10;
    public Vector3 direction = Vector3.forward;
    
    void OnEnable()
    {
        StartCoroutine(expireTimer());
    }

    public IEnumerator expireTimer()
    {
        yield return new WaitForSeconds(expireTime);
        ObjectPool.Instance.ReturnToPool(tag, gameObject);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}