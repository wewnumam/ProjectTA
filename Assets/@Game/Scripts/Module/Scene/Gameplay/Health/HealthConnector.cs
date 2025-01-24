using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Health
{
    public class HealthConnector : BaseConnector
    {
        private readonly HealthController _health = new();

        protected override void Connect()
        {
            Subscribe<AdjustHealthCountMessage>(_health.OnAdjustHealthCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AdjustHealthCountMessage>(_health.OnAdjustHealthCount);
        }
    }
}