using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventory
    {
        int capacity { get; set; }

        IInventoryItem GetItem(Type itemType);

        bool TryToAdd(IInventoryItem item);
        void Remove(Type itemType);
        bool HasItem(Type itemType, out IInventoryItem item);
    }
}

