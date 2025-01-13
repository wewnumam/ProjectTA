using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private Toggle _bgmToggle;
        [SerializeField] private Toggle _vibrateToggle;
        [SerializeField] private AudioMixer _audioMixer;

        public Toggle SfxToggle => _sfxToggle;
        public Toggle BgmToggle => _bgmToggle;
        public Toggle VibrateToggle => _vibrateToggle;
        public AudioMixer AudioMixer => _audioMixer;

        public void SetCallbacks(UnityAction<bool> sfx, UnityAction<bool> bgm, UnityAction<bool> vibrate)
        {
            _vibrateToggle.onValueChanged.RemoveAllListeners();
            _vibrateToggle.onValueChanged.AddListener(vibrate);

            _sfxToggle.onValueChanged.RemoveAllListeners();
            _sfxToggle.onValueChanged.AddListener(sfx);

            _bgmToggle.onValueChanged.RemoveAllListeners();
            _bgmToggle.onValueChanged.AddListener(bgm);
        }
    }
}