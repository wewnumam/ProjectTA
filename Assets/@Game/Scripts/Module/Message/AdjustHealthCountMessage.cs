namespace ProjectTA.Message
{
    public struct AdjustHealthCountMessage
    {
        public int Amount { get; }

        public AdjustHealthCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}