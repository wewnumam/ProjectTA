using Agate.MVC.Base;
using TMPro;

namespace ProjectTA.Module.HUD
{
    public class HUDView : ObjectView<IHUDModel>
    {
        public TMP_Text health;

        protected override void InitRenderModel(IHUDModel model)
        {
        }

        protected override void UpdateRenderModel(IHUDModel model)
        {
            health.SetText($"{model.CurrentHealth}/{model.InitialHealth}");
        }
    }
}