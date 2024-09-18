using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.HUD
{
    public class HUDConnector : BaseConnector
    {
        private HUDController _hud;

        protected override void Connect()
        {
            Subscribe<AddHealthMessage>(_hud.OnAddHealth);
            Subscribe<SubtractHealthMessage>(_hud.OnSubtractHealth);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AddHealthMessage>(_hud.OnAddHealth);
            Unsubscribe<SubtractHealthMessage>(_hud.OnSubtractHealth);
        }
    }
}