using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private Animator _animator;

    [Header("Collider2D ��� ����������")]
    public new Collider2D collider2D;

    [Header("������ RestartAndExitGame")]
    public GameObject canvasDiePlayer;

    private QuestKill _questKill;

    private QuestManager _questManager;
    [SerializeField] private AudioSource audioSource;

    public QuestKill questKill
    {
        get
        {
            return _questKill;
        }

        set 
        {
            _questKill = value;

            if (_questKill != null)
            {
                questKill.onQuestComplite += FirstQuest;
            }
        }

    }

    public float maxHealth => _maxHealth;
    public float maxMana => _maxMana;
    public float health => _health;
    public int defence => _defence;

    public bool isGettingDamage;
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
    public bool isDead { get; private set; }
    private List<ArmourItem> _armour;
    public Action<GunWeapon> OnGunSet;
    public Inventory inventory { get; private set; }
    [SerializeField] private GameObject uiInventory;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
            isGettingDamage = true;
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
        OnGunSet?.Invoke(weapon);
    }

    private IEnumerator RegenerateArmor()
    {
        yield return new WaitForSeconds(3);
        while (_defence < maxDefence)
        {
            if (isGettingDamage)
            {
                isGettingDamage = false;
                yield break;
            }
            _defence += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void Die()
    {
        audioSource.Play();
        collider2D.GetComponent<Collider2D>().enabled = false;

        _animator.SetTrigger("diePlayer");

        canvasDiePlayer.SetActive(true);
        transform.GetComponent<SpriteRenderer>().size = new Vector2(1.6f, 0.8f);
        isDead = true;
    }

    void FirstQuest()
    {
        Instantiate(QuestManager.instance.chestBox.gameObject, transform.position,Quaternion.identity);

        QuestManager.instance.questCompleted.SetActive(true);

        questKill.onQuestComplite -= FirstQuest;

        questKill = null;
    }
}
