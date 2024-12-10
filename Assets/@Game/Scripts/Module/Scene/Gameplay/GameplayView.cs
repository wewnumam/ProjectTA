using Agate.MVC.Base;
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

namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        public GamePauseView GamePauseView;
        public GameWinView GameWinView;
        public GameOverView GameOverView;
        public PlayerCharacterView PlayerCharacterView;
        public CheatFeatureView CheatFeatureView;
        public HUDView HUDView;
        public DialogueView DialogueView;
        public PuzzleBoardView PuzzleBoardView;
        public CameraEffectView CameraEffectView;
        public CountdownView CountdownView;
        public BulletPoolView BulletPoolView;
        public EnemyPoolView EnemyPoolView;
    }
}
