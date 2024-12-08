using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using System;
using UnityEngine;

namespace ProjectTA.Module.Mission
{
    public class MissionController : DataController<MissionController, MissionModel, IMissionModel>
    {
        public void SetCurrentLevelData(SO_LevelData levelData) => _model.SetCurrentLevelData(levelData);
        public void SetNextLevelData(SO_LevelData levelData) => _model.SetNextLevelData(levelData);

        public void SetPuzzlePieceCount(int puzzlePieceCount)
        {
            _model.SetPuzzleCount(puzzlePieceCount);
            _model.SetCollectedPuzzlePieceCount(0);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, true));
            Publish(new UpdatePuzzleSolvedCountMessage(_model.PadlockOnPlaceCount, _model.CollectedPuzzlePieceCount, true));
        }

        public void SetHiddenObjectCount(int hiddenObjectCount)
        {
            _model.SetHiddenObjectCount(hiddenObjectCount);
            _model.SetCollectedHiddenObjectCount(0);
            Publish(new UpdateHiddenObjectCountMessage(_model.HiddenObjectCount, _model.CollectedHiddenObjectCount, true));
        }

        internal void OnAddCollectedPuzzlePiece(AddCollectedPuzzlePieceCountMessage message)
        {
            _model.AddCollectedPuzzlePieceCount(message.Amount);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, true));
        }

        internal void OnAddCollectedHidenObject(AddCollectedHiddenObjectCountMessage message)
        {
            _model.AddCollectedHiddenObjectCount(message.Amount);
            Publish(new UpdateHiddenObjectCountMessage(_model.HiddenObjectCount, _model.CollectedHiddenObjectCount, true));
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

        internal void OnAddPadlockOnPlace(AddPadlockOnPlaceMessage message)
        {
            _model.AddPadlockOnPlaceCount(message.Amount);
            Publish(new UpdatePuzzleSolvedCountMessage(_model.PadlockOnPlaceCount, _model.CollectedPuzzlePieceCount, true));

            if (_model.IsPuzzleCompleted())
            {
                Publish(new GameWinMessage());
                
                if (_model.NextLevelData != null)
                {
                    Publish(new UnlockLevelMessage(_model.NextLevelData));
                }
            }
        }
    }
}