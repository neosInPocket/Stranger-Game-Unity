using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GunWeapon : InventoryItem
{
    public float FireRate { get; private set; }
    public int MagazineCapacity { get; private set; }
    public float ReloadTime { get; private set; }

    public int AmmoAmount
    {
        get
        {
            return _ammoAmount + attachments.extendedMag.ammoAddition;
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
    private int _ammoAmount;

    public event Action<object> OnFire; 
    public event Action<object> OnReload;
    void Awake()
    {
        _ammoAmount = gunInfo.ammoAmount;
        MagazineCapacity = gunInfo.magazineCapacity;
        ReloadTime = gunInfo.reloadTime;
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
                _ammoAmount = 0;
            }
            else
            {
                _ammoAmount -= MagazineCapacity - magazine;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
}
