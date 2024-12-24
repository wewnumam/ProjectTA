using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerModel : BaseModel, ILevelSelectionPlayerModel
    {
        public SOLevelData CurrentLevelData { get; private set; }
        public int CurrentLevelDataIndex { get; private set; } = 0;

        private SOLevelCollection _levelCollection = null;
        private List<SOLevelData> _unlockedLevels = new();

        public SOLevelCollection LevelCollection { get => _levelCollection; }
        public List<SOLevelData> UnlockedLevels { get => _unlockedLevels; }

        public void SetCurrentLevelData(SOLevelData levelData)
        {
            CurrentLevelData = levelData;
        }

        public void SetLevelCollection(SOLevelCollection levelCollection)
        {
            _levelCollection = levelCollection;
        }

        public void SetUnlockedLevels(List<SOLevelData> unlockedLevels)
        {
            _unlockedLevels = unlockedLevels;
        }

        public bool IsCurrentLevelUnlocked()
        {
            return _unlockedLevels.Contains(CurrentLevelData);
        }

        public void SetNextLevelData()
        {
            CurrentLevelDataIndex++;
            if (CurrentLevelDataIndex >= _levelCollection.LevelItems.Count)
            {
                CurrentLevelDataIndex = 0;
            }
            CurrentLevelData = _levelCollection.LevelItems[CurrentLevelDataIndex];
            
            SetDataAsDirty();
        }

        public void SetPreviousLevelData()
        {
            CurrentLevelDataIndex--;
            if (CurrentLevelDataIndex < 0)
            {
                CurrentLevelDataIndex = _levelCollection.LevelItems.Count - 1;
            }
            CurrentLevelData = _levelCollection.LevelItems[CurrentLevelDataIndex];

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
            foreach (var levelData in _unlockedLevels)
            {
                sb.AppendLine($"{levelData}\t\t: {levelData.name}");
            }

            return sb.ToString();
        }
    }
}