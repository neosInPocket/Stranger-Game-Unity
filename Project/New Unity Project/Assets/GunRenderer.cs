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
        Quaternion rotation = Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
        if (angle > 90f || angle < -90f)
        {
            rotation.y = 180f;
        }
        //else
        //{
        //    rotation.y = 0f;
        //}
        counter = rotation;
        parent.transform.rotation = rotation;
    }

    private void FlipGun()
    {
        parent.transform.Rotate(0f, 180f, 0f);
    }
}
