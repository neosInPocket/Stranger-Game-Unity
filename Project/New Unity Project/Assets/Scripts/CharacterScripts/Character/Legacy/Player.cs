using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Update()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }

        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = speed * direction;
        base.Animate();
    }
}
