using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.BulletManager;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.EnemyManager;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.PlayerCharacter;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        public GamePauseView GamePauseView;
        public GameWinView GameWinView;
        public GameOverView GameOverView;
        public PlayerCharacterView PlayerCharacterView;
        public BulletManagerView BulletManagerView;
        public EnemyManagerView EnemyManagerView;
        public CheatFeatureView CheatFeatureView;

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
