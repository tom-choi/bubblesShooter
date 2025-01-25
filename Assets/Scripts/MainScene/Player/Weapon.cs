using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Pistol,
        SingleShot
    }

    public WeaponType weaponType;
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    public int maxAmmo = 12;
    public int currentAmmo;
    
    public float maxChargeTime = 2f;     // 最大充能時間
    private float currentChargeTime = 0f;
    
    private bool isCharging = false;
    private bool isQTESuccess = false;
    
    private PlayerMicInput micInput;
    
    void Start()
    {
        currentAmmo = (weaponType == WeaponType.Pistol) ? maxAmmo : 0;
        micInput = GetComponent<PlayerMicInput>();
        if (micInput == null)
        {
            micInput = gameObject.AddComponent<PlayerMicInput>();
        }
    }

    public void Fire()
    {
        if (currentAmmo <= 0) return;
        
        if (weaponType == WeaponType.Pistol)
        {
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("FortBullet_auto", firePoint.position, firePoint.rotation);
            currentAmmo--;
        }
    }

    public void StartCharging()
    {
        isCharging = true;
        currentChargeTime = 0f;
        isQTESuccess = false;
    }

    public void StopCharging()
    {
        if (isCharging && isQTESuccess)
        {
            if (weaponType == WeaponType.Pistol)
            {
                currentAmmo = maxAmmo; // 填充彈藥
                Debug.Log("QTE Success");
            }
            else if (weaponType == WeaponType.SingleShot && currentAmmo == 0)
            {
                currentAmmo = 1;
                Fire(); // 單發槍充能完成後立即發射
                Debug.Log("QTE Success");
            }
            else
            {
                Debug.Log("QTE Failed"); // QTE失敗
            }
        }
        isCharging = false;
        currentChargeTime = 0f;
    }

    void Update()
    {
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            
            // QTE判定
            if (micInput.isBlowing() && 
                currentChargeTime > maxChargeTime * 0.3f && 
                currentChargeTime < maxChargeTime * 0.7f)
            {
                isQTESuccess = true;
            }
            
            // 超時失敗
            if (currentChargeTime >= maxChargeTime)
            {
                StopCharging();
            }
        }
    }
}