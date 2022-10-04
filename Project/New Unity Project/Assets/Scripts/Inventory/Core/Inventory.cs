using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


public struct Attachments
{
    public GunStock stock;
    public ExtendedMag extendedMag;
    public LaserSight laserSight;
    public Skullpiercer skullPiercer;
}
public class Inventory : IInventory
{
    public int capacity { get; set; }

    public Attachments attachmentItems
    {
        get
        {
            return _attachmentItems;
        }
        set
        {
            _attachmentItems = value;
        }
    }

    private List<IInventorySlot> _slots;
    private List<IInventorySlot> _charSlots;
    private Attachments _attachmentItems;
    public Action OnInventoryChanged;
    public Action<IInventoryItem> OnDrop;
    public Action<IInventoryItem> OnRemove;
    public Action<IInventoryItem> OnGunRemove;

    public Inventory(int capacity)
    {
        this.capacity = capacity;
        _attachmentItems = new Attachments();

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
        if (item.info.type != InventoryItemType.Default)
        {
            OnRemove?.Invoke(item);
        }

        if (item.type == InventoryItemType.Gun)
        {
            OnGunRemove?.Invoke(item);
        }

        if (item.GetType().IsSubclassOf(typeof(AttachmentItem)))
        {
            RemoveAttachment(item);
        }
        OnInventoryChanged?.Invoke();
    }

    private void RemoveAttachment(IInventoryItem item)
    {
        switch (item.type)
        {
            case InventoryItemType.GunStock:
                _attachmentItems.stock = null;
                break;

            case InventoryItemType.ExtendedMag:
                _attachmentItems.extendedMag = null;
                break;

            case InventoryItemType.LaserSight:
                _attachmentItems.laserSight = null;
                var gunItem = GetSlot(InventoryItemType.Gun).item as GunWeapon;
                if (gunItem)
                {
                    _attachmentItems.laserSight.Attach(gunItem, false);
                }
                break;
        }
    }

    public bool TryToAdd(IInventoryItem item)
    {
        if (item == null)
        {
            return false;
        }

        var itemSlot = GetSlot(item.type);
        switch (item.type)
        {
            case InventoryItemType.GunStock: _attachmentItems.stock = item.prefab.GetComponent<GunStock>();
                break;

            case InventoryItemType.ExtendedMag: _attachmentItems.extendedMag = item.prefab.GetComponent<ExtendedMag>();
                break;

            case InventoryItemType.LaserSight: 
                _attachmentItems.laserSight = item.prefab.GetComponent<LaserSight>();
                var gunItem = GetSlot(InventoryItemType.Gun).item as GunWeapon;
                if (gunItem)
                {
                    _attachmentItems.laserSight.Attach(GetSlot(InventoryItemType.Gun).item as GunWeapon, true);
                }
                Debug.Log("attached");
                break;
        }
        
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
        OnGunRemove?.Invoke(item);
        OnDrop?.Invoke(item);
    }

    public IInventorySlot GetSlot(InventoryItemType itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType);
    }
}
