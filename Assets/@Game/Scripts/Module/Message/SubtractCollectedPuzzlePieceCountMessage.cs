namespace ProjectTA.Message
{
    public struct SubtractCollectedPuzzlePieceCountMessage
    {
        public int Amount { get; }

        public SubtractCollectedPuzzlePieceCountMessage(int amount) 
        { 
            Amount = amount;
        }
    }
}