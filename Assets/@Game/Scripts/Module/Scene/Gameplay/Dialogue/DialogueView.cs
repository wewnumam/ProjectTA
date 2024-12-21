using DG.Tweening;
using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NaughtyAttributes;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueView : BaseView
    {
        [SerializeField] private TMP_Text _characterText;
        [SerializeField] private Text _messageText;
        [SerializeField] private UnityEvent _onStart;
        [SerializeField] private UnityEvent _onEnd; 
        [SerializeField] private UnityEvent _onLastLine; 
        private UnityAction _onNext;

        public TMP_Text CharacterText => _characterText;
        public Text MessageText => _messageText;
        public UnityEvent OnStart => _onStart;
        public UnityEvent OnEnd => _onEnd;
        public UnityEvent OnLastLine => _onLastLine;

        
        public void DisplayNextLine()
        {
            _onNext?.Invoke();
        }

        public void SetCallback(UnityAction onNext)
        {
            this._onNext = onNext;
        }
    }
}