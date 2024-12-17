using Agate.MVC.Base;
using DG.Tweening;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.GameState;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.GamePause
{
    public class GamePauseController : ObjectController<GamePauseController, GamePauseView>
    {
        public override void SetView(GamePauseView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnPause, OnResume, OnMainMenu, OnPlayAgain);
        }

        private void OnPause()
        {
            Publish(new GamePauseMessage());

            Publish(new GameStateMessage(EnumManager.GameState.Pause));
            Time.timeScale = 0;
            _view.OnGamePause?.Invoke();
        }

        private void OnResume()
        {
            Publish(new GameResumeMessage());

            Publish(new GameStateMessage(EnumManager.GameState.Playing));
            Time.timeScale = 1;
            _view.OnGameResume?.Invoke();
        }

        private void OnMainMenu()
        {
            Time.timeScale = 1;
            SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
        }

        private void OnPlayAgain()
        {
            Time.timeScale = 1;
            SceneLoader.Instance.RestartScene();
        }
    }
}