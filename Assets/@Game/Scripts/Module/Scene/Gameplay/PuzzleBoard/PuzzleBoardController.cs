using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardController : ObjectController<PuzzleBoardController, PuzzleBoardView>
    {
        private SOLevelData _levelData = null;
        private readonly List<PuzzleDragable> _puzzleDragables = new();

        public void SetLevelData(SOLevelData levelData)
        {
            _levelData = levelData;
        }

        public override void SetView(PuzzleBoardView view)
        {
            base.SetView(view);
            int currentLeftIndex = 0;
            int currentRightIndex = 0;
            for (int i = 0; i < _levelData.PuzzleObjects.Count; i++)
            {
                PuzzleObject puzzle = _levelData.PuzzleObjects[i];
                
                GameObject puzzleDragable = GameObject.Instantiate(view.PuzzleDragableTemplate.gameObject, view.Parent);

                GameObject puzzleTarget = GameObject.Instantiate(view.PuzzleTargetTemplate.gameObject, view.Parent);
                RectTransform target = puzzleTarget.GetComponent<RectTransform>();
                target.anchoredPosition = puzzle.RectPosition;

                RectTransform rectTransform = puzzleDragable.GetComponent<RectTransform>();
                rectTransform.anchoredPosition += new Vector2(i % 2 != 0 ? (rectTransform.rect.width * currentRightIndex) : (-rectTransform.rect.width * currentLeftIndex), 1);

                if (i % 2 != 0)
                    currentLeftIndex++;
                else
                    currentRightIndex++;

                PuzzleDragable dragable = puzzleDragable.GetComponent<PuzzleDragable>();
                dragable.targetPosition = target;
                dragable.onPlace += OnPuzzlePlaced;
                dragable.CollectibleData = puzzle.CollectibleData;
                _puzzleDragables.Add(dragable);

                TMP_Text label = puzzleDragable.GetComponentInChildren<TMP_Text>();
                if (label != null)
                {
                    label.SetText(puzzle.CollectibleData.Title);
                }

                puzzleDragable.SetActive(true);
                puzzleTarget.SetActive(true);
            }

            view.SetCallback(OnClose);
            view.QuestionText.SetText(_levelData.PuzzleQuestion);

            Debug.Log($"<color=green>[{view.GetType()}]</color> installed successfully!");
        }

        private void OnPuzzlePlaced(PuzzleDragable puzzleDragable)
        {
            Publish(new AddPadlockOnPlaceMessage(1));

            Debug.Log(puzzleDragable.gameObject.name);
        }

        private void OnClose()
        {
            Time.timeScale = 1;
        }

        internal void ShowPuzzleBoard(ShowPadlockMessage message)
        {
            Time.timeScale = 0;
            _view.OnShow?.Invoke();
        }

        internal void OnGameWin(GameWinMessage message)
        {
            _view.OnComplete?.Invoke();
        }

        internal void OnUnlockCollectible(UnlockCollectibleMessage message)
        {
            if (message.CollectibleData.Type == EnumManager.CollectibleType.Puzzle)
            {
                PuzzleDragable puzzleDragable = _puzzleDragables.Find(dragable => dragable.CollectibleData == message.CollectibleData);
                puzzleDragable.SetPuzzleDragableActive();
            }
        }
    }
}