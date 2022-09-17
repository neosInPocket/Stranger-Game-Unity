using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public float MaxHealth { get; set; } = 100;
    public float MaxMana { get; set; } = 100;
    public float Health { get; set; } = 100;
    public float Defence { get; set; }
    public float Mana { get; set; } = 100;
    public IWeapon Weapon { get; set; }

    public void GetDamage(float damage)
    {
        if (Health - damage <= 0)
        {
            Die();
        }
        else
        {
            Health -= damage;
        }
    }

    public void GetHealth(float health)
    {
        if (Health + health >= MaxHealth)
        {
            Health = 100;
        }
        else
        {
            Health += health;
        }
    }

    public void GetMana(float mana)
    {
        if (Mana + mana >= MaxMana)
        {
            Mana = 100;
        }
        else
        {
            Mana += mana;
        }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Resurrect()
    {
        throw new System.NotImplementedException();
    }
}
