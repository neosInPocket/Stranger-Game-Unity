using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IInventory
{
    public int capacity { get; set; }

    private List<IInventorySlot> _slots;
    public Action OnInventoryChanged;
    public Action<IInventoryItem> OnDrop;

    public Inventory(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public IInventoryItem[] GetEquipedItems()
    {
        var equipedItems = new List<IInventoryItem>();
        var requiredSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.isEquiped);

        foreach (var slot in requiredSlots)
        {
            equipedItems.Add(slot.item);
        }

        return equipedItems.ToArray();
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }

    public IInventoryItem[] GetAllItems(string id)
    {
        List<IInventoryItem> items = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if (slot.item.info.id == id)
            {
                items.Add(slot.item);
            }
        }

        return items.ToArray();
    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }

    public void Remove(Type itemType)
    {
        foreach (var slot in _slots)
        {
            if (slot.itemType == itemType)
            {
                slot.SetItem(null);
            }
        }
        OnInventoryChanged?.Invoke();
        Debug.Log("Remove event");
    }

    public bool TryToAdd(IInventoryItem item)
    {
        IInventorySlot emptySlot = _slots.Find(i => i.isEmpty);

        if (emptySlot != null)
        {
            emptySlot.SetItem(item);
            OnInventoryChanged?.Invoke();
            Debug.Log("Add event");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TransitFromSlotToSlot(IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty)
        {
            return;
        }
        
        var fromItem = fromSlot.item;

        if (!toSlot.isEmpty)
        {
            var toItem = toSlot.item;
            fromSlot.SetItem(toItem);
            toSlot.SetItem(fromItem);
            OnInventoryChanged?.Invoke();
            return;
        }

        fromSlot.Clear();
        toSlot.SetItem(fromItem);
        OnInventoryChanged?.Invoke();
        Debug.Log("Transit event");
    }

    public IInventorySlot[] GetAllSlots()
    {
        var slots = new List<IInventorySlot>();
        foreach (var slot in _slots)
        {
            slots.Add(slot);
        }
        return slots.ToArray();
    }

    public void Drop(IInventorySlot slot)
    {
        var item = slot.item;
        slot.Clear();
        OnInventoryChanged?.Invoke();
        OnDrop?.Invoke(item);
    }
}
