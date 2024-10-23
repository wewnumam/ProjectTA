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
        [Header("UI Component")]
        public TMP_Text characterText;
        public Text messageText;

        [Header("Dialogue Asset"), ReadOnly] 
        public Text dialogueAsset;
        [ReadOnly] public int currentDialogueAssetIndex;

        [Header("Callbacks")]
        public UnityEvent onStart;
        public UnityEvent onEnd;
        
        private UnityAction onNext;
        
        public void DisplayNextLine()
        {
            onNext?.Invoke();
        }

        public void SetCallback(UnityAction onNext)
        {
            this.onNext = onNext;
        }
    }
}