using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; } = new Inventory(12);
    private UIInventorySlot[] uiSlots;

    public void Awake()
    {
        inventory.OnInventoryChanged += OnInventoryChanged;
        uiSlots = GetComponentsInChildren<UIInventorySlot>();
        SetupInventory();
        gameObject.SetActive(false);
    }

    private void OnInventoryChanged()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Refresh();
        }
    }

    private void SetupInventory()
    {
        var slots = inventory.GetAllSlots();
        for (int i = 0; i < inventory.capacity; i++)
        {
            var slot = slots[i];
            var uiSlot = uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
        Debug.Log("Refreshed");
    }
}


