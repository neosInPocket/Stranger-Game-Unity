using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private TMP_Text textItemInfo;
    [SerializeField] private Image itemInfoImage;
    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
        itemsInRange = new List<IInventoryItem>();
        colliders = new List<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        itemInRange = collider.GetComponent<IInventoryItem>();
        if (itemInRange != null)
        {
            colliders.Add(collider);
            itemsInRange.Add(itemInRange);
            pickUpPref.SetActive(true);
        }
        else
        {
            return;
        }
        activeCollider = collider;
        textItemInfo.transform.parent.gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (itemInRange is null || collider2D.GetComponent<IInventoryItem>() is null)
        {
            return;
        }
        Dictionary<Collider2D, float> distances = new Dictionary<Collider2D, float>();
        foreach (var collider in colliders)
        {
            try
            {
                distances.Add(collider, Vector2.Distance(player.transform.position, collider.transform.position));
            }
            catch
            {
                itemInRange = null;
                return;
            }
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

        textItemInfo.text = itemInRange.info.description;
        itemInfoImage.sprite = itemInRange.info.spriteIcon;

        var itemSprite = itemInRange.info.spriteIcon;
        var sizeMultiplier = itemSprite.bounds.size.x / itemSprite.bounds.size.y;
        itemInfoImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * sizeMultiplier, 100);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (itemsInRange == null || itemInRange == null)
        {
            textItemInfo.transform.parent.gameObject.SetActive(false);
            return;
        }
        var itemExited = collider.GetComponent<IInventoryItem>();

        if (itemExited != null)
        {
            colliders.Remove(collider);
            itemsInRange.Remove(itemExited);
            collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            return;
        }

        if (itemsInRange.Count == 0)
        {
            pickUpPref.SetActive(false);
            itemInRange = null;
            activeCollider = null;
            textItemInfo.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && itemInRange != null)
        {
            var ammoItem = activeCollider.GetComponent<AmmoBox>();
            if (ammoItem && player.weapon)
            {
                player.weapon.AmmoAmount += ammoItem.ammoAmount;
                ammoItem.gameObject.SetActive(false);
            }

            bool result = player.inventory.TryToAdd(itemInRange);
            
            if (result)
            {
                var armorItem = activeCollider.GetComponent<ArmourItem>();
                if (armorItem)
                {
                    player.SetArmour(armorItem);
                }

                var gunItem = activeCollider.GetComponent<GunWeapon>();
                if (gunItem)
                {
                    player.SetWeapon(gunItem);
                    gunItem.attachments = player.inventory.attachmentItems;
                }

                var attachmentItem = activeCollider.GetComponent<AttachmentItem>();
                if (attachmentItem && player.weapon)
                {
                    player.weapon.attachments = player.inventory.attachmentItems;
                }
                activeCollider.gameObject.SetActive(false);
            }
        }

        if (activeCollider is null)
        {
            textItemInfo.transform.parent.gameObject.SetActive(false);
            pickUpPref.gameObject.SetActive(false);
        }
    }
}
