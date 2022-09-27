using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    private Type itemType;
    private IInventoryItem itemInRange;
    private Player parent;

    void Awake()
    {
        parent = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        itemInRange = collider.GetComponents<MonoBehaviour>()
            .Where((x) => x.GetType().GetInterface("IInventoryItem") != null).FirstOrDefault() as IInventoryItem;
    }

    void OnTriggerExit2D()
    {
        itemInRange = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && itemInRange != null)
        {
                parent.inventory.TryToAdd(itemInRange);
        }
    }
}
