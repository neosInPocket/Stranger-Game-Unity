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

    void Start()
    {
        player.OnGunSet += OnGunSet;
        player.inventory.OnGunRemove += OnGunRemove;
    }

    private void OnGunRemove(IInventoryItem obj)
    {
        (obj as GunWeapon).OnFire -= WeaponOnFire;
    }

    private void WeaponOnFire(GunWeapon weapon)
    {
        ammoText.text = weapon.AmmoAmount.ToString();
        magazineAmmoText.text = weapon.currentMagazineAmmo.ToString();
        Debug.Log(weapon.currentMagazineAmmo);
    }

    private void OnGunSet(GunWeapon weapon)
    {
        gunSprite.sprite = weapon.info.spriteIcon;
        ammoText.text = weapon.AmmoAmount.ToString();
        magazineAmmoText.text = weapon.MagazineCapacity.ToString();
        weapon.OnFire += WeaponOnFire;
    }
}
