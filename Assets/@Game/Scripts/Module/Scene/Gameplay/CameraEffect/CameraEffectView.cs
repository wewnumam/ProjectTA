using Agate.MVC.Base;
using UnityEngine.Rendering;
using UnityEngine;

namespace ProjectTA.Module.CameraEffect
{
    public class CameraEffectView : BaseView
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private VolumeProfile _normalVolume;
        [SerializeField] private VolumeProfile _blurVolume;

        public Volume Volume => _volume;
        public VolumeProfile NormalVolume => _normalVolume;
        public VolumeProfile BlurVolume => _blurVolume;
    }
}