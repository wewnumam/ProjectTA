using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.BulletPool;
using ProjectTA.Module.CameraEffect;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.Countdown;
using ProjectTA.Module.Dialogue;
using ProjectTA.Module.EnemyPool;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GameInduction;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.Health;
using ProjectTA.Module.HUD;
using ProjectTA.Module.Input;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.Mission;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Module.PuzzleBoard;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Module.Settings;
using ProjectTA.Module.SpatialDirection;
using ProjectTA.Module.Tutorial;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayLauncher : SceneLauncher<GameplayLauncher, GameplayView>
    {
        public override string SceneName { get { return TagManager.SCENE_GAMEPLAY; } }

        private readonly GameSettingsController _gameSettings = new();
        private readonly GameConstantsController _gameConstants = new();
        private readonly LevelDataController _levelData = new();

        private readonly GamePauseController _gamePause = new();
        private readonly GameWinController _gameWin = new();
        private readonly GameOverController _gameOver = new();
        private readonly PlayerCharacterController _playerCharacter = new();
        private readonly CheatFeatureController _cheatFeature = new();
        private readonly HealthController _health = new();
        private readonly HudController _hud = new();
        private readonly MissionController _mission = new();
        private readonly DialogueController _dialogue = new();
        private readonly PuzzleBoardController _puzzleBoard = new();
        private readonly CameraEffectController _cameraEffect = new();
        private readonly CountdownController _countdown = new();
        private readonly BulletPoolController _bulletPool = new();
        private readonly EnemyPoolController _enemyPool = new();
        private readonly TutorialController _tutorial = new();
        private readonly SettingsController _settings = new();
        private readonly SpatialDirectionController _spatialDirection = new();
        private readonly GameInductionController _gameInduction = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new GamePauseController(),
                new GameWinController(),
                new GameOverController(),
                new InputController(),
                new PlayerCharacterController(),
                new CheatFeatureController(),
                new HealthController(),
                new HudController(),
                new MissionController(),
                new DialogueController(),
                new PuzzleBoardController(),
                new CameraEffectController(),
                new CountdownController(),
                new BulletPoolController(),
                new EnemyPoolController(),
                new TutorialController(),
                new SettingsController(),
                new SpatialDirectionController(),
                new GameInductionController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new GameWinConnector(),
                new GameOverConnector(),
                new PlayerCharacterConnector(),
                new HealthConnector(),
                new HudConnector(),
                new MissionConnector(),
                new DialogueConnector(),
                new PuzzleBoardConnector(),
                new CameraEffectConnector(),
                new CountdownConnector(),
                new BulletPoolConnector(),
                new EnemyPoolConnector(),
                new SpatialDirectionConnector(),
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

            GameObject environmentObj = Instantiate(_levelData.Model.CurrentEnvironmentPrefab);
            InitializePuzzleObjects(environmentObj.transform);

            _gamePause.SetView(_view.GamePauseView);
            _gameWin.SetView(_view.GameWinView);
            _gameOver.SetView(_view.GameOverView);

            _playerCharacter.SetPlayerConstants(_gameConstants.Model.GameConstants.PlayerConstants);
            _playerCharacter.SetInitialVibration(_gameSettings.Model.SavedSettingsData.IsVibrationOn);
            _playerCharacter.SetView(_view.PlayerCharacterView);
            _playerCharacter.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.IsJoystickActive);

            _hud.SetView(_view.HudView);
            _hud.SetGateIcon(_levelData.Model.CurrentLevelData.Icon);

            _health.SetInitialHealth(_gameConstants.Model.GameConstants.InitialHealth);

            _dialogue.SetView(_view.DialogueView);

            _puzzleBoard.SetLevelData(_levelData.Model.CurrentLevelData);
            _puzzleBoard.SetView(_view.PuzzleBoardView);

            _mission.Init(_levelData.Model.CurrentLevelData);

            _cameraEffect.SetView(_view.CameraEffectView);

            _countdown.SetView(_view.CountdownView);
            _countdown.SetInitialCountdown(_levelData.Model.CurrentLevelData.Countdown);
            _countdown.SetCurrentCountdown(_levelData.Model.CurrentLevelData.Countdown);

            _bulletPool.SetShootingConstants(_gameConstants.Model.GameConstants.Shooting);
            _bulletPool.SetView(_view.BulletPoolView);

            _enemyPool.SetEnemyPrefab(_levelData.Model.CurrentLevelData.EnemyPrefab);
            _enemyPool.SetEnemyConstants(_gameConstants.Model.GameConstants.Enemy);
            _enemyPool.SetView(_view.EnemyPoolView);

            _tutorial.SetView(_view.TutorialView);

            _cheatFeature.SetView(_view.CheatFeatureView);
            _cheatFeature.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.IsJoystickActive);

            _settings.SetInitialSfx(_gameSettings.Model.SavedSettingsData.IsSfxOn);
            _settings.SetInitialBgm(_gameSettings.Model.SavedSettingsData.IsBgmOn);
            _settings.SetInitialVibrate(_gameSettings.Model.SavedSettingsData.IsVibrationOn);
            _settings.SetView(_view.SettingsView);

            _spatialDirection.SetView(_view.SpatialDirectionView);

            _gameInduction.SetIsGameInductionActive(_gameSettings.Model.SavedSettingsData.IsGameInductionActive);
            _gameInduction.SetView(_view.GameInductionView);

            Publish(new GameStateMessage(EnumManager.GameState.Playing));

            yield return null;
        }

        private void InitializePuzzleObjects(Transform parent)
        {
            foreach (var collectibleObject in _levelData.Model.CurrentLevelData.PuzzleObjects)
            {
                GameObject obj = GameObject.Instantiate(collectibleObject.CollectibleData.Prefab, parent);
                obj.transform.localPosition = collectibleObject.ObjectPosition;

                if (obj.TryGetComponent<CollectibleComponent>(out var co))
                {
                    co.Initialize(collectibleObject.CollectibleData);
                }
                else
                {
                    obj.AddComponent<CollectibleComponent>().Initialize(collectibleObject.CollectibleData);
                }
            }
        }
    }
}
