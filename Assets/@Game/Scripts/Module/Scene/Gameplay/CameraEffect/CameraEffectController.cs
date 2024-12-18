using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectController : ObjectController<CameraEffectController, CameraEffectView>
    {
        private void Reset() => _view.Volume.profile = _view.NormalVolume;

        private void Blur() => _view.Volume.profile = _view.BlurVolume;

        public void OnGamePause(GamePauseMessage message) => Blur();

        public void OnGameResume(GameResumeMessage message) => Reset();

        public void OnGameOver(GameOverMessage message) => Blur();

        public void OnCameraBlur(CameraBlurMessage message) => Blur();

        public void OnGameWin(GameWinMessage message) => Blur();

        public void OnCameraNormal(CameraNormalMessage message) => Reset();
    }
}