using System;
using UnityEngine;

public interface IInventoryItem
{
    bool isEquiped { get; set; }
    Type type { get; }
    InventoryItemInfo info { get; }
}

