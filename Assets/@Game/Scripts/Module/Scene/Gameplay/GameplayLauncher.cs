using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using ProjectTA.Module.LevelData;
using UnityEngine.SceneManagement;
using UnityEngine;
using ProjectTA.Module.GameConstants;
using ProjectTA.Message;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using ProjectTA.Module.GameSettings;
using ProjectTA.Module.CharacterData;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.GameOver;
using System;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayLauncher : SceneLauncher<GameplayLauncher, GameplayView>
    {
        public override string SceneName {get {return TagManager.SCENE_GAMEPLAY;}}

        private SaveSystemController _saveSystem;
        private GameConstantsController _gameConstants;
        private LevelDataController _levelData;
        private CharacterDataController _characterData;
        private GameSettingsController _gameSettings;

        private GamePauseController _gamePause;
        private GameWinController _gameWin;
        private GameOverController _gameOver;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new GamePauseController(),
                new GameWinController(),
                new GameOverController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new GamePauseConnector(),
                new GameWinConnector(),
                new GameOverConnector(),
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

            _view.SetTestCallbacks(TestGameOver, TestGameWin);

            _gamePause.SetView(_view.GamePauseView);
            _gameWin.SetView(_view.GameWinView);
            _gameOver.SetView(_view.GameOverView);

            yield return null;
        }

        private void TestGameOver()
        {
            Publish(new GameOverMessage());
        }

        private void TestGameWin()
        {
            Publish(new GameWinMessage());
        }
    }
}
