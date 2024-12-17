using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardView : BaseView
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private PuzzleDragable _puzzleDragableTemplate;
        [SerializeField] private RectTransform _puzzleTargetTemplate;
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private UnityEvent _onShow;
        [SerializeField] private UnityEvent _onComplete;
        private UnityAction onClose;

        public Transform Parent { get => _parent; }
        public PuzzleDragable PuzzleDragableTemplate { get => _puzzleDragableTemplate; }
        public RectTransform PuzzleTargetTemplate { get => _puzzleTargetTemplate; }
        public TMP_Text QuestionText => _questionText;
        public UnityEvent OnShow => _onShow;
        public UnityEvent OnComplete => _onComplete;

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