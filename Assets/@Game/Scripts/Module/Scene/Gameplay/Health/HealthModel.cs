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
            if (CurrentHealth > InitialHealth)
            {
                CurrentHealth = InitialHealth;
            }
            SetDataAsDirty();
        }

        public void SubtractCurrentHealth(int amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            SetDataAsDirty();
        }

        public bool IsCurrentHealthEqualsOrLessThanZero() => CurrentHealth <= 0;
    }
}