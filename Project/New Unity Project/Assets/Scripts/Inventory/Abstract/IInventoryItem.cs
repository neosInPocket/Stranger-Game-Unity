using System;
using UnityEngine;

public interface IInventoryItem
{
    bool isEquiped { get; set; }
    Type type { get; }
    ScriptableObject itemInfo { get; }
    InventoryItemInfo info { get; }
    GameObject prefab { get; }
}

