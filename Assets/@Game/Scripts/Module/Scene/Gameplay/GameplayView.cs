using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using UnityEngine.Events;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        public GamePauseView GamePauseView;
        public GameWinView GameWinView;
        public GameOverView GameOverView;

        private UnityAction onGameOver, onGameWin;

        public void SetTestCallbacks(UnityAction onGameOver, UnityAction onGameWin)
        {
            this.onGameOver = onGameOver;
            this.onGameWin = onGameWin;
        }

        [Button]
        public void SetGameOver() => onGameOver?.Invoke();
        
        [Button]
        public void SetGameWin() => onGameWin?.Invoke();
    }
}
