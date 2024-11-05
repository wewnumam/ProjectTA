using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;

namespace ProjectTA.Module.GameOver
{
    public class GameOverController : ObjectController<GameOverController, GameOverView>
    {
        public override void SetView(GameOverView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnPlayAgain, OnMainMenu);
        }

        private void OnPlayAgain() => SceneLoader.Instance.RestartScene();

        private void OnMainMenu() => SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);

        internal void OnGameOver(GameOverMessage message)
        {
            Publish(new GameStateMessage(EnumManager.GameState.GameOver));
            _view.SetDelay(_view.onGameOver);
        }
    }
}