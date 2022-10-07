using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunInfoRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text magazineAmmoText;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Image gunSprite;
    [SerializeField] private Image reloadScroll;
    private bool isReloaded;
    private GunWeapon currentWeapon;

    public void AwakeInfo(GunWeapon weapon)
    {
        gameObject.SetActive(true);
        currentWeapon = weapon;
        GunSetInfo(currentWeapon);
        currentWeapon.OnFire += WeaponFireInfo;
        currentWeapon.OnReloaded += CurrentWeaponOnReloaded;
        currentWeapon.OnReload += CurrentWeaponOnReload;
    }

    public void DestroyInfo()
    {
        currentWeapon.OnFire -= WeaponFireInfo;
        currentWeapon.OnReloaded -= CurrentWeaponOnReloaded;
        currentWeapon.OnReload -= CurrentWeaponOnReload;
        currentWeapon = null;
        gameObject.SetActive(false);
    }

    private void CurrentWeaponOnReload(GunWeapon obj)
    {
        isReloaded = false;
        reloadScroll.gameObject.SetActive(true);
        StartCoroutine(ReloadScrollStart());
    }

    private void CurrentWeaponOnReloaded(GunWeapon weapon)
    {
        ammoText.text = currentWeapon.AmmoAmount.ToString();
        magazineAmmoText.text = currentWeapon.currentMagazineAmmo.ToString();
        isReloaded = true;
    }

    private void WeaponFireInfo(GunWeapon weapon)
    {
        ammoText.text = currentWeapon.AmmoAmount.ToString();
        magazineAmmoText.text = currentWeapon.currentMagazineAmmo.ToString();
    }

    private void GunSetInfo(GunWeapon weapon)
    {
        gunSprite.sprite = weapon.info.spriteIcon;
        ammoText.text = weapon.AmmoAmount.ToString();
        magazineAmmoText.text = weapon.currentMagazineAmmo.ToString();
    }

    private IEnumerator ReloadScrollStart()
    {
        reloadScroll.fillAmount = 0f;
        while (!isReloaded)
        {
            reloadScroll.fillAmount += 0.05f / currentWeapon.ReloadTime;
            yield return new WaitForSeconds(0.05f);
        }
        reloadScroll.gameObject.SetActive(false);
    }
}
