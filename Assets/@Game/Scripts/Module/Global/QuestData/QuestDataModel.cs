using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public bool IsQuestComplete()
        {
            float points = 0;
            foreach (var questItem in QuestCollection.QuestItems)
            {
                float currentAmount = GetCurrentAmount(questItem);
                float progress = Mathf.Min(currentAmount / questItem.RequiredAmount * 100f, 100f); // Cap progress at 100%
                points += progress;
            }
            return points >= QuestCollection.QuestItems.Count * 100;
        }

        public float GetCurrentAmount(QuestItem questItem)
        {
            return questItem.Type switch
            {
                EnumManager.QuestType.Kill => CurrentQuestData.CurrentKillAmount,
                EnumManager.QuestType.Collectible => CurrentQuestData.CurrentCollectibleAmount,
                EnumManager.QuestType.Puzzle => CurrentQuestData.CurrentPuzzleAmount,
                EnumManager.QuestType.HiddenObject => CurrentQuestData.CurrentHiddenObjectAmount,
                EnumManager.QuestType.LevelPlayed => CurrentQuestData.LevelPlayed.Count,
                EnumManager.QuestType.GameWin => CurrentQuestData.CurrentGameWinAmount,
                EnumManager.QuestType.MinutesPlayed => CurrentQuestData.CurrentMinutesPlayedAmount,
                EnumManager.QuestType.QuizScore => CurrentQuestData.CurrentQuizScoreAmount,
                _ => 0
            };
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