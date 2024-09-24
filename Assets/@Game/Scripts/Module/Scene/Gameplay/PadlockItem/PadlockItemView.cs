using Agate.MVC.Base;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectTA.Module.PadlockItem
{
    public class PadlockItemView : BaseView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public TMP_Text label;
        [ReadOnly] public Canvas canvas;
        [ReadOnly] public RectTransform rectTransform;
        [ReadOnly] public RectTransform correctPlace;
        public UnityEvent onPlace;

        [ReadOnly] public bool isOnPlace;
        private Vector2 originalPosition;

        public void SetCallback(UnityAction onPlace)
        {
            this.onPlace.RemoveAllListeners();
            this.onPlace.AddListener(onPlace);
        }

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
            return RectTransformUtility.RectangleContainsScreenPoint(correctPlace, UnityEngine.Input.mousePosition, canvas.worldCamera);
        }
    }
}