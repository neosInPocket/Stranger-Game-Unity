using System;
using System.Collections;
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
    [SerializeField]  private float _speed = 5;

    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _uiInventory;

    [SerializeField] private AnimatorController _playerWeaponAnimator;

    [SerializeField] private AnimatorController _playerAnimator;

    [SerializeField] private AnimatorController _reloadingAnimator;

    [SerializeField] private GameObject _rotatePoint;

    [SerializeField] private GunInfoRenderer _gunInfoRenderer;
    private GameObject _gunInstance;

    private Inventory _inventory;

    private bool _isInInventory;

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
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

        GetComponent<Rigidbody2D>().velocity = direction * _speed;

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            _animator.SetLayerWeight(1, 0);
        }

    }
    public void AnimateMovement(Vector2 direction)
    {
        _animator.SetLayerWeight(1, 1);

        _animator.SetFloat("x", direction.x);

        _animator.SetFloat("y", direction.y);
    }

    void Start()
    {
        _animator = GetComponent<Animator>();

        _inventory = _uiInventory.GetComponent<UIInventory>().inventory;

        _inventory.OnDrop += OnDrop;

        _inventory.OnGunRemove += OnGunRemove;

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

        _inventory.playerGun = null;
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

        _inventory.playerGun = _gunInstance.GetComponent<GunWeapon>();

        _inventory.RefreshAttachments();

        _inventory.playerGun.OnReload += WeaponOnReload;
    }
    private void WeaponOnReload(GunWeapon weapon)
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        var weapon = GetComponentInChildren<GunWeapon>().gameObject;

        weapon.SetActive(false);

        GetComponent<Animator>().runtimeAnimatorController = _reloadingAnimator;

        yield return new WaitForSeconds(_gunInstance.GetComponent<GunWeapon>().ReloadTime);

        weapon.SetActive(true);

        GetComponent<Animator>().runtimeAnimatorController = _playerWeaponAnimator;
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
                    Debug.Log("Нет компонента GunWeapon");
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
                    Debug.Log("Нет компонента GunWeapon");
                }
            }
        }

        if (_isInInventory || transform.GetComponent<Player>().isDead)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            return;
        }

        Move();
    }
}
