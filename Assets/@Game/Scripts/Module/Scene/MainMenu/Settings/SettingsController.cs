using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;

namespace ProjectTA.Module.Settings
{
    public class SettingsController : ObjectController<SettingsController, SettingsView>
    {
        private bool _initialSfx, _initialBgm, _initialVibrate;

        private const float MUTED_VOLUME = -80f;
        private const float NORMAL_VOLUME = 0f;

        public void SetInitialSfx(bool isSfxOn) => _initialSfx = isSfxOn;

        public void SetInitialBgm(bool isBgmOn) => _initialBgm = isBgmOn;

        public void SetInitialVibrate(bool isVibrateOn) => _initialVibrate = isVibrateOn;

        public override void SetView(SettingsView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnSfx, OnBgm, OnVibrate);
            view.SfxToggle.isOn = _initialSfx;
            SetSfx(_initialSfx);
            view.BgmToggle.isOn = _initialBgm;
            SetBgm(_initialBgm);
            view.VibrateToggle.isOn = _initialVibrate;
        }

        private void SetSfx(bool isOn)
        {
            _view.AudioMixer.SetFloat(TagManager.MIXER_SFX_VOLUME, isOn ? NORMAL_VOLUME : MUTED_VOLUME);
        }

        private void SetBgm(bool isOn)
        {
            _view.AudioMixer.SetFloat(TagManager.MIXER_BGM_VOLUME, isOn ? NORMAL_VOLUME : MUTED_VOLUME);
        }

        private void OnSfx(bool sfx)
        {
            SetSfx(sfx);
            Publish(new ToggleSfxMessage(sfx));
        }

        private void OnBgm(bool bgm)
        {
            SetBgm(bgm);
            Publish(new ToggleBgmMessage(bgm));
        }

        private void OnVibrate(bool vibrate)
        {
            Publish(new ToggleVibrationMessage(vibrate));
        }
    }
}