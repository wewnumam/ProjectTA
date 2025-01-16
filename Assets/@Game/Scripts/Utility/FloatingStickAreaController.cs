using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace ProjectTA.Utility
{
    public class FloatingStickAreaController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        GameObject _joystick;
        [SerializeField]
        UnityEvent<bool> _onDrag;


        OnScreenStick _screenStick;
        RectTransform _mainRect;
        RectTransform _joystickRect;

        private void Awake()
        {
            _screenStick = _joystick.GetComponentInChildren<OnScreenStick>();
            _mainRect = GetComponent<RectTransform>();
            _joystickRect = _joystick.GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _onDrag?.Invoke(true);
            ExecuteEvents.dragHandler(_screenStick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 localPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainRect, eventData.position, eventData.pressEventCamera, out localPosition);

            _joystickRect.anchoredPosition = localPosition;

            ExecuteEvents.pointerDownHandler(_joystick.GetComponentInChildren<OnScreenStick>(), eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ExecuteEvents.pointerUpHandler(_screenStick, eventData);
            _onDrag?.Invoke(false);
        }
    }
}