using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : IInventory
{
    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => isFull);

    private List<IInventorySlot> _slots;
    public Action OnInventoryChanged;

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
    }

    public bool TryToAdd(IInventoryItem item)
    {
        IInventorySlot emptySlot = _slots.Find(i => i.isEmpty);

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
        if (fromSlot.isEmpty)
        {
            return;
        }

        if (!toSlot.isEmpty)
        {
            return;
        }

        var item = fromSlot.item;
        fromSlot.Clear();
        toSlot.SetItem(item);
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
}
