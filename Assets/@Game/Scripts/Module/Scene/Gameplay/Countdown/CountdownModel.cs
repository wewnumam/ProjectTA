using Agate.MVC.Base;

namespace ProjectTA.Module.Countdown
{
    public class CountdownModel : BaseModel, ICountdownModel
    {
        public float InitialCountdown {  get; private set; }

        public float CurrentCountdown {  get; private set; }

        public void SetInitialCountdown(float initialHealth)
        {
            InitialCountdown = initialHealth;
            SetDataAsDirty();
        }

        public void SetCurrentCountdown(float currentHealth)
        {
            CurrentCountdown = currentHealth;
            SetDataAsDirty();
        }

        public void AddCurrentCountdown(float amount)
        {
            CurrentCountdown += amount;
            SetDataAsDirty();
        }

        public void SubtractCurrentCountdown(float amount)
        {
            CurrentCountdown -= amount;
            SetDataAsDirty();
        }

        public bool IsCurrentCountdownLessThanZero() => CurrentCountdown < 0;
    }
}