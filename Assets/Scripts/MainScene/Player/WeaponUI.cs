using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Weapon weaponCache;
    public int skillIndex;
    public Player player;

    public Image weaponIcon;
    public TextMeshProUGUI ammoText; // Renamed to ammoText for clarity

    private void Start()
    {
        StartCoroutine(UpdateWeapon());
        UpdateAmmoDisplay();
    }

    private void Update()
    {
        // Update the ammo display each frame if the weapon has changed or ammo is updated
        UpdateAmmoDisplay();
    }
    public IEnumerator UpdateWeapon()
    {
        yield return new WaitUntil(() => player.isPlayerDataLoaded);
        Debug.Log("Player data loaded");
        weaponCache = player.playerController.weapon;
        weaponIcon.sprite = player.playerController.weapon.WeaponSprite;
    }
    public void UpdateAmmoDisplay()
    {
        if (weaponCache != null)
        {
            ammoText.text = $"{weaponCache.currentAmmo}/{weaponCache.maxAmmo}";
            UpdateAmmoColor();
        }
    }

    private void UpdateAmmoColor()
    {
        float ammoRatio = (float)weaponCache.currentAmmo / weaponCache.maxAmmo;

        // Change color based on ammo ratio
        if (ammoRatio < 0.2f) // Less than 20% ammo
        {
            ammoText.DOColor(Color.red, 0.5f);
        }
        else if (ammoRatio < 0.5f) // Less than 50% ammo
        {
            ammoText.DOColor(Color.yellow, 0.5f);
        }
        else // Above 50%
        {
            ammoText.DOColor(Color.black, 0.5f);
        }
    }
}