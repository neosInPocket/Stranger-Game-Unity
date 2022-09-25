using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Inventory.Abstract;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{

    public IInventoryItem item { get; private set; }
    public void Refresh(IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            Cleanup();
            return;
        }

        gameObject.SetActive(true);
        item = slot.item;
        gameObject.GetComponent<Image>().sprite = item.info.spriteIcon;

    }

    private void Cleanup()
    {
        gameObject.SetActive(false);
    }
}
