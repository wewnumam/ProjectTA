using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GamePause
{
    public class GamePauseView : BaseView
    {
        [SerializeField] Button pauseButton;
        [SerializeField] Button resumeButton;
        [SerializeField] Button mainMenuButton;
        [SerializeField] Button playAgainButton;

        public UnityEvent onGamePause;
        public UnityEvent onGameResume;

        public void SetCallbacks(UnityAction onPause, UnityAction onResume, UnityAction onMainMenu, UnityAction onPlayAgain)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(onPause);

            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(onResume);

            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(onMainMenu);

            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(onPlayAgain);
        }
    }
}