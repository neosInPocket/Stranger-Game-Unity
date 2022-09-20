using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRenderer : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Camera cam;
    [SerializeField] private Quaternion counter;
    private bool isFlipped;

    void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        Vector2 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;
        if (angle > 90f || angle < -90f)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        parent.transform.eulerAngles = new Vector3(0f, 0f, angle);
        parent.transform.localScale = localScale;
    }
}
