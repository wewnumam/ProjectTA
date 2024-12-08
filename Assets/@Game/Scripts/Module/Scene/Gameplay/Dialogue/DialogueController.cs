using Agate.MVC.Base;
using DG.Tweening;
using Ink.Runtime;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueController : ObjectController<DialogueController, DialogueView>
    {
        private Story _story;
        private string text;
        private bool isTextComplete = true;

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
                _view.onEnd?.Invoke();
                return;
            }

            if (isTextComplete)
            {
                text = _story.Continue();
            }
            ParseSentence(text?.Trim());
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

            _view.characterText.text = characterName;
            if (isTextComplete)
            {
                _view.messageText.text = "";
                isTextComplete = false;
                _view.messageText.DOText(sentence, sentence.Length / 20f).OnComplete(() => isTextComplete = true);
            }
            else
            {
                _view.messageText.text = sentence;
            }
        }

        internal void ShowDialogue(ShowDialogueMessage message)
        {
            if (message.TextAsset == null)
                return;

            Publish(new GamePauseMessage());

            _view.onStart?.Invoke();
            _story = new Story(message.TextAsset.text);
            DisplayNextLine();
        }
    }
}
