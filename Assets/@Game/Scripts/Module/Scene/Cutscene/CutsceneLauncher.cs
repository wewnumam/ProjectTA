using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.SaveSystem;
using UnityEngine;
using ProjectTA.Utility;
using ProjectTA.Module.CutscenePlayer;

namespace ProjectTA.Scene.Cutscene
{
    public class CutsceneLauncher : SceneLauncher<CutsceneLauncher, CutsceneView>
    {
        public override string SceneName {get {return TagManager.SCENE_CUTSCENE;}}

        private SaveSystemController _saveSystem;
        private LevelDataController _levelData;

        private CutscenePlayerController _cutscenePlayer;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new CutscenePlayerController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new CutscenePlayerConnector(),
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

            yield return StartCoroutine(_levelData.SetCurrentCutscene(_saveSystem.Model.SaveData.CurrentCutsceneName));

            GameObject environment = Instantiate(_levelData.Model.CurrentCutsceneData.environment);

            if (environment.TryGetComponent<CutsceneComponent>(out var cutsceneComponent))
            {
                _cutscenePlayer.SetCameras(cutsceneComponent.cameras);
            }
            _cutscenePlayer.SetCurrentCutsceneData(_levelData.Model.CurrentCutsceneData);
            _cutscenePlayer.SetView(_view.CutscenePlayerView);

            yield return null;
        }
    }
}
