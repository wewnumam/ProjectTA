namespace ProjectTA.Message
{
    public struct AddKillCountMessage
    {
        public int Amount { get; }

        public AddKillCountMessage(int amount) 
        { 
            Amount = amount;
        }
    }
}