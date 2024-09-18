using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Health
{
    public class HealthConnector : BaseConnector
    {
        private HealthController _health;

        protected override void Connect()
        {
            Subscribe<AddHealthMessage>(_health.OnAddHealth);
            Subscribe<SubtractHealthMessage>(_health.OnSubtractHealth);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AddHealthMessage>(_health.OnAddHealth);
            Unsubscribe<SubtractHealthMessage>(_health.OnSubtractHealth);
        }
    }
}