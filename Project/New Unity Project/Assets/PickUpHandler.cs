using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Video;
using Vector2 = UnityEngine.Vector2;

public class PickUpHandler : MonoBehaviour
{
    private Type itemType;
    private IInventoryItem itemInRange;
    private List<IInventoryItem> itemsInRange;
    private List<Collider2D> colliders;
    private Collider2D activeCollider;
    [SerializeField] private GameObject pickUpPref;
    private Player parent;

    void Awake()
    {
        parent = GetComponentInParent<Player>();
        itemsInRange = new List<IInventoryItem>();
        colliders = new List<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        itemInRange = collider.GetComponent<IInventoryItem>();
        activeCollider = collider;


        if (itemInRange != null)
        {
            colliders.Add(collider);
            itemsInRange.Add(itemInRange);
            pickUpPref.SetActive(true);
        }
    }

    void OnTriggerStay2D()
    {
        Dictionary<Collider2D, float> distances = new Dictionary<Collider2D, float>();
        foreach (var collider in colliders)
        {
            distances.Add(collider, Vector2.Distance(parent.transform.position, collider.transform.position));
        }

        var closestCollider = distances
            .Aggregate((l, r) => l.Value < r.Value ? l : r);
        foreach (var collider in colliders)
        {
            collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        }

        closestCollider.Key.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        itemInRange = closestCollider.Key.gameObject.GetComponent<IInventoryItem>();
        activeCollider = closestCollider.Key;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var itemExited = collider.GetComponent<IInventoryItem>();

        if (itemExited != null)
        {
            colliders.Remove(collider);
            itemsInRange.Remove(itemExited);
        }

        if (itemsInRange.Count == 0)
        {
            pickUpPref.SetActive(false);
            itemInRange = null;
            activeCollider = null;
        }
        collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && itemInRange != null)
        {
            Debug.Log("Clicked E");

            bool result = parent.inventory.TryToAdd(itemInRange);
            if (result)
            {
                var item = activeCollider.gameObject;
                Destroy(activeCollider.gameObject);
            }
        }
    }
}
