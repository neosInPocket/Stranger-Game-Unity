using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmourInfo", menuName = "Gameplay/New ArmourInfo")]
public class ArmourInfo : ScriptableObject
{
    [SerializeField]
    private float _defence;

    public float defence => this._defence;
}