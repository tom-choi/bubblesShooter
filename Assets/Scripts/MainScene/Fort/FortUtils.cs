using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified FortUtils
public static class FortUtils
{
    public static void shot(GameObject obj, string bulletTag, Vector3 direction, float speed = 10f)
    {
        GameObject bullet = ObjectPool.Instance.SpawnFromPool(bulletTag, obj.transform.position, Quaternion.identity);
        if (bullet != null)
        {
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.direction = direction;
            bulletComponent.speed = speed;
        }
    }
}