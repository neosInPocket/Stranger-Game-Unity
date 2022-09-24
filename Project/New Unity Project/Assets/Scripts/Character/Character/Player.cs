using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.Abstract;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public float MaxHealth { get; set; } = 100;
    public float MaxMana { get; set; } = 100;
    public float Health { get; set; } = 100;
    public float Defence { get; set; }
    public float Mana { get; set; } = 100;
    public GunWeapon Weapon { get; set; }
    private Inventory inventory;

    void Start()
    {
        inventory = new Inventory(12);
    }
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
