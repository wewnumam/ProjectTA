using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureView : BaseView
    {
        [SerializeField] Toggle activateJoystickToggle;

        public void SetCallbacks(UnityAction<bool> onActivateJoystick)
        {
            activateJoystickToggle.onValueChanged.RemoveAllListeners();
            activateJoystickToggle.onValueChanged.AddListener(onActivateJoystick);
        }
    }
}