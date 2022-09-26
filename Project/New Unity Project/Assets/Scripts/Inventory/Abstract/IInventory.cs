using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventory
    {
        int capacity { get; set; }
        bool isFull { get; }

        IInventoryItem GetItem(Type itemType);
        IInventoryItem[] GetEquipedItems();

        bool TryToAdd(IInventoryItem item);
        void Remove(Type itemType);
        bool HasItem(Type itemType, out IInventoryItem item);
    }
}

