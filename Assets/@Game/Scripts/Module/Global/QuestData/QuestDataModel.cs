using Agate.MVC.Base;
using System;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataModel : BaseModel, IQuestDataModel
    {
        public SOQuestCollection QuestCollection { get; private set; }
        public QuestData CurrentQuestData { get; private set; }

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
    }
}