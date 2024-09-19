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
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, true));
        }

        internal void OnAddHealth(AddHealthMessage message)
        {
            _model.AddCurrentHealth(message.Amount);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, true));
        }

        internal void OnSubtractHealth(SubtractHealthMessage message)
        {
            _model.SubtractCurrentHealth(message.Amount);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, false));

            if (_model.IsCurrentHealthLessThanZero())
                Publish(new GameOverMessage());
        }
    }
}