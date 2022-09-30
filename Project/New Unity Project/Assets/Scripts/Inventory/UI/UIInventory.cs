using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; } = new Inventory(19);
    private UIInventorySlot[] uiSlots;
    [SerializeField] private GameObject textHolder;
    private TMP_Text tmpText;

    public void AwakeInventory()
    {
        inventory.OnInventoryChanged += OnInventoryChanged;
        uiSlots = GetComponentsInChildren<UIInventorySlot>();
        SetupInventory();
        gameObject.SetActive(false);
        tmpText = textHolder.GetComponent<TMP_Text>();
    }

    private void OnInventoryChanged()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Refresh();
            uiSlots[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    private void SetupInventory()
    {
        var slots = inventory.GetAllSlots();
        for (int i = 0; i < inventory.capacity; i++)
        {
            var slot = slots[i];
            slot.itemType = uiSlots[i].type;
            var uiSlot = uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
        Debug.Log("Refreshed");
    }

    public void ShowItemInfo(IInventoryItem item)
    {
        tmpText.text = item.info.description;
    }
    
}


