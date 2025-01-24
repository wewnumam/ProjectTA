using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using UnityEngine;

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
        }

        public void SetPuzzleCount(int puzzleCount)
        {
            PuzzlePieceCount = puzzleCount;
        }

        public void SetCollectedPuzzlePieceCount(int collectedPuzzlePieceCount)
        {
            CollectedPuzzlePieceCount = collectedPuzzlePieceCount;
        }

        public void SetHiddenObjectCount(int hiddenObjectCount)
        {
            HiddenObjectCount = hiddenObjectCount;
        }

        public void SetCollectedHiddenObjectCount(int collectedHiddenObjectCount)
        {
            CollectedHiddenObjectCount = collectedHiddenObjectCount;
        }

        public void AdjustCollectedPuzzlePieceCount(int amount)
        {
            CollectedPuzzlePieceCount = Mathf.Clamp(CollectedPuzzlePieceCount + amount, 0, PuzzlePieceCount);
        }

        public void AdjustCollectedHiddenObjectCount(int amount)
        {
            CollectedHiddenObjectCount = Mathf.Clamp(CollectedHiddenObjectCount + amount, 0, HiddenObjectCount);
        }

        public void AdjustKillCount(int amount)
        {
            KillCount = Mathf.Max(KillCount + amount, 0);
        }

        public void AdjustPadlockOnPlaceCount(int amount)
        {
            PadlockOnPlaceCount = Mathf.Clamp(PadlockOnPlaceCount + amount, 0, PuzzlePieceCount);
        }

        public bool IsPuzzleCompleted() => PadlockOnPlaceCount >= PuzzlePieceCount;
    }
}