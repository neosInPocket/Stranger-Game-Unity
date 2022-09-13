using System;

namespace Assets.Scripts.Inventory.Abstract
{
    public interface IInventoryItem
    {
        bool isEquiped { get; set; }
        Type type { get; }
        int maxItemsPerSlot { get; }
        int amount { get; set; }

        IInventoryItem Clone();
    }
}

