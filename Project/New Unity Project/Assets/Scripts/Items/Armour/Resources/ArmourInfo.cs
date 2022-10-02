using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmourInfo", menuName = "Gameplay/New ArmourInfo")]
public class ArmourInfo : ScriptableObject
{
    [SerializeField]
    private int _armorPoints;

    public int armorPoints => this._armorPoints;
}