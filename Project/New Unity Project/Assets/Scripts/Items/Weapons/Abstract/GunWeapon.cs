using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.Universal;

public abstract class GunWeapon : InventoryItem
{
    public float FireRate { get; private set; }

    public int MagazineCapacity
    {
        get
        {
            return attachments.extendedMag is null ? _magazineCapacity : (int)(_magazineCapacity * attachments.extendedMag.ammoMultiplier);
        }
    }

    public float ReloadTime
    {
        get
        {
            return attachments.stock is null ? _reloadTime : _reloadTime * attachments.stock.reloadTimeDecrease;
        }
    }

    public float Accuracy
    {
        get
        {
            return attachments.stock is null ? gunInfo.accuracy : gunInfo.accuracy * attachments.stock.accuracyIncrease;
        }
    }

    public int AmmoAmount
    {
        get
        {
            return _ammoAmount;
        }
        set
        {
            _ammoAmount = value;
            OnAmmoSet?.Invoke(this);
        }
    }

    public int currentMagazineAmmo
    {
        get
        {
            return magazine;
        }

        set
        {
            magazine = value;
        }
    }

    public float Damage { get; private set; }
    public Attachments attachments;
    public GunInfo gunInfo
    {
        get
        {
            return itemInfo as GunInfo;
        }
    }

    private int magazine;
    private bool isReloading;
    private bool isFiring;
    private bool blockFire;
    private int _magazineCapacity;
    private float _reloadTime;
    private int _ammoAmount;

    public event Action<GunWeapon> OnFire; 
    public event Action<GunWeapon> OnReloaded;
    public event Action<GunWeapon> OnReload;
    public event Action<GunWeapon> OnAmmoSet;
    void Awake()
    {

        _magazineCapacity = gunInfo.magazineCapacity;
        AmmoAmount = gunInfo.ammoAmount;
        _reloadTime = gunInfo.reloadTime;
        FireRate = gunInfo.fireRate;
        Damage = gunInfo.damage;
        magazine = MagazineCapacity;
    }
    public IEnumerator Reload()
    {
        if (isReloading)
        {
            yield break;
        }
        isReloading = true;
        if (magazine < MagazineCapacity && AmmoAmount != 0)
        {
            OnReload?.Invoke(this);
            yield return new WaitForSeconds(ReloadTime);
            if (AmmoAmount - MagazineCapacity < 0)
            {
                magazine = AmmoAmount;
                AmmoAmount = 0;
            }
            else
            {
                AmmoAmount -= MagazineCapacity - magazine;
                magazine = MagazineCapacity;
            }
            OnReloaded?.Invoke(this);
        }
        isReloading = false;
    }
    public IEnumerator Fire()
    {
        if (isFiring)
        {
            yield break;
        }

        if (magazine != 0)
        {
            isFiring = true;
            magazine -= 1;
            OnFire?.Invoke(this);
            yield return new WaitForSeconds(1 / FireRate);
            isFiring = false;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }
    
    void Update()
    {
        if (blockFire || !isEquiped)
        {
            return;
        }
        if ((Input.GetButtonDown("Fire1") && !gunInfo.isFullAuto) || (Input.GetButton("Fire1") && gunInfo.isFullAuto))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    public void BlockFire(bool action)
    {
        blockFire = action;
    }

    public void HideGun(bool isVisible)
    {
        GetComponent<SpriteRenderer>().enabled = !isVisible;
        GetComponentInChildren<LineRenderer>().enabled = attachments.laserSight is null ? default : !isVisible;
        GetComponentInChildren<Light2D>().enabled = attachments.laserSight is null ? default : !isVisible;
    }

    public override void Use(Player player)
    {
        throw new NotImplementedException();
    }
}
