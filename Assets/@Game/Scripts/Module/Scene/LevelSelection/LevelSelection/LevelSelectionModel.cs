using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionModel : BaseModel, ILevelSelectionModel
    {
        public SO_LevelCollection LevelCollection { get; private set; }
        public SO_LevelData CurrentLevelData { get; private set; }
        public List<string> UnlockedLevels { get; private set; }

        public void SetLevelCollection(SO_LevelCollection levelCollection)
        {
            LevelCollection = levelCollection;
            SetDataAsDirty();
        }

        public void SetCurrentLevelData(SO_LevelData levelData)
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