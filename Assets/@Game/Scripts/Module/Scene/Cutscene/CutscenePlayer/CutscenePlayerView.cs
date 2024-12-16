using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerView : BaseView
    {
        [SerializeField] private TMP_Text _characterText;
        [SerializeField] private Text _messageText;

        public TMP_Text CharacterText => _characterText;
        public Text MessageText => _messageText;
        
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