using System;
using UnityEngine;

public interface IInventoryItem
{
    bool isEquiped { get; set; }
    InventoryItemType type { get; }
    ScriptableObject itemInfo { get; }
    InventoryItemInfo info { get; }
    GameObject prefab { get; }
    public float dropChance { get; }
    public void Use(Player player);
}

