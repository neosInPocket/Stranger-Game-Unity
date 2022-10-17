using System;
using System.Security.Cryptography;
using Mono.CompilerServices.SymbolWriter;
using TMPro.EditorUtilities;
using TMPro.Examples;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [SerializeField]  private float speed = 5;
    private Animator animator;
    private bool isInInventory;

    [SerializeField] private GameObject _uiInventory;
    [SerializeField] private AnimatorController _playerWeaponAnimator;
    [SerializeField] private AnimatorController _playerAnimator;
    [SerializeField] private AnimatorController _reloadingAnimator;
    [SerializeField] private GameObject _rotatePoint;
    [SerializeField] private GunInfoRenderer _gunInfoRenderer;
    private GameObject _gunInstance;
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
        var gun = obj as GunWeapon;
        var gunInstance = _gunInstance.GetComponent<GunWeapon>();
        gun.AmmoAmount = gunInstance.AmmoAmount;
        gun.currentMagazineAmmo = gunInstance.currentMagazineAmmo;
        _gunInfoRenderer.DestroyInfo();
        Destroy(_gunInstance);

        inventory.playerGun = null;
    }

    private void OnGunSet(GunWeapon gunItem)
    {
        GetComponent<Animator>().runtimeAnimatorController = _playerWeaponAnimator;

        var rotatePos = _rotatePoint.transform.position;
        var shiftedPos = new Vector2(rotatePos.x + .7f, rotatePos.y);

        _gunInstance = Instantiate(gunItem.info.handlingSpriteIcon, shiftedPos, Quaternion.identity);
        var gunWeapon = _gunInstance.GetComponent<GunWeapon>();
        GetComponent<Player>().weapon = _gunInstance.gameObject.GetComponent<GunWeapon>();

        gunWeapon.GetComponent<GunWeapon>().isEquiped = true;
        gunWeapon.AmmoAmount = gunItem.AmmoAmount;
        gunWeapon.currentMagazineAmmo = gunItem.currentMagazineAmmo;
        _gunInstance.transform.parent = _rotatePoint.transform;

        _gunInstance.transform.rotation = Quaternion.identity;
        _gunInstance.transform.localScale = Vector3.one;

        _gunInfoRenderer.AwakeInfo(_gunInstance.GetComponent<GunWeapon>());

        inventory.playerGun = _gunInstance.GetComponent<GunWeapon>();
        inventory.RefreshAttachments();

        inventory.playerGun.OnReload += WeaponOnReload;
        inventory.playerGun.OnReloaded += WeaponOnReloaded;
    }

    private void WeaponOnReloaded(GunWeapon weapon)
    {
        GetComponent<Animator>().runtimeAnimatorController = _playerWeaponAnimator;
    }
    private void WeaponOnReload(GunWeapon weapon)
    {
        GetComponent<Animator>().runtimeAnimatorController = _reloadingAnimator;
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
                try
                {
                    _gunInstance?.GetComponent<GunWeapon>().BlockFire(false);
                }
                catch
                {

                }
            }
            else
            {
                _uiInventory.SetActive(true);
                _uiInventory.gameObject.GetComponent<UIInventory>().Refresh();
                try
                {
                    _gunInstance?.GetComponent<GunWeapon>().BlockFire(true);
                }
                catch
                {

                }
            }
        }

        if (isInInventory || transform.GetComponent<Player>().isDead)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }
        Move();
    }
}
