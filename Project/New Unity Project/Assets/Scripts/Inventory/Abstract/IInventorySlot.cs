using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventorySlot
    {
        bool isEmpty { get; }

        IInventoryItem item { get; }
        Type itemType { get; }

        void SetItem(IInventoryItem item);
        void Clear();
    }
}
