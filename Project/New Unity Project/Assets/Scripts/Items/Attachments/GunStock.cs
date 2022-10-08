using UnityEngine.Rendering;

public class GunStock : AttachmentItem
{
    public float reloadTimeDecrease => (itemInfo as GunStockInfo).reloadTimeDecrease;

    public float accuracyIncrease => (itemInfo as GunStockInfo).accuracyIncrease;
}
