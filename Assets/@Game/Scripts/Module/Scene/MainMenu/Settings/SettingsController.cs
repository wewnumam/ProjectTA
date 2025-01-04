using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.Settings
{
    public class SettingsController : ObjectController<SettingsController, SettingsView>
    {
        private float initialAudioVolume;
        private bool initialVibrate;

        public void SetInitialVolume(float audioVolume) => initialAudioVolume = audioVolume;

        public void SetInitialVibrate(bool isVibrateOn) => initialVibrate = isVibrateOn;

        public override void SetView(SettingsView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnVolume, OnVibrate);
            view.audioVolumeSlider.value = initialAudioVolume;
            view.vibrateToggle.isOn = initialVibrate;
        }

        private void OnVolume(float volume)
        {
            if (volume > 0)
                _view.audioMixer.SetFloat(TagManager.MIXER_MASTER_VOLUME, Mathf.Log10(volume) * 20);
            else
                _view.audioMixer.SetFloat(TagManager.MIXER_MASTER_VOLUME, -80f);

            Publish(new GameSettingVolumeMessage(volume));
        }

        private void OnVibrate(bool vibrate)
        {
            Publish(new GameSettingVibrateMessage(vibrate));
        }
    }
}