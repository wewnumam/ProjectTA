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
        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _collectedPuzzleCountText;
        [SerializeField] private TMP_Text _hiddenObjectCountText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private float delayEvent;
        [SerializeField] private UnityEvent _onGameOver;

        public TMP_Text KillCountText => _killCountText;
        public TMP_Text CollectedPuzzleCountText => _collectedPuzzleCountText;
        public TMP_Text HiddenObjectCountText => _hiddenObjectCountText;
        public UnityEvent OnGameOver => _onGameOver;

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