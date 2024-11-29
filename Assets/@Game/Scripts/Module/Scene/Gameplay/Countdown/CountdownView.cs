using Agate.MVC.Base;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine;

namespace ProjectTA.Module.Countdown
{
    public class CountdownView : ObjectView<ICountdownModel>
    {
        [ReadOnly]
        public bool isPause;

        private UnityAction<float> onUpdate;

        private void Update()
        {
            if (!isPause)
            {
                onUpdate?.Invoke(Time.deltaTime);
            }
        }

        public void SetCallback(UnityAction<float> onUpdate)
        {
            this.onUpdate = onUpdate;
        }

        protected override void InitRenderModel(ICountdownModel model)
        {
        }

        protected override void UpdateRenderModel(ICountdownModel model)
        {
        }
    }
}