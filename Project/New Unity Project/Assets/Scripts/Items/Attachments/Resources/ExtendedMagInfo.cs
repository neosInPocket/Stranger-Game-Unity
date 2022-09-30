using UnityEngine;

[CreateAssetMenu(fileName = "ExtendedMagInfo", menuName = "Gameplay/Items/New ExtendedMagInfo")]
public class ExtendedMagInfo : ScriptableObject
{
    [SerializeField]
    private float _bulletsAddition;

    public float bulletsAddition => this._bulletsAddition;
}