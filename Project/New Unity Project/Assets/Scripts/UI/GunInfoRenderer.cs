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

    private void CurrentWeaponOnReload(GunWeapon obj)
    {
        isReloaded = false;
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
        Debug.Log(currentWeapon.currentMagazineAmmo);
    }

    private void GunSetInfo(GunWeapon weapon)
    {
        gunSprite.sprite = weapon.info.spriteIcon;
        ammoText.text = weapon.AmmoAmount.ToString();
        magazineAmmoText.text = weapon.MagazineCapacity.ToString();
    }

    private IEnumerator ReloadScrollStart()
    {
        while (!isReloaded)
        {
            reloadScroll.fillAmount += 0.1f;
            yield return new WaitForSeconds(1 / currentWeapon.ReloadTime);
        }
    }
}
