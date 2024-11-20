using Agate.MVC.Base;
using Ink.Runtime;
using ProjectTA.Module.LevelData;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ProjectTA.Boot;
using ProjectTA.Utility;

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

            for (int i = 0; i < _model.CurrentCutsceneData.sceneSprites.Count; i++)
            {
                Sprite sceneSprite = _model.CurrentCutsceneData.sceneSprites[i];
                GameObject obj = GameObject.Instantiate(view.imageTemplate.gameObject, view.imageParent);
                obj.GetComponent<Image>().sprite = sceneSprite;

                Vector3 imageTemplatePosition = view.imageTemplate.transform.position;
                obj.transform.position = new Vector3(i % 2 == 0 ? imageTemplatePosition.x : -imageTemplatePosition.x, imageTemplatePosition.y, imageTemplatePosition.z + (i * view.distance));
                obj.SetActive(true);           
            }

            ReverseOrder(view.imageParent);
        }

        public void DisplayNextLine()
        {
            if (!_story.canContinue)
            {
                //_view.onEnd?.Invoke();
                SceneLoader.Instance.LoadScene(TagManager.SCENE_LEVELSELECTION);
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

        void ReverseOrder(Transform parent)
        {
            int childCount = parent.childCount;

            for (int i = 0; i < childCount / 2; i++)
            {
                // Get the current and corresponding child to swap
                Transform childA = parent.GetChild(i);
                Transform childB = parent.GetChild(childCount - 1 - i);

                // Swap their sibling indices
                int siblingIndexA = childA.GetSiblingIndex();
                int siblingIndexB = childB.GetSiblingIndex();

                childA.SetSiblingIndex(siblingIndexB);
                childB.SetSiblingIndex(siblingIndexA);
            }
        }
    }
}