using NaughtyAttributes;
using ProjectTA.Module.CollectibleData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleDragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [ReadOnly] public SOCollectibleData CollectibleData;
        [ReadOnly] public bool isActive;

        public RectTransform targetPosition; // Reference to the target position (TargetImage's RectTransform)
        public Canvas Canvas;               // Reference to the canvas
        [ReadOnly] public RectTransform DraggableRect; // Reference to the draggable image's RectTransform
        [ReadOnly] public CanvasGroup CanvasGroup;
        [ReadOnly] public Vector2 InitialAnchoredPosition;

        public UnityAction<PuzzleDragable> onPlace;
        public UnityEvent onActive;

        void Start()
        {
            // Use TryGetComponent to avoid allocation when no component is found
            if (!TryGetComponent(out DraggableRect))
            {
                Debug.LogError("RectTransform not found on this GameObject.");
            }

            if (Canvas == null)
            {
                Debug.LogError("Canvas is null reference");
            }

            // Use TryGetComponent to avoid allocation when no CanvasGroup is found
            if (!TryGetComponent(out CanvasGroup))
            {
                // If no CanvasGroup is found, add one
                CanvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            // Cache the initial anchored position
            InitialAnchoredPosition = DraggableRect.anchoredPosition;
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

            CanvasGroup.blocksRaycasts = false;

            // You can add additional functionality if needed when dragging starts
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isActive)
                return;

            // Move the image with the drag
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)Canvas.transform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                DraggableRect.anchoredPosition = localPoint;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isActive)
                return;

            CanvasGroup.blocksRaycasts = true;

            // Get the target's boundaries
            Vector2 targetMin = targetPosition.anchoredPosition - targetPosition.sizeDelta / 2;
            Vector2 targetMax = targetPosition.anchoredPosition + targetPosition.sizeDelta / 2;

            // Get the draggable's current center position
            Vector2 draggableCenter = DraggableRect.anchoredPosition;

            // Check if the draggable image's center is within the target bounds
            if (draggableCenter.x > targetMin.x && draggableCenter.x < targetMax.x &&
                draggableCenter.y > targetMin.y && draggableCenter.y < targetMax.y)
            {
                // Snap to the target position if within bounds
                DraggableRect.anchoredPosition = targetPosition.anchoredPosition;
                isActive = false;
                onPlace?.Invoke(this);
            }
            else
            {
                DraggableRect.anchoredPosition = InitialAnchoredPosition;
            }
        }
    }
}