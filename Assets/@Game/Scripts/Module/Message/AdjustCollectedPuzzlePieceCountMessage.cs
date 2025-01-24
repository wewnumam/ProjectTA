namespace ProjectTA.Message
{
    public struct AdjustCollectedPuzzlePieceCountMessage
    {
        public int Amount { get; }

        public AdjustCollectedPuzzlePieceCountMessage(int amount)
        {
            Amount = amount;
        }
    }
}