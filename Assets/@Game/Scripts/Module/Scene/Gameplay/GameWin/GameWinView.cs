using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameWin
{
    public class GameWinView : BaseView
    {
        [SerializeField] Button playAgainButton;
        [SerializeField] Button continueButton;
        
        public UnityEvent onGameWin;

        public void SetCallbacks(UnityAction onMainMenu, UnityAction onContinue)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(onMainMenu);
            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(onContinue);
        }
    }
}