using UnityEngine;

[CreateAssetMenu(fileName = "ExtendedMagInfo", menuName = "Gameplay/Items/New ExtendedMagInfo")]
public class ExtendedMagInfo : ScriptableObject
{
    [SerializeField]
    private int _bulletsAddition;

    public int bulletsAddition => this._bulletsAddition;
}