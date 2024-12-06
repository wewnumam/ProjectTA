using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemModel : BaseModel, ISaveSystemModel
    {
        public SaveData SaveData { get; private set; }

        public void SetSaveData(SaveData saveData)
        {
            SaveData = saveData;
            SetDataAsDirty();
        }

        public void SetCurrentCutsceneName(string cutsceneName)
        {
            SaveData.CurrentCutsceneName = cutsceneName;
            SetDataAsDirty();
        }

        public void SetCurrentLevelName(string levelName)
        {
            SaveData.CurrentLevelName = levelName;
            SetDataAsDirty();
        }

        public void AddUnlockedLevel(string levelName)
        {
            if (!IsLevelUnlocked(levelName))
            {
                SaveData.UnlockedLevels.Add(levelName);
            }
            else
            {
                Debug.Log($"{levelName} is already unlocked!");
            }
            SetDataAsDirty();
        }

        public bool IsLevelUnlocked(string levelName)
        {
            SetDataAsDirty();
            return SaveData.UnlockedLevels.Contains(levelName);
        }

        public void AddUnlockedCollectible(string collectibleName)
        {
            if (!IsCollectibleUnlocked(collectibleName))
            {
                SaveData.UnlockedCollectibles.Add(collectibleName);
            }
            else
            {
                Debug.Log($"{collectibleName} is already unlocked!");
            }
            SetDataAsDirty();
        }

        public bool IsCollectibleUnlocked(string collectibleName)
        {
            SetDataAsDirty();
            return SaveData.UnlockedCollectibles.Contains(collectibleName);
        }
    }
}