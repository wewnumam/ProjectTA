using Agate.MVC.Base;
using DG.Tweening;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.GameWin
{
    public class GameWinController : ObjectController<GameWinController, GameWinView>
    {
        public override void SetView(GameWinView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnContinue, OnPlayAgain);
        }

        private void OnContinue()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_CUTSCENE);
        }

        private void OnPlayAgain()
        {
            SceneLoader.Instance.RestartScene();
        }

        internal void OnGameWin(GameWinMessage message)
        {
            Publish(new GameStateMessage(EnumManager.GameState.GameWin));
            _view.onGameWin?.Invoke();
        }
    }
}