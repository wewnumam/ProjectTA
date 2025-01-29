using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardModel : BaseModel
    {
        public List<PuzzleObject> PuzzleObjects { get; private set; } = new();
        public List<PuzzleDragable> PuzzleDragables { get; private set; } = new();
        public string PuzzleQuestion { get; private set; } = string.Empty;


        public void SetPuzzleObjects(List<PuzzleObject> puzzleObjects)
        {
            PuzzleObjects = puzzleObjects ?? new();
        }

        public void SetPuzzleQuestion(string puzzleQuestion)
        {
            PuzzleQuestion = puzzleQuestion ?? string.Empty;
        }

        public void InitObjects(GameObject puzzleDragableTemplate, GameObject puzzleTargetTemplate, Transform parent, UnityAction<PuzzleDragable> onPuzzlePlaced)
        {
            if (puzzleDragableTemplate == null || puzzleTargetTemplate == null || parent == null || onPuzzlePlaced == null)
            {
                Debug.LogError("InitObjects: One or more parameters are null.");
                return;
            }

            if (PuzzleObjects == null || PuzzleObjects.Count == 0)
            {
                Debug.LogWarning("InitObjects: PuzzleObjects list is null or empty.");
                return;
            }

            int currentLeftIndex = 0;
            int currentRightIndex = 0;

            for (int i = 0; i < PuzzleObjects.Count; i++)
            {
                PuzzleObject puzzle = PuzzleObjects[i];
                if (puzzle == null)
                {
                    Debug.LogWarning($"InitObjects: PuzzleObject at index {i} is null.");
                    continue;
                }

                GameObject puzzleTarget = GameObject.Instantiate(puzzleTargetTemplate, parent);
                if (puzzleTarget.TryGetComponent(out RectTransform target))
                {
                    target.anchoredPosition = puzzle.RectPosition;
                }
                else
                {
                    Debug.LogWarning("InitObjects: PuzzleTarget does not have a RectTransform.");
                }

                GameObject puzzleDragable = GameObject.Instantiate(puzzleDragableTemplate, parent);
                if (puzzleDragable.TryGetComponent(out RectTransform rectTransform))
                {
                    rectTransform.anchoredPosition += new Vector2(i % 2 != 0 ? (rectTransform.rect.width * currentRightIndex) : (-rectTransform.rect.width * currentLeftIndex), 1);
                }
                else
                {
                    Debug.LogWarning("InitObjects: PuzzleDragable does not have a RectTransform.");
                }

                if (i % 2 != 0)
                    currentLeftIndex++;
                else
                    currentRightIndex++;

                if (puzzleDragable.TryGetComponent(out PuzzleDragable dragable))
                {
                    dragable.targetPosition = target;
                    dragable.onPlace += onPuzzlePlaced;
                    dragable.CollectibleData = puzzle.CollectibleData;
                    PuzzleDragables.Add(dragable);
                }
                else
                {
                    Debug.LogWarning("InitObjects: PuzzleDragable does not have a PuzzleDragable component.");
                }

                TMP_Text label = puzzleDragable.GetComponentInChildren<TMP_Text>();
                if (label != null && puzzle.CollectibleData != null)
                {
                    label.SetText(puzzle.CollectibleData.Title);
                }
                else
                {
                    Debug.LogWarning("InitObjects: TMP_Text component missing or CollectibleData is null.");
                }

                puzzleDragable.SetActive(true);
                puzzleTarget.SetActive(true);
            }
        }
    }
}
