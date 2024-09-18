namespace ProjectTA.Message
{
    public struct AddHealthMessage
    {
        public int Amount { get; }

        public AddHealthMessage(int amount) 
        { 
            Amount = amount;
        }
    }
}