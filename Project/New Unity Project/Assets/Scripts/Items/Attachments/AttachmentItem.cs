using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentItem : InventoryItem
{
    public ArmourInfo armourInfo
    {
        get
        {
            return this.itemInfo as ArmourInfo;
        }
    }
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
