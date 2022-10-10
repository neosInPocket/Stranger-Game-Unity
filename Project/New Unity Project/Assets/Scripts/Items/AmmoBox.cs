using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : InventoryItem
{
    public int ammoAmount
    {
        get
        {
            return (itemInfo as AmmoBoxInfo).ammoAmount;
        }
    }

    public override void Use(Player player)
    {
        throw new System.NotImplementedException();
    }
}
