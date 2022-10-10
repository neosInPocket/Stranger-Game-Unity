using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestBoxInfo", menuName = "Gameplay/New ChestBoxInfo")]
public class ChestBoxInfo : ScriptableObject
{
    [SerializeField] private GameObject[] _items;
    public GameObject[] items => _items;
}
