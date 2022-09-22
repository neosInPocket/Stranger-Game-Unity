using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class UITrashBin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    private Sprite _defaultSprite;
    [SerializeField]
    private Sprite _onEnterImage;
    private Image _activeImage;
    private RectTransform _rectTransform;

    void Start()
    {
        _activeImage = GetComponent<Image>();
        _defaultSprite = _activeImage.sprite;
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
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
