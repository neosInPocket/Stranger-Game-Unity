using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    }

    public void Remove(IInventoryItem item)
    {
        var slot = _slots.Find(slot => slot.item == item);
        slot.Clear();
        OnInventoryChanged?.Invoke();
    }

    public bool TryToAdd(IInventoryItem item)
    {
        if (item == null)
        {
            return false;
        }

        var itemSlot = GetSlot(item.type);
        if (itemSlot != null)
        {
            if (!itemSlot.isEmpty && item.type != InventoryItemType.Default && item.info.value > itemSlot.item.info.value)
            {
                Drop(itemSlot);
                itemSlot.SetItem(item);
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        else
        {
            return false;
        }

        IInventorySlot emptySlot = _slots.Find(i => i.isEmpty && i.itemType == item.type);

        if (emptySlot != null)
        {
            emptySlot.SetItem(item);
            OnInventoryChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TransitFromSlotToSlot(IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty || !toSlot.isEmpty)
        {
            return;
        }

        var fromItem = fromSlot.item;
        fromSlot.Clear();
        toSlot.SetItem(fromItem);
        OnInventoryChanged?.Invoke();
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

    public IInventorySlot GetSlot(InventoryItemType itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType);
    }
}
