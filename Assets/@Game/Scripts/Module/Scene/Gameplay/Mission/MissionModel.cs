using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.Mission
{
    public class MissionModel : BaseModel, IMissionModel
    {
        public SO_LevelData CurrentLevelData { get; private set; }
        public SO_LevelData NextLevelData { get; private set; }
        public int PuzzlePieceCount { get; private set; }
        public int CollectedPuzzlePieceCount { get; private set; }
        public int KillCount { get; private set; }
        public int PadlockOnPlaceCount { get; private set; }

        public void SetCurrentLevelData(SO_LevelData levelData)
        {
            CurrentLevelData = levelData;
            SetDataAsDirty();
        }

        public void SetNextLevelData(SO_LevelData levelData)
        {
            NextLevelData = levelData;
            SetDataAsDirty();
        }

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

        public void AddPadlockOnPlaceCount(int amount)
        {
            PadlockOnPlaceCount += amount;
            SetDataAsDirty();
        }

        public bool IsPuzzleCompleted() => PadlockOnPlaceCount >= PuzzlePieceCount;
    }
}