using Assets.Scripts.Inventory.Abstract;
using System;

namespace Assets.Scripts.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool isEmpty => item == null;
        public IInventoryItem item { get; private set; }
        public Type itemType => item.type;

        public void Clear()
        {
            if (isEmpty)
                return;
            
            item = null;
        }

        public void SetItem(IInventoryItem item)
        {
            if (!isEmpty)
                return;

            this.item = item;
        }
    }
}
