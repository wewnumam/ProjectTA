using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.PadlockItem;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.Padlock
{
    public class PadlockController : ObjectController<PadlockController, PadlockView>
    {
        private List<string> _puzzleLabels = new List<string>();
        private List<GameObject> _padlockItemObjs = new List<GameObject>();

        public void SetPuzzleLabels(List<string> puzzleLabels) => _puzzleLabels = puzzleLabels;

        public override void SetView(PadlockView view)
        {
            base.SetView(view);
            view.SetCallback(OnClose);

            foreach (var puzzleLabel in _puzzleLabels)
            {
                GameObject padlockItemPlaceObj = GameObject.Instantiate(_view.padlockItemPlaceTemplate.gameObject, _view.padlockItemPlaceTemplateParent);
                PadlockItemPlace padlockItemPlace = padlockItemPlaceObj.GetComponent<PadlockItemPlace>();
                padlockItemPlace.label.SetText(puzzleLabel);
                padlockItemPlaceObj.SetActive(true);

                GameObject padlockItemObj = GameObject.Instantiate(_view.padlockItemTemplate.gameObject, _view.padlockItemTemplateParent);
                PadlockItemView padlockItemView = padlockItemObj.GetComponent<PadlockItemView>();
                padlockItemView.label.SetText(puzzleLabel);
                padlockItemView.canvas = _view.canvas;
                padlockItemView.GetComponent<RectTransform>().anchoredPosition = new Vector2(UnityEngine.Random.Range(100, 600), UnityEngine.Random.Range(-300, 0));
                padlockItemView.rectTransform = padlockItemView.GetComponent<RectTransform>();
                padlockItemView.correctPlace = padlockItemPlaceObj.GetComponent<RectTransform>();

                PadlockItemController padlockItemController = new PadlockItemController();
                InjectDependencies(padlockItemController);
                padlockItemController.Init(padlockItemView);

                _padlockItemObjs.Add(padlockItemObj);
            }
        }

        internal void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            for (int i = 0; i < message.CollectedPuzzlePieceCount; i++)
            {
                _padlockItemObjs[i].SetActive(true);
            }
        }

        internal void ShowPadlock(ShowPadlockMessage message)
        {
            _view.onShowPadlock?.Invoke();
            Time.timeScale = 0;
            Publish(new GameStateMessage(EnumManager.GameState.ShowPadlock));
        }

        private void OnClose()
        {
            _view.onClosePadlock?.Invoke();
            Time.timeScale = 1;
            Publish(new GameStateMessage(EnumManager.GameState.Playing));
        }
    }
}