using UnityEngine.Rendering;

public class ArmourItem : InventoryItem
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