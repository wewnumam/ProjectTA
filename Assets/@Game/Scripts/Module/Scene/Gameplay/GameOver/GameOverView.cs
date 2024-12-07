using Agate.MVC.Base;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.GameOver
{
    public class GameOverView : BaseView
    {
        public TMP_Text KillCountText;
        public TMP_Text CollectedPuzzleCountText;
        public TMP_Text HiddenObjectCountText;

        [SerializeField] Button playAgainButton;
        [SerializeField] Button mainMenuButton;

        [SerializeField] float delayEvent;
        public UnityEvent onGameOver;

        public void SetCallbacks(UnityAction onPlayAgain, UnityAction onMainMenu)
        {
            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(onPlayAgain);
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(onMainMenu);
        }

        public void SetDelay(UnityEvent callback)
        {
            StartCoroutine(Delay(callback));
        }

        public IEnumerator Delay(UnityEvent callback)
        {
            yield return new WaitForSeconds(delayEvent);
            callback?.Invoke();
        }
    }
}