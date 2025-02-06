using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerController : ObjectController<LevelSelectionPlayerController, LevelSelectionPlayerModel, ILevelSelectionPlayerModel, LevelSelectionPlayerView>
    {
        #region UTILITY
        
        public void SetModel(LevelSelectionPlayerModel model)
        {
            _model = model;
        }

        public LevelSelectionPlayerView GetNewQuizPlayerView()
        {
            GameObject obj = new GameObject(nameof(LevelSelectionPlayerView));
            GameObject.DontDestroyOnLoad(obj);
            return obj.AddComponent<LevelSelectionPlayerView>();
        }

        #endregion

        public void InitModel(ILevelDataModel levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("LEVELDATA IS NULL");
                return;
            }

            if (!levelData.IsMemberValid())
            {
                return;
            }

            _model.SetLevelCollection(levelData.LevelCollection);
            _model.SetUnlockedLevels(levelData.GetUnlockedLevels());
        }

        public override void SetView(LevelSelectionPlayerView view)
        {
            view.SetCallbacks(OnPlay, OnMainMenu, OnNext, OnPrevious);
            OnNext();
            view.SetModel(_model);
        }

        #region CALLBACK LISTENER

        private void OnPlay()
        {
            Publish(new AddLevelPlayedMessage(_model.CurrentLevelData.name));
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

        #endregion
    }
}