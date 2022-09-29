using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour, IInventoryItem
{
    public bool isEquiped { get; set; }
    public Type type => this.GetType();
    public ScriptableObject itemInfo => _itemInfo;
    public InventoryItemInfo info => _info;
    public GameObject prefab => _prefab;
    [SerializeField] private ScriptableObject _itemInfo;
    [SerializeField] private InventoryItemInfo _info;
    [SerializeField] private GameObject _prefab;

    public abstract void Use();
}
