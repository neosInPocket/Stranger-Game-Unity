using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    public static List<MoveToMouse> moveToMice = new List<MoveToMouse>();
    public float speed = 5f;

    private Vector3 target;

    private bool selected;
    private bool looksRight = true;


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveToMice.Add(this);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && selected)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            animator.Play("Run");
            animator.Play("IdelPlayerRun");


            if (target.x > transform.position.x && !looksRight)
            {
                Flip();
            }
            if(target.x < transform.position.x && looksRight)
            {
                Flip();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            animator.Play("Idel");
            animator.Play("IdelPlayer");
        }
    }

    private void OnMouseDown()
    {
        selected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        foreach (MoveToMouse obj in moveToMice)
        {
            if (obj != this)
            {
                obj.selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void Flip()
    {
        looksRight = !looksRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
