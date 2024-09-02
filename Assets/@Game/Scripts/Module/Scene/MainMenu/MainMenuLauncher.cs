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
using ProjectTA.Module.GameSettings;
using ProjectTA.Module.CharacterData;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName {get {return TagManager.SCENE_MAINMENU;}}

        private SaveSystemController _saveSystem;
        private LevelDataController _levelData;
        private CharacterDataController _characterData;
        private GameSettingsController _gameSettings;

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

            Publish(new GameStateMessage(Utility.EnumManager.GameState.PreGame));

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));

            yield return StartCoroutine(_levelData.SetCurrentLevel(_saveSystem.Model.SaveData.CurrentLevelName));

            _view.SetButtonCallback(GoToGameplay);

            yield return null;
        }

        private void GoToGameplay()
        {
            SceneLoader.Instance.LoadScene("Gameplay");
        }
    }
}
