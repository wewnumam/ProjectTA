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
using ProjectTA.Module.LevelSelection;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName {get {return TagManager.SCENE_MAINMENU;}}

        private SaveSystemController _saveSystem;
        private LevelDataController _levelData;
        private GameSettingsController _gameSettings;

        private LevelSelectionController _levelSelection;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new LevelSelectionController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new LevelSelectionConnector(),
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

            _view.SetButtonCallback(OnPlay, OnQuit);

            _levelSelection.SetLevelCollection(_levelData.Model.LevelCollection);
            _levelSelection.SetCurrentLevelData(_levelData.Model.CurrentLevelData);
            _levelSelection.SetUnlockedLevels(_saveSystem.Model.SaveData.UnlockedLevels);
            _levelSelection.SetView(_view.LevelSelectionView);

            yield return null;
        }

        private void OnPlay()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_GAMEPLAY);
        }

        private void OnQuit()
        {
            Application.Quit();
        }
    }
}
