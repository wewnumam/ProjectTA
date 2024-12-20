using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.Mission
{
    public class MissionModel : BaseModel, IMissionModel
    {
        public SOLevelData CurrentLevelData { get; private set; }
        public SOLevelData NextLevelData { get; private set; }
        public int PuzzlePieceCount { get; private set; } = 0;
        public int CollectedPuzzlePieceCount { get; private set; } = 0;
        public int HiddenObjectCount { get; private set; } = 0;
        public int CollectedHiddenObjectCount { get; private set; } = 0;
        public int KillCount { get; private set; } = 0;
        public int PadlockOnPlaceCount { get; private set; } = 0;

        public void SetCurrentLevelData(SOLevelData levelData)
        {
            CurrentLevelData = levelData;
            NextLevelData = levelData.NextLevel;
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
            if (CollectedPuzzlePieceCount < PuzzlePieceCount)
            {
                CollectedPuzzlePieceCount += amount;
            }
            SetDataAsDirty();
        }

        public void SubtractCollectedPuzzlePieceCount(int amount)
        {
            if (CollectedPuzzlePieceCount > 0)
            {
                CollectedPuzzlePieceCount -= amount;
            }
            SetDataAsDirty();
        }

        public void SetHiddenObjectCount(int hiddenObjectCount)
        {
            HiddenObjectCount = hiddenObjectCount;
            SetDataAsDirty();
        }

        public void SetCollectedHiddenObjectCount(int collectedHiddenObjectCount)
        {
            CollectedHiddenObjectCount = collectedHiddenObjectCount;
            SetDataAsDirty();
        }

        public void AddCollectedHiddenObjectCount(int amount)
        {
            if (CollectedHiddenObjectCount < HiddenObjectCount)
            {
                CollectedHiddenObjectCount += amount;
            }
            SetDataAsDirty();
        }

        public void SubtractHiddenObjectCount(int amount)
        {
            if (CollectedHiddenObjectCount > 0)
            {
                CollectedHiddenObjectCount -= amount;
            }
            SetDataAsDirty();
        }

        public void AddKillCount(int amount)
        {
            KillCount += amount;
            SetDataAsDirty();
        }

        public void SubtractKillCount(int amount)
        {
            if (KillCount > 0)
            {
                KillCount -= amount;
            }
            SetDataAsDirty();
        }

        public void AddPadlockOnPlaceCount(int amount)
        {
            if (PadlockOnPlaceCount < PuzzlePieceCount)
            {
                PadlockOnPlaceCount += amount;
            }
            SetDataAsDirty();
        }

        public bool IsPuzzleCompleted() => PadlockOnPlaceCount >= PuzzlePieceCount;
    }
}