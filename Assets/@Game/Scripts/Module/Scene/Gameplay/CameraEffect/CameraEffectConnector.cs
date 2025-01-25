using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectConnector : BaseConnector
    {
        private readonly CameraEffectController _cameraEffect = new();

        protected override void Connect()
        {
            Subscribe<GamePauseMessage>(_cameraEffect.OnGamePause);
            Subscribe<GameResumeMessage>(_cameraEffect.OnGameResume);
            Subscribe<GameOverMessage>(_cameraEffect.OnGameOver);
            Subscribe<GameWinMessage>(_cameraEffect.OnGameWin);
            Subscribe<CameraBlurMessage>(_cameraEffect.OnCameraBlur);
            Subscribe<CameraNormalMessage>(_cameraEffect.OnCameraNormal);
            Subscribe<UpdateHealthMessage>(_cameraEffect.OnUpdateHealth);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_cameraEffect.OnGamePause);
            Unsubscribe<GameResumeMessage>(_cameraEffect.OnGameResume);
            Unsubscribe<GameOverMessage>(_cameraEffect.OnGameOver);
            Unsubscribe<GameWinMessage>(_cameraEffect.OnGameWin);
            Unsubscribe<CameraBlurMessage>(_cameraEffect.OnCameraBlur);
            Unsubscribe<CameraNormalMessage>(_cameraEffect.OnCameraNormal);
            Unsubscribe<UpdateHealthMessage>(_cameraEffect.OnUpdateHealth);
        }
    }
}