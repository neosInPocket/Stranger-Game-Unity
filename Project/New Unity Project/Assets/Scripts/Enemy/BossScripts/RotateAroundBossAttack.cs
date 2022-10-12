using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundBossAttack : MonoBehaviour
{
    [Header("Центр вращения обьекта")]
    [SerializeField] private GameObject _center;

    [Header("Ось вращения")]
    [SerializeField] private Vector3 _axis;

    private void Update()
    {
        RotateBossAround();
    }

    public void RotateBossAround()
    {
        transform.RotateAround(_center.transform.position, _axis, 0.1f);
    }
}
