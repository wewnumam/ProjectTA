using Agate.MVC.Base;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerView : ObjectView<ICutscenePlayerModel>
    {
        [SerializeField] private TMP_Text _characterText;
        [SerializeField] private Text _messageText;
        [SerializeField, ResizableTextArea, ReadOnly] private string _log;

        private Tween _tween;
        private UnityAction _onNext;

        public double CurrentDialogueText { get; set; }

        public void DisplayNextLine()
        {
            _onNext?.Invoke();
        }
        public void SetCallback(UnityAction onNext)
        {
            this._onNext = onNext;
        }

        protected override void InitRenderModel(ICutscenePlayerModel model)
        {
            _log = model.GetLog();
        }

        protected override void UpdateRenderModel(ICutscenePlayerModel model)
        {
            _characterText.SetText(model.CharacterName);

            if (model.IsTextAnimationComplete)
            {
                _messageText.text = string.Empty;

                _tween = _messageText.DOText(model.Message, model.Message.Length / 20f).OnComplete(() => model.OnTextAnimationComplete?.Invoke());
            }
            else
            {
                _tween.Kill();
                _messageText.text = model.Message;
            }

            _log = model.GetLog();
        }
    }
}