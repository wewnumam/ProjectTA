namespace ProjectTA.Message
{
    public struct AdjustKillCountMessage
    {
        public int Amount { get; }

        public AdjustKillCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}