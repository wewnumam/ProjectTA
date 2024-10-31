namespace ProjectTA.Message
{
    public struct UpdatePuzzleSolvedCountMessage
    {
        public int PuzzlePieceCount { get; }
        public int SolvedPuzzlePieceCount { get; }
        public bool IsIncrease { get; }
        
        public UpdatePuzzleSolvedCountMessage(int puzzlePieceCount, int collectedPuzzlePieceCount, bool isIncrease)
        {
            PuzzlePieceCount = puzzlePieceCount;
            SolvedPuzzlePieceCount = collectedPuzzlePieceCount;
            IsIncrease = isIncrease;
        }
    }
}