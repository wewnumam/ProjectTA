namespace ProjectTA.Message
{
    public struct AdjustCollectedHiddenObjectCountMessage
    {
        public int Amount { get; }

        public AdjustCollectedHiddenObjectCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}