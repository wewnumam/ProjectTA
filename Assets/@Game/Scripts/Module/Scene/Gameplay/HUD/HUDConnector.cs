using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.HUD
{
    public class HUDConnector : BaseConnector
    {
        private HUDController _hud;

        protected override void Connect()
        {
            Subscribe<UpdateHealthMessage>(_hud.OnUpdateHealth);
            Subscribe<UpdatePuzzleCountMessage>(_hud.OnUpdatePuzzleCount);
            Subscribe<UpdateKillCountMessage>(_hud.OnUpdateKillCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateHealthMessage>(_hud.OnUpdateHealth);
            Unsubscribe<UpdatePuzzleCountMessage>(_hud.OnUpdatePuzzleCount);
            Unsubscribe<UpdateKillCountMessage>(_hud.OnUpdateKillCount);
        }
    }
}