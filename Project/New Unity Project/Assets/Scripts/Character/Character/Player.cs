using System;
using System.Collections;
using System.Collections.Generic;
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
    public GunWeapon weapon { get; private set; }
    public List<ArmourItem> armour => _armour;

    private int _defence;
    private float _maxHealth;
    private float _maxMana;
    private float _health;
    private float _mana;
    private List<ArmourItem> _armour;
    public Action OnGunSet;
    public Inventory inventory { get; private set; }
    [SerializeField] private GameObject uiInventory;

    void Awake()
    {
        uiInventory.GetComponent<UIInventory>().AwakeInventory();
        inventory = uiInventory.GetComponent<UIInventory>().inventory;
        inventory.OnRemove += OnInventoryRemove;
        inventory.OnDrop += OnInventoryDrop;
        _maxHealth = 100;
        _maxMana = 100;
        _health = 100;
        _mana = 100;
        _armour = new List<ArmourItem>(3);
    }

    private void OnInventoryDrop(IInventoryItem obj)
    {
        if (obj.GetType() == typeof(ArmourItem))
        {
            _armour.Remove(obj as ArmourItem);
            _defence = maxDefence;
        }
    }

    private void OnInventoryRemove(IInventoryItem obj)
    {
        if (obj.GetType() == typeof(ArmourItem))
        {
            _armour.Remove(obj as ArmourItem);
            _defence = maxDefence;
        }
    }

    public void GetDamage(float damage)
    {
        if (_defence != 0)
        {
            _defence -= 1;
            StartCoroutine(RegenerateArmor());
            return;
        }

        if (_health - damage <= 0)
        {
            Die();
        }
        else
        {
            _health -= damage;
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
        var existingItem = _armour.Find(x => x.info.type == armor.type);
        if (!existingItem)
        {
            _armour.Add(armor);
            _defence = maxDefence;
        }
        else
        {
            if (existingItem.info.value < armor.info.value)
            {
                _armour.Remove(existingItem);
                _armour.Add(armor);
                _defence = maxDefence;
            }
        }
    }

    public void SetWeapon(GunWeapon weapon)
    {
        this.weapon = weapon;
        OnGunSet?.Invoke();
    }

    private IEnumerator RegenerateArmor()
    {
        yield return new WaitForSeconds(3);
        while (_defence < maxDefence)
        {
            _defence += 1;
            yield return new WaitForSeconds(1);
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
