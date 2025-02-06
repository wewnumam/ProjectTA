using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerModel : BaseModel, ILevelSelectionPlayerModel
    {
        public SOLevelData CurrentLevelData { get; set; } = null;
        public int CurrentLevelDataIndex { get; set; } = 0;

        public SOLevelCollection LevelCollection { get; private set; } = null;
        public List<SOLevelData> UnlockedLevels { get; private set; } = null;

        public void SetLevelCollection(SOLevelCollection levelCollection)
        {
            LevelCollection = levelCollection;
        }

        public void SetUnlockedLevels(List<SOLevelData> unlockedLevels)
        {
            UnlockedLevels = unlockedLevels;
        }

        public bool IsCurrentLevelUnlocked()
        {
            return UnlockedLevels.Contains(CurrentLevelData);
        }

        public void SetNextLevelData()
        {
            CurrentLevelDataIndex++;
            if (CurrentLevelDataIndex >= LevelCollection.LevelItems.Count)
            {
                CurrentLevelDataIndex = 0;
            }
            CurrentLevelData = LevelCollection.LevelItems[CurrentLevelDataIndex];

            SetDataAsDirty();
        }

        public void SetPreviousLevelData()
        {
            CurrentLevelDataIndex--;
            if (CurrentLevelDataIndex < 0)
            {
                CurrentLevelDataIndex = LevelCollection.LevelItems.Count - 1;
            }
            CurrentLevelData = LevelCollection.LevelItems[CurrentLevelDataIndex];

            SetDataAsDirty();
        }

        public string GetLog()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("LevelSelectionPlayerModel Log:");
            if (CurrentLevelData != null)
            {
                sb.AppendLine($"{nameof(CurrentLevelData)}\t: {CurrentLevelData.name}");
                sb.AppendLine($"{nameof(CurrentLevelData.Title)}\t\t: {CurrentLevelData.Title}");
            }
            sb.AppendLine($"{nameof(CurrentLevelDataIndex)}\t\t: {CurrentLevelDataIndex}");


            sb.AppendLine("\nUnlocked Level:");
            foreach (var levelData in UnlockedLevels)
            {
                sb.AppendLine($"{levelData}\t\t: {levelData.name}");
            }

            return sb.ToString();
        }
    }
}