using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventory
    {
        int capacity { get; set; }

        IInventoryItem GetItem(InventoryItemType itemType);

        bool TryToAdd(IInventoryItem item);
        void Remove(InventoryItemType itemType);
        bool HasItem(InventoryItemType itemType, out IInventoryItem item);
    }
}

