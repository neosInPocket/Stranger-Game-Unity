using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour, IInventoryItem
{
    public bool isEquiped { get; set; }
    public InventoryItemType type => _type;
    public ItemGenericType genericType => _genericType;
    public ScriptableObject itemInfo => _itemInfo;
    public InventoryItemInfo info => _info;
    public GameObject prefab => _prefab;
    [SerializeField] private ScriptableObject _itemInfo;
    [SerializeField] private InventoryItemInfo _info;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private InventoryItemType _type;
    [SerializeField] private ItemGenericType _genericType;

    public abstract void Use();
}
