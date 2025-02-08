using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardView : BaseView
    {
        [field: SerializeField]
        public Transform Parent { get; set; }
        [field: SerializeField]
        public PuzzleDragable PuzzleDragableTemplate { get; set; }
        [field: SerializeField]
        public RectTransform PuzzleTargetTemplate { get; set; }
        [field: SerializeField]
        public TMP_Text QuestionText { get; set; }
        [field: SerializeField]
        public UnityEvent OnShow { get; private set; }
        [field: SerializeField]
        public UnityEvent OnComplete { get; private set; }

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