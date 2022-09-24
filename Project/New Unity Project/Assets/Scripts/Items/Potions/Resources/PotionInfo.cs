using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionInfo", menuName = "Gameplay/New PotionInfo")]
public class PotionInfo : ScriptableObject
{
    [SerializeField]
    private float _healthAmount;

    public float healthAmount => this._healthAmount;
}
