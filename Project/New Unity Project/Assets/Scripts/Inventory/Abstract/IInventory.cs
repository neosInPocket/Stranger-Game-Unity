using System;

public interface IInventory
{
    int capacity { get; set; }
    bool isFull { get; }

    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetEquipedItems();

    bool TryToAdd(object sender, IInventoryItem item);
    void Remove(object sender, Type itemType);
    bool HasItem(Type itemType, out IInventoryItem item);
}

