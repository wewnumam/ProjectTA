using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.Settings
{
    public class SettingsController : ObjectController<SettingsController, SettingsView>
    {
        private bool _initialSfx, _initialBgm, _initialVibrate;

        private const float MUTED_VOLUME = -80f;
        private const float NORMAL_VOLUME = 0f;

        public void InitModel(IGameSettingsModel gameSettings)
        {
            if (!ValidateGameSettings(gameSettings))
            {
                return;
            }

            _initialSfx = gameSettings.SavedSettingsData.IsSfxOn;
            _initialBgm = gameSettings.SavedSettingsData.IsBgmOn;
            _initialVibrate = gameSettings.SavedSettingsData.IsVibrationOn;
        }

        public override void SetView(SettingsView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnSfx, OnBgm, OnVibrate);
            view.SfxToggle.isOn = _initialSfx;
            OnSfx(_initialSfx);
            view.BgmToggle.isOn = _initialBgm;
            OnBgm(_initialBgm);
            view.VibrateToggle.isOn = _initialVibrate;
        }

        #region PRIVATE METHOD

        private bool ValidateGameSettings(IGameSettingsModel gameSettings)
        {
            if (gameSettings == null)
                return LogError("GAMESETTINGS IS NULL");

            if (gameSettings.SavedSettingsData == null)
                return LogError("SAVEDSETTINGSDATA IS NULL");

            return true;
        }

        private bool LogError(string message)
        {
            Debug.LogError(message);
            return false;
        }

        #endregion

        #region CALLBACK LISTENER

        private void OnSfx(bool isOn)
        {
            _view.AudioMixer.SetFloat(TagManager.MIXER_SFX_VOLUME, isOn ? NORMAL_VOLUME : MUTED_VOLUME);
            Publish(new ToggleSfxMessage(isOn));
        }

        private void OnBgm(bool isOn)
        {
            _view.AudioMixer.SetFloat(TagManager.MIXER_BGM_VOLUME, isOn ? NORMAL_VOLUME : MUTED_VOLUME);
            Publish(new ToggleBgmMessage(isOn));
        }

        private void OnVibrate(bool vibrate)
        {
            Publish(new ToggleVibrationMessage(vibrate));
        }

        #endregion
    }
}