using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.Mission
{
    public interface IMissionModel : IBaseModel
    {
        SO_LevelData CurrentLevelData { get; }
        int PuzzlePieceCount { get; }
        int CollectedPuzzlePieceCount { get; }
        int PadlockOnPlaceCount { get; }
        int KillCount { get; }
    }
}