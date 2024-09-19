using Agate.MVC.Base;

namespace ProjectTA.Module.Mission
{
    public class MissionModel : BaseModel, IMissionModel
    {
        public int PuzzlePieceCount { get; private set; }

        public int CollectedPuzzlePieceCount { get; private set; }

        public int KillCount { get; private set; }

        public void SetPuzzleCount(int puzzleCount)
        {
            PuzzlePieceCount = puzzleCount;
            SetDataAsDirty();
        }

        public void SetCollectedPuzzlePieceCount(int collectedPuzzlePieceCount)
        {
            CollectedPuzzlePieceCount = collectedPuzzlePieceCount;
            SetDataAsDirty();
        }

        public void AddCollectedPuzzlePieceCount(int amount)
        {
            CollectedPuzzlePieceCount += amount;
            SetDataAsDirty();
        }

        public void SubtractCollectedPuzzlePieceCount(int amount)
        {
            CollectedPuzzlePieceCount -= amount;
            SetDataAsDirty();
        }

        public void SetKillCount(int killCount)
        {
            KillCount = killCount;
            SetDataAsDirty();
        }

        public void AddKillCount(int amount)
        {
            KillCount += amount;
            SetDataAsDirty();
        }

        public void SubtractKillCount(int amount)
        {
            KillCount -= amount;
            SetDataAsDirty();
        }

        public bool IsCollectedPuzzleCompleted() => CollectedPuzzlePieceCount >= PuzzlePieceCount;
    }
}