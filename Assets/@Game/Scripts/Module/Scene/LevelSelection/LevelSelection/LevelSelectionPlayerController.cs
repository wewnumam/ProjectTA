using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections.Generic;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerController : ObjectController<LevelSelectionPlayerController, LevelSelectionPlayerModel, ILevelSelectionPlayerModel, LevelSelectionPlayerView>
    {
        private int currentIndex = 0;

        public void SetLevelCollection(SOLevelCollection levelCollection) => _model.SetLevelCollection(levelCollection);
        public void SetCurrentLevelData(SOLevelData levelData) => _model.SetCurrentLevelData(levelData);
        public void SetUnlockedLevels(List<string> unlockedLevels) => _model.SetUnlockedLevels(unlockedLevels);

        public override void SetView(LevelSelectionPlayerView view)
        {
            base.SetView(view);

            view.SetCallbacks(OnPlay, OnMainMenu, OnNext, OnPrevious);
            OnNext();
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
            currentIndex++;
            currentIndex = currentIndex >= _model.LevelCollection.LevelItems.Count ? 0 : currentIndex;
            UpdateContent();
        }

        private void OnPrevious()
        {
            currentIndex--;
            currentIndex = currentIndex < 0 ? _model.LevelCollection.LevelItems.Count - 1 : currentIndex;
            UpdateContent();
        }

        private void UpdateContent()
        {
            var currentLevel = _model.LevelCollection.LevelItems[currentIndex];
            Publish(new ChooseLevelMessage(currentLevel));
            
            SetupCamera();
            
            _view.PlayButton.interactable = _model.IsLevelUnlocked(currentLevel.name);
            
            if (_model.IsLevelUnlocked(currentLevel.name))
            {
                _view.OnUnlock?.Invoke();
            }
            else
            {
                _view.OnLock?.Invoke();
            }
        }

        private void SetupCamera()
        {
            _view.VirtualCamera.Follow = _view.ListedModels[currentIndex];
            _view.VirtualCamera.LookAt = _view.ListedModels[currentIndex];
        }

        public void OnChooseLevel(ChooseLevelMessage message)
        {
            _model.SetCurrentLevelData(message.LevelData);
        }
    }
}