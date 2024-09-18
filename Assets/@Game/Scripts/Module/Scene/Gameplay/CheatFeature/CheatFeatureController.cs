using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureController : ObjectController<CheatFeatureController, CheatFeatureView>
    {
        public override void SetView(CheatFeatureView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnActivateJoystick);
        }

        private void OnActivateJoystick(bool isJoysticActive)
        {
            Publish(new ActivateJoystickMessage(isJoysticActive));
        }
    }
}