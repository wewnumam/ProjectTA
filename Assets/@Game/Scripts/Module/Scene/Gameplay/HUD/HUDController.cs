using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;

namespace ProjectTA.Module.HUD
{
    public class HUDController : ObjectController<HUDController, HUDView>
    {
        internal void OnUpdateHealth(UpdateHealthMessage message)
        {
            _view.healthText.SetText($"{message.CurrentHealth}/{message.InitialHealth}");
        }

        internal void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            _view.puzzleCountText.SetText($"{message.CollectedPuzzlePieceCount}/{message.PuzzlePieceCount}");
        }

        internal void OnUpdatePuzzleSolvedCount(UpdatePuzzleSolvedCountMessage message)
        {
            _view.solvedCountText.SetText($"{message.SolvedPuzzlePieceCount}/{message.PuzzlePieceCount}");
        }

        internal void OnUpdateKillCount(UpdateKillCountMessage message)
        {
            _view.killCountText.SetText($"{message.KillCount}");
        }
    }
}