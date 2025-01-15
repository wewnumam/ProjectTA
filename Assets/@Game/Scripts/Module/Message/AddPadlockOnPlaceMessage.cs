namespace ProjectTA.Message
{
    public struct AddPadlockOnPlaceMessage
    {
        public int Amount { get; }

        public AddPadlockOnPlaceMessage(int amount)
        {
            Amount = amount;
        }
    }
}