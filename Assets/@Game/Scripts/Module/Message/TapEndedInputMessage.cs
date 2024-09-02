namespace ProjectTA.Message
{
    public struct TapEndedInputMessage
    {
        public float Duration { get; }

        public TapEndedInputMessage(float duration)
        {
            Duration = duration;
        }
    }
}