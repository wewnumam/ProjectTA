using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameWin
{
    public class GameWinView : BaseView
    {
        public TMP_Text KillCountText;
        public TMP_Text CollectedPuzzleCountText;
        public TMP_Text HiddenObjectCountText;
        public TMP_Text TimeCompletionText;

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