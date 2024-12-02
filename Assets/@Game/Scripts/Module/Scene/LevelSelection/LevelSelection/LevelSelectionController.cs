using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelItem;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionController : ObjectController<LevelSelectionController, LevelSelectionModel, ILevelSelectionModel, LevelSelectionView>
    {
        private int currentIndex;

        public void SetLevelCollection(SO_LevelCollection levelCollection) => _model.SetLevelCollection(levelCollection);
        public void SetCurrentLevelData(SO_LevelData levelData) => _model.SetCurrentLevelData(levelData);
        public void SetUnlockedLevels(List<string> unlockedLevels) => _model.SetUnlockedLevels(unlockedLevels);

        public override void SetView(LevelSelectionView view)
        {
            base.SetView(view);

            view.SetCallbacks(OnPlay, OnMainMenu, OnNext, OnPrevious);
        }

        private void OnPlay()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_GAMEPLAY);
        }

        private void OnMainMenu()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
        }

        private void OnNext()
        {
            currentIndex--;
            currentIndex = currentIndex < 0 ? _model.LevelCollection.levelItems.Count - 1 : currentIndex;
            Publish(new ChooseLevelMessage(_model.LevelCollection.levelItems[currentIndex]));
            SetupCamera();
            _view.playButton.interactable = _model.IsLevelUnlocked(_model.CurrentLevelData.name);
        }

        private void OnPrevious()
        {
            currentIndex++;
            currentIndex = currentIndex >= _model.LevelCollection.levelItems.Count ? 0 : currentIndex;
            Publish(new ChooseLevelMessage(_model.LevelCollection.levelItems[currentIndex]));
            SetupCamera();
            _view.playButton.interactable = _model.IsLevelUnlocked(_model.CurrentLevelData.name);
        }

        private void SetupCamera()
        {
            _view.VirtualCamera.Follow = _view.ListedModels[currentIndex];
            _view.VirtualCamera.LookAt = _view.ListedModels[currentIndex];
        }

        internal void OnChooseLevel(ChooseLevelMessage message)
        {
            _model.SetCurrentLevelData(message.LevelData);
        }
    }
}