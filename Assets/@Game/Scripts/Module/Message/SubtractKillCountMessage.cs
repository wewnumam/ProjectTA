namespace ProjectTA.Message
{
    public struct SubtractKillCountMessage
    {
        public int Amount { get; }

        public SubtractKillCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}