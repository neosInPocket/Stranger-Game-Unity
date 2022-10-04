using UnityEngine;

public class LaserSight : AttachmentItem
{
    public void Attach(GunWeapon weapon, bool action)
    {
        weapon.GetComponentInParent<LineRenderer>().enabled = action;
    }
}
