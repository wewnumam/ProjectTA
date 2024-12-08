using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectController : ObjectController<CameraEffectController, CameraEffectView>
    {
        private void Reset() => _view.volume.profile = _view.normalVolume;

        private void Blur() => _view.volume.profile = _view.blurVolume;

        internal void OnGamePause(GamePauseMessage message) => Blur();

        internal void OnGameResume(GameResumeMessage message) => Reset();

        internal void OnGameOver(GameOverMessage message) => Blur();

        internal void OnCameraBlur(CameraBlurMessage message) => Blur();

        internal void OnGameWin(GameWinMessage message) => Blur();

        internal void OnCameraNormal(CameraNormalMessage message) => Reset();
    }
}