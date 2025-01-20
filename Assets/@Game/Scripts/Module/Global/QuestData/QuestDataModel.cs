using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataModel : BaseModel, IQuestDataModel
    {
        public SOQuestCollection QuestCollection { get; private set; }
        public SavedQuestData CurrentQuestData { get; private set; }
        public SavedUnlockedCollectibles UnlockedCollectibles { get; private set; }

        public float CurrentSessionInMinutes { get; set; }

        public void SetQuestCollection(SOQuestCollection questCollection)
        {
            QuestCollection = questCollection;
        }

        public void SetCurrentQuestData(SavedQuestData questData)
        {
            CurrentQuestData = questData;
        }

        public void SetUnlockedCollectibles(SavedUnlockedCollectibles unlockedCollectibles)
        {
            UnlockedCollectibles = unlockedCollectibles;
        }

        public void AddCurrentKillAmount(int amount)
        {
            CurrentQuestData.CurrentKillAmount += amount;
        }

        public void SetCurrentCollectibleAmount(int amount)
        {
            CurrentQuestData.CurrentCollectibleAmount = amount;
        }

        public int GetCurrentCollectibleByTypeAmount(
            List<SOCollectibleData> collectibleItems, 
            List<string> unlockedCollectible, 
            EnumManager.CollectibleType type)
        {
            if (collectibleItems == null)
                throw new ArgumentNullException(nameof(collectibleItems), "collectibleItems cannot be null.");
            if (unlockedCollectible == null)
                throw new ArgumentNullException(nameof(unlockedCollectible), "unlockedCollectible cannot be null.");

            int amount = (from item in collectibleItems
                          where unlockedCollectible.Contains(item.name)
                          where item.Type == type
                          select item).Count();

            return amount;
        }

        public void SetCurrentPuzzleAmount(int amount)
        {
            CurrentQuestData.CurrentPuzzleAmount = amount;
        }

        public void SetCurrentHiddenObjectAmount(int amount)
        {
            CurrentQuestData.CurrentHiddenObjectAmount = amount;
        }

        public void AddCurrentGameWinAmount(int amount)
        {
            CurrentQuestData.CurrentGameWinAmount += amount;
        }

        public void AddCurrentMinutesPlayedAmount(float amount)
        {
            CurrentQuestData.CurrentMinutesPlayedAmount += amount;
        }

        public void SetCurrentQuizScoreAmount(float amount)
        {
            if (amount > CurrentQuestData.CurrentQuizScoreAmount)
            {
                CurrentQuestData.CurrentQuizScoreAmount = amount;
            }
        }

        public void SetCurrentSessionPlayed(float seconds)
        {
            CurrentSessionInMinutes = seconds / 60f;
        }

        public void AddLevelPlayed(string levelName)
        {
            if (!CurrentQuestData.LevelPlayed.Contains(levelName))
            {
                CurrentQuestData.LevelPlayed.Add(levelName);
            }
        }
    }
}