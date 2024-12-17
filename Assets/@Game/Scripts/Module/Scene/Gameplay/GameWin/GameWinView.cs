using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameWin
{
    public class GameWinView : BaseView
    {
        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _collectedPuzzleCountText;
        [SerializeField] private TMP_Text _hiddenObjectCountText;
        [SerializeField] private TMP_Text _timeCompletionText;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private UnityEvent _onGameWin;

        public TMP_Text KillCountText => _killCountText;
        public TMP_Text CollectedPuzzleCountText => _collectedPuzzleCountText;
        public TMP_Text HiddenObjectCountText => _hiddenObjectCountText;
        public TMP_Text TimeCompletionText => _timeCompletionText;
        public UnityEvent OnGameWin => _onGameWin;

        public void SetCallbacks(UnityAction onMainMenu, UnityAction onContinue)
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(onMainMenu);
            _playAgainButton.onClick.RemoveAllListeners();
            _playAgainButton.onClick.AddListener(onContinue);
        }
    }
}