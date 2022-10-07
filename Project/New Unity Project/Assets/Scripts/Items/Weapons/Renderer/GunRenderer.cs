using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GunRenderer : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject bullet;
    private Transform rotatePoint;
    private bool isFlipped;

    void Start()
    {
        GunWeapon gun = parent.gameObject.GetComponent<GunWeapon>();
        gun.OnFire += OnFire;
        rotatePoint = parent.transform.parent;
        cam = Camera.main;
    }

    private void OnFire(object obj)
    {
        var randomRotation = Quaternion.Lerp(firePoint.rotation, new Quaternion(Random.Range(-3, 3), Random.Range(-3, 3), 0, 0), 0.1f);
        var bulletInst = Instantiate(bullet, firePoint.position, randomRotation);
        var effectInst = Instantiate(fireEffect, firePoint.position, firePoint.rotation);
        CinemachineShake.instance.ShakeCamera(2f, .1f); 

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
