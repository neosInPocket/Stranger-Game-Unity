using UnityEngine;

[CreateAssetMenu(fileName = "GunStockInfo", menuName = "Gameplay/Items/New GunStockInfo")]
public class GunStockInfo : ScriptableObject
{
    [SerializeField]
    private float _reloadTimeDecrease;

    public float reloadTimeDecrease => this._reloadTimeDecrease;
}