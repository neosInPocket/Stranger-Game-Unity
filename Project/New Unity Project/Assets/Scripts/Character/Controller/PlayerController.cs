using System;
using Mono.CompilerServices.SymbolWriter;
using TMPro.EditorUtilities;
using UnityEngine.EventSystems;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [SerializeField]  private float speed = 5;
    private Animator animator;
    private bool isInInventory;

    [SerializeField] private GameObject _uiInventory;
    private Inventory inventory;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public void Move()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }

        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

    }
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        inventory = _uiInventory.GetComponent<UIInventory>().inventory;
        inventory.OnDrop += OnDrop;
    }

    private void OnDrop(IInventoryItem obj)
    {
        Instantiate(obj.prefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_uiInventory.activeSelf)
            {
                _uiInventory.SetActive(false);
                isInInventory = false;
            }
            else
            {
                _uiInventory.SetActive(true);
                isInInventory = true;
            }
        }

        if (isInInventory)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }
        Move();
    }
}
