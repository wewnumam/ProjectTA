namespace ProjectTA.Message
{
    public struct MovePlayerCharacterEarlyMessage
    {
        public float MoveAmount { get; }
        public float Duration { get; }

        public MovePlayerCharacterEarlyMessage(float moveAmount, float duration)
        {
            MoveAmount = moveAmount;
            Duration = duration;
        }
    }
}