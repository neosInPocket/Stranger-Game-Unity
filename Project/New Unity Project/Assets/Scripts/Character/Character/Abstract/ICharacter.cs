public interface ICharacter
{
    public float Health { get; set; }
    public float Defence { get; set; }
    public float Mana { get; set; }
    public IWeapon Weapon { get; set; } 
    public void GetDamage(float damage);
    public void GetHealth(float health);
    public void GetMana(float mana);
    public void Die();
    public void Resurrect();
}
