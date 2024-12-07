using UnityEngine;

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

        public string GetFormattedCurrentCountdown(bool isGameplayTime = false)
        {
            float time = isGameplayTime
                ? InitialCountdown - CurrentCountdown
                : CurrentCountdown;

            return FormatTime(time);
        }

        private string FormatTime(float time)
        {
            float absTime = Mathf.Abs(time);
            int minutes = Mathf.FloorToInt(absTime / 60f);
            int seconds = Mathf.FloorToInt(absTime % 60f);

            return time >= 0
                ? $"{minutes}:{seconds:00}"
                : $"-{minutes}:{seconds:00}";
        }

    }
}