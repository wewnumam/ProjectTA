using Agate.MVC.Base;
using ProjectTA.Module.BulletPool;
using ProjectTA.Module.CameraEffect;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.Countdown;
using ProjectTA.Module.Dialogue;
using ProjectTA.Module.EnemyPool;
using ProjectTA.Module.GameInduction;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.HUD;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Module.PuzzleBoard;
using ProjectTA.Module.Settings;
using ProjectTA.Module.SpatialDirection;
using ProjectTA.Module.Tutorial;
using UnityEngine;

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        [field: SerializeField]
        public GamePauseView GamePauseView { get; private set; }
        [field: SerializeField]
        public GameWinView GameWinView { get; private set; }
        [field: SerializeField]
        public GameOverView GameOverView { get; private set; }
        [field: SerializeField]
        public PlayerCharacterView PlayerCharacterView { get; private set; }
        [field: SerializeField]
        public CheatFeatureView CheatFeatureView { get; private set; }
        [field: SerializeField]
        public HudView HudView { get; private set; }
        [field: SerializeField]
        public DialogueView DialogueView { get; private set; }
        [field: SerializeField]
        public PuzzleBoardView PuzzleBoardView { get; private set; }
        [field: SerializeField]
        public CameraEffectView CameraEffectView { get; private set; }
        [field: SerializeField]
        public CountdownView CountdownView { get; private set; }
        [field: SerializeField]
        public BulletPoolView BulletPoolView { get; private set; }
        [field: SerializeField]
        public EnemyPoolView EnemyPoolView { get; private set; }
        [field: SerializeField]
        public TutorialView TutorialView { get; private set; }
        [field: SerializeField]
        public SettingsView SettingsView { get; private set; }
        [field: SerializeField]
        public SpatialDirectionView SpatialDirectionView { get; private set; }
        [field: SerializeField]
        public GameInductionView GameInductionView { get; private set; }
    }
}
