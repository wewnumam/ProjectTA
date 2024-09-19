namespace ProjectTA.Message
{
    public struct UpdatePuzzleCountMessage
    {
        public int PuzzlePieceCount { get; }
        public int CollectedPuzzlePieceCount { get; }
        public bool IsIncrease { get; }
        
        public UpdatePuzzleCountMessage(int puzzlePieceCount, int collectedPuzzlePieceCount, bool isIncrease)
        {
            PuzzlePieceCount = puzzlePieceCount;
            CollectedPuzzlePieceCount = collectedPuzzlePieceCount;
            IsIncrease = isIncrease;
        }
    }
}