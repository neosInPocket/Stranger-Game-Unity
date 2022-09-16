public interface IWeapon
{
    public float FireRate { get; set; }
    public int MagazineCapacity { get; set; }
    public float ReloadTime { get; set; }
    public float AmmoAmount { get; set; }

    public void Reload();
    public void Fire();
}
