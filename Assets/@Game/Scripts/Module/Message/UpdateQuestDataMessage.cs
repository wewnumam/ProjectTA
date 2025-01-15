using ProjectTA.Module.QuestData;

namespace ProjectTA.Message
{
    public struct UpdateQuestDataMessage
    {
        public QuestData QuestData { get; private set; }

        public UpdateQuestDataMessage(QuestData questData)
        {
            QuestData = questData;
        }
    }
}