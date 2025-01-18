using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataConnector : BaseConnector
    {
        private readonly LevelDataController _levelData = new();

        protected override void Connect()
        {
            Subscribe<ChooseLevelMessage>(_levelData.OnChooseLevel);
            Subscribe<UnlockLevelMessage>(_levelData.OnUnlockLevel);
            Subscribe<DeleteSaveDataMessage>(_levelData.OnDeleteSaveData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseLevelMessage>(_levelData.OnChooseLevel);
            Unsubscribe<UnlockLevelMessage>(_levelData.OnUnlockLevel);
            Unsubscribe<DeleteSaveDataMessage>(_levelData.OnDeleteSaveData);
        }
    }
}