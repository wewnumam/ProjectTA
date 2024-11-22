using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectConnector : BaseConnector
    {
        private CameraEffectController _cameraEffect;

        protected override void Connect()
        {
            Subscribe<GamePauseMessage>(_cameraEffect.OnGamePause);
            Subscribe<GameResumeMessage>(_cameraEffect.OnGameResume);
            Subscribe<GameOverMessage>(_cameraEffect.OnGameOver);
            Subscribe<GameWinMessage>(_cameraEffect.OnGameWin);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_cameraEffect.OnGamePause);
            Unsubscribe<GameResumeMessage>(_cameraEffect.OnGameResume);
            Unsubscribe<GameOverMessage>(_cameraEffect.OnGameOver);
            Unsubscribe<GameWinMessage>(_cameraEffect.OnGameWin);
        }
    }
}