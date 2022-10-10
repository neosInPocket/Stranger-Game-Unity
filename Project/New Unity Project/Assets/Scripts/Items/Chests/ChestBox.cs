using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    [SerializeField] private ChestBoxInfo chestInfo;
    [SerializeField] private Sprite openedChestSprite;
    public bool isOpened { get; private set; }

    public void Open()
    {
        var counter = 0;
        var firstItemPosition = transform;

        while (counter != 3)
        {
            foreach (var itemGO in chestInfo.items)
            {
                var item = itemGO.GetComponent<IInventoryItem>();
                if (Random.Range(0, 1f) <= item.info.dropChance)
                {
                    Instantiate(itemGO, transform.position + new Vector3(-1.4f + (counter + 1) * 0.7f, -0.5f, 0), Quaternion.identity);
                    counter++;
                }

                if (counter == 3)
                {
                    break;
                }
            }
        }

        var spriteRenderer = GetComponent<SpriteRenderer>().sprite;
        Debug.Log(spriteRenderer.rect);
        GetComponent<SpriteRenderer>().sprite = openedChestSprite;
        Debug.Log(spriteRenderer.rect);
        isOpened = true;
    }
}
