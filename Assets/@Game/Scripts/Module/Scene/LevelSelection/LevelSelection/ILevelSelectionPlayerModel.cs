using Agate.MVC.Base;
using ProjectTA.Module.LevelData;

namespace ProjectTA.Module.LevelSelection
{
    public interface ILevelSelectionPlayerModel : IBaseModel
    {
        SOLevelData CurrentLevelData { get; }
        int CurrentLevelDataIndex { get; }

        bool IsCurrentLevelUnlocked();
        string GetLog();
    }
}