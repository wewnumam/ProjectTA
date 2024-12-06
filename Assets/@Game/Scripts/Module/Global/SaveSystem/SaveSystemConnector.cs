using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemConnector : BaseConnector
    {
        private SaveSystemController _saveSystem;

        protected override void Connect()
        {
            Subscribe<UnlockLevelMessage>(_saveSystem.UnlockLevel);
            Subscribe<UnlockCollectibleMessage>(_saveSystem.UnlockCollectible);
            Subscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UnlockLevelMessage>(_saveSystem.UnlockLevel);
            Unsubscribe<UnlockCollectibleMessage>(_saveSystem.UnlockCollectible);
            Unsubscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
        }
    }
}