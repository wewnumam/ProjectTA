using Agate.MVC.Core;
using NaughtyAttributes;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragablePuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public RectTransform rectTransform;
    public RectTransform correctPlace;
    [ReadOnly] public bool isOnPlace;
    private Vector2 originalPosition;
    public UnityEvent onPlace;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isOnPlace)
            return;

        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isOnPlace)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOnPlace)
            return;

        rectTransform.anchoredPosition = originalPosition;

        if (IsOverCorrectPlace())
        {
            onPlace?.Invoke();
            rectTransform.anchoredPosition = correctPlace.anchoredPosition;
            isOnPlace = true;
        }
    }

    private bool IsOverCorrectPlace()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(correctPlace, Input.mousePosition, canvas.worldCamera);
    }
}
