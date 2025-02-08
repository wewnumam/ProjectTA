using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

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

        public void OnUpdateHealth(UpdateHealthMessage message)
        {
            if (message.CurrentHealth < (message.InitialHealth / 2))
            {
                // Normalize CurrentHealth to the range of 0 to 1
                float normalizedHealth = (float)message.CurrentHealth / ((float)message.InitialHealth / 2f);

                // Ensure the value is clamped between 0 and 1 to prevent invalid alpha values
                normalizedHealth = Mathf.Clamp01(normalizedHealth);

                // Calculate and set the alpha based on normalized health
                Color modAlpha = _view.criticalEffect.color;
                modAlpha.a = 1f - normalizedHealth; // Alpha decreases as health increases
                _view.criticalEffect.color = modAlpha;
            }
        }
    }
}