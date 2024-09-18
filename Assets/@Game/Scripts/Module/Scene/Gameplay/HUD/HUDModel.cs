using Agate.MVC.Base;

namespace ProjectTA.Module.HUD
{
    public class HUDModel : BaseModel, IHUDModel
    {
        public int InitialHealth { get; private set; }

        public int CurrentHealth { get; private set; }

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
    }
}