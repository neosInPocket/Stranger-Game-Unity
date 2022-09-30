using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol : GunWeapon
{
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
