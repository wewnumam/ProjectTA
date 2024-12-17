using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GamePause
{
    public class GamePauseView : BaseView
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private UnityEvent _onGamePause;
        [SerializeField] private UnityEvent _onGameResume;

        public UnityEvent OnGamePause => _onGamePause;
        public UnityEvent OnGameResume => _onGameResume;

        public void SetCallbacks(UnityAction onPause, UnityAction onResume, UnityAction onMainMenu, UnityAction onPlayAgain)
        {
            _pauseButton.onClick.RemoveAllListeners();
            _pauseButton.onClick.AddListener(onPause);

            _resumeButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.AddListener(onResume);

            _mainMenuButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.AddListener(onMainMenu);

            _playAgainButton.onClick.RemoveAllListeners();
            _playAgainButton.onClick.AddListener(onPlayAgain);
        }
    }
}