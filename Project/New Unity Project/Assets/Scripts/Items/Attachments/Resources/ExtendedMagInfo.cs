using UnityEngine;

[CreateAssetMenu(fileName = "ExtendedMagInfo", menuName = "Gameplay/Items/New ExtendedMagInfo")]
public class ExtendedMagInfo : ScriptableObject
{
    [SerializeField]
    private float _ammoMultiplier;

    public float ammoMultiplier => this._ammoMultiplier;
}