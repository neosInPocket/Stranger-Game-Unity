using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunInfo", menuName = "Gameplay/New GunInfo")]
public class GunInfo : ScriptableObject
{
    [SerializeField] private float _fireRate;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _ammoAmount;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isFullAuto;

    public float fireRate => this._fireRate;
    public int magazineCapacity => this._magazineCapacity;
    public float reloadTime => this._reloadTime;
    public int ammoAmount => this._ammoAmount;
    public float damage => this._damage;
    public bool isFullAuto => this._isFullAuto;
}
