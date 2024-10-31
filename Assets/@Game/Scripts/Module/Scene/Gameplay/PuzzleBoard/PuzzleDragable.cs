using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleDragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [ReadOnly] public bool isActive;

        public RectTransform targetPosition; // Reference to the target position (TargetImage's RectTransform)
        private RectTransform draggableRect; // Reference to the draggable image's RectTransform
        private Canvas canvas;               // Reference to the canvas
        private CanvasGroup canvasGroup;

        public UnityAction<PuzzleDragable> onPlace;
        public UnityEvent onActive;

        void Start()
        {
            draggableRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();

            // Ensure a CanvasGroup is attached to the DraggableImage
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public void SetPuzzleDragableActive()
        {
            isActive = true;
            onActive?.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isActive)
                return;

            canvasGroup.blocksRaycasts = false;

            // You can add additional functionality if needed when dragging starts
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isActive)
                return;

            // Move the image with the drag
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                draggableRect.anchoredPosition = localPoint;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isActive)
                return;

            canvasGroup.blocksRaycasts = true;

            // Get the target's boundaries
            Vector2 targetMin = targetPosition.anchoredPosition - targetPosition.sizeDelta / 2;
            Vector2 targetMax = targetPosition.anchoredPosition + targetPosition.sizeDelta / 2;

            // Get the draggable's current center position
            Vector2 draggableCenter = draggableRect.anchoredPosition;

            // Check if the draggable image's center is within the target bounds
            if (draggableCenter.x > targetMin.x && draggableCenter.x < targetMax.x &&
                draggableCenter.y > targetMin.y && draggableCenter.y < targetMax.y)
            {
                // Snap to the target position if within bounds
                draggableRect.anchoredPosition = targetPosition.anchoredPosition;
                isActive = false;
                onPlace?.Invoke(this);
            }
        }
    }
}