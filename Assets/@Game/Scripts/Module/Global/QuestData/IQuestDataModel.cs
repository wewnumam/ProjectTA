using Agate.MVC.Base;

namespace ProjectTA.Module.QuestData
{
    public interface IQuestDataModel : IBaseModel
    {
        SOQuestCollection QuestCollection { get; }
        SavedQuestData CurrentQuestData { get; }
    }
}