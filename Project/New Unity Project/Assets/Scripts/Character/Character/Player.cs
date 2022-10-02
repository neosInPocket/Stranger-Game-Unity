using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.Abstract;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public float maxHealth => _maxHealth;
    public float maxMana => _maxMana;
    public float health => _health;
    public int defence => _defence;
    public int maxDefence
    {
        get
        {
            int amount = 0;
            foreach (var armour in _armour)
            {
                if (armour != null)
                {
                    amount += armour.armourInfo.armorPoints;
                }
            }

            return amount;
        }

        private set
        {
            maxDefence = value;
        }
    }

    public float mana => _mana;
    public GunWeapon weapon { get; set; }
    public List<ArmourItem> armour => _armour;

    private int _defence;
    private float _maxHealth;
    private float _maxMana;
    private float _health;
    private float _mana;
    private List<ArmourItem> _armour;
    public Inventory inventory { get; private set; }
    [SerializeField] private GameObject uiInventory;

    void Start()
    {
        uiInventory.GetComponent<UIInventory>().AwakeInventory();
        inventory = uiInventory.GetComponent<UIInventory>().inventory;
        _maxHealth = 100;
        _maxMana = 100;
        _health = 100;
        _mana = 100;
        _armour = new List<ArmourItem>(3);
    }
    public void GetDamage(float damage)
    {
        if (defence != 0)
        {
        }
        
        if (_health - damage / defence <= 0)
        {
            Die();
        }
        else
        {
            _health -= damage / defence;
        }
    }

    public void GetHealth(float health)
    {
        if (_health + health >= maxHealth)
        {
            _health = 100;
        }
        else
        {
            _health += health;
        }
    }

    public void GetMana(float mana)
    {
        if (_mana + mana >= _maxMana)
        {
            mana = 100;
        }
        else
        {
            this._mana += mana;
        }
    }
    public void SetArmour(ArmourItem armor)
    {
        
    }

    private IEnumerator RegenerateArmor()
    {
        yield return new WaitForSeconds(3);
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
