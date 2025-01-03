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
        public QuestData CurrentQuestData { get; private set; }

        public float CurrentSessionInMinutes { get; private set; }

        public void SetGameConstants(SOQuestCollection gameConstants)
        {
            QuestCollection = gameConstants;
            SetDataAsDirty();
        }

        public void SetCurrentQuestData(QuestData questData)
        {
            CurrentQuestData = questData;
            SetDataAsDirty();
        }

        public void AddCurrentKillAmount(int amount)
        {
            CurrentQuestData.CurrentKillAmount += amount;
            SetDataAsDirty();
        }

        public void SetCurrentCollectibleAmount(int amount)
        {
            CurrentQuestData.CurrentCollectibleAmount = amount;
            SetDataAsDirty();
        }

        public int GetCurrentCollectibleByTypeAmount(List<SOCollectibleData> collectibleItems, List<string> unlockedCollectible, EnumManager.CollectibleType type)
        {
            int amount = (from item in collectibleItems
                          where unlockedCollectible.Contains(item.name)
                          where item.Type == type
                          select item).Count();
            SetDataAsDirty();
            return amount;
        }

        public void SetCurrentPuzzleAmount(int amount)
        {
            CurrentQuestData.CurrentPuzzleAmount = amount;
            SetDataAsDirty();
        }

        public void SetCurrentHiddenObjectAmount(int amount)
        {
            CurrentQuestData.CurrentHiddenObjectAmount = amount;
            SetDataAsDirty();
        }

        public void SetCurrentLevelPlayedAmount(int amount)
        {
            CurrentQuestData.CurrentLevelPlayedAmount = amount;
            SetDataAsDirty();
        }

        public void AddCurrentGameWinAmount(int amount)
        {
            CurrentQuestData.CurrentGameWinAmount += amount;
            SetDataAsDirty();
        }

        public void AddCurrentMinutesPlayedAmount(float amount)
        {
            CurrentQuestData.CurrentMinutesPlayedAmount += amount;
            SetDataAsDirty();
        }

        public void SetCurrentQuizScoreAmount(float amount)
        {
            if (amount > CurrentQuestData.CurrentQuizScoreAmount)
            {
                CurrentQuestData.CurrentQuizScoreAmount = amount;
            }
            SetDataAsDirty();
        }

        public void SetCurrentSessionPlayed(float seconds)
        {
            CurrentSessionInMinutes = seconds / 60f;
            SetDataAsDirty();
        }
    }
}