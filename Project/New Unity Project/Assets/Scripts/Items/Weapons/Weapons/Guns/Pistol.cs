using System;
using System.Collections;
using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

public class Pistol : MonoBehaviour, IWeapon
{
    public float FireRate { get; set; } = 2;
    public int MagazineCapacity { get; set; } = 7;
    public float ReloadTime { get; set; } = 1.5f;
    public int AmmoAmount { get; set; } = 50;

    
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gunshotEffect;
    private int magazine;
    private bool isReloading;
    private bool isFiring;
    public IEnumerator Reload()
    {
        if (isReloading)
        {
            yield break;
        }
        isReloading = true;
        if (magazine != MagazineCapacity && AmmoAmount != 0)
        {
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
            Debug.Log("Reloaded");
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
            var bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            var effect = Instantiate(gunshotEffect, firePoint.position, firePoint.rotation);
            Destroy(effect, 0.4f);
            Destroy(bullet, 1);
            magazine--;
            yield return new WaitForSeconds(1 / FireRate);
            isFiring = false;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    void Start()
    {
        magazine = 7;
    }
    void Update()
    {
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
