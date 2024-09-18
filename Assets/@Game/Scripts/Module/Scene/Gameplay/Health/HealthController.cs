using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;

namespace ProjectTA.Module.Health
{
    public class HealthController : DataController<HealthController, HealthModel, IHealthModel>
    {
        public void SetInitialHealth(int initialHealth)
        {
            _model.SetInitialHealth(initialHealth);
            _model.SetCurrentHealth(initialHealth);
        }

        internal void OnAddHealth(AddHealthMessage message)
        {
            _model.AddCurrentHealth(message.Amount);
        }

        internal void OnSubtractHealth(SubtractHealthMessage message)
        {
            _model.SubtractCurrentHealth(message.Amount);

            if (_model.IsCurrentHealthLessThanZero())
                Publish(new GameOverMessage());
        }
    }
}