﻿using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/New InventoryItemInfo")]
public class InventoryItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _spriteIcon;
    [SerializeField] private GameObject _worldObject;
    [SerializeField] private InventoryItemType _type;
    [SerializeField] private int _value;
    [SerializeField] private float _dropChance;

    public string id => _id;
    public string title => _title;
    public string description => _description;
    public Sprite spriteIcon => _spriteIcon;
    public GameObject handlingSpriteIcon => _worldObject;
    public InventoryItemType type => _type;
    public int value => _value;
    public float dropChance => _dropChance;
}
