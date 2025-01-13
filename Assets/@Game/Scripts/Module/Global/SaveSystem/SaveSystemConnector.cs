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
            Subscribe<AddLevelPlayedMessage>(_saveSystem.AddLevelPlayed);
            Subscribe<ToggleGameInductionMessage>(_saveSystem.ToggleGameInduction);
            Subscribe<ToggleSfxMessage>(_saveSystem.ToggleSfx);
            Subscribe<ToggleBgmMessage>(_saveSystem.ToggleBgm);
            Subscribe<ToggleVibrationMessage>(_saveSystem.ToggleVibration);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseLevelMessage>(_saveSystem.ChooseLevel);
            Unsubscribe<UnlockLevelMessage>(_saveSystem.UnlockLevel);
            Unsubscribe<UnlockCollectibleMessage>(_saveSystem.UnlockCollectible);
            Unsubscribe<ChoicesRecordsMessage>(_saveSystem.SaveChoicesRecords);
            Unsubscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
            Unsubscribe<UpdateQuestDataMessage>(_saveSystem.SaveQuestData);
            Unsubscribe<AddLevelPlayedMessage>(_saveSystem.AddLevelPlayed);
            Unsubscribe<ToggleGameInductionMessage>(_saveSystem.ToggleGameInduction);
            Unsubscribe<ToggleSfxMessage>(_saveSystem.ToggleSfx);
            Unsubscribe<ToggleBgmMessage>(_saveSystem.ToggleBgm);
            Unsubscribe<ToggleVibrationMessage>(_saveSystem.ToggleVibration);
        }
    }
}