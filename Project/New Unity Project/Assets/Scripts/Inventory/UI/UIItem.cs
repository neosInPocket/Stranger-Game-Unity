using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsLastSibling();
        slotTransform.gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            var slot = eventData.pointerDrag.GetComponentInParent<UIInventorySlot>().slot;
                eventData.pointerDrag.GetComponentInParent<UIInventory>().inventory.Drop(slot);
        }
        _rectTransform.parent.gameObject.GetComponent<Image>().color = new Color(255,255,255);
    }
}
