using Agate.MVC.Base;
using ProjectTA.Module.Settings;
using ProjectTA.Module.BulletPool;
using ProjectTA.Module.CameraEffect;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.Countdown;
using ProjectTA.Module.Dialogue;
using ProjectTA.Module.EnemyPool;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.HUD;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Module.PuzzleBoard;
using ProjectTA.Module.Tutorial;
using UnityEngine;
using ProjectTA.Module.SpatialDirection;
using ProjectTA.Module.GameInduction;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        [SerializeField] private GamePauseView _gamePauseView;
        [SerializeField] private GameWinView _gameWinView;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private PlayerCharacterView _playerCharacterView;
        [SerializeField] private CheatFeatureView _cheatFeatureView;
        [SerializeField] private HudView _hudView;
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private PuzzleBoardView _puzzleBoardView;
        [SerializeField] private CameraEffectView _cameraEffectView;
        [SerializeField] private CountdownView _countdownView;
        [SerializeField] private BulletPoolView _bulletPoolView;
        [SerializeField] private EnemyPoolView _enemyPoolView;
        [SerializeField] private TutorialView _tutorialView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private SpatialDirectionView _spatialDirectionView;
        [SerializeField] private GameInductionView _gameInductionView;

        public GamePauseView GamePauseView => _gamePauseView;
        public GameWinView GameWinView => _gameWinView;
        public GameOverView GameOverView => _gameOverView;
        public PlayerCharacterView PlayerCharacterView => _playerCharacterView;
        public CheatFeatureView CheatFeatureView => _cheatFeatureView;
        public HudView HudView => _hudView;
        public DialogueView DialogueView => _dialogueView;
        public PuzzleBoardView PuzzleBoardView => _puzzleBoardView;
        public CameraEffectView CameraEffectView => _cameraEffectView;
        public CountdownView CountdownView => _countdownView;
        public BulletPoolView BulletPoolView => _bulletPoolView;
        public EnemyPoolView EnemyPoolView => _enemyPoolView;
        public TutorialView TutorialView => _tutorialView;
        public SettingsView SettingsView => _settingsView;
        public SpatialDirectionView SpatialDirectionView => _spatialDirectionView;
        public GameInductionView GameInductionView => _gameInductionView;
    }
}
