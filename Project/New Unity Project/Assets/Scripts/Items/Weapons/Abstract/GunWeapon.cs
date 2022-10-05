using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;

public abstract class GunWeapon : InventoryItem
{
    public float FireRate { get; private set; }

    public int MagazineCapacity
    {
        get
        {
            return attachments.extendedMag is null ? _magazineCapacity : _magazineCapacity + attachments.extendedMag.ammoAddition;
        }
    }

    public float ReloadTime
    {
        get
        {
            return attachments.stock is null ? _reloadTime : _reloadTime - attachments.stock.reloadTimeDecrease;
        }
    }

    public int AmmoAmount { get; set; }

    public int currentMagazineAmmo
    {
        get
        {
            Debug.Log(magazine);
            return magazine;
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
    private int _magazineCapacity;
    private float _reloadTime;

    public event Action<GunWeapon> OnFire; 
    public event Action<GunWeapon> OnReload;
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
        Debug.Log("reloading");
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
            isReloading = false;
        }
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
            OnFire?.Invoke(this);
            magazine -= 1;
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
        if (EventSystem.current.IsPointerOverGameObject() || !isEquiped)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
}
