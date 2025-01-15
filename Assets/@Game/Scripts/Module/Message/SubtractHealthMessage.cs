namespace ProjectTA.Message
{
    public struct SubtractHealthMessage
    {
        public int Amount { get; }

        public SubtractHealthMessage(int amount)
        {
            Amount = amount;
        }
    }
}