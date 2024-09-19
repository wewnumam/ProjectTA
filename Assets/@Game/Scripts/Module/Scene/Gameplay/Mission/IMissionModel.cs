using Agate.MVC.Base;

namespace ProjectTA.Module.Mission
{
    public interface IMissionModel : IBaseModel
    {
        int PuzzlePieceCount { get; }
        int CollectedPuzzlePieceCount { get; }
        int KillCount { get; }
    }
}