using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerModel : BaseModel, ILevelSelectionPlayerModel
    {
        public SOLevelCollection LevelCollection { get; private set; }
        public SOLevelData CurrentLevelData { get; private set; }
        public List<string> UnlockedLevels { get; private set; }

        public void SetLevelCollection(SOLevelCollection levelCollection)
        {
            LevelCollection = levelCollection;
            SetDataAsDirty();
        }

        public void SetCurrentLevelData(SOLevelData levelData)
        {
            CurrentLevelData = levelData;
            SetDataAsDirty();
        }

        public void SetUnlockedLevels(List<string> unlockedLevels)
        {
            UnlockedLevels = unlockedLevels;
            SetDataAsDirty();
        }

        public bool IsLevelUnlocked(string levelName)
        {
            return UnlockedLevels.Contains(levelName);
        }
    }
}