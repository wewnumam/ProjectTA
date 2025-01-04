using Agate.MVC.Base;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.Settings
{
    public class SettingsView : BaseView
    {
        public Slider audioVolumeSlider;
        public Toggle vibrateToggle;
        public AudioMixer audioMixer;

        public void SetCallbacks(UnityAction<float> volume, UnityAction<bool> vibrate)
        {
            audioVolumeSlider.onValueChanged.RemoveAllListeners();
            audioVolumeSlider.onValueChanged.AddListener(volume);
            
            vibrateToggle.onValueChanged.RemoveAllListeners();
            vibrateToggle.onValueChanged.AddListener(vibrate);
        }
    }
}