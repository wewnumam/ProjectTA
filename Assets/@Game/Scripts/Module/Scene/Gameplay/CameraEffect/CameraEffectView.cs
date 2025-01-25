using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectView : BaseView
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private VolumeProfile _normalVolume;
        [SerializeField] private VolumeProfile _blurVolume;
        [SerializeField] private Image _criticalEffect;

        public Volume Volume => _volume;
        public VolumeProfile NormalVolume => _normalVolume;
        public VolumeProfile BlurVolume => _blurVolume;
        public Image criticalEffect => _criticalEffect;
    }
}