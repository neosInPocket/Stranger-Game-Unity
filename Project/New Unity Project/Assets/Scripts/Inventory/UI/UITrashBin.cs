using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class UITrashBin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    private Sprite _defaultSprite;
    [SerializeField] private Sprite _onEnterImage;
    private Image _activeImage;
    private RectTransform _rectTransform;
    private UIInventory _uiInventory;

    void Start()
    {
        _activeImage = GetComponent<Image>();
        _defaultSprite = _activeImage.sprite;
        _rectTransform = GetComponent<RectTransform>();
        _uiInventory = GetComponentInParent<UIInventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var uiItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        if (uiItem != null)
        {
            Destroy(uiItem.item.prefab.gameObject);
            _uiInventory.inventory.Remove(uiItem.item);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _activeImage.sprite = _onEnterImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _activeImage.sprite = _defaultSprite;
    }
}
