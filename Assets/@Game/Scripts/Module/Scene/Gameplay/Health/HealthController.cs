using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Health
{
    public class HealthController : DataController<HealthController, HealthModel>
    {
        public void SetModel(HealthModel model)
        {
            _model = model;
        }

        public void SetInitialHealth(int initialHealth)
        {
            _model.SetInitialHealth(initialHealth);
            _model.SetCurrentHealth(initialHealth);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, true));
        }

        public void OnAdjustHealthCount(AdjustHealthCountMessage message)
        {
            _model.AdjustCurrentHealthCount(message.Amount);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, message.Amount > 0));
            if (_model.IsCurrentHealthEqualsOrLessThanZero())
                Publish(new GameOverMessage());
        }
    }
}