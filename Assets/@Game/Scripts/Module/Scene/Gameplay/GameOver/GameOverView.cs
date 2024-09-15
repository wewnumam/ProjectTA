using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameOver
{
    public class GameOverView : BaseView
    {
        [SerializeField] Button playAgainButton;
        [SerializeField] Button mainMenuButton;

        public UnityEvent onGameOver;

        public void SetCallbacks(UnityAction onPlayAgain, UnityAction onMainMenu)
        {
            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(onPlayAgain);
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(onMainMenu);
        }
    }
}