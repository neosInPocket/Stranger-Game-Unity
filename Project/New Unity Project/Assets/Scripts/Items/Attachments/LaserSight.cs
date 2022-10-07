using UnityEngine;

public class LaserSight : AttachmentItem
{
    public void Attach(GunWeapon weapon, bool action)
    {
        weapon.GetComponentInChildren<LineRenderer>().enabled = action;
    }
}
