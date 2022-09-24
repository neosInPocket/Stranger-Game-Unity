using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    public float offSet;

    void Update()
    {
        Vector3 diferense = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float rotateZ = Mathf.Atan2(diferense.y, diferense.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offSet);

        Vector3 LocalScale = Vector3.one;

        if (rotateZ > 90 || rotateZ < -90)
        {
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.y = +1f;
        }

        transform.localScale = LocalScale;
    }
}
