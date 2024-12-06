using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardController : ObjectController<PuzzleBoardController, PuzzleBoardView>
    {
        private int _currentPuzzleIndex;

        private SO_LevelData _levelData;

        public void SetLevelData(SO_LevelData levelData)
        {
            _levelData = levelData;
        }

        public override void SetView(PuzzleBoardView view)
        {
            base.SetView(view);
            int currentLeftIndex = 0;
            int currentRightIndex = 0;
            for (int i = 0; i < _levelData.collectibleObjects.Count; i++)
            {
                CollectibleObject puzzle = _levelData.collectibleObjects[i];
                
                GameObject puzzleDragable = GameObject.Instantiate(view.puzzleDragableTemplate.gameObject, view.parent);

                GameObject puzzleTarget = GameObject.Instantiate(view.puzzleTargetTemplate.gameObject, view.parent);
                RectTransform target = puzzleTarget.GetComponent<RectTransform>();
                target.anchoredPosition = puzzle.rectPosition;

                RectTransform rectTransform = puzzleDragable.GetComponent<RectTransform>();
                rectTransform.anchoredPosition += new Vector2(i % 2 != 0 ? (rectTransform.rect.width * currentRightIndex) : (-rectTransform.rect.width * currentLeftIndex), 1);

                if (i % 2 != 0)
                    currentLeftIndex++;
                else
                    currentRightIndex++;

                PuzzleDragable dragable = puzzleDragable.GetComponent<PuzzleDragable>();
                dragable.targetPosition = target;
                dragable.onPlace += OnPuzzlePlaced;
                dragable.CollectibleData = puzzle.collectibleData;
                view.draggables.Add(dragable);

                TMP_Text label = puzzleDragable.GetComponentInChildren<TMP_Text>();
                if (label != null)
                {
                    label.SetText(puzzle.collectibleData.Title);
                }

                puzzleDragable.SetActive(true);
                puzzleTarget.SetActive(true);
            }

            view.SetCallback(OnClose);
            view.questionText.SetText(_levelData.puzzleQuestion);

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
            _view.onShow?.Invoke();
        }

        internal void TeleportToPuzzle(TeleportToPuzzleMessage message)
        {
            GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform.position = _levelData.collectibleObjects[_currentPuzzleIndex].objectPosition;
            
            _currentPuzzleIndex++;
            
            if (_currentPuzzleIndex >= _levelData.collectibleObjects.Count)
                _currentPuzzleIndex = 0;
        }

        internal void OnGameWin(GameWinMessage message)
        {
            _view.onComplete?.Invoke();
        }

        internal void OnUnlockCollectible(UnlockCollectibleMessage message)
        {
            PuzzleDragable puzzleDragable = _view.draggables.FirstOrDefault(dragable => dragable.CollectibleData == message.CollectibleData);
            puzzleDragable.SetPuzzleDragableActive();
        }
    }
}