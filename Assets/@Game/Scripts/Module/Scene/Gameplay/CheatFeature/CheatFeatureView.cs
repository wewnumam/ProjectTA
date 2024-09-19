using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureView : BaseView
    {
        [SerializeField] Toggle activateJoystickToggle;
        [Header("Game State")]
        [SerializeField] Button gameWinButton;
        [SerializeField] Button gameOverButton;

        [Header("Health")]
        [SerializeField] Button addHealthButton;
        [SerializeField] Button subtractHealthButton;
        
        [Header("Mission")]
        [SerializeField] Button addCollectedPuzzlePieceCountButton;
        [SerializeField] Button subtractCollectedPuzzlePieceCountButton;
        [SerializeField] Button addKillCountButton;
        [SerializeField] Button subtractKillCountButton;


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

        public void SetMissionCallbacks(UnityAction onAddPuzzlePieceCount, UnityAction onSubtractPuzzlePieceCount, UnityAction onAddKillCount, UnityAction onSubtractKillCount)
        {
            addCollectedPuzzlePieceCountButton.onClick.RemoveAllListeners();
            addCollectedPuzzlePieceCountButton.onClick.AddListener(onAddPuzzlePieceCount);
            subtractCollectedPuzzlePieceCountButton.onClick.RemoveAllListeners();
            subtractCollectedPuzzlePieceCountButton.onClick.AddListener(onSubtractPuzzlePieceCount);
            addKillCountButton.onClick.RemoveAllListeners();
            addKillCountButton.onClick.AddListener(onAddKillCount);
            subtractKillCountButton.onClick.RemoveAllListeners();
            subtractKillCountButton.onClick.AddListener(onSubtractKillCount);
        }
    }
}