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
using ProjectTA.Module.Input;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Module.BulletManager;
using ProjectTA.Module.EnemyManager;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.Health;
using ProjectTA.Module.HUD;
using ProjectTA.Module.Mission;
using ProjectTA.Module.Dialogue;
using ProjectTA.Module.PuzzleBoard;
using ProjectTA.Module.CameraEffect;

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
        private DialogueController _dialogue;
        private PuzzleBoardController _puzzleBoard;
        private CameraEffectController _cameraEffect;

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
                new DialogueController(),
                new PuzzleBoardController(),
                new CameraEffectController(),
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
                new DialogueConnector(),
                new PuzzleBoardConnector(),
                new CameraEffectConnector(),
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

            GameObject environmentObj = Instantiate(_levelData.Model.CurrentEnvironmentPrefab);

            _gamePause.SetView(_view.GamePauseView);
            _gameWin.SetView(_view.GameWinView);
            _gameOver.SetView(_view.GameOverView);

            _playerCharacter.SetView(_view.PlayerCharacterView);
            _playerCharacter.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.isJoystickActive);

            _bulletManager.SetShootingRate(_gameConstants.Model.GameConstants.shootingRate);
            _bulletManager.SetView(_view.BulletManagerView);
            _bulletManager.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.isJoystickActive);

            _enemyManager.SetView(_view.EnemyManagerView);

            _cheatFeature.SetView(_view.CheatFeatureView);
            _cheatFeature.SetInitialActivateJoystick(_gameConstants.Model.GameConstants.isJoystickActive);

            _hud.SetView(_view.HUDView);
            _hud.SetGateIcon(_levelData.Model.CurrentLevelData.icon);
            
            _health.SetInitialHealth(_gameConstants.Model.GameConstants.initialHealth);

            _dialogue.SetView(_view.DialogueView);

            int puzzleCount = 0;

            if (environmentObj.TryGetComponent<PuzzleBoardView>(out var puzzleBoardView))
            {
                _puzzleBoard.SetView(puzzleBoardView);
                puzzleCount = puzzleBoardView.puzzles.Count;
            }

            _mission.SetCurrentLevelData(_levelData.Model.CurrentLevelData);
            _mission.SetPuzzlePieceCount(puzzleCount);

            _cameraEffect.SetView(_view.CameraEffectView);

            Publish(new GameStateMessage(EnumManager.GameState.Playing));

            yield return null;
        }
    }
}
