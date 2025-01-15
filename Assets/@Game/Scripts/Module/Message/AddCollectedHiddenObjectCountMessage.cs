namespace ProjectTA.Message
{
    public struct AddCollectedHiddenObjectCountMessage
    {
        public int Amount { get; }

        public AddCollectedHiddenObjectCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}