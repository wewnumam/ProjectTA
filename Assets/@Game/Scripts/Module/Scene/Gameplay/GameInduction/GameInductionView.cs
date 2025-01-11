using Agate.MVC.Base;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.GameInduction
{
    public class GameInductionView : BaseView
    {
        [SerializeField]
        private float startGameInductionDelay = 3f;

        [SerializeField]
        private UnityEvent _onStartGameInduction;
        
        [SerializeField]
        private UnityEvent _onEndGameInduction;
        
        private UnityAction _onCloseGameInduction;

        public void SetCallback(UnityAction onCloseGameInduction)
        {
            _onCloseGameInduction = onCloseGameInduction;
        }

        public void CloseGameInduction()
        {
            _onEndGameInduction?.Invoke();
            _onCloseGameInduction?.Invoke();
        }

        public void StartGameInduction()
        {
            StartCoroutine(StartGameInductionDelay());
        }

        private IEnumerator StartGameInductionDelay()
        {
            yield return new WaitForSeconds(startGameInductionDelay);
            Time.timeScale = 0;
            _onStartGameInduction?.Invoke();
        }
    }
}