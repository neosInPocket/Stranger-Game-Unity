using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class Inventory : IInventory
    {
        public int capacity { get; set; }
        public bool isFull => _slots.All(slot => isFull);

        private List<IInventorySlot> _slots;

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

        public bool HasItem(Type itemType, out IInventoryItem item)
        {
            item = GetItem(itemType);
            return item != null;
        }

        public void Remove(object sender, Type itemType)
        {
            foreach (var slot in _slots)
            {
                if (slot.itemType == itemType)
                {
                    slot.SetItem(null);
                }
            }
        }

        public bool TryToAdd(object sender, IInventoryItem item)
        {
            IInventorySlot emptySlot = _slots.Find(i => i.isEmpty);

            if (emptySlot != null)
            {
                emptySlot.SetItem(item);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
