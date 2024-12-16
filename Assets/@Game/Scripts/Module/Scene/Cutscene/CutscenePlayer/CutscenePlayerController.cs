using Agate.MVC.Base;
using Ink.Runtime;
using ProjectTA.Module.LevelData;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ProjectTA.Boot;
using ProjectTA.Utility;
using Cinemachine;
using System.Collections.Generic;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerController : ObjectController<CutscenePlayerController, CutscenePlayerModel, CutscenePlayerView>
    {
        private Story _story = null;
        private string _text = String.Empty;
        private bool _isTextComplete = true;
        private int _currentIndex = 0;

        public void SetCurrentCutsceneData(SOCutsceneData cutsceneData) => _model.SetCurrentCutsceneData(cutsceneData);
        public void SetCameras(List<CinemachineVirtualCamera> cameras) => _model.SetCameras(cameras);

        public override void SetView(CutscenePlayerView view)
        {
            base.SetView(view);

            view.SetCallback(DisplayNextLine);

            _story = new Story(_model.CurrentCutsceneData.DialogueAsset.text);
            DisplayNextLine();
        }

        public void DisplayNextLine()
        {
            if (!_story.canContinue)
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_LEVELSELECTION);
                return;
            }

            if (_isTextComplete)
            {   
                _text = _story.Continue();
            }
            
            ParseSentence(_text?.Trim());
            
            for (int i = 0; i < _model.Cameras.Count; i++)
            {
                _model.Cameras[i].enabled = i == _currentIndex;
            }

            _currentIndex++;
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

            _view.CharacterText.text = characterName;
            if (_isTextComplete)
            {
                _view.MessageText.text = String.Empty;
                _isTextComplete = false;
                _view.MessageText.DOText(sentence, sentence.Length / 20f).OnComplete(() => _isTextComplete = true);
            }
            else
            {
                _view.MessageText.text = sentence;
            }
        }
    }
}