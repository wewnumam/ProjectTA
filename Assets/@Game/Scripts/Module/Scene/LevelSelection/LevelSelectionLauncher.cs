using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelSelection;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.LevelSelection
{
    public class LevelSelectionLauncher : SceneLauncher<LevelSelectionLauncher, LevelSelectionView>
    {
        public override string SceneName { get { return TagManager.SCENE_LEVELSELECTION; } }

        private readonly SaveSystemController _saveSystem = new();
        private readonly LevelDataController _levelData = new();

        private readonly LevelSelectionPlayerController _levelSelection = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new LevelSelectionPlayerController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
            };
        }

        protected override IEnumerator LaunchScene()
        {
            yield return null;
        }

        protected override IEnumerator InitSceneObject()
        {
            Time.timeScale = 1;

            Publish(new GameStateMessage(EnumManager.GameState.PreGame));

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));

            _levelSelection.SetLevelCollection(_levelData.Model.LevelCollection);
            _levelSelection.SetUnlockedLevels(_levelData.Model.GetUnlockedLevels());
            _levelSelection.SetView(_view.levelSelectionView);

            yield return null;
        }
    }
}
