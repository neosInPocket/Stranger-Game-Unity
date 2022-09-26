using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : IInventoryItem
{
    public bool isEquiped { get; set; }
    public Type type { get; }
    public InventoryItemInfo info
    {
        get => _info;
    }

    [SerializeField] private PotionInfo potionInfo;
    [SerializeField] private InventoryItemInfo _info;

    public HealthPotion(PotionInfo potionInfo, InventoryItemInfo invInfo)
    {
        this.potionInfo = potionInfo;
        this._info = invInfo;
    }
}
