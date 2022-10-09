using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : UISlot, IPointerClickHandler
{
    [SerializeField] private UIInventoryItem _uiItem;
    [SerializeField] private InventoryItemType _type;
    public IInventorySlot slot { get; private set; }
    public UIInventory _uiInventory { get; private set; }
    public InventoryItemType type => _type;
    public UIInventoryItem uiItem { get; private set; }

    void Start()
    {
        _uiInventory = GetComponentInParent<UIInventory>();
    }

    public void SetSlot(IInventorySlot newSlot)
    {
        slot = newSlot;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;

        if (otherSlotUI._type != _type)
        {
            return;
        }
        var inventory = _uiInventory.inventory;
        otherItemUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

        inventory.TransitFromSlotToSlot(otherSlot, slot);
        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (slot != null)
        {
            _uiItem.Refresh(slot);
            _uiItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            _uiItem.transform.localPosition = Vector2.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            try
            {
                _uiItem.item.Use(_uiInventory.handler);
            }
            catch (NotImplementedException ex)
            {

            }
        }
        
        if (_uiItem.item != null)
        {
            _uiInventory.ShowItemInfo(_uiItem.item, eventData.pointerClick.gameObject);
            eventData.pointerClick.GetComponent<Image>().color = new Color(0, 255, 255);
        }
        
    }
}
