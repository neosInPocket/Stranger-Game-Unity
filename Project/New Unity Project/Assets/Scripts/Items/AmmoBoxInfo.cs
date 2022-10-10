using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoBoxInfo", menuName = "Gameplay/New AmmoBoxInfo")]
public class AmmoBoxInfo : ScriptableObject
{
    [SerializeField] private int _ammoAmount;
    public int ammoAmount => _ammoAmount;
}