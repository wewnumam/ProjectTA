namespace ProjectTA.Message
{
    public struct UpdateHealthMessage
    {
        public int InitialHealth { get; }
        public int CurrentHealth { get; }
        public bool IsIncrease { get; }

        public UpdateHealthMessage(int initialHealth, int currentHealth, bool isIncrease) 
        { 
            InitialHealth = initialHealth;
            CurrentHealth = currentHealth;
            IsIncrease = isIncrease;
        }
    }
}