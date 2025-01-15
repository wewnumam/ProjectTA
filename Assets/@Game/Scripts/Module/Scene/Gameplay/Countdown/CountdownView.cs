using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.Countdown
{
    public class CountdownView : ObjectView<ICountdownModel>
    {
        public bool IsPause { get; set; }

        private UnityAction<float> _onUpdate;

        private void Update()
        {
            if (!IsPause)
            {
                _onUpdate?.Invoke(Time.deltaTime);
            }
        }

        public void SetCallback(UnityAction<float> onUpdate)
        {
            this._onUpdate = onUpdate;
        }

        protected override void InitRenderModel(ICountdownModel model)
        {
        }

        protected override void UpdateRenderModel(ICountdownModel model)
        {
        }
    }
}