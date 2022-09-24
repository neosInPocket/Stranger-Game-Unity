using Assets.Scripts.Inventory.Abstract;
using System;
using UnityEngine;
using Input = UnityEngine.Input;

public abstract class HealthPotionAbstract : MonoBehaviour, IInventoryItem
{
    public bool isEquiped { get; set; }
    public Type type { get; }
    public int maxItemsPerSlot { get; }
    public int amount { get; set; }

    [SerializeField] private GameObject _pickUpPref;
    [SerializeField] protected PotionInfo _potionInfo;
    protected float healthAmount;
    private GameObject _message;
    private bool isInRange;
    private GameObject _enteredGO;

    void OnTriggerEnter2D(Collider2D collider)
    {
        _message = Instantiate(_pickUpPref, transform.GetChild(0).position, Quaternion.identity);
        isInRange = true;
        _enteredGO = collider.gameObject;
    }

    void OnTriggerExit2D()
    {
        Destroy(_message);
        isInRange = false;
    }

    public void Use(Player player)
    {
        player.GetHealth(healthAmount);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {

        }
    }
    public IInventoryItem Clone()
    {
        return this;
    }
}
