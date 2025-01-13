using Agate.MVC.Base;
using ProjectTA.Module.QuizPlayer;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemModel : BaseModel, ISaveSystemModel
    {
        public SaveData SaveData { get; private set; } = new();

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

        public void SetChoicesRecords(List<ChoicesRecord> choicesRecords)
        {
            SaveData.ChoicesRecords = choicesRecords;
            SetDataAsDirty();
        }

        public void SetCurrentQuestData(QuestData.QuestData questData)
        {
            SaveData.CurrentQuestData = questData;
            SetDataAsDirty();
        }

        public void AddLevelPlayed(string levelName)
        {
            if (!SaveData.LevelPlayed.Contains(levelName))
            {
                SaveData.LevelPlayed.Add(levelName);
            }
            SetDataAsDirty();
        }

        public void SetIsGameIndctionActive(bool isGameIndctionActive)
        {
            SaveData.IsGameInductionActive = isGameIndctionActive;
            SetDataAsDirty();
        }

        public void SetIsSfxOn(bool isSfxOn)
        {
            SaveData.IsSfxOn = isSfxOn;
            SetDataAsDirty();
        }

        public void SetIsBgmOn(bool isBgmOn)
        {
            SaveData.IsBgmOn = isBgmOn;
            SetDataAsDirty();
        }

        public void SetIsVibrationOn(bool isVibrationOn)
        {
            SaveData.IsVibrationOn = isVibrationOn;
            SetDataAsDirty();
        }
    }
}