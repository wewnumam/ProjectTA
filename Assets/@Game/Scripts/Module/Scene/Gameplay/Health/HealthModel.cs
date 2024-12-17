using Agate.MVC.Base;

namespace ProjectTA.Module.Health
{
    public class HealthModel : BaseModel, IHealthModel
    {
        public int InitialHealth {  get; private set; } = 0;

        public int CurrentHealth { get; private set; } = 0;

        public void SetInitialHealth(int initialHealth)
        {
            InitialHealth = initialHealth;
            SetDataAsDirty();
        }

        public void SetCurrentHealth(int currentHealth)
        {
            CurrentHealth = currentHealth;
            SetDataAsDirty();
        }

        public void AddCurrentHealth(int amount)
        {
            CurrentHealth += amount;
            SetDataAsDirty();
        }

        public void SubtractCurrentHealth(int amount)
        {
            CurrentHealth -= amount;
            SetDataAsDirty();
        }

        public bool IsCurrentHealthLessThanZero() => CurrentHealth < 0;
    }
}