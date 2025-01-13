using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct ToggleVibrationMessage
    {
        public bool Vibration { get; }

        public ToggleVibrationMessage(bool vibrate)
        {
            Vibration = vibrate;
        }
    }
}