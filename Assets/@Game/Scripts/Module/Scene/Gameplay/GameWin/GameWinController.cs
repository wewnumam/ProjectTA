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

        public void OnGameWin(GameWinMessage message)
        {
            Publish(new GameStateMessage(EnumManager.GameState.GameWin));
            _view.OnGameWin?.Invoke();
        }

        public void OnUpdateKillCount(UpdateKillCountMessage message)
        {
            _view.KillCountText.SetText(message.KillCount.ToString());
        }

        public void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            _view.CollectedPuzzleCountText.SetText($"{message.CollectedPuzzlePieceCount}/{message.PuzzlePieceCount}");
        }

        public void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _view.TimeCompletionText.SetText(message.GetFormattedCurrentCountdown(true));
        }

        public void OnUpdateHiddenObjectCount(UpdateHiddenObjectCountMessage message)
        {
            _view.HiddenObjectCountText.SetText($"{message.CollectedHiddenObjectCount}/{message.HiddenObjectCount}");
        }
    }
}