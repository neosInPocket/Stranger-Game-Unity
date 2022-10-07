using UnityEngine.Rendering;

public class GunStock : AttachmentItem
{
    public float reloadTimeDecrease
    {
        get
        {
            return (itemInfo as GunStockInfo).reloadTimeDecrease;
        }
    }
}
