using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemConnector : BaseConnector
    {
        private readonly SaveSystemController _saveSystem = new();

        protected override void Connect()
        {
            Subscribe<ChooseLevelMessage>(_saveSystem.ChooseLevel);
            Subscribe<UnlockLevelMessage>(_saveSystem.UnlockLevel);
            Subscribe<UnlockCollectibleMessage>(_saveSystem.UnlockCollectible);
            Subscribe<ChoicesRecordsMessage>(_saveSystem.SaveChoicesRecords);
            Subscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
            Subscribe<UpdateQuestDataMessage>(_saveSystem.SaveQuestData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseLevelMessage>(_saveSystem.ChooseLevel);
            Unsubscribe<UnlockLevelMessage>(_saveSystem.UnlockLevel);
            Unsubscribe<UnlockCollectibleMessage>(_saveSystem.UnlockCollectible);
            Unsubscribe<ChoicesRecordsMessage>(_saveSystem.SaveChoicesRecords);
            Unsubscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
            Unsubscribe<UpdateQuestDataMessage>(_saveSystem.SaveQuestData);
        }
    }
}