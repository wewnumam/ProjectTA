namespace ProjectTA.Message
{
    public struct AddCollectedPuzzlePieceCountMessage
    {
        public int Amount { get; }

        public AddCollectedPuzzlePieceCountMessage(int amount) 
        { 
            Amount = amount;
        }
    }
}