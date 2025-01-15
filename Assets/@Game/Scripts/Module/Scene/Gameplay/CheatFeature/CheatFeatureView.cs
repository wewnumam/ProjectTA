using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureView : BaseView
    {
        [Header("Utility")]
        [SerializeField] private Button _deleteSaveDataButton;
        [SerializeField] private Toggle _activateJoystickToggle;
        public Toggle ActivateJoystickToggle => _activateJoystickToggle;

        [Header("Game State")]
        [SerializeField] private Button _gameWinButton;
        [SerializeField] private Button _gameOverButton;

        [Header("Health")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _subtractHealthButton;

        [Header("Mission")]
        [SerializeField] private Button _addCollectedPuzzlePieceCountButton;
        [SerializeField] private Button _subtractCollectedPuzzlePieceCountButton;
        [SerializeField] private Button _addCollectedHiddenObjectCountButton;
        [SerializeField] private Button _subtractCollectedHiddenObjectCountButton;
        [SerializeField] private Button _addKillCountButton;
        [SerializeField] private Button _subtractKillCountButton;

        [Header("Environment")]
        [SerializeField] private Button _teleportToPuzzleButton;
        [SerializeField] private Button _teleportToCollectibleButton;

        [Header("Effects")]
        [SerializeField] private Button _blurCameraButton;
        [SerializeField] private Button _normalCameraButon;

        [Header("Countdown")]
        [SerializeField] private Button _restartCountdownButton;
        [SerializeField] private Button _resetCountdownButton;

        public void SetUtilityCallbacks(UnityAction ondeleteSaveData, UnityAction<bool> onActivateJoystick)
        {
            _deleteSaveDataButton.onClick.RemoveAllListeners();
            _deleteSaveDataButton.onClick.AddListener(ondeleteSaveData);

            ActivateJoystickToggle.onValueChanged.RemoveAllListeners();
            ActivateJoystickToggle.onValueChanged.AddListener(onActivateJoystick);
        }

        public void SetGameStateCallbacks(UnityAction onGameWin, UnityAction onGameOver)
        {
            _gameWinButton.onClick.RemoveAllListeners();
            _gameWinButton.onClick.AddListener(onGameWin);
            _gameOverButton.onClick.RemoveAllListeners();
            _gameOverButton.onClick.AddListener(onGameOver);
        }

        public void SetHealthCallbacks(UnityAction onAddHealth, UnityAction onSubtractHealth)
        {
            _addHealthButton.onClick.RemoveAllListeners();
            _addHealthButton.onClick.AddListener(onAddHealth);
            _subtractHealthButton.onClick.RemoveAllListeners();
            _subtractHealthButton.onClick.AddListener(onSubtractHealth);
        }

        public void SetMissionCallbacks(
            UnityAction onAddPuzzlePieceCount,
            UnityAction onSubtractPuzzlePieceCount,
            UnityAction onAddHiddenObjectCount,
            UnityAction onSubtractHiddenObjectCount,
            UnityAction onAddKillCount,
            UnityAction onSubtractKillCount)
        {
            _addCollectedPuzzlePieceCountButton.onClick.RemoveAllListeners();
            _addCollectedPuzzlePieceCountButton.onClick.AddListener(onAddPuzzlePieceCount);
            _subtractCollectedPuzzlePieceCountButton.onClick.RemoveAllListeners();
            _subtractCollectedPuzzlePieceCountButton.onClick.AddListener(onSubtractPuzzlePieceCount);
            _addCollectedHiddenObjectCountButton.onClick.RemoveAllListeners();
            _addCollectedHiddenObjectCountButton.onClick.AddListener(onAddHiddenObjectCount);
            _subtractCollectedHiddenObjectCountButton.onClick.RemoveAllListeners();
            _subtractCollectedHiddenObjectCountButton.onClick.AddListener(onSubtractHiddenObjectCount);
            _addKillCountButton.onClick.RemoveAllListeners();
            _addKillCountButton.onClick.AddListener(onAddKillCount);
            _subtractKillCountButton.onClick.RemoveAllListeners();
            _subtractKillCountButton.onClick.AddListener(onSubtractKillCount);
        }

        public void SetEnvironmentCallbacks(UnityAction onTeleportToPuzzle, UnityAction onTeleportToCollectible)
        {
            _teleportToPuzzleButton.onClick.RemoveAllListeners();
            _teleportToPuzzleButton.onClick.AddListener(onTeleportToPuzzle);
            _teleportToCollectibleButton.onClick.RemoveAllListeners();
            _teleportToCollectibleButton.onClick.AddListener(onTeleportToCollectible);
        }

        public void SetEffectsCallbacks(UnityAction onBlurCamera, UnityAction onNormalCamera)
        {
            _blurCameraButton.onClick.RemoveAllListeners();
            _blurCameraButton.onClick.AddListener(onBlurCamera);
            _normalCameraButon.onClick.RemoveAllListeners();
            _normalCameraButon.onClick.AddListener(onNormalCamera);
        }

        public void SetCountdownCallbacks(UnityAction onRestartCountdown, UnityAction onResetCountdown)
        {
            _restartCountdownButton.onClick.RemoveAllListeners();
            _restartCountdownButton.onClick.AddListener(onRestartCountdown);
            _resetCountdownButton.onClick.RemoveAllListeners();
            _resetCountdownButton.onClick.AddListener(onResetCountdown);
        }
    }
}