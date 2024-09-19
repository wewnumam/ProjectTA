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
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.GameOver;
using System;
using ProjectTA.Module.Input;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Module.BulletManager;
using ProjectTA.Module.EnemyManager;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.Health;
using ProjectTA.Module.HUD;
using ProjectTA.Module.Mission;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayLauncher : SceneLauncher<GameplayLauncher, GameplayView>
    {
        public override string SceneName {get {return TagManager.SCENE_GAMEPLAY;}}

        private SaveSystemController _saveSystem;
        private GameConstantsController _gameConstants;
        private LevelDataController _levelData;
        private GameSettingsController _gameSettings;

        private GamePauseController _gamePause;
        private GameWinController _gameWin;
        private GameOverController _gameOver;
        private PlayerCharacterController _playerCharacter;
        private BulletManagerController _bulletManager;
        private EnemyManagerController _enemyManager;
        private CheatFeatureController _cheatFeature;
        private HealthController _health;
        private HUDController _hud;
        private MissionController _mission;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new GamePauseController(),
                new GameWinController(),
                new GameOverController(),
                new InputController(),
                new PlayerCharacterController(),
                new BulletManagerController(),
                new EnemyManagerController(),
                new CheatFeatureController(),
                new HealthController(),
                new HUDController(),
                new MissionController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new GamePauseConnector(),
                new GameWinConnector(),
                new GameOverConnector(),
                new PlayerCharacterConnector(),
                new BulletManagerConnector(),
                new EnemyManagerConnector(),
                new HealthConnector(),
                new HUDConnector(),
                new MissionConnector(),
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

            Instantiate(_levelData.Model.CurrentEnvironmentPrefab);

            _gamePause.SetView(_view.GamePauseView);
            _gameWin.SetView(_view.GameWinView);
            _gameOver.SetView(_view.GameOverView);

            _playerCharacter.SetView(_view.PlayerCharacterView);
            _playerCharacter.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.isJoystickActive);

            _bulletManager.SetView(_view.BulletManagerView);

            _enemyManager.SetView(_view.EnemyManagerView);

            _cheatFeature.SetView(_view.CheatFeatureView);

            _hud.SetView(_view.HUDView);
            
            _health.SetInitialHealth(_gameConstants.Model.GameConstants.initialHealth);

            _mission.SetPuzzlePieceCount(3);

            yield return null;
        }
    }
}
