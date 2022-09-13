using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] 
    public float speed;

    private Animator animator;

    protected Vector2 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Animate();
    }

    public void Animate()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
    }
}
