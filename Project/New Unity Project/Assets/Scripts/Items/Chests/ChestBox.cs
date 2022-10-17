using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    [SerializeField] private ChestBoxInfo chestInfo;
    [SerializeField] private Sprite openedChestSprite;
    public bool isOpened { get; private set; }
    public AudioSource audioSource;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Open()
    {
        audioSource.Play();
        var counter = 0;
        var firstItemPosition = transform;

        while (counter != 3)
        {
            foreach (var itemGO in chestInfo.items)
            {
                var item = itemGO.GetComponent<IInventoryItem>();
                if (Random.Range(0, 1f) <= item.info.dropChance)
                {
                    Instantiate(itemGO, transform.position + new Vector3(-1.8f + (counter + 1), -1f, 0), Quaternion.identity);
                    counter++;
                }

                if (counter == 3)
                {
                    break;
                }
            }
        }

        var spriteRenderer = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = openedChestSprite;
        isOpened = true;
    }
}
