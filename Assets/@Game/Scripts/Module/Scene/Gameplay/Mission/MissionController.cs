using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using UnityEngine;

namespace ProjectTA.Module.Mission
{
    public class MissionController : DataController<MissionController, MissionModel, IMissionModel>
    {
        public void SetModel(MissionModel model)
        {
            _model = model;
        }

        public void InitModel(SOLevelData currentLevelData)
        {
            if (currentLevelData == null)
            {
                Debug.LogError("SOLEVELDATA IS NULL");
                return;
            }

            _model.SetCurrentLevelData(currentLevelData);

            if (currentLevelData.PuzzleObjects == null)
            {
                Debug.LogError("PUZZLE OBJECTS IS NULL");
                return;
            }

            _model.SetPuzzleCount(currentLevelData.PuzzleObjects.Count);
            _model.SetCollectedPuzzlePieceCount(0);

            if (currentLevelData.HiddenObjects == null)
            {
                Debug.LogError("HIDDEN OBJECTS IS NULL");
                return;
            }

            _model.SetHiddenObjectCount(currentLevelData.HiddenObjects.Count);
            _model.SetCollectedHiddenObjectCount(0);
        }

        #region MESSAGE LISTENER

        public void OnAdjustCollectedPuzzlePieceCount(AdjustCollectedPuzzlePieceCountMessage message)
        {
            _model.AdjustCollectedPuzzlePieceCount(message.Amount);
            Publish(new UpdatePuzzleCountMessage(_model.PuzzlePieceCount, _model.CollectedPuzzlePieceCount, message.Amount > 0));
        }

        public void OnAdjustCollectedHiddenObjectCount(AdjustCollectedHiddenObjectCountMessage message)
        {
            _model.AdjustCollectedHiddenObjectCount(message.Amount);
            Publish(new UpdateHiddenObjectCountMessage(_model.HiddenObjectCount, _model.CollectedHiddenObjectCount, message.Amount > 0));
        }

        public void OnAdjustKillCount(AdjustKillCountMessage message)
        {
            _model.AdjustKillCount(message.Amount);
            Publish(new UpdateKillCountMessage(_model.KillCount, true));
        }

        public void OnAdjustPadlockOnPlaceCount(AdjustPadlockOnPlaceCountMessage message)
        {
            _model.AdjustPadlockOnPlaceCount(message.Amount);
            Publish(new UpdatePuzzleSolvedCountMessage(_model.PadlockOnPlaceCount, _model.CollectedPuzzlePieceCount, message.Amount > 0));

            if (_model.IsPuzzleCompleted())
            {
                Publish(new GameWinMessage());

                if (_model.NextLevelData != null)
                {
                    Publish(new UnlockLevelMessage(_model.NextLevelData));
                }
            }
        }

        #endregion
    }
}