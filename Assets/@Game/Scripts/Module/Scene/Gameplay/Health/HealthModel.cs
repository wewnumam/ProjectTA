using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.Health
{
    public class HealthModel : BaseModel
    {
        public int InitialHealth { get; private set; } = 0;

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

        public void AdjustCurrentHealthCount(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, InitialHealth);
            SetDataAsDirty();
        }

        public bool IsCurrentHealthEqualsOrLessThanZero() => CurrentHealth <= 0;
    }
}