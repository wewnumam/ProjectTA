using Agate.MVC.Base;
using ProjectTA.Module.BulletManager;
using ProjectTA.Module.CameraEffect;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.Countdown;
using ProjectTA.Module.Dialogue;
using ProjectTA.Module.EnemyManager;
using ProjectTA.Module.GameOver;
using ProjectTA.Module.GamePause;
using ProjectTA.Module.GameWin;
using ProjectTA.Module.HUD;
using ProjectTA.Module.PlayerCharacter;


namespace ProjectTA.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        public GamePauseView GamePauseView;
        public GameWinView GameWinView;
        public GameOverView GameOverView;
        public PlayerCharacterView PlayerCharacterView;
        public BulletManagerView BulletManagerView;
        public EnemyManagerView EnemyManagerView;
        public CheatFeatureView CheatFeatureView;
        public HUDView HUDView;
        public DialogueView DialogueView;
        public CameraEffectView CameraEffectView;
        public CountdownView CountdownView;
    }
}
