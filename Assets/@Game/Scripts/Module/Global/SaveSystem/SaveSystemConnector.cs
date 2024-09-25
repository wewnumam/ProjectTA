using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemConnector : BaseConnector
    {
        private SaveSystemController _saveSystem;

        protected override void Connect()
        {
            Subscribe<GameResultMessage>(_saveSystem.SaveGameResult);
            Subscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GameResultMessage>(_saveSystem.SaveGameResult);
            Unsubscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
        }
    }
}