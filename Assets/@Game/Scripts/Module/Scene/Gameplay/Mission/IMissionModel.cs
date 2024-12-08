using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.Mission
{
    public interface IMissionModel : IBaseModel
    {
        SO_LevelData CurrentLevelData { get; }
        SO_LevelData NextLevelData { get; }
        int PuzzlePieceCount { get; }
        int CollectedPuzzlePieceCount { get; }
        int HiddenObjectCount { get; }
        int CollectedHiddenObjectCount { get; }
        int PadlockOnPlaceCount { get; }
        int KillCount { get; }
    }
}