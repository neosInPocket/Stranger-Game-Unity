using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundBossAttack : MonoBehaviour
{
    Quaternion rotationZ;

    private void FixedUpdate()
    {
        RotateBossAround();
    }

    public void RotateBossAround()
    {
        rotationZ = Quaternion.AngleAxis(1, new Vector3(0, 0, 1));
        transform.rotation *= rotationZ;
    }
}
