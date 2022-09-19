using System.Collections;

public interface IWeapon
{
    public float FireRate { get; set; }
    public int MagazineCapacity { get; set; }
    public float ReloadTime { get; set; }
    public int AmmoAmount { get; set; }

    public IEnumerator Reload();
    public IEnumerator Fire();
}
