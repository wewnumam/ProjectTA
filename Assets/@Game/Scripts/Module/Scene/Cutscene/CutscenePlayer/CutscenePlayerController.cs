using Agate.MVC.Base;
using Ink.Runtime;
using ProjectTA.Module.LevelData;
using System;
using DG.Tweening;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerController : ObjectController<CutscenePlayerController, CutscenePlayerModel, ICutscenePlayerModel, CutscenePlayerView>
    {
        private Story _story;
        private string text;
        private bool isTextComplete = true;
        private int currentIndex;

        public void SetCurrentCutsceneData(SO_CutsceneData cutsceneData) => _model.SetCurrentCutsceneData(cutsceneData);

        public override void SetView(CutscenePlayerView view)
        {
            base.SetView(view);

            view.SetCallback(DisplayNextLine);

            _story = new Story(_model.CurrentCutsceneData.dialogueAsset.text);
            DisplayNextLine();
        }

        public void DisplayNextLine()
        {
            if (!_story.canContinue)
            {
                //_view.onEnd?.Invoke();
                return;
            }

            if (isTextComplete)
            {
                text = _story.Continue();
            }
            ParseSentence(text?.Trim());
            _view.image.sprite = _model.CurrentCutsceneData.sceneSprites[currentIndex];
            currentIndex = currentIndex < _model.CurrentCutsceneData.sceneSprites.Count - 1 ? currentIndex+1 : _model.CurrentCutsceneData.sceneSprites.Count - 1;
        }

        void ParseSentence(string sentence)
        {
            string characterName = string.Empty;
            if (sentence.Contains(":"))
            {
                int endIndex = sentence.IndexOf(':');
                characterName = sentence.Substring(0, endIndex);
                sentence = sentence.Replace(characterName + ": ", "");
            }

            _view.characterText.text = characterName;
            if (isTextComplete)
            {
                _view.messageText.text = String.Empty;
                isTextComplete = false;
                _view.messageText.DOText(sentence, sentence.Length / 20f).OnComplete(() => isTextComplete = true);
            }
            else
            {
                _view.messageText.text = sentence;
            }
        }
    }
}