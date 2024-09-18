using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureView : BaseView
    {
        [SerializeField] Toggle activateJoystickToggle;
        [SerializeField] Button gameWinButton;
        [SerializeField] Button gameOverButton;
        
        public int healthAmount = 1;
        [SerializeField] Button addHealthButton;
        [SerializeField] Button subtractHealthButton;

        public void SetActivateJoystickCallbacks(UnityAction<bool> onActivateJoystick)
        {
            activateJoystickToggle.onValueChanged.RemoveAllListeners();
            activateJoystickToggle.onValueChanged.AddListener(onActivateJoystick);
        }

        public void SetGameStateCallbacks(UnityAction onGameWin, UnityAction onGameOver)
        {
            gameWinButton.onClick.RemoveAllListeners();
            gameWinButton.onClick.AddListener(onGameWin);
            gameOverButton.onClick.RemoveAllListeners();
            gameOverButton.onClick.AddListener(onGameOver);
        }

        public void SetHealthCallbacks(UnityAction onAddHealth, UnityAction onSubtractHealth)
        {
            addHealthButton.onClick.RemoveAllListeners();
            addHealthButton.onClick.AddListener(onAddHealth);
            subtractHealthButton.onClick.RemoveAllListeners();
            subtractHealthButton.onClick.AddListener(onSubtractHealth);
        }
    }
}