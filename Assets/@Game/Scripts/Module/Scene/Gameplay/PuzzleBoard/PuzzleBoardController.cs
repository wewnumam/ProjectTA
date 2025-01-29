using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardController : ObjectController<PuzzleBoardController, PuzzleBoardModel, PuzzleBoardView>
    {
        public void InitModel(ILevelDataModel levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("LEVELDATA IS NULL");
                return;
            }

            if (levelData.CurrentLevelData == null)
            {
                Debug.LogError("CURRENTLEVELDATA IS NULL");
                return;
            }

            if (levelData.CurrentLevelData.PuzzleObjects == null)
            {
                Debug.LogError("PUZZLEOBJECTS IS NULL");
                return;
            }

            _model.SetPuzzleQuestion(levelData.CurrentLevelData.PuzzleQuestion);
            _model.SetPuzzleObjects(levelData.CurrentLevelData.PuzzleObjects);
        }

        public override void SetView(PuzzleBoardView view)
        {
            base.SetView(view);
            view.SetCallback(OnClose);
            view.QuestionText.SetText(_model.PuzzleQuestion);

            _model.InitObjects(view.PuzzleDragableTemplate.gameObject, view.PuzzleTargetTemplate.gameObject, view.Parent, OnPuzzlePlaced);

            Debug.Log($"<color=green>[{view.GetType()}]</color> installed successfully!");
        }

        private void OnPuzzlePlaced(PuzzleDragable puzzleDragable)
        {
            Publish(new AdjustPadlockOnPlaceCountMessage(1));

            Debug.Log(puzzleDragable.gameObject.name);
        }

        private void OnClose()
        {
            Time.timeScale = 1;
        }

        public void ShowPuzzleBoard(ShowPadlockMessage message)
        {
            Time.timeScale = 0;
            _view.OnShow?.Invoke();
        }

        public void OnGameWin(GameWinMessage message)
        {
            _view.OnComplete?.Invoke();
        }

        public void OnUnlockCollectible(UnlockCollectibleMessage message)
        {
            if (message.CollectibleData.Type == EnumManager.CollectibleType.Puzzle)
            {
                PuzzleDragable puzzleDragable = _model.PuzzleDragables.Find(dragable => dragable.CollectibleData == message.CollectibleData);
                puzzleDragable.SetPuzzleDragableActive();
            }
        }
    }
}