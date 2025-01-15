using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Utility;

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

        public void OnGameOver(GameOverMessage message)
        {
            Publish(new GameStateMessage(EnumManager.GameState.GameOver));
            _view.SetDelay(_view.OnGameOver);
        }

        public void OnUpdateKillCount(UpdateKillCountMessage message)
        {
            _view.KillCountText.SetText(message.KillCount.ToString());
        }

        public void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            _view.CollectedPuzzleCountText.SetText($"{message.CollectedPuzzlePieceCount}/{message.PuzzlePieceCount}");
        }

        public void OnUpdateHiddenObjectCount(UpdateHiddenObjectCountMessage message)
        {
            _view.HiddenObjectCountText.SetText($"{message.CollectedHiddenObjectCount}/{message.HiddenObjectCount}");
        }
    }
}