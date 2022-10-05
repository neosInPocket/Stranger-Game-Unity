using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunInfoRenderer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text magazineAmmoText;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Image gunSprite;
    private GunWeapon currentWeapon;

    public void AwakeInfo(GunWeapon weapon)
    {
        currentWeapon = weapon;
        GunSetInfo(currentWeapon);
        currentWeapon.OnFire += WeaponFireInfo;
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
}
