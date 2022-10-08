using UnityEngine;

[CreateAssetMenu(fileName = "GunStockInfo", menuName = "Gameplay/Items/New GunStockInfo")]
public class GunStockInfo : ScriptableObject
{
    [SerializeField]
    private float _reloadTimeDecrease;

    [SerializeField] private float _accuracyIncrease;

    public float reloadTimeDecrease => this._reloadTimeDecrease;
    public float accuracyIncrease => this._accuracyIncrease;
}