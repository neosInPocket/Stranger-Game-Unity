using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GunRenderer : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bullet;
    private Transform rotatePoint;
    private bool isFlipped;
    [SerializeField] private AudioSource audiopistol;
    

    void Start()
    {
        GunWeapon gun = parent.gameObject.GetComponent<GunWeapon>();
        gun.OnFire += OnFire;
        rotatePoint = parent.transform.parent;
        cam = Camera.main;
        audiopistol = GetComponent<AudioSource>();
        
    }

    private void OnFire(object obj)
    {
        GunWeapon gun = GetComponentInParent<Player>().weapon;
        float gunAccuracy = gun.Accuracy;

        var randomRotation = Quaternion.Lerp(
            firePoint.rotation, 
            new Quaternion(Random.Range(-gunAccuracy, gunAccuracy), Random.Range(-gunAccuracy, gunAccuracy), 0, 0), 0.1f);

        var bulletInst = Instantiate(bullet, firePoint.position, randomRotation);
        bulletInst.gameObject.GetComponent<PistolBullet>().Damage = gun.Damage;

        var effectInst = Instantiate(fireEffect, firePoint.position, firePoint.rotation);

        audiopistol.PlayOneShot(audiopistol.clip);
        

        CinemachineShake.instance.ShakeCamera(gun.gunInfo.damage / 7.5f, .1f);
        Destroy(Instantiate(muzzleFlash, firePoint.position, Quaternion.identity), 0.1f);

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
