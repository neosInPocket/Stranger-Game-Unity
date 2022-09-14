using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterController
{
    public float Speed { get; set; } = 3;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Move()
    {
        var direction = Vector2.zero;

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
        GetComponent<Rigidbody2D>().velocity = Speed * direction;
        Animate(direction);
    }

    public void Animate(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
    }
    protected void Update()
    {
        Move();
    }
}
