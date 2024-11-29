namespace ProjectTA.Message
{
    public struct UpdateCountdownMessage
    {
        public float InitialCountdown { get; }
        public float CurrentCountdown { get; }
        public bool IsIncrease { get; }

        public UpdateCountdownMessage(float initialHealth, float currentHealth, bool isIncrease) 
        { 
            InitialCountdown = initialHealth;
            CurrentCountdown = currentHealth;
            IsIncrease = isIncrease;
        }
    }
}