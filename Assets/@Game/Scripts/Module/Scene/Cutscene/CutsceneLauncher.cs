using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.CutscenePlayer;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.Cutscene
{
    public class CutsceneLauncher : SceneLauncher<CutsceneLauncher, CutsceneView>
    {
        public override string SceneName { get { return TagManager.SCENE_CUTSCENE; } }

        private readonly LevelDataController _levelData = new();

        private readonly CutscenePlayerController _cutscenePlayer = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new CutscenePlayerController(),
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

            _cutscenePlayer.Init(_levelData.Model.CurrentCutsceneData);
            _cutscenePlayer.SetView(_view.CutscenePlayerView);

            yield return null;
        }
    }
}
