using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserSight : AttachmentItem
{
    public void Attach(GunWeapon weapon, bool action)
    {
        weapon.GetComponentInChildren<LineRenderer>().enabled = action;
        weapon.GetComponentInChildren<Light2D>().enabled = action;
    }
}
