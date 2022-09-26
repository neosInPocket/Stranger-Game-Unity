using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _uiItem;

    public IInventorySlot slot { get; private set; }
    private UIInventory _uiInventory;

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
        var inventory = _uiInventory.inventory;

        inventory.TransitFromSlotToSlot(otherSlot, slot);
        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (slot != null && _uiItem != null)
        {
            _uiItem.Refresh(slot);
        }
    }
}
