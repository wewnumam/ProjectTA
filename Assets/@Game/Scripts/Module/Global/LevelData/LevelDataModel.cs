using Agate.MVC.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataModel : BaseModel, ILevelDataModel
    {
        public SOCutsceneData CurrentCutsceneData { get; private set; }
        public SOLevelData CurrentLevelData { get; private set; }
        public SOLevelCollection LevelCollection { get; private set; }
        public SavedLevelData SavedLevelData { get; private set; } = new();

        public GameObject CurrentEnvironmentPrefab { get; private set; }

        public void SetCurrentCutsceneData(SOCutsceneData currentCutsceneData)
        {
            CurrentCutsceneData = currentCutsceneData;
            SavedLevelData.CurrentCutsceneName = currentCutsceneData.name;
        }

        public void SetCurrentLevelData(SOLevelData levelData)
        {
            CurrentLevelData = levelData;
            SavedLevelData.CurrentLevelName = levelData.name;
        }

        public void SetLevelCollection(SOLevelCollection levelCollection)
        {
            LevelCollection = levelCollection;
        }

        public void SetCurrentEnvironmentPrefab(GameObject currentEnvironmentPrefab)
        {
            CurrentEnvironmentPrefab = currentEnvironmentPrefab;
        }

        public void SetUnlockedLevels(SavedLevelData unlockedLevels)
        {
            SavedLevelData = unlockedLevels;
        }

        public void AddUnlockedLevel(string levelName)
        {
            if (!SavedLevelData.UnlockedLevels.Contains(levelName))
            {
                SavedLevelData.UnlockedLevels.Add(levelName);
            }
            else
            {
                Debug.Log($"{levelName} is already unlocked!");
            }
        }

        public List<SOLevelData> GetUnlockedLevels()
        {
            List<SOLevelData> unlockedLevels = new();
            unlockedLevels.AddRange(from levelData in LevelCollection.LevelItems
                                    where SavedLevelData.UnlockedLevels.Contains(levelData.name)
                                    select levelData);
            return unlockedLevels;
        }
    }
}