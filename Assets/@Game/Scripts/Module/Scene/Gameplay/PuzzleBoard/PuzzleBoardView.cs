using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.CollectibleData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardView : BaseView
    {
        public Transform parent;
        public PuzzleDragable puzzleDragableTemplate;
        public RectTransform puzzleTargetTemplate;
        public TMP_Text questionText;

        [ReadOnly]
        public List<PuzzleDragable> draggables; 
        [ReadOnly]
        public List<CollectibleComponent> puzzles; 

        public UnityEvent onShow, onComplete;

        private UnityAction onClose;

        public void SetCallback(UnityAction onClose)
        {
            this.onClose = onClose;
        }

        public void Close()
        {
            onClose?.Invoke();
        }
    }
}