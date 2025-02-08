using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
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
            // Validate input parameters
            if (!AreParametersValid(puzzleDragableTemplate, puzzleTargetTemplate, parent, onPuzzlePlaced))
            {
                Debug.LogError("InitObjects: One or more parameters are null.");
                return;
            }

            // Validate PuzzleObjects list
            if (IsPuzzleObjectsInvalid())
            {
                Debug.LogWarning("InitObjects: PuzzleObjects list is null or empty.");
                return;
            }

            int currentLeftIndex = 0;
            int currentRightIndex = 0;

            foreach (var puzzle in PuzzleObjects)
            {
                if (puzzle == null)
                {
                    Debug.LogWarning($"InitObjects: PuzzleObject is null.");
                    continue;
                }

                // Create and configure the target object
                var puzzleTarget = InstantiateAndConfigureTarget(puzzleTargetTemplate, parent, puzzle.RectPosition);
                if (puzzleTarget == null) continue;

                // Create and configure the draggable object
                var puzzleDragable = InstantiateAndConfigureDraggable(
                    puzzleDragableTemplate,
                    parent,
                    puzzleTarget.GetComponent<RectTransform>(),
                    ref currentLeftIndex,
                    ref currentRightIndex,
                    onPuzzlePlaced,
                    puzzle.CollectibleData
                );
                if (puzzleDragable == null) continue;

                // Set the label text for the draggable object
                SetLabelText(puzzleDragable, puzzle.CollectibleData?.Title);

                // Activate both objects
                puzzleTarget.SetActive(true);
                puzzleDragable.SetActive(true);
            }
        }

        // Helper method to validate input parameters
        private bool AreParametersValid(GameObject puzzleDragableTemplate, GameObject puzzleTargetTemplate, Transform parent, UnityAction<PuzzleDragable> onPuzzlePlaced)
        {
            return puzzleDragableTemplate != null &&
                   puzzleTargetTemplate != null &&
                   parent != null &&
                   onPuzzlePlaced != null;
        }

        // Helper method to validate PuzzleObjects
        private bool IsPuzzleObjectsInvalid()
        {
            return PuzzleObjects == null || PuzzleObjects.Count == 0;
        }

        // Helper method to instantiate and configure the target object
        private GameObject InstantiateAndConfigureTarget(GameObject template, Transform parent, Vector2 position)
        {
            var instance = GameObject.Instantiate(template, parent);
            if (instance.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.anchoredPosition = position;
                return instance;
            }
            Debug.LogWarning("InitObjects: PuzzleTarget does not have a RectTransform.");
            return null;
        }

        // Helper method to instantiate and configure the draggable object
        private GameObject InstantiateAndConfigureDraggable(
            GameObject template,
            Transform parent,
            RectTransform target,
            ref int currentLeftIndex,
            ref int currentRightIndex,
            UnityAction<PuzzleDragable> onPuzzlePlaced,
            SOCollectibleData collectibleData)
        {
            var instance = GameObject.Instantiate(template, parent);
            if (instance.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.anchoredPosition += CalculateDraggablePosition(rectTransform, ref currentLeftIndex, ref currentRightIndex);
            }
            else
            {
                Debug.LogWarning("InitObjects: PuzzleDragable does not have a RectTransform.");
                return null;
            }

            if (instance.TryGetComponent(out PuzzleDragable dragable))
            {
                dragable.targetPosition = target;
                dragable.onPlace += onPuzzlePlaced;
                dragable.CollectibleData = collectibleData;
                PuzzleDragables.Add(dragable);
                return instance;
            }

            Debug.LogWarning("InitObjects: PuzzleDragable does not have a PuzzleDragable component.");
            return null;
        }

        // Helper method to calculate the draggable object's position
        private Vector2 CalculateDraggablePosition(RectTransform rectTransform, ref int currentLeftIndex, ref int currentRightIndex)
        {
            if (currentLeftIndex % 2 != 0)
            {
                currentRightIndex++;
                return new Vector2(rectTransform.rect.width * currentRightIndex, 1);
            }
            else
            {
                currentLeftIndex++;
                return new Vector2(-rectTransform.rect.width * currentLeftIndex, 1);
            }
        }

        // Helper method to set the label text
        private void SetLabelText(GameObject puzzleDragable, string title)
        {
            var label = puzzleDragable.GetComponentInChildren<TMP_Text>();
            if (label != null && !string.IsNullOrEmpty(title))
            {
                label.SetText(title);
            }
            else
            {
                Debug.LogWarning("InitObjects: TMP_Text component missing or Title is null/empty.");
            }
        }
    }
}
