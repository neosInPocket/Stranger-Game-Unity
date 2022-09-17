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
    public float AmmoAmount { get; set; } = 50;
    public Camera cam;

    private int magazine;
    public GameObject bullet;
    public Transform firePoint;
    public void Reload()
    {
        if (magazine != MagazineCapacity)
        {
            AnimateReload();
            AmmoAmount = MagazineCapacity - magazine;
            magazine = MagazineCapacity;
        }
    }

    public IEnumerator AnimateReload()
    {
        yield return new WaitForSeconds(ReloadTime);    
    }

    public void Fire()
    {
        var boolet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        Destroy(boolet,1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        Vector2 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
        transform.rotation = rotation;

    }
}
