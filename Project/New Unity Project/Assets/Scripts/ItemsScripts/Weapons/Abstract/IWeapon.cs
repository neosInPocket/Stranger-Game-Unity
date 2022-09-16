public interface IWeapon
{
    public float FireRate { get; set; }
    public int MagazineCapacity { get; set; }
    public float ReloadTime { get; set; }

    public bool Reload();
    public void Fire();
}
