using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.Mission
{
    public class MissionController : DataController<MissionController, MissionModel, IMissionModel>
    {
        public void SetPuzzlePieceCount(int puzzlePieceCount)
        {
            _model.SetPuzzleCount(puzzlePieceCount);
            _model.SetCollectedPuzzlePieceCount(0);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, true));
        }

        internal void OnAddCollectedPuzzlePiece(AddCollectedPuzzlePieceCountMessage message)
        {
            _model.AddCollectedPuzzlePieceCount(message.Amount);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, true));

            if (_model.IsCollectedPuzzleCompleted())
                Publish(new GameWinMessage());
        }

        internal void OnAddKillCount(AddKillCountMessage message)
        {
            _model.AddKillCount(message.Amount);
            Publish(new UpdateKillCountMessage(_model.KillCount, true));
        }

        internal void OnSubtractCollectedPuzzlePiece(SubtractCollectedPuzzlePieceCountMessage message)
        {
            _model.SubtractCollectedPuzzlePieceCount(message.Amount);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, false));
        }

        internal void OnSubtractKillCount(SubtractKillCountMessage message)
        {
            _model.SubtractKillCount(message.Amount);
            Publish(new UpdateKillCountMessage(_model.KillCount, false));
        }
    }
}