using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IInventory
{
    public int capacity { get; set; }

    private List<IInventorySlot> _slots;
    private List<IInventorySlot> _charSlots;
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

    public IInventoryItem GetItem(InventoryItemType itemType)
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

    public bool HasItem(InventoryItemType itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }

    public void Remove(InventoryItemType itemType)
    {
        foreach (var slot in _slots)
        {
            if (slot.itemType == itemType)
            {
                slot.Clear();
            }
        }
        OnInventoryChanged?.Invoke();
        Debug.Log("Remove event");
    }

    public bool TryToAdd(IInventoryItem item)
    {
        if (item == null)
        {
            return false;
        }

        var emptyItemSlot = GetEmptySlot(item.type);
        if (emptyItemSlot != null && !emptyItemSlot.isEmpty)
        {
            return false;
        }
        IInventorySlot emptySlot = _slots.Find(i => i.isEmpty && i.itemType == item.type);

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

    public IInventorySlot GetEmptySlot(InventoryItemType itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType && slot.isEmpty);
    } 
}
