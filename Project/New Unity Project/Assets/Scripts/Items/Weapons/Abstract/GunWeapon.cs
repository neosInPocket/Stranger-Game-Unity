using System;
using System.Collections;
using UnityEngine;

public abstract class GunWeapon : MonoBehaviour
{
    public float FireRate { get; private set; }
    public int MagazineCapacity { get; private set; }
    public float ReloadTime { get; private set; }
    public int AmmoAmount { get; private set; }
    public float Damage { get; private set; }
    
    [SerializeField] private GunInfo gunsInfo;
    private int magazine;
    private bool isReloading;
    private bool isFiring;

    public event Action<object> OnFire; 
    public event Action<object> OnReload;
    void Awake()
    {
        AmmoAmount = gunsInfo.ammoAmount;
        MagazineCapacity = gunsInfo.magazineCapacity;
        ReloadTime = gunsInfo.reloadTime;
        FireRate = gunsInfo.fireRate;
        Damage = gunsInfo.damage;
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
}
