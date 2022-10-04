using System;
using System.Security.Cryptography;
using Mono.CompilerServices.SymbolWriter;
using TMPro.EditorUtilities;
using TMPro.Examples;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.EventSystems;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [SerializeField]  private float speed = 5;
    private Animator animator;
    private bool isInInventory;

    [SerializeField] private GameObject _uiInventory;
    [SerializeField] private AnimatorController _playerWeaponAnimator;
    [SerializeField] private AnimatorController _playerAnimator;
    [SerializeField] private GameObject _rotatePoint;
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
        inventory.OnGunRemove += OnGunRemove;
        gameObject.GetComponent<Player>().OnGunSet += OnGunSet;
    }

    private void OnGunRemove(IInventoryItem obj)
    {
        GetComponent<Animator>().runtimeAnimatorController = _playerAnimator;
        Destroy(_rotatePoint.transform.GetChild(0).gameObject); 
    }

    private void OnGunSet()
    {
        var gunItem = inventory.GetSlot(InventoryItemType.Gun).item;
        GetComponent<Animator>().runtimeAnimatorController = _playerWeaponAnimator;

        var rotatePos = _rotatePoint.transform.position;
        Debug.Log(rotatePos);
        var shiftedPos = new Vector2(rotatePos.x + .7f, rotatePos.y);

        var instance = Instantiate(gunItem.info.handlingSpriteIcon, shiftedPos, Quaternion.identity);
        instance.transform.parent = _rotatePoint.transform;

        instance.transform.rotation = Quaternion.identity;
        instance.transform.localScale = Vector3.one;
    }

    private void OnDrop(IInventoryItem obj)
    {
        if (obj != null)
        {
            obj.prefab.gameObject.SetActive(true);
            obj.prefab.transform.position = transform.position;
            _rotatePoint.transform.localScale = new Vector3(1f, 1f, 1f);
            _rotatePoint.transform.rotation = Quaternion.identity;
        }
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
