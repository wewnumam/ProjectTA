using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.HUD
{
    public class HudConnector : BaseConnector
    {
        private readonly HudController _hud = new();

        protected override void Connect()
        {
            Subscribe<UpdateHealthMessage>(_hud.OnUpdateHealth);
            Subscribe<UpdatePuzzleCountMessage>(_hud.OnUpdatePuzzleCount);
            Subscribe<UpdateKillCountMessage>(_hud.OnUpdateKillCount);
            Subscribe<UpdateCountdownMessage>(_hud.OnUpdateCountdown);
            Subscribe<UpdateHiddenObjectCountMessage>(_hud.OnUpdateHiddenObjectCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateHealthMessage>(_hud.OnUpdateHealth);
            Unsubscribe<UpdatePuzzleCountMessage>(_hud.OnUpdatePuzzleCount);
            Unsubscribe<UpdateKillCountMessage>(_hud.OnUpdateKillCount);
            Unsubscribe<UpdateCountdownMessage>(_hud.OnUpdateCountdown);
            Unsubscribe<UpdateHiddenObjectCountMessage>(_hud.OnUpdateHiddenObjectCount);
        }
    }
}