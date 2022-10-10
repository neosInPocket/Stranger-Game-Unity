using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; }
    private UIInventorySlot[] uiSlots;
    [SerializeField] private GameObject textInfoHolder;
    [SerializeField] private GameObject textNameHolder;
    [SerializeField] private GameObject textNotification;
    [SerializeField] private Player _handler;
    private TMP_Text tmpTextInfo;
    private TMP_Text tmpTextName;
    private GameObject _activeSlot;
    
    public Player handler => _handler;
    public void AwakeInventory()
    {
        inventory = new Inventory(19);
        inventory.OnInventoryChanged += OnInventoryChanged;
        uiSlots = GetComponentsInChildren<UIInventorySlot>();
        SetupInventory();
        gameObject.SetActive(false);
        tmpTextInfo = textInfoHolder.GetComponentInChildren<TMP_Text>();
        tmpTextName = textNameHolder.GetComponentInChildren<TMP_Text>();
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
    }

    public void ShowItemInfo(IInventoryItem item, GameObject uiSlot)
    {
        foreach (var slot in uiSlots)
        {
            slot.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }
        tmpTextInfo.text = item.info.description;
        tmpTextName.text = item.info.title;
        _activeSlot = uiSlot;
    }

    void OnDisable()
    {
        if (_activeSlot == null)
        {
            return;
        }
        _activeSlot.GetComponent<Image>().color = new Color(255, 255, 255);
        tmpTextInfo.text = "свойства";
        tmpTextName.text = "название";
        _activeSlot = null;
    }

    public void Refresh()
    {
        for (int i = 0; i < inventory.capacity; i++)
        {

            uiSlots[i].Refresh();
        }
    }

    public IEnumerator ShowNotification(string text)
    {
        textNotification.GetComponentInChildren<TMP_Text>().text = text;
        textNotification.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        textNotification.gameObject.SetActive(false);
    }
    
}


