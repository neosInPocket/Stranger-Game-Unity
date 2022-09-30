using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventorySlot
    {
        bool isEmpty { get; }

        IInventoryItem item { get; }
        InventoryItemType itemType { get; set; }

        void SetItem(IInventoryItem item);
        void Clear();
    }
}
