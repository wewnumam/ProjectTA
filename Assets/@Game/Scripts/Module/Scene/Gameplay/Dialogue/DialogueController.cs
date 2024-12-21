using Agate.MVC.Base;
using DG.Tweening;
using Ink.Runtime;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueController : ObjectController<DialogueController, DialogueView>
    {
        private Story _story = null;
        private string _text = String.Empty;
        private bool _isTextComplete = true;

        public override void SetView(DialogueView view)
        {
            base.SetView(view);
            view.SetCallback(DisplayNextLine);
        }

        public void DisplayNextLine()
        {
            if (!_story.canContinue)
            {
                Publish(new GameResumeMessage());
                Publish(new GameStateMessage(EnumManager.GameState.Playing));
                _story.ResetState();
                _story = null;
                _view.OnEnd?.Invoke();
                return;
            }

            if (_isTextComplete)
            {
                _text = _story.Continue();
                if (!_story.canContinue)
                {
                    _view.OnLastLine?.Invoke();
                }
            }
            ParseSentence(_text?.Trim());
        }

        void ParseSentence(string sentence)
        {
            string characterName = "";
            if (sentence.Contains(":"))
            {
                int endIndex = sentence.IndexOf(':');
                characterName = sentence.Substring(0, endIndex);
                sentence = sentence.Replace(characterName + ": ", "");
            }

            _view.CharacterText.text = characterName;
            if (_isTextComplete)
            {
                _view.MessageText.text = "";
                _isTextComplete = false;
                _view.MessageText.DOText(sentence, sentence.Length / 20f).OnComplete(() => _isTextComplete = true);
            }
            else
            {
                _view.MessageText.text = sentence;
            }
        }

        public void ShowDialogue(ShowDialogueMessage message)
        {
            if (message.TextAsset == null)
                return;

            Publish(new GamePauseMessage());
            Publish(new GameStateMessage(EnumManager.GameState.Dialogue));
            _story = new Story(message.TextAsset.text);
            DisplayNextLine();
            _view.OnStart?.Invoke();
        }
    }
}
