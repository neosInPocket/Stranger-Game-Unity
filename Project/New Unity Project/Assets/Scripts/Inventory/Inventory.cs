using Assets.Scripts.Inventory.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class Inventory : IInventory
    {
        public event Action<object, IInventoryItem, int> OnInventoryAdd;
        public event Action<object, Type, int> OnInventoryRemoved;

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

        public IInventoryItem[] GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            foreach (var slot in _slots)
            {
                if (!slot.isEmpty)
                    allItems.Add(slot.item);
            }

            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllItems(Type itemType)
        {
            var allItemsOfType = new List<IInventoryItem>();
            var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

            foreach (var slot in slotsOfType)
            {
                allItemsOfType.Add(slot.item);
            }

            return allItemsOfType.ToArray();
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

        public int GetItemsAmount(Type itemType)
        {
            var amount = 0;
            var allItemSlots = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

            foreach (var slot in allItemSlots)
            {
                amount += slot.amount;
            }

            return amount;
        }

        public bool HasItem(Type itemType, out IInventoryItem item)
        {
            item = GetItem(itemType);
            return item != null;
        }

        public void Remove(object sender, Type itemType, int amount = 1)
        {
            var slotsWithItem = GetAllSlots(itemType);
            if (slotsWithItem.Length == 0)
            {
                return;
            }

            var amountToRemove = amount;
            var count = slotsWithItem.Length;

            for (int i = count - 1; i >= 0; i--)
            {
                var slot = slotsWithItem[i];
                if (slot.amount >= amountToRemove)
                {
                    slot.item.amount -= amountToRemove;

                    if (slot.amount <= 0)
                    {
                        slot.Clear();
                    }

                    Debug.Log($"Item {itemType} removed from inv ({amountToRemove})");
                    OnInventoryRemoved?.Invoke(sender, itemType, amountToRemove);

                    break;
                }

                var amountRemoved = slot.amount;
                amountToRemove -= slot.amount;
                slot.Clear();

                Debug.Log($"Item {itemType} removed from inv ({amountRemoved})");
                OnInventoryRemoved?.Invoke(sender, itemType, amountRemoved);
            }
        }

        private IInventorySlot[] GetAllSlots(Type itemType)
        {
            return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
        }

        public bool TryToAdd(object sender, IInventoryItem item)
        {
            var slotWithSameName = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);

            if (slotWithSameName != null)
            {
                return AddToSlot(sender, slotWithSameName, item);
            }

            var emptySlot = _slots.Find(slot => slot.isEmpty);
            if (emptySlot != null)
            {
                return AddToSlot(sender, emptySlot, item);
            }

            Debug.Log("Inv is full");
            return false;
        }

        private bool AddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
        {
            var fits = slot.amount + item.amount <= item.maxItemsPerSlot;
            var amountToAdd = fits ? item.amount : item.maxItemsPerSlot - slot.amount;
            var amountLeft = item.amount - amountToAdd;
            var clonedItem = item.Clone();
            clonedItem.amount = amountToAdd;

            if (slot.isEmpty)
            {
                slot.SetItem(clonedItem);
            }
            else
            {
                slot.item.amount += amountToAdd;
            }

            Debug.Log($"Item {item.type} added ({amountToAdd})");
            OnInventoryAdd?.Invoke(sender, item, amountToAdd);

            if (amountLeft <= 0)
            {
                return true;
            }

            item.amount = amountLeft;
            return TryToAdd(sender, item);
        }
    }
}
