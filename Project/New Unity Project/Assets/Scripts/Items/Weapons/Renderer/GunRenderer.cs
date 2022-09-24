using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GunRenderer : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rotatePoint;
    private bool isFlipped;

    void Awake()
    {
        GunWeapon gun = parent.gameObject.GetComponent<GunWeapon>();
        gun.OnFire += OnFire;
        gun.OnReload += OnReload;
    }

    private void OnReload(object obj)
    {
        
    }

    private void OnFire(object obj)
    {
        var bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);
        var effectInst = Instantiate(fireEffect, firePoint.position, firePoint.rotation);

        Destroy(bulletInst, 1f);
        Destroy(effectInst, .4f);
    }

    void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        Vector2 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - rotatePoint.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;
        if (angle > 90f || angle < -90f)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        rotatePoint.transform.eulerAngles = new Vector3(0f, 0f, angle);
        rotatePoint.transform.localScale = localScale;
    }
}
