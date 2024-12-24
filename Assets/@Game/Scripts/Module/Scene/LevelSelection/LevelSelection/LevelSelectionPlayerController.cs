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
        public void SetLevelCollection(SOLevelCollection levelCollection) => _model.SetLevelCollection(levelCollection);

        public void SetUnlockedLevels(List<SOLevelData> unlockedLevels) => _model.SetUnlockedLevels(unlockedLevels);

        public override void SetView(LevelSelectionPlayerView view)
        {
            view.SetCallbacks(OnPlay, OnMainMenu, OnNext, OnPrevious);
            OnNext();
            view.SetModel(_model);
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
            _model.SetNextLevelData();
            Publish(new ChooseLevelMessage(_model.CurrentLevelData));
        }

        private void OnPrevious()
        {
            _model.SetPreviousLevelData();
            Publish(new ChooseLevelMessage(_model.CurrentLevelData));
        }
    }
}