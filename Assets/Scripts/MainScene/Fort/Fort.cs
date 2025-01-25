using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified Fort script
public class Fort : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float expireTime = 99f;
    
    void Start()
    {
        StartCoroutine(expireTimer());
        StartCoroutine(Shooting());
    }

    public IEnumerator expireTimer()
    {
        yield return new WaitForSeconds(expireTime);
        Destroy(gameObject);
    }

    public IEnumerator Shooting()
    {
        while (true)
        {
            // 4顆子彈方向為前後左右
            FortUtils.shot(this.gameObject, "FortBullet", Vector3.forward); // 前方
            FortUtils.shot(this.gameObject, "FortBullet", Vector3.back);   // 後方
            FortUtils.shot(this.gameObject, "FortBullet", Vector3.left);   // 左方
            FortUtils.shot(this.gameObject, "FortBullet", Vector3.right);  // 右方

            yield return new WaitForSeconds(1f);
        }
    }
}