using System;
using System.Collections;
using System.Numerics;
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

    public Camera cam;

    
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform firePoint;
    private int magazine;
    private bool isReloading;
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

    public void Fire()
    {
        if (magazine != 0)
        {
            var bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            Destroy(bullet, 1);
            magazine--;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    private void RotateGun()
    {
        Vector2 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
        transform.rotation = rotation;
    }

    void Start()
    {
        magazine = 7;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        RotateGun();

    }
}
