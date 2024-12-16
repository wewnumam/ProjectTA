using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.Mission
{
    public interface IMissionModel : IBaseModel
    {
        SOLevelData CurrentLevelData { get; }
        SOLevelData NextLevelData { get; }
        int PuzzlePieceCount { get; }
        int CollectedPuzzlePieceCount { get; }
        int HiddenObjectCount { get; }
        int CollectedHiddenObjectCount { get; }
        int PadlockOnPlaceCount { get; }
        int KillCount { get; }
    }
}