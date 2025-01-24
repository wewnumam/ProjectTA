namespace ProjectTA.Message
{
    public struct AdjustPadlockOnPlaceCountMessage
    {
        public int Amount { get; }

        public AdjustPadlockOnPlaceCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}