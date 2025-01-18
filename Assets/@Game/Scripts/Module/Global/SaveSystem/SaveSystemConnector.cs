using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemConnector : BaseConnector
    {
        private readonly SaveSystemController _saveSystem = new();

        protected override void Connect()
        {
            Subscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
            Subscribe<ToggleGameInductionMessage>(_saveSystem.ToggleGameInduction);
            Subscribe<ToggleSfxMessage>(_saveSystem.ToggleSfx);
            Subscribe<ToggleBgmMessage>(_saveSystem.ToggleBgm);
            Subscribe<ToggleVibrationMessage>(_saveSystem.ToggleVibration);
        }

        protected override void Disconnect()
        {
            Unsubscribe<DeleteSaveDataMessage>(_saveSystem.DeleteSaveData);
            Unsubscribe<ToggleGameInductionMessage>(_saveSystem.ToggleGameInduction);
            Unsubscribe<ToggleSfxMessage>(_saveSystem.ToggleSfx);
            Unsubscribe<ToggleBgmMessage>(_saveSystem.ToggleBgm);
            Unsubscribe<ToggleVibrationMessage>(_saveSystem.ToggleVibration);
        }
    }
}