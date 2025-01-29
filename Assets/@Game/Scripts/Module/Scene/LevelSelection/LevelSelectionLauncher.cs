using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelSelection;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.LevelSelection
{
    public class LevelSelectionLauncher : SceneLauncher<LevelSelectionLauncher, LevelSelectionView>
    {
        public override string SceneName { get { return TagManager.SCENE_LEVELSELECTION; } }

        private readonly LevelDataController _levelData = new();
        private readonly LevelSelectionPlayerController _levelSelectionPlayer = new();

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

            _levelSelectionPlayer.Init(_levelData.Model);
            _levelSelectionPlayer.SetView(_view.LevelSelectionPlayerView);

            yield return null;
        }
    }
}
