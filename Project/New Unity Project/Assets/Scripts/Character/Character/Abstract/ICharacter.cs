public interface ICharacter
{
    public float maxHealth { get; }
    public float health { get; }
    public int defence { get; }
    public float mana { get; }
    public GunWeapon weapon { get; } 
    public void GetDamage(float damage);
    public void GetHealth(float health);
    public void GetMana(float mana);
    public void Die();
}
