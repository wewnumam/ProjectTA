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

namespace ProjectTA.Scene.Cutscene
{
    public class CutsceneLauncher : SceneLauncher<CutsceneLauncher, CutsceneView>
    {
        public override string SceneName {get {return TagManager.SCENE_CUTSCENE;}}

        private SaveSystemController _saveSystem;
        private LevelDataController _levelData;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
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

            yield return StartCoroutine(_levelData.SetCurrentLevel(_saveSystem.Model.SaveData.CurrentLevelName));

            yield return null;
        }
    }
}
