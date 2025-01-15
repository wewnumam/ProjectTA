namespace ProjectTA.Message
{
    public struct UpdateHiddenObjectCountMessage
    {
        public int HiddenObjectCount { get; }
        public int CollectedHiddenObjectCount { get; }
        public bool IsIncrease { get; }

        public UpdateHiddenObjectCountMessage(int puzzlePieceCount, int collectedPuzzlePieceCount, bool isIncrease)
        {
            HiddenObjectCount = puzzlePieceCount;
            CollectedHiddenObjectCount = collectedPuzzlePieceCount;
            IsIncrease = isIncrease;
        }
    }
}