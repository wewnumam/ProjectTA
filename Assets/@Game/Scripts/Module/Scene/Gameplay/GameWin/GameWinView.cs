using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameWin
{
    public class GameWinView : BaseView
    {
        [SerializeField] Button playAgainButton;
        [SerializeField] Button mainMenuButton;
        
        public UnityEvent onGameWin;

        public void SetCallbacks(UnityAction onMainMenu, UnityAction onPlayAgain)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(onMainMenu);
            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(onPlayAgain);
        }
    }
}