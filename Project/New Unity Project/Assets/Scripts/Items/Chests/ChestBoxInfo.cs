using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestBoxInfo", menuName = "Gameplay/New ChestBoxInfo")]
public class ChestBoxInfo : ScriptableObject
{
    [SerializeField] private GameObject[] items;
}
